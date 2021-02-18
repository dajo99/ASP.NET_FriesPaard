using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BSFP.Migrations
{
    public partial class Updatepaard3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Geboortedatum",
                table: "Paarden");

            migrationBuilder.AddColumn<string>(
                name: "ImageName1",
                table: "Paarden",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName2",
                table: "Paarden",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName3",
                table: "Paarden",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName4",
                table: "Paarden",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath1",
                table: "Paarden",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath2",
                table: "Paarden",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath3",
                table: "Paarden",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath4",
                table: "Paarden",
                type: "varchar(250)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName1",
                table: "Paarden");

            migrationBuilder.DropColumn(
                name: "ImageName2",
                table: "Paarden");

            migrationBuilder.DropColumn(
                name: "ImageName3",
                table: "Paarden");

            migrationBuilder.DropColumn(
                name: "ImageName4",
                table: "Paarden");

            migrationBuilder.DropColumn(
                name: "ImagePath1",
                table: "Paarden");

            migrationBuilder.DropColumn(
                name: "ImagePath2",
                table: "Paarden");

            migrationBuilder.DropColumn(
                name: "ImagePath3",
                table: "Paarden");

            migrationBuilder.DropColumn(
                name: "ImagePath4",
                table: "Paarden");

            migrationBuilder.AddColumn<DateTime>(
                name: "Geboortedatum",
                table: "Paarden",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
