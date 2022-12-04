using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASU.Database.Migrations
{
    public partial class SubjectGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectGroupId",
                schema: "dbo",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubjectGroups",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectGroupId",
                schema: "dbo",
                table: "Subjects",
                column: "SubjectGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_SubjectGroups_SubjectGroupId",
                schema: "dbo",
                table: "Subjects",
                column: "SubjectGroupId",
                principalSchema: "dbo",
                principalTable: "SubjectGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_SubjectGroups_SubjectGroupId",
                schema: "dbo",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "SubjectGroups",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SubjectGroupId",
                schema: "dbo",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SubjectGroupId",
                schema: "dbo",
                table: "Subjects");
        }
    }
}
