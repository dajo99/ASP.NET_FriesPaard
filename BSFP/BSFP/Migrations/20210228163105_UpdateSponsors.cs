using Microsoft.EntityFrameworkCore.Migrations;

namespace BSFP.Migrations
{
    public partial class UpdateSponsors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WebsiteLink",
                table: "Sponsors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebsiteLink",
                table: "Sponsors");
        }
    }
}
