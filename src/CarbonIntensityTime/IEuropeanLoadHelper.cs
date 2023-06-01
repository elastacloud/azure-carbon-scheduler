using CarbonIntensityTypes;

namespace CarbonIntensityTime;

/// <summary>
/// A helper used to get the current more value of the carbon intensity in CO2 lbs/MWh
/// </summary>
public interface IEuropeanLoadHelper
{
    public string? GetEntsoeId(string countryCode);

    /// <summary>
    /// Gets the previous 24 hour of values for the psr for a particular country
    /// </summary>
    public Task<string> GetCurrentValue(string psr, string inDomain);

    public Task<string> GetForecastValue(string psr, string inDomain);
    public Task<List<EntsoeCodes>> GetEntsoeFromJsonFile(string fileName);

    /// <summary>
    /// GEts the installed capacity of each generator type per country
    /// </summary>
    public Task<List<CountryPsrCapacity>> GetInstalledCapacityByCountry(string inDomain);
}