using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidStatCruncher.Infrastructure.Data;
using CovidStatCruncher.Normalizer.Covid19Api.Handlers;
using Microsoft.Extensions.Logging;
using CovidStatCruncher.Infrastructure.Models;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Request;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.ByCountry.ByCountryAllStatus;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Countries;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.LiveByCountry.AllStatus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CovidStatCruncher.Normalizer.Covid19Api.DataMethods.Startup
{
    public class GetStartupData : IGetStartUpDataService
    {
        private readonly ILogger _logger;
        private readonly ICovidHandler _covidHandler;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public GetStartupData(ILogger<GetStartupData> logger, ICovidHandler covidHandler, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _covidHandler = covidHandler;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<bool> GetCountries()
        {
            try
            {
                _logger.LogInformation("Fetching Country Data from the Covid19Api");
                var result = await _covidHandler.GetCovidData<List<Country>>(RequestType.Countries);
                _logger.LogInformation($"Returned {result.Count} countries");

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<CovidStatCruncherContext>();


                    {
                        foreach (var country in result)
                        {
                            var countryToAdd = new Countries
                            {
                                CountryName = country.Name,
                                CountrySlug = country.Slug,
                                Iso2 = country.ISO2,
                                HasData = false
                            };
                            _logger.LogInformation($"Added {countryToAdd.CountryName} to the context");
                            context.Countries.Add(countryToAdd);
                        }

                        _logger.LogInformation($"Storing {context.Countries.Count()} Countries");

                        context.SaveChanges();

                    }
                }
            }
            catch (Exception exc)
            {
                _logger.LogError("Error fetching and storing startup country data.");
            }

            return true;
        }

        public async Task<bool> GetCountryData()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CovidStatCruncherContext>();
                var countries = await context.Countries.AsNoTracking().ToListAsync();

                CountryData updateToAdd = new CountryData();
                foreach (var country in countries)
                {
                    _logger.LogInformation($"Checking if Country: {country.CountryName} has been updated");
                    if (country.HasData == null || country.HasData == false)
                    {
                        _logger.LogInformation($"Updating Country:{country.CountryName}");

                        var results =
                            await _covidHandler.GetCovidData<List<CountryUpdates>>(RequestType.ByCountryAllStatus,
                                country.CountrySlug);

                        var coordResults =
                            await _covidHandler.GetCovidData<List<LiveByCountryUpdates>>(
                                RequestType.LiveByCountryAllStatus, country.CountrySlug);
                        _logger.LogInformation("Getting historical and georgraphical data.");

                        var coordResultsToUse = coordResults.FirstOrDefault();
                        bool hasCoords = coordResultsToUse != null;

                        foreach (var result in results)
                        {
                            if (hasCoords)
                            {
                                _logger.LogInformation($"Country:{country.CountryName}, has valid coords");
                                updateToAdd = new CountryData
                                {
                                    CountryName = result.Country,
                                    CountryId = country.CountryId,
                                    City = result.City,
                                    CountryCode = coordResultsToUse.CountryCode ?? result.CountryCode,
                                    Province = result.Province,
                                    Latitude = coordResultsToUse.Lat ?? result.Latitude.ToString(),
                                    Longitude = coordResultsToUse.Lon ?? result.Longitude.ToString(),
                                    ActiveCases = result.Active,
                                    Deaths = result.Deaths,
                                    Recovered = result.Recovered,
                                    ConfirmedCases = result.Confirmed,
                                    DateTime = result.Date
                                };
                            }
                            else
                            {
                                _logger.LogInformation($"Country:{country.CountryName}, does not have valid coords");

                                updateToAdd = new CountryData
                                {
                                    CountryName = result.Country,
                                    CountryId = country.CountryId,
                                    City = result.City,
                                    CountryCode = result.CountryCode,
                                    Province = result.Province,
                                    Latitude = result.Latitude.ToString(),
                                    Longitude = result.Longitude.ToString(),
                                    ActiveCases = result.Active,
                                    Deaths = result.Deaths,
                                    Recovered = result.Recovered,
                                    ConfirmedCases = result.Confirmed,
                                    DateTime = result.Date
                                };
                            }

                            //Add the Country Updates one by one. 
                            context.CountryData.Add(updateToAdd);
                            context.SaveChanges();

                            //Change the hasData value in the DB for that country.
                            country.HasData = true;
                            context.Countries.Update(country);

                            //context.ChangeTracker.AcceptAllChanges();
                        }

                    }
                }

                return true;
            }
        }

    }
}
