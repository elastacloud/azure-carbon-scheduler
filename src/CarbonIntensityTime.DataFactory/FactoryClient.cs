using Azure.Core;
using Azure.Identity;
using CarbonIntensityTime.Core.Configuration;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Rest;

namespace CarbonIntensityTime.DataFactory;

/// <summary>
/// Methods for interacting with Azure Data Factory
/// </summary>
public class FactoryClient : IFactoryClient
{
    private readonly FactorySettings _factorySettings;
    private readonly ILogger<FactoryClient> _logger;

    private DataFactoryManagementClient? _client = null;
    
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
        var pipelines = await client.Pipelines.ListByFactoryAsync(_factorySettings.ResourceGroup, _factorySettings.Name);

        return pipelines.Select(p => p.Name).ToList();
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
        var requestContext = new TokenRequestContext(new[] {"https://management.azure.com/.default"});
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