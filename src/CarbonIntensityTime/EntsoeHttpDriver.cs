using System.Web;
using CarbonIntensityTypes;
using System.Xml.Serialization;
using CarbonIntensityTime.Core;
using Microsoft.Extensions.Logging;

namespace CarbonIntensityTime;

/// <summary>
/// The Http driver that makes the requests to entsoe
/// </summary>
public class EntsoeHttpDriver : IEntsoeHttpDriver
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<EntsoeHttpDriver> _logger;

    /// <summary>
    /// The Entsoe endpoint
    /// </summary>
    private const string EntsoeEndpoint = "https://web-api.tp.entsoe.eu/api";

    /// <summary>
    /// Creates a new instance of the <see cref="EntsoeHttpDriver"/> type
    /// </summary>
    /// <param name="httpClientFactory">An HTTP client factory instance for creating HTTP clients from</param>
    /// <param name="logger">Logger instance</param>
    public EntsoeHttpDriver(IHttpClientFactory httpClientFactory, ILogger<EntsoeHttpDriver> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    /// <summary>
    /// Given a request of documentType, process, psr and a security token should return a response with the requisite generation details for european countries
    /// </summary>
    public async Task<GL_MarketDocument?> EntsoeGetRequestWithPsr(EntsoeRequest request)
    {
        using var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(EntsoeEndpoint);

         string domain = request.DocumentType == "A65" ? "outBiddingZone_Domain" : "in_Domain";
         var requestParameters = HttpUtility.ParseQueryString("");
        requestParameters.Add("securityToken", request.SecurityToken);
        requestParameters.Add("processType", request.ProcessType);
        requestParameters.Add("documentType", request.DocumentType);
        requestParameters.Add("periodStart", request.StartDate.ToString("yyyyMMddHH00"));
        requestParameters.Add("periodEnd", request.EndDate.ToString("yyyyMMddHH00"));
      if (request.DocumentType != "A65") {
         requestParameters.Add("in_Domain", request.InDomain); 
      } else {
         requestParameters.Add("outBiddingZone_Domain", request.InDomain);
      }
        if (!string.IsNullOrEmpty(request.Psr))
        {
            requestParameters.Add("psrType", request.Psr);
        }

        var response = await client.GetAsync(client.uri);
        var responseData = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            var errorResponseText = GetErrorResponse(responseData);
                    
            _logger.LogError(
                "Non-successful status code '{StatusCode}' returned from Entsoe API for request '{Request}' with message\n{Message}",
                response.StatusCode,
                response.RequestMessage?.RequestUri,
                errorResponseText);
                    
            throw new EntsoeException($"Entsoe API request returned a non-successful status code '{response.StatusCode}'");
        }
            
        var mySerializer = new XmlSerializer(typeof(GL_MarketDocument));
        return mySerializer.Deserialize(new StringReader(responseData)) as GL_MarketDocument;
    }

    /// <summary>
    /// Converts the error response into an Acknowledgement document to retrieve the error context from. If this fails
    /// then the full response content is returned
    /// </summary>
    /// <param name="responseContent">The response content to deserialize</param>
    /// <returns>The response from the deserialized content, or the full response content</returns>
    private static string GetErrorResponse(string responseContent)
    {
        var mySerializer = new XmlSerializer(typeof(Acknowledgement_MarketDocument));

        try
        {
            if (mySerializer.Deserialize(new StringReader(responseContent)) is Acknowledgement_MarketDocument acknowledgement)
            {
                return acknowledgement.Reason[0].text;
            }

            return responseContent;
        }
        catch
        {
            return responseContent;
        }
    }
}