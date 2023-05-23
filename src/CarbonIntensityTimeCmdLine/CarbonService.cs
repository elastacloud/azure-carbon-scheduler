using Microsoft.Extensions.Options;

namespace CarbonIntensityTimeCmdLine
{
   public class CarbonService
   {
      private readonly AppSettings _appSettings;

      public CarbonService(IOptions<AppSettings> appSettings)
      {
         _appSettings = appSettings.Value;
      }

      // Use _databaseSettings and _appSettings as needed...
   }

}
