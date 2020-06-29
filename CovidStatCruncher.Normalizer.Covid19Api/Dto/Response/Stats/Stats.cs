using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CovidStatCruncher.Normalizer.Covid19Api.Dto.Response.Stats
{
    [Serializable]
    [DataContract]
    public class Stats
    {
        [DataMember(Name = "Total")]
        public int Total { get; set; }
        [DataMember(Name = "All")]
        public int All { get; set; }
        [DataMember(Name = "AllUpdated")]
        public string AllUpdated { get; set; }
        [DataMember(Name = "Countries")]
        public int Countries { get; set; }
        [DataMember(Name = "CountriesUpdated")]
        public string CountriesUpdated { get; set; }
        [DataMember(Name = "ByCountry")]
        public int ByCountry { get; set; }
        [DataMember(Name = "ByCountryUpdated")]
        public string ByCountryUpdated { get; set; }
        [DataMember(Name = "ByCountryLive")]
        public int ByCountryLive { get; set; }
        [DataMember(Name = "ByCountryLiveUpdated")]
        public string ByCountryLiveUpdated { get; set; }
        [DataMember(Name = "ByCountryTotal")]
        public int ByCountryTotal { get; set; }
        [DataMember(Name = "ByCountryTotalUpdated")]
        public string ByCountryTotalUpdated { get; set; }
        [DataMember(Name = "DayOne")]
        public int DayOne { get; set; }
        [DataMember(Name = "DayOneUpdated")]
        public string DayOneUpdated { get; set; }
        [DataMember(Name = "DayOneLive")]
        public int DayOneLive { get; set; }
        [DataMember(Name = "DayOneLiveUpdated")]
        public string DayOneLiveUpdated { get; set; }
        [DataMember(Name = "DayOneTotal")]
        public int DayOneTotal { get; set; }
        [DataMember(Name = "DayOneTotalUpdated")]
        public string DayOneTotalUpdated { get; set; }
        [DataMember(Name = "LiveCountryStatus")]
        public int LiveCountryStatus { get; set; }
        [DataMember(Name = "LiveCountryStatusUpdated")]
        public string LiveCountryStatusUpdated { get; set; }
        [DataMember(Name = "LiveCountryStatusAfterDate")]
        public int LiveCountryStatusAfterDate { get; set; }
        [DataMember(Name = "LiveCountryStatusAfterDateUpdated")]
        public string LiveCountryStatusAfterDateUpdated { get; set; }
        [DataMember(Name = "StatsTotal")]
        public int StatsTotal { get; set; }
        [DataMember(Name = "StatsTotalUpdated")]
        public string StatsTotalUpdated { get; set; }
        [DataMember(Name = "Default")]
        public int Default { get; set; }
        [DataMember(Name = "DefaultUpdated")]
        public string DefaultUpdated { get; set; }
        [DataMember(Name = "SubmitWebhook")]
        public int SubmitWebhook { get; set; }
        [DataMember(Name = "SubmitWebhookUpdated")]
        public string SubmitWebhookUpdated { get; set; }
        [DataMember(Name = "Summary")]
        public int Summary { get; set; }
        [DataMember(Name = "SummaryUpdated")]
        public string SummaryUpdated { get; set; }


    }
}
