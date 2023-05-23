namespace CarbonIntensityTime
{
   /// <summary>
   /// A helper used to get the current more value of the carbon intensity in CO2 lbs/MWh
   /// </summary>
   public interface IEuropeanLoadHelper
   {
      public Task<float> GetCurrentValue(string country);
      public Task<float> GetForecastValue(string country);
      public Task<List<EntsoeCodes>> GetEnsoeFromJsonFile(string fileName);
   }
}