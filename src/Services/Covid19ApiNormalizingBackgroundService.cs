using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Request;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Countries;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Default;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Summary;
using CovidStatCruncher.Normalizer.Covid19Api.Handlers;
using Microsoft.Extensions.Logging;

namespace CovidStatCruncher.Normalizer.Covid19Api.Services
{
    public class Covid19ApiNormalizingBackgroundService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ICovidHandler _covidHandler;

        public Covid19ApiNormalizingBackgroundService(ILogger<Covid19ApiNormalizingBackgroundService> logger, ICovidHandler covidHandler)
        {
            _logger = logger;
            _covidHandler = covidHandler;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Normalizing Background service is starting");
            _logger.LogInformation($"Fetching Summary Data");
            var summaryData = _covidHandler.GetCovidData<Summary>(RequestType.Summary);
            _logger.LogInformation($"Fetching Default Data");
            var defaultData = _covidHandler.GetCovidData<Default>(RequestType.Default);
            _logger.LogInformation($"Fetching Country Data");
            var countryData = _covidHandler.GetCovidData<List<Country>>(RequestType.Countries);

            Task.WaitAll(summaryData, defaultData, countryData);

            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Normalizing Background service is stopping");
            return Task.CompletedTask;
        }
    }
}