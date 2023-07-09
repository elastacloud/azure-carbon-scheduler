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
using CarbonIntensityTime.Core.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace CarbonIntensityTime
{
   public class EuropeanLoadHelper : IEuropeanLoadHelper
   {
      private readonly string _token;
      private readonly ICollection<FuelCodes>? _fuelCodes;
      private readonly ICollection<EntsoeCodes>? _entsoeCodes;
      private readonly IEntsoeHttpDriver _entsoeHttpDriver;
      private readonly IHttpClientFactory _httpClientFactory;
      private readonly ILogger<EuropeanLoadHelper> _logger;
      public const string ENTSOE_Endpoint = "https://web-api.tp.entsoe.eu/api";

      public EuropeanLoadHelper(IEntsoeHttpDriver entsoeHttpDriver, ICodesLoader codesLoader,
          IOptions<AppSettings> appSettings, IHttpClientFactory httpClientFactory,
          ILogger<EuropeanLoadHelper> logger)
      {
         _entsoeHttpDriver = entsoeHttpDriver;
         _httpClientFactory = httpClientFactory;
         _logger = logger;

         _token = appSettings.Value.ApiKey;
         _entsoeCodes = codesLoader.Entsoe();
         _fuelCodes = codesLoader.Fuel();
      }

      public string? GetEntsoeId(string countryCode)
      {
         return _entsoeCodes.SingleOrDefault(c =>
             string.Equals(c.Code, countryCode, StringComparison.OrdinalIgnoreCase))?.EntsoeId;
      }

      /// <summary>
      /// Gets the previous 24 hours of values 
      /// </summary>
      public async Task<string> GetCurrentValue(string psr, string inDomain)
      {
         string? responseData = null;
         using (HttpClient client = _httpClientFactory.CreateClient())
         {
            client.BaseAddress = new Uri(ENTSOE_Endpoint);
            // Construct the query string parameters
            string periodStart = DateTime.UtcNow.AddDays(-1).ToString("yyyyMMddHH00");
            string periodEnd = DateTime.UtcNow.ToString("yyyyMMddHH00");
            var queryString =
                $"?securityToken={_token}&processType=A16&psrType={psr}&documentType=A73&periodStart={periodStart}&periodEnd={periodEnd}&in_Domain={inDomain}";
            HttpResponseMessage response = await client.GetAsync(queryString);
            responseData = await response.Content.ReadAsStringAsync();
         }

         return responseData;
      }

      /// <summary>
      /// Gets the leading forecast of values for the next 24 hours
      /// For both the entire 
      /// </summary>
      public async Task<List<AggregatePeriodValue>> GetForecastValue(string inDomain)
      {
         var generationWithRenew = new List<AggregatePeriodValue>();
         var entsoeRequest = new EntsoeRequest();
         var installedCapacity = await GetAggregateForecastValue(inDomain, ref entsoeRequest);
         
         // Gets the entsoe request for solar and wind only
         entsoeRequest.DocumentType = "A69";
         var response = await _entsoeHttpDriver.EntsoeGetRequestWithPsr(entsoeRequest);
         var period = response.TimeSeries.FirstOrDefault().Period[0];
         double numberOfMinutesInterval = (24D / Convert.ToDouble(period.Point.Length)) * 60D;
         for (int i = 0; i < period.Point.Length; i++)
         {
            var existingPoint = installedCapacity.SingleOrDefault(point => point.StartDate == startDate.AddMinutes(numberOfMinutesInterval * i).UtcDateTime);
            existingPoint.RenewableValue = int.Parse(period.Point[i].quantity);
            generationWithRenew.Add(existingPoint);
            _logger.LogInformation($"At time: {existingPoint.StartDate.ToString("f")}, total generation={existingPoint.TotalValue}, renewable generation={existingPoint.RenewableValue}");
         }
         return generationWithRenew;
      }

      private async Task<List<AggregatePeriodValue>> GetAggregateForecastValue(string inDomain, ref EntsoeRequest entsoeRequest)
      {
         // get the aggregate value first 
         _logger.LogInformation($"Gets the aggregate value for UK on {DateTime.UtcNow:D}");
         var initialDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, 0, 0);

         var installedCapacity = new List<AggregatePeriodValue>();
         entsoeRequest = new EntsoeRequest()
         {
            DocumentType = "A71",
            ProcessType = "A01",
            InDomain = inDomain,
            SecurityToken = _token,
            StartDate = initialDate.AddHours(-2),
            EndDate = initialDate.AddHours(-2).AddDays(1)
         };
         // Gets the entsoe request for the total energy generation
         var response = await _entsoeHttpDriver.EntsoeGetRequestWithPsr(entsoeRequest);
         var period = response.TimeSeries.FirstOrDefault().Period[0];
         double numberOfMinutesInterval = (24D / Convert.ToDouble(period.Point.Length)) * 60D;
         DateTimeOffset startDate = DateTimeOffset.ParseExact(response.time_PeriodtimeInterval[0].start,
            "yyyy-MM-ddTHH:00Z", CultureInfo.InvariantCulture);
         for (int i = 0; i < period.Point.Length; i++)
         {
            var aggValue = new AggregatePeriodValue()
            {
               StartDate = startDate.AddMinutes(numberOfMinutesInterval * i).UtcDateTime,
               TotalValue = int.Parse(period.Point[i].quantity)
            };
            installedCapacity.Add(aggValue);
         }
      }

      public Task<List<EntsoeCodes>> GetEnsoeFromJsonFile(string fileName)
      {
         throw new NotImplementedException();
      }
      /// <summary>
      /// Gets the installed capacity of the country across the particular network
      /// </summary>
      public async Task<List<CountryPsrCapacity>> GetInstalledCapacityByCountry(string inDomain)
      {
         _logger.LogInformation($"Gets the installed capacity PSRs for UK on {DateTime.UtcNow:D}");
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

         var response = await _entsoeHttpDriver.EntsoeGetRequestWithPsr(entsoeRequest);
         // Build linq expression to sum all of the installed capacities over the network for the type of generation
         var results = from ts in response.TimeSeries
                       group Convert.ToInt32(ts.Period[0].Point[0].quantity) by ts.MktPSRType[0].psrType
                       into groupTimeSeries
                       select groupTimeSeries;
         foreach (var psr in results)
         {
            var capacity = new CountryPsrCapacity()
            {
               Country = _entsoeCodes.First(code => code.EntsoeId == inDomain).Country,
               Date = entsoeRequest.StartDate,
               Capacity = psr.Sum(),
               Psr = _fuelCodes.Where(code => code.Code == psr.Key).Select(code =>
                   $"{code.Type} | " + (String.IsNullOrEmpty(code.Info) ? "N/A" : code.Info)).FirstOrDefault()
            };
            installedCapacity.Add(capacity);
            _logger.LogInformation($"{capacity.Country} with {capacity.Capacity}MW for PSR: {capacity.Psr}");
         }

         return installedCapacity;
      }
   }
}