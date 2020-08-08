using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidStatCruncher.Infrastructure.Migrations
{
    public partial class AddCountryCode_To_CountrStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "CountryStatistics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "CountryStatistics");
        }
    }
}
