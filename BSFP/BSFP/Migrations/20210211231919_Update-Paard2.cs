using Microsoft.EntityFrameworkCore.Migrations;

namespace BSFP.Migrations
{
    public partial class UpdatePaard2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Informatie",
                table: "Paarden",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Leeftijd",
                table: "Paarden",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LocatiePaard",
                table: "Paarden",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Informatie",
                table: "Paarden");

            migrationBuilder.DropColumn(
                name: "Leeftijd",
                table: "Paarden");

            migrationBuilder.DropColumn(
                name: "LocatiePaard",
                table: "Paarden");
        }
    }
}
