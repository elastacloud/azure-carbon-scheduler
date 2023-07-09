using CarbonIntensityTypes;

namespace CarbonIntensityTime;

/// <summary>
/// The Http driver that makes the requests to entsoe
/// </summary>
public interface IEntsoeHttpDriver
{
    /// <summary>
    /// Given a request of documentType, process, psr and a security token should return a response with the requisite generation details for european countries
    /// </summary>
    public Task<GL_MarketDocument?> EntsoeGetRequestWithPsr(EntsoeRequest request);
}