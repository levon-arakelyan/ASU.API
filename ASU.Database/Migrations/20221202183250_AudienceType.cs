using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASU.Database.Migrations
{
    public partial class AudienceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBlackboard",
                schema: "dbo",
                table: "Audiences");

            migrationBuilder.DropColumn(
                name: "HasComputers",
                schema: "dbo",
                table: "Audiences");

            migrationBuilder.DropColumn(
                name: "HasProjector",
                schema: "dbo",
                table: "Audiences");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "dbo",
                table: "Audiences",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "dbo",
                table: "Audiences");

            migrationBuilder.AddColumn<bool>(
                name: "HasBlackboard",
                schema: "dbo",
                table: "Audiences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasComputers",
                schema: "dbo",
                table: "Audiences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasProjector",
                schema: "dbo",
                table: "Audiences",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
