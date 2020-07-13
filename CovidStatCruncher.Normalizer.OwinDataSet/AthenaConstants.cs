using System;
using System.Collections.Generic;
using System.Text;

namespace CovidStatCruncher.Normalizer.OwinDataSet
{
    public static class AthenaConstants
    {
        public static string ATHENA_CATALOG = "AwsDataCatalog";
        public static string ATHENA_OUTPUT_BUCKET = "s3://covid-global-data-enriched/query-results/";
        public static string ATHENA_DEFAULT_DATABASE = "covidglobaldataenriched";

        public static string ATHENA_COUNTRY_QUERY = "SELECT DISTINCT iso_code from countrydata";

        public static string ATHENA_COUNTRY_DATA_QUERY = "SELECT " +
                                                         "m1.iso_code, "+
                                                         "m1.population, " +
                                                         "m1.median_age, " +
                                                         "m1.population_density, " +
                                                         "m1.aged_65_older, " +
                                                         "m1.aged_70_older, " +
                                                         "m1.gdp_per_capita, " +
                                                         "m1.diabetes_prevalence, " +
                                                         "m1.handwashing_facilities, " +
                                                         "m1.life_expectancy, " +
                                                         "m1.hospital_beds_per_thousand " +
                                                         "FROM countrydata m1 LEFT JOIN countrydata m2 " +
                                                         "ON (m1.iso_code = m2.iso_code AND m1.date<m2.date) " +
                                                         "WHERE m2.date IS NULL;";
    }
}