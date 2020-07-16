using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.LiveByCountry.AllStatus
{
    [Serializable]
    [DataContract]
    public class LiveByCountryUpdates
    {
        [DataMember(Name = "Country")]
        public string Country { get; set; }
        [DataMember(Name = "CountryCode")]
        public string CountryCode { get; set; }
        [DataMember(Name = "Province")]
        public string Province { get; set; }
        [DataMember(Name = "City")]
        public string City { get; set; }
        [DataMember(Name = "CityCode")]
        public string CityCode { get; set; }
        [DataMember(Name = "Lat")]
        public string Lat { get; set; }
        [DataMember(Name = "Lon")]
        public string Lon { get; set; }
        [DataMember(Name = "Confirmed")]
        public int Confirmed { get; set; }
        [DataMember(Name = "Deaths")]
        public int Deaths { get; set; }
        [DataMember(Name = "Recovered")]
        public int Recovered { get; set; }
        [DataMember(Name = "Active")]
        public int Active { get; set; }
        [DataMember(Name = "Date")]
        public DateTime Date { get; set; }



    }

}
