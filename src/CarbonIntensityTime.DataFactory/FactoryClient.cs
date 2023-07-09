using Azure.Core;
using Azure.Identity;
using CarbonIntensityTime.Core.Configuration;
using CarbonIntensityTime.DataFactory.Models;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Rest;
using PipelineRun = CarbonIntensityTime.DataFactory.Models.PipelineRun;

namespace CarbonIntensityTime.DataFactory;

/// <summary>
/// Methods for interacting with Azure Data Factory
/// </summary>
public class FactoryClient : IFactoryClient
{
    private readonly FactorySettings _factorySettings;
    private readonly ILogger<FactoryClient> _logger;

    private DataFactoryManagementClient? _client;

    /// <summary>
    /// Creates a new instance of the <see cref="FactoryClient"/> class
    /// </summary>
    /// <param name="factorySettings">Options for defining which Factory instance to connect to and how</param>
    /// <param name="logger">Logger instance</param>
    public FactoryClient(IOptions<FactorySettings> factorySettings, ILogger<FactoryClient> logger)
    {
        _factorySettings = factorySettings.Value;
        _logger = logger;
    }

    /// <summary>
    /// List pipelines in the Azure Data Factory instance
    /// </summary>
    public async Task<IList<string>> ListPipelines()
    {
        var client = await GetClient();

        _logger.LogDebug("Retrieving pipelines");
        var pipelines =
            await client.Pipelines.ListByFactoryAsync(_factorySettings.ResourceGroup, _factorySettings.Name);

        return pipelines.Select(p => p.Name).ToList();
    }

    /// <summary>
    /// Retrieves a collection of pipeline runs in the last x minutes
    /// </summary>
    /// <returns>A collection of pipeline runs</returns>
    public async Task<ICollection<PipelineRun>> ListPipelineRuns(int minutes)
    {
        var end = DateTime.UtcNow;
        var start = end.AddMinutes(minutes * -1);
        var filter = new RunFilterParameters(start, end);

        var client = await GetClient();

        _logger.LogDebug("Retrieving pipelines runs");
        var getPipelineRuns = (await client.PipelineRuns.QueryByFactoryAsync(_factorySettings.ResourceGroup, _factorySettings.Name,
            filter)).Value;

        var pipelineRuns = getPipelineRuns.Select(async pr => new PipelineRun
        {
            RunId = pr.RunId,
            PipelineName = pr.PipelineName,
            Status = pr.Status,
            Start = pr.RunStart,
            End = pr.RunEnd,
            Duration = pr.DurationInMs,
            InvokedByName = pr.InvokedBy.Name,
            InvokedByType = pr.InvokedBy.InvokedByType,
            Activities =
                (await ListPipelineRunActivities(pr.RunId, filter))
                .Select(ar => new PipelineActivity
                {
                    ActivityId = ar.ActivityRunId,
                    Name = ar.ActivityName,
                    Type = ar.ActivityType,
                    Status = ar.Status,
                    Start = ar.ActivityRunStart,
                    End = ar.ActivityRunEnd,
                    Duration = ar.DurationInMs,
                    Input = PipelineActivity.ConvertToDictionary(ar.Input),
                    PipelineName = ar.PipelineName,
                    PipelineRunId = ar.PipelineRunId
                }).ToList()
        }).ToList();
        
        return await Task.WhenAll(pipelineRuns);
    }
    
    /// <summary>
    /// Retrieves a collection of activities within a specific pipeline run
    /// </summary>
    /// <returns>A collection of activities</returns>
    public async Task<IList<ActivityRun>> ListPipelineRunActivities(string runId, RunFilterParameters filter)
    {
        var client = await GetClient();
        var activityRuns = await client.ActivityRuns.QueryByPipelineRunAsync(_factorySettings.ResourceGroup, _factorySettings.Name,
            runId, filter);
        return activityRuns.Value;
    }

    /// <summary>
    /// Retrieves an Azure Data Factory client for working with Azure ADF
    /// </summary>
    /// <returns>A Data Factory Management client instance</returns>
    private async Task<DataFactoryManagementClient> GetClient()
    {
        if (_client is not null)
        {
            _logger.LogDebug("Using existing client");
            return _client;
        }

        TokenCredential credential;
        if (!string.IsNullOrEmpty(_factorySettings.ClientId) && !string.IsNullOrEmpty(_factorySettings.ClientSecret))
        {
            _logger.LogDebug("Connecting to Data Factory using client secret credentials");
            credential = new ClientSecretCredential(_factorySettings.TenantId, _factorySettings.ClientId,
                _factorySettings.ClientSecret);
        }
        else
        {
            _logger.LogDebug("Connecting to Data Factory using default credentials");
            credential = new DefaultAzureCredential();
        }

        _logger.LogDebug("Requesting management access token");
        var requestContext = new TokenRequestContext(new[] { "https://management.azure.com/.default" });
        var cancellationToken = new CancellationToken();
        var token = await credential.GetTokenAsync(requestContext, cancellationToken);

        _logger.LogDebug("Creating Data Factory management client");
        var adfCredentials = new TokenCredentials(token.Token);
        _client = new DataFactoryManagementClient(adfCredentials)
        {
            SubscriptionId = _factorySettings.SubscriptionId
        };

        return _client;
    }
}