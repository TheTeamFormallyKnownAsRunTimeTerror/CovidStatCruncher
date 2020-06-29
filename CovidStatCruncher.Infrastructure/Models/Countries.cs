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
    }
}
