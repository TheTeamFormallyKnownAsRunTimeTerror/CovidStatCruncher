using System;
using System.Runtime.Serialization;

namespace CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Default
{
    [Serializable]
    [DataContract]
    public class CountryTotalRoute
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "Path")]
        public string Path { get; set; }
    }
}
