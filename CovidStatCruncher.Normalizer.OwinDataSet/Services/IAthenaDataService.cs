using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.Athena;
using Amazon.Athena.Model;

namespace CovidStatCruncher.Normalizer.OwinDataSet.Services
{
    public interface IAthenaDataService
    {
        Task <List<CountryEnrichedData>> Run(IAmazonAthena client, QueryExecutionContext qContext, ResultConfiguration resConf);
        Task<List<Dictionary<String, String>>> GetQueryExecution(IAmazonAthena client, String id);
    }
}
