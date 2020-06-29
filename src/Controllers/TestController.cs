using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidStatCruncher.Infrastructure.Data;
using CovidStatCruncher.Infrastructure.Models;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Request;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.ByCountry.ByCountryAllStatus;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Countries;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Default;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.LiveByCountry.AllStatus;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Stats;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Summary;
using CovidStatCruncher.Normalizer.Covid19Api.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Countries = CovidStatCruncher.Infrastructure.Models.Countries;

namespace CovidStatCruncher.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICovidHandler _covidHandler;

        private readonly CovidStatCruncherContext _context;
        //private readonly CovidStatCruncherContext _context;

        public TestController(ILogger<TestController> logger, ICovidHandler covidHandler, CovidStatCruncherContext context)
        {
            _logger = logger;
            _covidHandler = covidHandler;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(RequestType requestType)
        {

            var result = _covidHandler.GetCovidData<Summary>(requestType);

            return Ok(result.Result);
        }

        [HttpGet]
        [Route("summary")]
        public IActionResult GetSummary()
        {

            var result = _covidHandler.GetCovidData<Summary>(RequestType.Summary);

            return Ok(result.Result);
        }

        [HttpGet]
        [Route("default")]
        public IActionResult GetDefault()
        {

            var result = _covidHandler.GetCovidData<Default>(RequestType.Default);

            return Ok(result.Result);
        }

        [HttpGet]
        [Route("countries")]
        public IActionResult GetCountries()
        {

            var result = _covidHandler.GetCovidData<List<Country>>(RequestType.Countries);

            using (CovidStatCruncherContext context = new CovidStatCruncherContext())
            {
                foreach (var country in result.Result)
                {
                    var countryToAdd = new Countries
                    {
                        CountryName = country.Name,
                        CountrySlug = country.Slug,
                        Iso2 = country.ISO2,
                        HasData = false
                    };

                    context.Countries.Add(countryToAdd);
                }

                context.SaveChanges();
            }


            return Ok(result.Result);
        }


        [HttpGet]
        [Route("getcountries")]
        public async Task<List<Countries>> GetCountriesFromDataBase()
        {
            using (CovidStatCruncherContext context = new CovidStatCruncherContext())
            {
                var countries = await context.Countries.ToListAsync();
                return countries;
            }
        }

        [HttpGet]
        [Route("bycountry")]
        public IActionResult GetByCountry()
        {

            var result = _covidHandler.GetCovidData<List<CountryUpdates>>(RequestType.ByCountryAllStatus);
            
            return Ok(result.Result);
        }
        [HttpGet]
        [Route("livebycountry")]
        public IActionResult GetLiveByCountry()
        {

            var result = _covidHandler.GetCovidData<List<LiveByCountryUpdates>>(RequestType.LiveByCountryAllStatus, "ireland");

            return Ok(result.Result);
        }

        [HttpGet]
        [Route("startup")]
        public async Task<IActionResult> Startup()
        {

            using (CovidStatCruncherContext context = new CovidStatCruncherContext())
            {
                var countries = await context.Countries.AsNoTracking().ToListAsync();

                foreach (var country in countries)
                {
                    _logger.LogInformation($"Checking if Country:{country.CountryName} has been updated");
                    if (country.HasData == null || country.HasData == false)
                    {
                        _logger.LogInformation($"Updating Country:{country.CountryName}");
                        var results = await _covidHandler.GetCovidData<List<CountryUpdates>>(RequestType.ByCountryAllStatus, country.CountrySlug);
                        var coordResults = await _covidHandler.GetCovidData<List<LiveByCountryUpdates>>(RequestType.LiveByCountryAllStatus, country.CountrySlug);
                        var coordResultsToUse = coordResults.FirstOrDefault();
                        bool hasCoords = coordResultsToUse != null;
                        CountryData updateToAdd = new CountryData();
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
            }

            return Ok("All G Homie");
        }
        [HttpGet]
        [Route("stats")]
        public IActionResult GetStats()
        {

            var result = _covidHandler.GetCovidData<Stats>(RequestType.Stats);

            return Ok(result.Result);
        }

        [HttpGet]
        [Route("GetCountryUpdatesRange")]
        public IActionResult GetCountryUpdatesRange()
        {
            var dateTime = DateTime.Now.Subtract(TimeSpan.FromDays(3));
            var result = _covidHandler.GetCovidData<List<CountryUpdates>>(RequestType.ByCountryAllStatusRange,"ireland", dateTime );

            return Ok(result.Result);
        }
    }
}
