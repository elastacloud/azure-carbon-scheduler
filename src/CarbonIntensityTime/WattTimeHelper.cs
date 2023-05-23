/*
 * EU Client

    EU Now uses a RESTful interface, https://transparency.entsoe.eu/api
    This uses a Security Token rather than a username/password combination.
    You need to register on the Transparency site to get a token.  Instructions
    are in the REST manual:
    https://transparency.entsoe.eu/content/static_content/Static%20content/web%20api/Guide.html
    Readable names for resources now use an alphanumeric code, e.g. A71 for the
    generation forecast series.  Those in use at the moment are:

    A65 = Total Load
    A69 = Generation Forecast (Wind/Solar)
    A71 = Generation Forecast By Type
    A73 = Generation Actual
    A74 = Generation Actual (Wind/Solar)
    A75 = Generation Actual by Type

    Within these are codes for the type of report.  E.g.

    A01 = Day ahead hourly
    A16 = Realised
*/
using System.Text.Json;

namespace CarbonIntensityTime
{
   public class WattTimeHelper : IEuropeanLoadHelper
   {
      private readonly string _token;
      private readonly string _countryCode;
      private DateTime _startTime = DateTime.UtcNow;
      private int _secondsPassed = 0;

      public const string ENTSOE_Endpoint = "https://web-api.tp.entsoe.eu/api";

      public WattTimeHelper(string securityToken, string countryCode)
      {
         _token = securityToken;
         _countryCode = countryCode;
      }
      /// <summary>
      /// Gets the previous 24 hours of values 
      /// </summary>
      public async Task<float> GetCurrentValue(string country)
      {
         using (HttpClient client = new HttpClient())
         {
            client.BaseAddress = new Uri(ENTSOE_Endpoint);
            // Construct the query string parameters
            var queryString = $"?securityToken={_token}";
            // Send GET request
            HttpResponseMessage response = await client.GetAsync("endpoint" + queryString);
            string responseData = await response.Content.ReadAsStringAsync();
         }
         return 1f;
      }
      /// <summary>
      /// Gets the leading forecast of values
      /// </summary>
      public async Task<float> GetForecastValue(string country)
      {
         using (HttpClient client = new HttpClient())
         {
            client.BaseAddress = new Uri(ENTSOE_Endpoint);
            // Construct the query string parameters
            var queryString = $"?securityToken={_token}";
            // Send GET request
            HttpResponseMessage response = await client.GetAsync("endpoint" + queryString);
            string responseData = await response.Content.ReadAsStringAsync();
         }
         return 1f;
      }

      public Task<List<EntsoeCodes>> GetEnsoeFromJsonFile(string fileName)
      {
         throw new NotImplementedException();
      }
   }
}