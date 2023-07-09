using Newtonsoft.Json;

namespace CarbonIntensityTime;

/// <summary>
/// The type of codes that entsoe 
/// </summary>
public class EntsoeCodes
{
    [JsonProperty("country")]
    public string? Country { get; set; }
    [JsonProperty("Code")]
    public string? Code { get; set; }
    [JsonProperty("ENTSOe_ID")]
    public string? EntsoeId { get; set; }
    [JsonProperty("gen_freq")]
    public string? GenFrequency { get; set; }
    [JsonProperty("gen_market")]
    public string? GenMarket { get; set; }
}