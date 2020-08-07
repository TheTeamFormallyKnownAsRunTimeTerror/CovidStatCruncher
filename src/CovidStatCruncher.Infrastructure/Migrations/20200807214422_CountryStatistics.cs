using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidStatCruncher.Infrastructure.Migrations
{
    public partial class CountryStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryStatistics",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(nullable: true),
                    MeasureImportances = table.Column<string>(nullable: true),
                    GrangerStatistics = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryStatistics", x => x.CountryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryStatistics");
        }
    }
}
