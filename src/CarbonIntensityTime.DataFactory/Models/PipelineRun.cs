using Microsoft.Azure.Management.DataFactory.Models;

namespace CarbonIntensityTime.DataFactory.Models;

public class PipelineRun
{
    public string RunId { get; init; } = string.Empty;
    public string PipelineName { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public DateTime? Start { get; init; }
    public DateTime? End { get; init; }
    public int? Duration { get; init; }
    public string InvokedByName { get; init; } = string.Empty;
    public string InvokedByType { get; init; } = string.Empty;
    public IList<PipelineActivity> Activities { get; init; }
}

public class PipelineActivity
{
    public string ActivityId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public DateTime? Start { get; init; }
    public DateTime? End { get; init; }
    public int? Duration { get; init; }
    public object Input { get; init; }
    public string PipelineName { get; init; } = string.Empty;
    public string PipelineRunId { get; init; } = string.Empty;
}