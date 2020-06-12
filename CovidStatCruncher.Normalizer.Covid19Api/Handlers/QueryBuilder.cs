using System;
using System.Collections.Generic;
using System.Text;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Request;
using CovidStatCruncher.Normalizer.Covid19Api.Settings;
using Microsoft.Extensions.Logging;

namespace CovidStatCruncher.Normalizer.Covid19Api.Handlers
{
    public class QueryBuilder : IQueryBuilder 
    {
        private readonly ILogger _logger;
        //private readonly Covid19ApiNormalizingSettings _settings;

        private string BaseUrl = "https://api.covid19api.com/";

        public QueryBuilder(ILogger<QueryBuilder> logger)
        {
            _logger = logger;
            //_settings = settings;
        }

        public string BuildRequestUri(RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.Default:
                    return GetDefault();
                case RequestType.Summary:
                    return GetSummary();
                case RequestType.Countries:
                    return GetCountryList();
            }

            _logger.LogWarning($"Request Type: {requestType} not found, returned default");
            return GetDefault();
        }
        private string GetDefault()
        {
            return $"{BaseUrl}";
        }
        private  string GetSummary()
        {
            return $"{BaseUrl}/summary";
        }

        private  string GetCountryList()
        {
            return $"{BaseUrl}/countries";
        }
    }
}
