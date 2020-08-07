using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidStatCruncher.Infrastructure.Migrations
{
    public partial class change_population_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Population",
                table: "Countries",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Population",
                table: "Countries",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
