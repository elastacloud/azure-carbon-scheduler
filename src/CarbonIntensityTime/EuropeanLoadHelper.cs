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
using CarbonIntensityTypes;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace CarbonIntensityTime
{
   public class EuropeanLoadHelper : IEuropeanLoadHelper
   {
      private readonly string _token;
      private readonly FuelCodes[] _fuelCodes;
      private readonly EntsoeCodes[] _entsoeCodes;
      public const string ENTSOE_Endpoint = "https://web-api.tp.entsoe.eu/api";

      public EuropeanLoadHelper(string securityToken, EntsoeCodes[] entsoeCodes, FuelCodes[] fuelCodes)
      {
         _token = securityToken;
         _fuelCodes = fuelCodes;
         _entsoeCodes = entsoeCodes;
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

      public async Task<List<CountryPsrCapacity>> GetInstalledCapacityByCountry(string inDomain)
      {
         var installedCapacity = new List<CountryPsrCapacity>();
         var entsoeRequest = new EntsoeRequest()
         {
            DocumentType = "A71",
            ProcessType = "A33",
            InDomain = inDomain,
            SecurityToken = _token,
            StartDate = DateTime.UtcNow.AddHours(-4),
            EndDate = DateTime.UtcNow.AddHours(-3)
         };
         var request = new EntsoeHttpDriver();
         var response = await request.EntsoeGetRequestWithPsr(entsoeRequest);
         // Build linq expression to sum all of the installed capacities over the network for the type of generation
         var results = from ts in response.TimeSeries
                       group Convert.ToInt32(ts.Period[0].Point[0].quantity) by ts.MktPSRType[0].psrType into groupTimeSeries
                       select groupTimeSeries;
         foreach (var psr in results)
         {
            installedCapacity.Add(new CountryPsrCapacity()
            {
               Country = inDomain,
               Date = entsoeRequest.StartDate,
               Capacity = psr.Sum(),
               Psr = _fuelCodes.Where(code => code.Code == psr.Key).Select(code => $"{code.Type} | " + (String.IsNullOrEmpty(code.Info) ? "N/A" : code.Info)).FirstOrDefault()
            });
         }

         return installedCapacity;
      }
   }
}