namespace CarbonIntensityTime.Core;

public class EntsoeException : Exception
{
    public EntsoeException() {}
    
    public EntsoeException(string message) : base(message) {}
    
    public EntsoeException(string message, Exception innerException) : base(message, innerException) {}
}