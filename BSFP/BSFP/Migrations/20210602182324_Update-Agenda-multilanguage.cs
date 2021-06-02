using Microsoft.EntityFrameworkCore.Migrations;

namespace BSFP.Migrations
{
    public partial class UpdateAgendamultilanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Omschrijving",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "Titel",
                table: "Agenda");

            migrationBuilder.AddColumn<string>(
                name: "Omschrijving_fr",
                table: "Agenda",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Omschrijving_nl",
                table: "Agenda",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titel_fr",
                table: "Agenda",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titel_nl",
                table: "Agenda",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Omschrijving_fr",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "Omschrijving_nl",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "Titel_fr",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "Titel_nl",
                table: "Agenda");

            migrationBuilder.AddColumn<string>(
                name: "Omschrijving",
                table: "Agenda",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titel",
                table: "Agenda",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
