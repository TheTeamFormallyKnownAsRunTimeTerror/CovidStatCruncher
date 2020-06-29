using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CovidStatCruncher.Infrastructure.Models;

namespace CovidStatCruncher.Normalizer.Covid19Api.DataMethods.Startup
{
    public interface IGetStartUpDataService
    {
        Task<bool> GetCountries();
        Task<bool> GetCountryData();
    }
}
