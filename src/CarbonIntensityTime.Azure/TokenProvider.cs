using Azure.Core;
using Azure.Identity;
using CarbonIntensityTime.Core.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarbonIntensityTime.Azure;

public class TokenProvider : ITokenProvider
{
    private readonly FactorySettings _factorySettings;
    private readonly ILogger<TokenProvider> _logger;

    private AccessToken _token;

    public TokenProvider(IOptions<FactorySettings> factorySettings, ILogger<TokenProvider> logger)
    {
        _factorySettings = factorySettings.Value;
        _logger = logger;
    }
    
    public async Task<string> GetToken(params string[] scopes)
    {
        if (_token.ExpiresOn > DateTimeOffset.UtcNow)
        {
            return _token.Token;
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
        var requestContext = new TokenRequestContext(scopes);
        var cancellationToken = new CancellationToken();
        var token = await credential.GetTokenAsync(requestContext, cancellationToken);

        _token = token;
        return token.Token;
    }
}