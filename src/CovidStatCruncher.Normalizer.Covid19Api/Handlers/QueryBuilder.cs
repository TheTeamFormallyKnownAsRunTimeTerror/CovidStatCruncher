using System;
using System.Collections.Generic;
using System.Text;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Request;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.ByCountry.ByCountryAllStatus;
using CovidStatCruncher.Normalizer.Covid19Api.Settings;
using Microsoft.Extensions.Logging;

namespace CovidStatCruncher.Normalizer.Covid19Api.Handlers
{
    public class QueryBuilder : IQueryBuilder 
    {
        private readonly ILogger _logger;
        //private readonly Covid19ApiNormalizingSettings _settings;

        private string BaseUrl = "https://api.covid19api.com";

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
                case RequestType.Stats:
                    return GetStatsUpdates();
            }
            _logger.LogWarning($"Request Type: {requestType} not found, returned default");
            return GetDefault();
        }

        public string BuildRequestUri(RequestType requestType, string countryName)
        {
            switch (requestType)
            {
                case RequestType.ByCountryAllStatus:
                    return ByCountryAllStatus(countryName);
                case RequestType.LiveByCountryAllStatus:
                    return LiveByCountryAllStatus(countryName);
            }
            _logger.LogWarning($"Request Type: {requestType} not found, returned default");
            return GetDefault();
        }

        public string BuildRequestUri(RequestType requestType, string countryName, DateTime fromDate)
        {
            switch (requestType)
            {
                case RequestType.ByCountryAllStatusRange:
                    return ByCountryAllStatusRange(countryName, fromDate);

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

        private string GetStatsUpdates()
        {
            return $"{BaseUrl}/stats";
        }

        private string ByCountryAllStatusRange(string countryName, DateTime fromDate)
        {
            DateTime toDate = DateTime.Now.AddDays(-1);
            return $"{BaseUrl}/country/{countryName}?from={fromDate:yyyy-MM-dd}&to={toDate:yyyy-MM-dd}";
        }

        private string ByCountryAllStatus(string countryName)
        {
            return $"{BaseUrl}/total/country/{countryName}";
        }

        private string LiveByCountryAllStatus(string countryName)
        {
            return $"{BaseUrl}/live/country/{countryName}";
        }
    }
}
