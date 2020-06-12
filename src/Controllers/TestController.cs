using System.Collections.Generic;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Request;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Countries;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Default;
using CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Summary;
using CovidStatCruncher.Normalizer.Covid19Api.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CovidStatCruncher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        //private readonly ILogger _logger;
        private readonly ICovidHandler _covidHandler;

        public TestController(ICovidHandler covidHandler)
        {
            //_logger = logger;
            _covidHandler = covidHandler;
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

            return Ok(result.Result);
        }
    }
}
