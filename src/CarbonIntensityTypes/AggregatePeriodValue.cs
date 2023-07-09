using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonIntensityTypes
{
   /// <summary>
   /// An aggregate value for the day ahead generation
   /// </summary>
   public class AggregatePeriodValue
   {
      /// <summary>
      /// The time and date when this started for the day ahead
      /// </summary>
      public DateTime StartDate { get; set; }
      /// <summary>
      /// The value of the hour
      /// </summary>
      public int TotalValue { get; set; }
      /// <summary>
      /// Gets the solar and wind values which affect carbon intensity 
      /// </summary>
      public int RenewableValue { get; set; }
   }
}
