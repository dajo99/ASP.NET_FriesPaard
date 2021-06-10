using Microsoft.EntityFrameworkCore.Migrations;

namespace BSFP.Migrations
{
    public partial class UpdateSponsorsAndTarifs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Omschrijving",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "Titel",
                table: "Sponsors");

            migrationBuilder.AddColumn<string>(
                name: "Omschrijving_fr",
                table: "Sponsors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Omschrijving_nl",
                table: "Sponsors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titel_fr",
                table: "Sponsors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titel_nl",
                table: "Sponsors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tarieven",
                columns: table => new
                {
                    TariefID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omschrijving_nl = table.Column<string>(nullable: false),
                    Omschrijving_fr = table.Column<string>(nullable: false),
                    Prijs = table.Column<decimal>(nullable: false),
                    IsTeruggave = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarieven", x => x.TariefID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarieven");

            migrationBuilder.DropColumn(
                name: "Omschrijving_fr",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "Omschrijving_nl",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "Titel_fr",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "Titel_nl",
                table: "Sponsors");

            migrationBuilder.AddColumn<string>(
                name: "Omschrijving",
                table: "Sponsors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titel",
                table: "Sponsors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
