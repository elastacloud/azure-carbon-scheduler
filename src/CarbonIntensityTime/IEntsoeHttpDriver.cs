using CarbonIntensityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonIntensityTime
{
   /// <summary>
   /// The Http driver that makes the requests to entsoe
   /// </summary>
   public interface IEntsoeHttpDriver
   {
      /// <summary>
      /// Given a request of documenttype, process, psr and a security token should return a response with the requisite generation details for european countries
      /// </summary>
      public Task<GL_MarketDocument> EntsoeGetRequestWithPsr(EntsoeRequest request);
   }
}
