using System.Net.Http.Headers;
using System.Web;
using CarbonIntensityTime.Core.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarbonIntensityTime.Azure.DataFactory;

/// <summary>
/// Methods for interacting with Azure Data Factory
/// </summary>
public class FactoryClient : IFactoryClient
{
    private const string Scope = "https://management.azure.com/.default";
    private const string ApiVersion = "2018-06-01";
    
    private static readonly Uri BaseAddress = new Uri("https://management.azure.com");
    
    private readonly ITokenProvider _tokenProvider;
    private readonly FactorySettings _factorySettings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<FactoryClient> _logger;
    
    /// <summary>
    /// Creates a new instance of the <see cref="FactoryClient"/> class
    /// </summary>
    /// <param name="tokenProvider">Token provider instance for retrieving Data Factory access tokens</param>
    /// <param name="httpClientFactory">HTTP client factory instance for retrieving HTTP client instances</param>
    /// <param name="logger">Logger instance</param>
    public FactoryClient(ITokenProvider tokenProvider,IOptions<FactorySettings> factorySettings, IHttpClientFactory httpClientFactory, ILogger<FactoryClient> logger)
    {
        _tokenProvider = tokenProvider;
        _factorySettings = factorySettings.Value;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        
        // https://management.azure.com/subscriptions/6b4676ac-9931-4c64-a4c6-2b9e473f424c/resourceGroups/talks01/providers/Microsoft.DataFactory/factories/ec-ingest-factoryjbahpfshi5cyc/queryPipelineRuns?api-version=2018-06-01
    }

    /// <summary>
    /// List pipelines in the Azure Data Factory instance
    /// </summary>
    public async Task<IList<string>> ListPipelineRuns()
    {
        var token = await _tokenProvider.GetToken(Scope);
        
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var parameters = HttpUtility.ParseQueryString(string.Empty);
        parameters.Add("api-version", ApiVersion);
        
        var uriBuilder = new UriBuilder(BaseAddress)
        {
            Path = $"subscriptions/{_factorySettings.SubscriptionId}/resourceGroups/{_factorySettings.ResourceGroup}/providers/Microsoft.DataFactory/factories/{_factorySettings.Name}/queryPipelineRuns",
            Query = parameters.ToString()
        };

        var request = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Uri);
        var response = await client.SendAsync(request);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("FUCKITY {StatusCode}: {Error}", response.StatusCode, responseContent);
            return new List<string>();
        }

        Console.WriteLine(responseContent);
        
        return new List<string>();
    }
    
    
}