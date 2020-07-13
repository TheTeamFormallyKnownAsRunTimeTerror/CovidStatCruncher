using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidStatCruncher.Infrastructure.Migrations
{
    public partial class AddEnrichedData_CountryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Aged65Older",
                table: "Countries",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Aged70Older",
                table: "Countries",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiabetesPrevalence",
                table: "Countries",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GdpPerCapita",
                table: "Countries",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HandwashingFacilities",
                table: "Countries",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HospitalBedsPerThousand",
                table: "Countries",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LifeExpectancy",
                table: "Countries",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MedianAge",
                table: "Countries",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "Population",
                table: "Countries",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "PopulationDensity",
                table: "Countries",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aged65Older",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Aged70Older",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "DiabetesPrevalence",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "GdpPerCapita",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "HandwashingFacilities",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "HospitalBedsPerThousand",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "LifeExpectancy",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "MedianAge",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Population",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "PopulationDensity",
                table: "Countries");
        }
    }
}
