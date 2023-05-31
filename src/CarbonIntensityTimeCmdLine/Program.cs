using CarbonIntensityTime;
using CarbonIntensityTime.Core.Configuration;
using CarbonIntensityTime.DataFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Building Carbon Application");

var configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", false, true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .Build();

var serviceProvider = new ServiceCollection()
    .AddOptions()
    .AddHttpClient()
    .AddLogging(loggingBuilder =>
    {
        loggingBuilder
            .AddFilter("Microsoft", LogLevel.Warning)
            .AddFilter("System", LogLevel.Warning)
            .AddFilter("CarbonIntensityTime", LogLevel.Information)
            .AddSimpleConsole(options =>
            {
                options.SingleLine = true;
                options.IncludeScopes = true;
                options.UseUtcTimestamp = false;
                options.TimestampFormat = "HH:mm:ss ";
            });
    })
    .Configure<AppSettings>(configuration.GetSection("AppSettings"))
    .Configure<CodesSettings>(configuration.GetSection("Codes"))
    .Configure<FactorySettings>(configuration.GetSection("DataFactory"))
    .AddScoped<ICodesLoader, CodesLoader>()
    .AddScoped<IEntsoeHttpDriver, EntsoeHttpDriver>()
    .AddScoped<IEuropeanLoadHelper, EuropeanLoadHelper>()
    .AddScoped<IFactoryClient, FactoryClient>()
    .BuildServiceProvider();

var euro = serviceProvider.GetService<IEuropeanLoadHelper>();
var ukCode = euro.GetEntsoeId("CTA|National Grid");

var installedCapacity = await euro.GetInstalledCapacityByCountry(ukCode);

var adfClient = serviceProvider.GetService<IFactoryClient>();
var pipelines = await adfClient.ListPipelines();
foreach (var pipeline in pipelines)
{
    Console.WriteLine(pipeline);
}

Console.WriteLine("PRESS ANY KEY TO END ....");
Console.Read();
