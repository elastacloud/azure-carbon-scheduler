using CarbonIntensityTypes;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;

namespace CarbonIntensityTime
{
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
        public const string ENTSOE_Endpoint = "https://web-api.tp.entsoe.eu/api";

        public EntsoeHttpDriver(IHttpClientFactory httpClientFactory, ILogger<EntsoeHttpDriver> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Given a request of documenttype, process, psr and a security token should return a response with the requisite generation details for european countries
        /// </summary>
        public async Task<GL_MarketDocument> EntsoeGetRequestWithPsr(EntsoeRequest request)
        {
            GL_MarketDocument glMarketDocument;
            Acknowledgement_MarketDocument acknowledgement;
            string? responseData = null;
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                client.BaseAddress = new Uri(ENTSOE_Endpoint);
                // Construct the query string parameters
                var queryString =
                    $"?securityToken={request.SecurityToken}&processType={request.ProcessType}&documentType={request.DocumentType}&periodStart={request.StartDate.ToString("yyyyMMddHH00")}&periodEnd={request.EndDate.ToString("yyyyMMddHH00")}&in_Domain={request.InDomain}";
                if (!String.IsNullOrEmpty(request.Psr))
                {
                    queryString += $"&psrType={request.Psr}";
                }
                
                _logger.LogDebug("Requesting Entsoe with query string: {QueryString}", queryString);

                HttpResponseMessage response = await client.GetAsync(queryString);
                responseData = await response.Content.ReadAsStringAsync();
            }

            try
            {
                var mySerializer = new XmlSerializer(typeof(GL_MarketDocument));
                glMarketDocument = (GL_MarketDocument) mySerializer.Deserialize(new StringReader(responseData));
            }
            catch (Exception)
            {
                var mySerializer = new XmlSerializer(typeof(Acknowledgement_MarketDocument));
                acknowledgement =
                    (Acknowledgement_MarketDocument) mySerializer.Deserialize(new StringReader(responseData));
                throw new ApplicationException(acknowledgement.Reason[0].text);
            }

            return glMarketDocument;
        }
    }
}