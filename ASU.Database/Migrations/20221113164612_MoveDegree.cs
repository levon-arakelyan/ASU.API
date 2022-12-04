using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASU.Database.Migrations
{
    public partial class MoveDegree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                schema: "dbo",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                schema: "dbo",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "dbo",
                table: "Schedules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                schema: "dbo",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                schema: "dbo",
                table: "Schedules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Degree",
                schema: "dbo",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "dbo",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                schema: "dbo",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Degree",
                schema: "dbo",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "Degree",
                schema: "dbo",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
