namespace CarbonIntensityTime.Azure;

public interface ITokenProvider
{
    Task<string> GetToken(params string[] scope);
}