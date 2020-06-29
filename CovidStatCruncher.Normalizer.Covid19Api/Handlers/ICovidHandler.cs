using System;
using System.Threading.Tasks;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Request;

namespace CovidStatCruncher.Normalizer.Covid19Api.Handlers
{
    public interface ICovidHandler
    {
        Task<TResponse> GetCovidData<TResponse> (RequestType requestType);
        Task<TResponse> GetCovidData<TResponse>(RequestType requestType, string CountryName);
        Task<TResponse> GetCovidData<TResponse>(RequestType requestType, string CountryName, DateTime fromDate);
    }
}
