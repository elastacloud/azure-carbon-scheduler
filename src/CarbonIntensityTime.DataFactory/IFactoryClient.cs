using CarbonIntensityTime.DataFactory.Models;

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

    /// <summary>
    /// Retrieves a collection of pipeline runs in the last x minutes
    /// </summary>
    /// <returns>A collection of pipeline runs</returns>
    Task<ICollection<PipelineRun>> ListPipelineRuns(int minutes);
}