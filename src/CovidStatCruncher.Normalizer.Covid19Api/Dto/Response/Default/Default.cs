using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Default
{
    [Serializable]
    [DataContract]
    public class Default
    {
        [DataMember(Name = "allRoute")]
        public AllRoute AllRoute { get; set; }
        [DataMember(Name = "CountriesRoute")]
        public CountriesRoute CountriesRoute { get; set; }
        [DataMember(Name = "CountryDayOneRoute")] 
        public CountryDayOneRoute CountryDayOneRoute { get; set; }
        [DataMember(Name = "CountryDayOneTotalRoute")]
        public CountryDayOneTotalRoute CountryDayOneTotalRoute { get; set; }
        [DataMember(Name = "CountryRoute")]
        public CountryRoute CountryRoute { get; set; }
        [DataMember(Name = "CountryStatusDayOneLiveRoute")]
        public CountryStatusDayOneLiveRoute CountryStatusDayOneLiveRoute { get; set; }
        [DataMember(Name = "CountryStatusDayOneRoute")]
        public CountryStatusDayOneRoute CountryStatusDayOneRoute { get; set; }
        [DataMember(Name = "CountryStatusDayOneTotalRoute")]
        public CountryStatusDayOneTotalRoute CountryStatusDayOneTotalRoute { get; set; }
        [DataMember(Name = "CountryStatusLiveRoute")]
        public CountryStatusLiveRoute CountryStatusLiveRoute { get; set; }
        [DataMember(Name = "CountryStatusRoute")]
        public CountryStatusRoute CountryStatusRoute { get; set; }
        [DataMember(Name = "CountryStatusTotalRoute")]
        public CountryStatusTotalRoute CountryStatusTotalRoute { get; set; }
        [DataMember(Name = "CountryTotalRoute")]
        public CountryTotalRoute CountryTotalRoute { get; set; }
        [DataMember(Name = "ExportRoute")]
        public ExportRoute ExportRoute { get; set; }
        [DataMember(Name = "LiveCountryRoute")]
        public LiveCountryRoute LiveCountryRoute { get; set; }
        [DataMember(Name = "LiveCountryStatusAfterDateRoute")]
        public LiveCountryStatusAfterDateRoute LiveCountryStatusAfterDateRoute { get; set; }
        [DataMember(Name = "LiveCountryStatusRoute")]
        public LiveCountryStatusRoute LiveCountryStatusRoute { get; set; }
        [DataMember(Name = "SummaryRoute")]
        public SummaryRoute SummaryRoute { get; set; }
        [DataMember(Name = "WebhookRoute")]
        public WebhookRoute WebhookRoute { get; set; }
    }
}
