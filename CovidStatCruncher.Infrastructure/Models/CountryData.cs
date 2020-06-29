using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CovidStatCruncher.Infrastructure.Models
{
    public class CountryData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UpdateId { get; set; }
        [Required]
        public int CountryId { get; set; }

        [Required]
        public string CountryName { get; set; }

        public string CountryCode { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int ConfirmedCases { get; set; }
        public int ActiveCases { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
        public DateTime DateTime { get; set; }
    }
}
