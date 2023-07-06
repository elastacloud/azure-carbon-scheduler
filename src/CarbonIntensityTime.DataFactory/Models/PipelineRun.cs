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
    public List<PipelineActivity> Activities { get; init; } = new();
}