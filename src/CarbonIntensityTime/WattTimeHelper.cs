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
   public class EuropeanLoadHelper : IEuropeanLoadHelper
   {
      private readonly string _token;

      public const string ENTSOE_Endpoint = "https://web-api.tp.entsoe.eu/api";

      public EuropeanLoadHelper(string securityToken)
      {
         _token = securityToken;
      }
      /// <summary>
      /// Gets the previous 24 hours of values 
      /// </summary>
      public async Task<string> GetCurrentValue(string psr, string inDomain)
      {
         string? responseData = null;
         using (HttpClient client = new HttpClient())
         {
            client.BaseAddress = new Uri(ENTSOE_Endpoint);
            // Construct the query string parameters
            string periodStart = DateTime.UtcNow.AddDays(-1).ToString("yyyyMMddHH00");
            string periodEnd = DateTime.UtcNow.ToString("yyyyMMddHH00");
            var queryString = $"?securityToken={_token}&processType=A16&psrType={psr}&documentType=A73&periodStart={periodStart}&periodEnd={periodEnd}&in_Domain={inDomain}";
            HttpResponseMessage response = await client.GetAsync(queryString);
            responseData = await response.Content.ReadAsStringAsync();
         }
         return responseData;
      }
      /// <summary>
      /// Gets the leading forecast of values
      /// </summary>
      public async Task<string> GetForecastValue(string psr, string inDomain)
      {
         string? responseData = null;
         using (HttpClient client = new HttpClient())
         {
            client.BaseAddress = new Uri(ENTSOE_Endpoint);
            // Construct the query string parameters
            string periodStart = DateTime.UtcNow.ToString("yyyyMMddHH00");
            string periodEnd = DateTime.UtcNow.AddDays(1).ToString("yyyyMMddHH00");
            var queryString = $"?securityToken={_token}&processType=A71&psrType={psr}&documentType=A73&periodStart={periodStart}&periodEnd={periodEnd}&in_Domain={inDomain}";
            HttpResponseMessage response = await client.GetAsync(queryString);
            responseData = await response.Content.ReadAsStringAsync();
         }
         return responseData;
      }

      public Task<List<EntsoeCodes>> GetEnsoeFromJsonFile(string fileName)
      {
         throw new NotImplementedException();
      }

      public async Task<string> GetInstalledCapacityByCountry(string inDomain)
      {
         string? responseData = null;
         using (HttpClient client = new HttpClient())
         {
            client.BaseAddress = new Uri(ENTSOE_Endpoint);
            // Construct the query string parameters
            string periodStart = DateTime.UtcNow.AddYears(-5).ToString("yyyyMMddHH00");
            string periodEnd = DateTime.UtcNow.AddYears(-4).ToString("yyyyMMddHH00");
            var queryString = $"?securityToken={_token}&processType=A33&documentType=A71&periodStart={periodStart}&periodEnd={periodEnd}&in_Domain={inDomain}";
            HttpResponseMessage response = await client.GetAsync(queryString);
            responseData = await response.Content.ReadAsStringAsync();
         }
         return responseData;
      }
   }
}