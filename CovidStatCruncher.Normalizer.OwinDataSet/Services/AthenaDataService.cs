using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.Athena;
using Amazon.Athena.Model;
using Microsoft.Extensions.Logging;

namespace CovidStatCruncher.Normalizer.OwinDataSet.Services
{
    public class AthenaDataService : IAthenaDataService
    {
        private readonly ILogger _logger;

        public AthenaDataService(ILogger<AthenaDataService> logger)
        {
            _logger = logger;
        }

        public async Task<List<CountryEnrichedData>> Run(IAmazonAthena client, QueryExecutionContext queryContext, ResultConfiguration resultConfig)
        {
            /* Execute a simple query on a table */
            var queryRequest = new StartQueryExecutionRequest()
            {
                QueryString = AthenaConstants.ATHENA_COUNTRY_DATA_QUERY,
                QueryExecutionContext = queryContext,
                ResultConfiguration = resultConfig
            };

            try
            {
                var ListOfCountryEnrichedData = new List<CountryEnrichedData>();
                /* Executes the query in an async manner */
                var queryResult = await client.StartQueryExecutionAsync(queryRequest);
                /* Call internal method to parse the results and return a list of key/value dictionaries */
                List<Dictionary<String, String>> items = await GetQueryExecution(client, queryResult.QueryExecutionId);
                foreach (var item in items)
                {
                    var countryUpdate = new CountryEnrichedData();
                    foreach (KeyValuePair<String, String> pair in item)
                    {
                        

                        switch (pair.Key)
                        {
                            case "iso_code":
                                if (pair.Value == "iso_code")
                                {
                                    break;
                                }
                                else
                                {
                                    countryUpdate.IsoCode = pair.Value;
                                }
                                break;
                            case "population":
                                if (pair.Value == "population")
                                {
                                    break;
                                }
                                else
                                {
                                    countryUpdate.Population = Convert.ToDouble(pair.Value);
                                }
                                break;
                            case "population_density":
                                if (pair.Value == "population_density")
                                {
                                    break;
                                }
                                else
                                {
                                    countryUpdate.PopulationDensity = Convert.ToDouble(pair.Value);
                                }
                                break;
                            case "median_age":
                                if (pair.Value == "median_age")
                                {
                                    break;
                                }
                                else
                                {
                                    countryUpdate.MedianAge = Convert.ToDouble(pair.Value);
                                }
                                break;
                            case "aged_65_older":
                                if (pair.Value == "aged_65_older")
                                {
                                    break;
                                }
                                else
                                {
                                    countryUpdate.Aged65Older = Convert.ToDouble(pair.Value);
                                }
                                break;
                            case "aged_70_older":
                                if (pair.Value == "aged_70_older")
                                {
                                    break;
                                }
                                else
                                {
                                    countryUpdate.Aged70Older = Convert.ToDouble(pair.Value);
                                }
                                break;
                            case "gdp_per_capita":
                                if (pair.Value == "gdp_per_capita")
                                {
                                    break;
                                }
                                else
                                {
                                    countryUpdate.GdpPerCapita = Convert.ToDouble(pair.Value);
                                }
                                break;
                            case "diabetes_prevalence":
                                if (pair.Value == "diabetes_prevalence")
                                {
                                    break;
                                }
                                else
                                {
                                    countryUpdate.DiabetesPrevalence = Convert.ToDouble(pair.Value);
                                }
                                break;
                            case "handwashing_facilities":
                                if (pair.Value == "handwashing_facilities")
                                {
                                    break;
                                }
                                else
                                {
                                    countryUpdate.HandwashingFacilities = Convert.ToDouble(pair.Value);
                                }
                                break;
                            case "hospital_beds_per_thousand":
                                if (pair.Value == "hospital_beds_per_thousand")
                                {
                                    break;
                                }
                                else
                                {
                                    countryUpdate.HospitalBedsPerThousand = Convert.ToDouble(pair.Value);
                                }
                                break;
                            case "life_expectancy":
                                if (pair.Value == "life_expectancy")
                                {
                                    break;
                                }
                                else
                                {
                                    countryUpdate.LifeExpectancy = Convert.ToDouble(pair.Value);
                                }
                                break;

                        }
                    }
                    ListOfCountryEnrichedData.Add(countryUpdate);
                }

                return ListOfCountryEnrichedData;
            }

            catch (InvalidRequestException e)
            { 
                _logger.LogInformation("Run Error: {0}", e.Message);
            }

            return new List<CountryEnrichedData>();
        }

        public async Task<List<Dictionary<string, string>>> GetQueryExecution(IAmazonAthena client, string id)
        {
            List<Dictionary<String, String>> items = new List<Dictionary<String, String>>();
            GetQueryExecutionResponse results = null;
            QueryExecution queryExecution = null;
            /* Declare query execution request object */
            GetQueryExecutionRequest queryExReq = new GetQueryExecutionRequest()
            {
                QueryExecutionId = id
            };
            /* Poll API to determine when the query completed */
            do
            {
                try
                {
                    results = await client.GetQueryExecutionAsync(queryExReq);
                    queryExecution = results.QueryExecution;
                    _logger.LogInformation("Status: {0}... {1}", queryExecution.Status.State, queryExecution.Status.StateChangeReason);

                    await Task.Delay(5000); //Wait for 5sec before polling again
                }
                catch (InvalidRequestException e)
                {
                    _logger.LogInformation("GetQueryExec Error: {0}", e.Message);
                }
            } while (queryExecution.Status.State == "RUNNING" || queryExecution.Status.State == "QUEUED");

            _logger.LogInformation("Data Scanned for {0}: {1} Bytes", id, queryExecution.Statistics.DataScannedInBytes);

            /* Declare query results request object */
            GetQueryResultsRequest queryResultRequest = new GetQueryResultsRequest()
            {
                QueryExecutionId = id,
                MaxResults = 10
            };

            GetQueryResultsResponse queryResult = null;
            /* Page through results and request additional pages if available */
            do
            {
                queryResult = await client.GetQueryResultsAsync(queryResultRequest);
                /* Loop over result set and create a dictionary with column name for key and data for value */
                foreach (Row row in queryResult.ResultSet.Rows)
                {
                    Dictionary<String, String> dict = new Dictionary<String, String>();
                    for (var i = 0; i < queryResult.ResultSet.ResultSetMetadata.ColumnInfo.Count; i++)
                    {
                        dict.Add(queryResult.ResultSet.ResultSetMetadata.ColumnInfo[i].Name, row.Data[i].VarCharValue);
                    }

                    items.Add(dict);
                }

                if (queryResult.NextToken != null)
                {
                    queryResultRequest.NextToken = queryResult.NextToken;
                }
            } while (queryResult.NextToken != null);

            /* Return List of dictionary per row containing column name and value */
            return items;
        }
    }

    public class CountryEnrichedData
    {
        public string IsoCode{ get; set; }
        public double Population { get; set; }
        public double PopulationDensity { get; set; }
        public double MedianAge { get; set; }
        public double Aged65Older { get; set; }
        public double Aged70Older { get; set; }
        public double GdpPerCapita { get; set; }
        public double DiabetesPrevalence { get; set; }
        public double HandwashingFacilities { get; set; }
        public double HospitalBedsPerThousand { get; set; }
        public double LifeExpectancy { get; set; }
    }
}
