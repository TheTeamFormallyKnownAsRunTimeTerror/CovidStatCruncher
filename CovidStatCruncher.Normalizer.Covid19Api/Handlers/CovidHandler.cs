using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Request;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CovidStatCruncher.Normalizer.Covid19Api.Handlers
{
    public class CovidHandler : ICovidHandler
    {
        private readonly ILogger _logger;
        private readonly IQueryBuilder _queryBuilder;

        public CovidHandler(ILogger<CovidHandler> logger, IQueryBuilder queryBuilder)
        {
            _logger = logger;
            _queryBuilder = queryBuilder;
        }

        public async Task<TResponse> GetCovidData<TResponse>(RequestType requestType)
        {
            _logger.LogInformation($"Attempting to build request to retrieve api data.");
            var query = _queryBuilder.BuildRequestUri(requestType);

            var result = await GetAsync<TResponse>(query);

            Console.WriteLine($"{result}");

            return result;
        }

        public async Task<TResponse> GetCovidData<TResponse>(RequestType requestType, string countryName)
        {
            _logger.LogInformation($"Attempting to build request to retrieve api data.");
            var query = _queryBuilder.BuildRequestUri(requestType, countryName);

            var result = await GetAsync<TResponse>(query);

            Console.WriteLine($"{result}");

            return result;
        }

        public async Task<TResponse> GetCovidData<TResponse>(RequestType requestType, string countryName, DateTime fromDate)
        {
            _logger.LogInformation($"Attempting to build request to retrieve api data.");
            var query = _queryBuilder.BuildRequestUri(requestType, countryName, fromDate);

            var result = await GetAsync<TResponse>(query);

            return result;
        }


        private HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private async Task<TResponse> GetAsync<TResponse>(string requestUri)
        {
            using (var client = GetHttpClient())
            {
                _logger.LogDebug($"Calling Api with at {requestUri}");

                var response = await client.GetAsync(requestUri);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TResponse>(content);

                return result;
            }
        }
    }
}
