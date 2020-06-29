using CovidStatCruncher.Normalizer.Covid19Api.DataMethods.Startup;
using CovidStatCruncher.Normalizer.Covid19Api.DataMethods.UpdatedData;
using CovidStatCruncher.Normalizer.Covid19Api.Handlers;
using CovidStatCruncher.Normalizer.Covid19Api.Services;
using CovidStatCruncher.Normalizer.Covid19Api.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CovidStatCruncher.Ioc
{
    public static class Covid19ApiServices
    {
        public static void AddCovid19ApiServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<Covid19ApiNormalizingSettings>(config.GetSection("Covid19ApiNormalizingSettings"));

            services.AddTransient<IQueryBuilder, QueryBuilder>();
            services.AddTransient<ICovidHandler, CovidHandler>();
            services.AddTransient<IGetStartUpDataService, GetStartupData>();
            services.AddTransient<IGetUpdatedDataService, GetUpdatedDataService>();
            services.AddHostedService<Covid19ApiNormalizingBackgroundService>();
        }
    }
}
