using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BSFP.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agenda",
                columns: table => new
                {
                    AgendaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(nullable: true),
                    Omschrijving = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    Starttijd = table.Column<DateTime>(nullable: false),
                    Eindtijd = table.Column<DateTime>(nullable: false),
                    Locatie = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.AgendaID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agenda");
        }
    }
}
