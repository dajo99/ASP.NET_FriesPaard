using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BSFP.Migrations
{
    public partial class UpdatePaard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paarden",
                columns: table => new
                {
                    PaardID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Paardnaam = table.Column<string>(nullable: true),
                    Geslacht = table.Column<string>(nullable: true),
                    Levensnummer = table.Column<string>(nullable: true),
                    Geboortedatum = table.Column<DateTime>(nullable: false),
                    Gebruiksdiscipline = table.Column<string>(nullable: true),
                    Niveau = table.Column<string>(nullable: true),
                    Stokmaat = table.Column<int>(nullable: false),
                    Prijs = table.Column<int>(nullable: false),
                    CustomUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paarden", x => x.PaardID);
                    table.ForeignKey(
                        name: "FK_Paarden_AspNetUsers_CustomUserID",
                        column: x => x.CustomUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sponsors",
                columns: table => new
                {
                    SponsorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(nullable: true),
                    Omschrijving = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsors", x => x.SponsorID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paarden_CustomUserID",
                table: "Paarden",
                column: "CustomUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paarden");

            migrationBuilder.DropTable(
                name: "Sponsors");
        }
    }
}
