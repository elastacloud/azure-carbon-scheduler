using System.Text.Json;
using CarbonIntensityTime.Core.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarbonIntensityTime;

public interface ICodesLoader
{
    ICollection<EntsoeCodes> Entsoe();

    ICollection<FuelCodes> Fuel();
}

public class CodesLoader : ICodesLoader
{
    private readonly string _entsoePath;
    private readonly string _fuelsPath;
    private readonly ILogger<CodesLoader> _logger;

    public CodesLoader(IOptions<CodesSettings> settings, ILogger<CodesLoader> logger)
    {
        _entsoePath = settings.Value.Entsoe;
        _fuelsPath = settings.Value.Fuels;
        _logger = logger;
    }
    
    public ICollection<EntsoeCodes> Entsoe()
    {
        return ReadCodes<EntsoeCodes>(_entsoePath);
    }

    public ICollection<FuelCodes> Fuel()
    {
        return ReadCodes<FuelCodes>(_fuelsPath);
    }

    private ICollection<T> ReadCodes<T>(string sourcePath)
    {
        _logger.LogDebug("Loading codes for {CodesType}", typeof(T).Name);
        
        using var sourceReader = File.OpenRead(sourcePath);
        var collection = JsonSerializer.Deserialize<List<T>>(sourceReader) ?? new List<T>();

        _logger.LogInformation("Reading {CodeType}. There are {Count} codes present", typeof(T).Name, collection.Count);

        return collection;
    }
}