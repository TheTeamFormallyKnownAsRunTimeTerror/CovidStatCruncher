using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidStatCruncher.Normalizer.OwinDataSet.Services
{
    public interface IAthenaUpdateService
    {
        Task MapAndUpdate(List<CountryEnrichedData> listOfCountryUpdates);

    }
}
