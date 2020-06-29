using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using CovidStatCruncher.Normalizer.Covid19Api.DataMethods.Startup;
using CovidStatCruncher.Normalizer.Covid19Api.DataMethods.UpdatedData;
using CovidStatCruncher.Normalizer.Covid19Api.Handlers;
using CovidStatCruncher.Settings.Deployment;
using Microsoft.Extensions.Logging;

namespace CovidStatCruncher.Normalizer.Covid19Api.Services
{
    public class Covid19ApiNormalizingBackgroundService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ICovidHandler _covidHandler;
        private readonly DeploymentSettings _deploymentSettings;
        private readonly IGetStartUpDataService _getStartUpDataService;
        private readonly IGetUpdatedDataService _getUpdatedDataService;

        public Covid19ApiNormalizingBackgroundService(ILogger<Covid19ApiNormalizingBackgroundService> logger,
            ICovidHandler covidHandler, IGetStartUpDataService getStartUpDataService, IGetUpdatedDataService getUpdatedDataService)
        {
            _logger = logger;
            _covidHandler = covidHandler;
            _getStartUpDataService = getStartUpDataService;
            _getUpdatedDataService = getUpdatedDataService;
            _deploymentSettings = new DeploymentSettings() {IsFirstDeploy = false};
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Normalizing Background service is starting");
            _logger.LogInformation($"IsFirstDeploy:{_deploymentSettings.IsFirstDeploy}");

            if (_deploymentSettings.IsFirstDeploy)
            {
                // Start up and populate the database from scratch
                _logger.LogInformation($"First Deployment - Fetching country data"); 
                await _getStartUpDataService.GetCountries();
                _logger.LogInformation($"Country list complete, Fetching country daily data");
                //Query the Url and get a list of countries 
                await _getStartUpDataService.GetCountryData();
                _logger.LogInformation("Country covid data table populated");
            }

            _logger.LogInformation($"Starting Update Data Job.");
            _getUpdatedDataService.GetUpdatedCountryData();
            // Startup and read latest entries from the database, and then fetch any data later than the last updated date. 

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Normalizing Background service is stopping");
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}