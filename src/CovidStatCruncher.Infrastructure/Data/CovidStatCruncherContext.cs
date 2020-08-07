using CovidStatCruncher.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CovidStatCruncher.Infrastructure.Data
{
    public class CovidStatCruncherContext : DbContext
    {
        public CovidStatCruncherContext()
        {
        }

        public CovidStatCruncherContext(DbContextOptions<CovidStatCruncherContext> options)
            : base(options)
        {
        }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<CountryData> CountryData { get; set; }
        public DbSet<CountryStatistics> CountryStatistics { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySql("server=covid-data-runtimeterror-dev.cmfckbnwttaa.eu-west-1.rds.amazonaws.com;user id=admin;password=8n6FaTZYujfIxGDHlIuD;port=3306;database=CovidStatsProject;");
        //}
    }
}
