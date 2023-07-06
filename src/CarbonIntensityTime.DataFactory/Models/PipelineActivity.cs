using Newtonsoft.Json;

namespace CarbonIntensityTime.DataFactory.Models;

public class PipelineActivity
{
    public string ActivityId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public DateTime? Start { get; init; }
    public DateTime? End { get; init; }
    public int? Duration { get; init; }
    public Dictionary<string, string> Input { get; init; }
    public string PipelineName { get; init; } = string.Empty;
    public string PipelineRunId { get; init; } = string.Empty;

    public static Dictionary<string, string> ConvertToDictionary(object obj)
    {
        var input = JsonConvert.DeserializeObject<Dictionary<string, object>>(obj.ToString());
        return input.ToDictionary(d => d.Key, d => d.Value.ToString() ?? string.Empty);
    }
}

