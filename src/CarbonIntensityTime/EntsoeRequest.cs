namespace CarbonIntensityTime
{
   /// <summary>
   /// Represents the request for the entsoe service
   /// </summary>
   public class EntsoeRequest
   {
      /// <summary>
      /// The start date of the request
      /// </summary>
      public DateTime StartDate { get; set; }
      /// <summary>
      /// The end date of the request 
      /// </summary>
      public DateTime EndDate { get; set; }
      /// <summary>
      /// From the process type table in the form of Axx
      /// </summary>
      public string? ProcessType { get; set; }
      /// <summary>
      /// From the document type table in the form of Axx
      /// </summary>
      public string? DocumentType { get; set; }
      /// <summary>
      /// Sometimes this needs to be populated - it is a the type of energy generation e.g. offshore wind
      /// </summary>
      public string? Psr { get; set; }
      /// <summary>
      /// The country indomain value that is being used - every request needs a country 
      /// </summary>
      public string? InDomain { get; set; }
      /// <summary>
      /// The security token - this has to be requested via entsoe and can be changed through the console - only one at a time
      /// </summary>
      public string? SecurityToken { get; set; }
   }
}
