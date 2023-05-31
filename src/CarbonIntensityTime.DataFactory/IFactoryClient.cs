namespace CarbonIntensityTime.DataFactory;

/// <summary>
/// Methods for interacting with Azure Data Factory
/// </summary>
public interface IFactoryClient
{
    /// <summary>
    /// List pipelines in the Azure Data Factory instance
    /// </summary>
    Task<IList<string>> ListPipelines();
}