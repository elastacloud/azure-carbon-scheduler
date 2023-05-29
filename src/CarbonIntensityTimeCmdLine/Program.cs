﻿using CarbonIntensityTime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text.Json;

Console.WriteLine("Building Carbon Application");
var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", false, true)
            .AddEnvironmentVariables();
var configuration = builder.Build();
// Get the api key for the entsone api
string? apiKey = configuration.GetSection("AppSettings")["ApiKey"];
// Create the Entsoe codes
string? entsoe = await File.ReadAllTextAsync("entsoe.json");
var entsoeCodes = JsonSerializer.Deserialize<EntsoeCodes[]>(entsoe);
Console.WriteLine($"Reading entsoe codes data, there are {entsoeCodes?.Length} codes present");
// Create the Fuel codes
string? fuels = await File.ReadAllTextAsync("fuels.json");
var fuelsCodes = JsonSerializer.Deserialize<FuelCodes[]>(fuels);
Console.WriteLine($"Reading fuels codes, there are {fuelsCodes?.Length} codes present");
// Get a sample of the output for a particular entsoecode
var euro = new EuropeanLoadHelper(apiKey, entsoeCodes, fuelsCodes);
string? ukCode = entsoeCodes?.Single(country => country.Code == "CTA|National Grid").EntsoeId;
Console.WriteLine("Gets the installed capacity PSRs for UK on " + DateTime.UtcNow.ToString("D"));
var installedCapacity = await euro.GetInstalledCapacityByCountry(ukCode);
installedCapacity.ForEach(installed => Console.WriteLine($"{installed.Psr}: {installed.Capacity} MW"));
Console.WriteLine("PRESS ANY KEY TO END ....");
Console.Read();
