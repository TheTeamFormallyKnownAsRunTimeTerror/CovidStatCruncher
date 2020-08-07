using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CovidStatCruncher.Infrastructure.Data;
using CovidStatCruncher.Infrastructure.Models;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Request;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.ByCountry.ByCountryAllStatus;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Stats;
using CovidStatCruncher.Normalizer.Covid19Api.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CovidStatCruncher.Normalizer.Covid19Api.DataMethods.UpdatedData
{
    public class GetUpdatedDataService : IGetUpdatedDataService
    {
        private readonly ILogger _logger;
        private readonly ICovidHandler _covidHandler;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public GetUpdatedDataService(ILogger<GetUpdatedDataService> logger, ICovidHandler covidHandler, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _covidHandler = covidHandler;
            _serviceScopeFactory = serviceScopeFactory;
        }


        public bool GetUpdatedCountryData()
        {
            _logger.LogInformation("Checking database for list of countries to update");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CovidStatCruncherContext>();

                var countriesToCheck = context.Countries
                    .Where(country => country.HasData == true)
                    .ToListAsync();

                _logger.LogInformation($"ReturnCountries:{countriesToCheck.Result.Count}");

                foreach (var country in countriesToCheck.Result)
                {
                    var lastUpdatedDate = context.CountryData
                        .Where(c => c.CountryName == country.CountryName)
                        .OrderByDescending(c => c.DateTime)
                        .FirstOrDefault();


                    if (lastUpdatedDate.DateTime >= DateTime.Today.Date.Subtract(TimeSpan.FromDays(2)))
                    {
                        _logger.LogInformation($"Country: {country.CountryName} is all ready up to date");
                        continue;
                    }

                    _logger.LogDebug($"Fetching new updates for {country.CountryName}");
                    var newUpdates = _covidHandler.GetCovidData<List<CountryUpdates>>(RequestType.ByCountryAllStatusRange, country.CountrySlug, lastUpdatedDate.DateTime.AddDays(1));
                    _logger.LogInformation($"Country:{country.CountryName},NumberOfUpdates:{newUpdates.Result.Count}");

                    foreach (var mappedUpdate in newUpdates.Result.Select(updateToMap => new CountryData
                    {
                        CountryName = updateToMap.Country,
                        CountryId = country.CountryId,
                        City = updateToMap.City,
                        CountryCode = country.Iso2,
                        Province = updateToMap.Province,
                        Latitude = updateToMap.Latitude.ToString(),
                        Longitude = updateToMap.Longitude.ToString(),
                        ActiveCases = updateToMap.Active,
                        Deaths = updateToMap.Deaths,
                        Recovered = updateToMap.Recovered,
                        ConfirmedCases = updateToMap.Confirmed,
                        DateTime = updateToMap.Date
                    }) )
                    {
                        context.CountryData.Add(mappedUpdate);
                        context.SaveChanges();
                    }
                    _logger.LogInformation($"Country:{country.CountryName}, UpdateStatus:Complete, NumberOfSavedUpdates{newUpdates.Result.Count}");
                }
            }
            return true;
        }
    }
}
