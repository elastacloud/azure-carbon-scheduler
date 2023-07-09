using Microsoft.Azure.Management.DataFactory.Models;
using PipelineRun = CarbonIntensityTime.DataFactory.Models.PipelineRun;

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

    /// <summary>
    /// Retrieves a collection of activities within a specific pipeline run
    /// </summary>
    /// <returns>A collection of activities</returns>
    Task<IList<ActivityRun>> ListPipelineRunActivities(string runId, RunFilterParameters filter);
}