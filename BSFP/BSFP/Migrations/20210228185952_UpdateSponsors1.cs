using Microsoft.EntityFrameworkCore.Migrations;

namespace BSFP.Migrations
{
    public partial class UpdateSponsors1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Sponsors",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Sponsors",
                type: "varchar(250)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Sponsors");
        }
    }
}
