using System;
using System.Runtime.Serialization;

namespace CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Countries
{
    [Serializable]
    [DataContract]
    public class Country
    {
        [DataMember(Name = "Country")] 
        public string Name { get; set; }
        [DataMember(Name = "Slug")]
        public string Slug { get; set; }
        [DataMember(Name = "ISO2")]
        public string ISO2 { get; set; }

    }
}
