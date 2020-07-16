using CovidStatCruncher.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidStatCruncher.Normalizer.OwinDataSet.Services
{
    public class AthenaUpdateService : IAthenaUpdateService
    {
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AthenaUpdateService(ILogger<AthenaUpdateService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public Task MapAndUpdate(List<CountryEnrichedData> listOfCountryUpdates)
        {
            _logger.LogInformation("Checking database for list of countries to update");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CovidStatCruncherContext>();

                var countriesToCheck = context.Countries
                    .Where(country => country.HasData == true)
                    .ToListAsync();

            _logger.LogInformation($"ReturnCountries:{countriesToCheck.Result.Count}");

                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                List<RegionInfo> countries = new List<RegionInfo>();
                foreach (CultureInfo ci in cultures)
                {
                    RegionInfo regionInfo = new RegionInfo(ci.Name);
                    if (countries.Count(x => x.EnglishName == regionInfo.EnglishName) <= 0)
                        countries.Add(regionInfo);
                }


                foreach (var x in listOfCountryUpdates)
                {
                    _logger.LogInformation($"Check if data exists for : {x.IsoCode}");
                    if (x.IsoCode != null)
                    {
                        var countryToUpdate = countries.FirstOrDefault(iso => iso.ThreeLetterISORegionName == x.IsoCode);
                        if (countryToUpdate != null)
                        {
                            var itemToChange = countriesToCheck.Result.FirstOrDefault(d => d.Iso2 == countryToUpdate.TwoLetterISORegionName);
                            if (itemToChange != null)
                            {
                                _logger.LogInformation($"Updating {itemToChange.CountryName}");
                                itemToChange.Population = x.Population;
                                itemToChange.PopulationDensity = x.PopulationDensity;
                                itemToChange.MedianAge = x.MedianAge;
                                itemToChange.Aged65Older = x.Aged65Older;
                                itemToChange.Aged70Older = x.Aged70Older;
                                itemToChange.DiabetesPrevalence = x.DiabetesPrevalence;
                                itemToChange.LifeExpectancy = x.LifeExpectancy;
                                itemToChange.GdpPerCapita = x.GdpPerCapita;
                                itemToChange.HandwashingFacilities = x.HandwashingFacilities;
                                itemToChange.HospitalBedsPerThousand = x.HospitalBedsPerThousand;

                                context.Countries.Update(itemToChange);
                                context.SaveChanges();
                            }
                        }
                    }
                }

            }
            return Task.CompletedTask;
        }
    }
}
