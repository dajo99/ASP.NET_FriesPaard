using Microsoft.EntityFrameworkCore.Migrations;

namespace BSFP.Migrations
{
    public partial class UpdateStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Nieuws");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Nieuws",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Nieuws",
                type: "varchar(250)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Nieuws");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Nieuws");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Nieuws",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
