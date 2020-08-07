using System;
using System.Runtime.Serialization;

namespace CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Summary
{
    [Serializable]
    [DataContract]
    public class Global
    {
        [DataMember(Name = "NewConfirmed")]
        public int NewConfirmed { get; set; }
        [DataMember(Name = "TotalConfirmed")]
        public int TotalConfirmed { get; set; }
        [DataMember(Name = "NewDeaths")]
        public int NewDeaths { get; set; }
        [DataMember(Name = "TotalDeaths")]
        public int TotalDeaths{ get; set; }
        [DataMember(Name = "NewRecovered")]
        public int NewRecovered{ get; set; }
        [DataMember(Name = "TotalRecovered")]
        public int TotalRecovered { get; set; }


    }
}
