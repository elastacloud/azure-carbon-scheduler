﻿/*
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

using System.Web;
using CarbonIntensityTypes;
using CarbonIntensityTime.Core.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace CarbonIntensityTime;

public class EuropeanLoadHelper : IEuropeanLoadHelper
{
    private readonly string _token;
    private readonly ICollection<FuelCodes> _fuelCodes;
    private readonly ICollection<EntsoeCodes> _entsoeCodes;
    private readonly IEntsoeHttpDriver _entsoeHttpDriver;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<EuropeanLoadHelper> _logger;
    private const string EntsoeEndpoint = "https://web-api.tp.entsoe.eu/api";

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
        using HttpClient client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(EntsoeEndpoint);
            
        // Construct the query string parameters
        string periodStart = DateTime.UtcNow.AddDays(-1).ToString("yyyyMMddHH00");
        string periodEnd = DateTime.UtcNow.ToString("yyyyMMddHH00");
            
        var requestParameters = HttpUtility.ParseQueryString("");
        requestParameters.Add("securityToken", _token);
        requestParameters.Add("processType", "A16");
        requestParameters.Add("psrType", psr);
        requestParameters.Add("documentType", "A73");
        requestParameters.Add("periodStart", periodStart);
        requestParameters.Add("periodEnd", periodEnd);
        requestParameters.Add("in_Domain", inDomain);
            
        // var queryString =
        //     $"?securityToken={_token}&processType=A16&psrType={psr}&documentType=A73&periodStart={periodStart}&periodEnd={periodEnd}&in_Domain={inDomain}";
        HttpResponseMessage response = await client.GetAsync($"?{requestParameters}");
        var responseData = await response.Content.ReadAsStringAsync();

        return responseData;
    }

    /// <summary>
    /// Gets the leading forecast of values
    /// </summary>
    public async Task<string> GetForecastValue(string psr, string inDomain)
    {
        using HttpClient client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(EntsoeEndpoint);
            
        // Construct the query string parameters
        string periodStart = DateTime.UtcNow.ToString("yyyyMMddHH00");
        string periodEnd = DateTime.UtcNow.AddDays(1).ToString("yyyyMMddHH00");
        var queryString =
            $"?securityToken={_token}&processType=A71&psrType={psr}&documentType=A73&periodStart={periodStart}&periodEnd={periodEnd}&in_Domain={inDomain}";
        HttpResponseMessage response = await client.GetAsync(queryString);
        var responseData = await response.Content.ReadAsStringAsync();

        return responseData;
    }

    public Task<List<EntsoeCodes>> GetEntsoeFromJsonFile(string fileName)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CountryPsrCapacity>> GetInstalledCapacityByCountry(string inDomain)
    {
        _logger.LogInformation("Gets the installed capacity PSRs for UK on {UtcDate}", DateTime.UtcNow);
        var installedCapacity = new List<CountryPsrCapacity>();
        var entsoeRequest = new EntsoeRequest
        {
            DocumentType = "A71",
            ProcessType = "A33",
            InDomain = inDomain,
            SecurityToken = _token,
            StartDate = DateTime.UtcNow.AddHours(-4),
            EndDate = DateTime.UtcNow.AddHours(-3)
        };

        var response = await _entsoeHttpDriver.EntsoeGetRequestWithPsr(entsoeRequest);

        // If no installed capacities were retrieved then return an empty collection
        if (response is null)
        {
            return new List<CountryPsrCapacity>();
        }
            
        // Build linq expression to sum all of the installed capacities over the network for the type of generation
        var results = from ts in response.TimeSeries
            group Convert.ToInt32(ts.Period[0].Point[0].quantity) by ts.MktPSRType[0].psrType
            into groupTimeSeries
            select groupTimeSeries;
            
        foreach (var psr in results)
        {
            var capacity = new CountryPsrCapacity
            {
                Country = _entsoeCodes.First(code => code.EntsoeId == inDomain).Country ?? string.Empty,
                Date = entsoeRequest.StartDate,
                Capacity = psr.Sum(),
                Psr = _fuelCodes.Where(code => code.Code == psr.Key).Select(code =>
                    $"{code.Type} | " + (string.IsNullOrEmpty(code.Info) ? "N/A" : code.Info)).FirstOrDefault() ?? string.Empty
            };
            installedCapacity.Add(capacity);
            _logger.LogInformation("{Country} with {Capacity}MW for PSR: {Psr}", capacity.Country, capacity.Capacity, capacity.Psr);
        }

        return installedCapacity;
    }
}