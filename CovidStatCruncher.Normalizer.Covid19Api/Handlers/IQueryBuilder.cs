using System;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Request;

namespace CovidStatCruncher.Normalizer.Covid19Api.Handlers
{
    public interface IQueryBuilder
    {
        string BuildRequestUri(RequestType requestType);
        string BuildRequestUri(RequestType requestType, string countryName);
        string BuildRequestUri(RequestType requestType, string countryName, DateTime fromDate);

    }
}
