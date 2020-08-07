using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CovidStatCruncher.Infrastructure.Models
{
    public class CountryStatistics
    {
        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        //Json blobs with statistics
        public string MeasureImportances { get; set; }

        public string GrangerStatistics { get; set; }
    }
}
