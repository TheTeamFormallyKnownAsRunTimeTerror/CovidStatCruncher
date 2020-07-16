using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CovidStatCruncher.Infrastructure.Models
{
    public class Countries
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        public string CountryName { get; set; }
        public string CountrySlug { get; set; }
        public string Iso2 { get; set; }
        public bool? HasData { get; set; }

        public ICollection<CountryData> CountryData { get; set; }

        // Enriched Data from OWID

        public double Population { get; set; }
        public double PopulationDensity { get; set; }
        public double MedianAge { get; set; }
        public double Aged65Older { get; set; }
        public double Aged70Older { get; set; }
        public double GdpPerCapita { get; set; }
        public double DiabetesPrevalence { get; set; }
        public double HandwashingFacilities { get; set; }
        public double HospitalBedsPerThousand { get; set; }
        public double LifeExpectancy { get; set; }


    }
}
