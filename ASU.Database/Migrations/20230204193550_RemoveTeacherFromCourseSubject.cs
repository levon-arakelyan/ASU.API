using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASU.Database.Migrations
{
    public partial class RemoveTeacherFromCourseSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubjects_Teachers_TeacherId",
                schema: "dbo",
                table: "CourseSubjects");

            migrationBuilder.DropIndex(
                name: "IX_CourseSubjects_TeacherId",
                schema: "dbo",
                table: "CourseSubjects");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                schema: "dbo",
                table: "CourseSubjects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                schema: "dbo",
                table: "CourseSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubjects_TeacherId",
                schema: "dbo",
                table: "CourseSubjects",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubjects_Teachers_TeacherId",
                schema: "dbo",
                table: "CourseSubjects",
                column: "TeacherId",
                principalSchema: "dbo",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
