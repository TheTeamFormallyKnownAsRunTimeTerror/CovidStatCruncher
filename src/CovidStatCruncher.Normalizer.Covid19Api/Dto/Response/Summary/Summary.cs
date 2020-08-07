using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Summary
{
    [Serializable]
    [DataContract]
    public class Summary
    {
        [DataMember(Name = "Global")]
        public Global Global { get; set; }
        [DataMember(Name = "Countries")]
        public List<Countries> Countries { get; set; }
        [DataMember(Name = "Date")]
        public DateTime DateTime { get; set; }
    }
}
