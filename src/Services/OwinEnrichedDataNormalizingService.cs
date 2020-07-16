using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Athena;
using Amazon.Athena.Model;
using CovidStatCruncher.Infrastructure.SecurityProvider;
using CovidStatCruncher.Normalizer.OwinDataSet;
using CovidStatCruncher.Normalizer.OwinDataSet.Services;
using CovidStatCruncher.Settings.Deployment;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CovidStatCruncher.Services
{
    public class OwinEnrichedDataNormalizingService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IAwsSecurityProvider _securityProvider;
        private readonly IAthenaDataService _athenaDataService;
        private readonly IAthenaUpdateService _athenaUpdateService;
        private readonly DeploymentSettings _deploymentSettings;


        public OwinEnrichedDataNormalizingService(ILogger<OwinEnrichedDataNormalizingService> logger,
            IAwsSecurityProvider securityProvider, IAthenaDataService athenaDataService,
            IAthenaUpdateService athenaUpdateService)
        {
            _logger = logger;
            _securityProvider = securityProvider;
            _athenaDataService = athenaDataService;
            _athenaUpdateService = athenaUpdateService;
            _deploymentSettings = new DeploymentSettings() { IsFirstDeploy = false };

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting Owin Normalizing Service");
            if (_deploymentSettings.IsFirstDeploy)
            {
                _logger.LogInformation("Fetching AWS Security Credentials");
                var securityCredentials = await _securityProvider.GetTemporaryCredentialsAsync();
                _logger.LogInformation($"Security Credientials fetched correctly - Temp Token = {securityCredentials.GetCredentials().Token} ");

                using (var athenaClient = new AmazonAthenaClient(securityCredentials,
                    Amazon.RegionEndpoint.EUWest1))
                {
                    var queryContext = new QueryExecutionContext
                    {
                        Catalog = AthenaConstants.ATHENA_CATALOG,
                        Database = AthenaConstants.ATHENA_DEFAULT_DATABASE
                    };
                    var ressultConfig = new ResultConfiguration();
                    ressultConfig.OutputLocation = AthenaConstants.ATHENA_OUTPUT_BUCKET;

                    _logger.LogInformation("Created Athena Client");
                    var listOfCountryData = await _athenaDataService.Run(athenaClient, queryContext, ressultConfig);

                    foreach (var countryEnrichedData in listOfCountryData)
                    {
                        _logger.LogInformation($"CountryCode: {countryEnrichedData.IsoCode}, Population: {countryEnrichedData.Population}, Median Age: {countryEnrichedData.MedianAge}, GPD Per Capita: {countryEnrichedData.GdpPerCapita}");
                    }


                    List<CountryEnrichedData> list = new List<CountryEnrichedData>();
                    await _athenaUpdateService.MapAndUpdate(listOfCountryData);
                }
            }
            _logger.LogInformation($"Data is currently up to date, shutting down Owin Normalizer");
        }
    }
}
