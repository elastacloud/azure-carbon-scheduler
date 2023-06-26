# Azure Carbon Intensity Scheduler 

This project aims to enable users to move their Azure Data Factory workloads in time based on the regional Carbon Intensity. 

The project provides an accurate representation of power generation in each country in Europe and focusses on working out the split between fossil fuel generation and generation by renewable source. As each source (also called a PSR) has an associated carbon emission, the combined pro-rated and dynamic emission is determined by a formula which is generally the sum of generation emissions per type in **gCO2eq/kWh**. This summ isn't scaled effectively enough so will be normalised between 1 to 100 to allow ease of comparison.

The Carbon Intensity reads from the Entsoe database every 30 minutes and updates the current generation numbers and turns into a carbon intensity score. It does this for all countries and saves current data in an Azure Storage Account for the trailing 24 hour period. In addition to this it uses the forecast API to get the leading 24 hour forecast and uses the forecast to determine whether to move the ADF workload.

If it finds a window on a non-critical job it will use this and move workload based on the lowest forecast window with constraints defined by the user. After the job is moved the actual is logged in lieu of the forecast value and the amount of carbon intensity saving is calculated. 

There is no Azure Sustainability API currently available so any big data jobs via Databricks or Synapse using job clusters will be tracked and the VM carbon consumption estimated so that the carbon saving on the job cluster can be calculated and logged.

The intent of this solution is to be able to collect anonymous telemetry from all users to show an understand of the carbon savings that people can undertake by time shifting their data workloads where possible.

![Architecture](docs/Carbon%20Intensity.png "Architecture")

`appsettings.json`
```json
{
  "AppSettings": {
    "ApiKey": ""
  },
  "Codes": {
    "Entsoe": "entsoe.json",
    "Fuels": "fuels.json"
  },
  "DataFactory": {
    "Name": "<ADF instance name>",
    "SubscriptionId": "<Subscription id for ADF instance>",
    "ResourceGroup": "<Resource group name for ADF instance>",
    "TenantId": "<Tenant id for ADF instance>",
    "ClientId": null,
    "ClientSecret": null
  }
}

```
