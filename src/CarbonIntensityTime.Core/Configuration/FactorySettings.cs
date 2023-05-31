namespace CarbonIntensityTime.Core.Configuration;

public class FactorySettings
{
    /// <summary>
    /// Gets, sets the name of the Azure Data Factory instance
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets, sets the subscription id which the Azure Data Factory instance has been deployed to
    /// </summary>
    public string SubscriptionId { get; set; } = string.Empty;

    /// <summary>
    /// Gets, sets the resource group which the Azure Data Factory instance has been deployed to
    /// </summary>
    public string ResourceGroup { get; set; } = string.Empty;

    /// <summary>
    /// Gets, sets the tenant id for the tenant the Azure Data Factory instance has been deployed to
    /// </summary>
    public string TenantId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets, sets the client id. If this is null or empty then the default Azure credential will be used instead
    /// </summary>
    public string? ClientId { get; set; }
    
    /// <summary>
    /// Gets, sets the client secret. If this is null or empty then the default Azure credential will be used instead
    /// </summary>
    public string? ClientSecret { get; set; }
}