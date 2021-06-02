using Microsoft.EntityFrameworkCore.Migrations;

namespace BSFP.Migrations
{
    public partial class UpdateNieuwsmultilanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intro",
                table: "Nieuws");

            migrationBuilder.DropColumn(
                name: "Omschrijving",
                table: "Nieuws");

            migrationBuilder.DropColumn(
                name: "SoortVereneging",
                table: "Nieuws");

            migrationBuilder.DropColumn(
                name: "Titel",
                table: "Nieuws");

            migrationBuilder.AddColumn<string>(
                name: "Intro_fr",
                table: "Nieuws",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Intro_nl",
                table: "Nieuws",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Omschrijving_fr",
                table: "Nieuws",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Omschrijving_nl",
                table: "Nieuws",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Titel_fr",
                table: "Nieuws",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Titel_nl",
                table: "Nieuws",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Vereneging",
                table: "Nieuws",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intro_fr",
                table: "Nieuws");

            migrationBuilder.DropColumn(
                name: "Intro_nl",
                table: "Nieuws");

            migrationBuilder.DropColumn(
                name: "Omschrijving_fr",
                table: "Nieuws");

            migrationBuilder.DropColumn(
                name: "Omschrijving_nl",
                table: "Nieuws");

            migrationBuilder.DropColumn(
                name: "Titel_fr",
                table: "Nieuws");

            migrationBuilder.DropColumn(
                name: "Titel_nl",
                table: "Nieuws");

            migrationBuilder.DropColumn(
                name: "Vereneging",
                table: "Nieuws");

            migrationBuilder.AddColumn<string>(
                name: "Intro",
                table: "Nieuws",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Omschrijving",
                table: "Nieuws",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SoortVereneging",
                table: "Nieuws",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titel",
                table: "Nieuws",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
