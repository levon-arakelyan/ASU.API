using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASU.Database.Migrations
{
    public partial class TeacherDegree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Degree",
                schema: "dbo",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                schema: "dbo",
                table: "Teachers");
        }
    }
}
