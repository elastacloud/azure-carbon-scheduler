using CarbonIntensityTypes;

namespace CarbonIntensityTime;



/// <summary>
/// Gets the previous 24 hour of values for the psr for a particular country
/// </summary>
public interface IEuropeanLoadHelper
{
   public string? GetEntsoeId(string countryCode);

   /// <summary>
   /// Gets the previous 24 hour of values for the psr for a particular country
   /// </summary>
   public Task<string> GetCurrentValue(string psr, string inDomain);
   /// <summary>
   /// Gets the forecast value for an indomain
   /// </summary>
   public Task<List<AggregatePeriodValue>> GetForecast(string inDomain);
   public Task<List<EntsoeCodes>> GetEnsoeFromJsonFile(string fileName);

   /// <summary>
   /// GEts the installed capacity of each generator type per country
   /// </summary>
   public Task<List<CountryPsrCapacity>> GetInstalledCapacityByCountry(string inDomain);
}
