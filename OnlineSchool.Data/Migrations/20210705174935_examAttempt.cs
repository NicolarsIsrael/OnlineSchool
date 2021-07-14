using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSchool.Data.Migrations
{
    public partial class examAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExamMcqAttemptId",
                table: "McqOption",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExamAttempt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateCreatedUtc = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateModifiedUtc = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ExamId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAttempt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamMcqAttempt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateCreatedUtc = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateModifiedUtc = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    McqId = table.Column<int>(nullable: false),
                    SelectedOptionId = table.Column<int>(nullable: false),
                    ExamAttemptId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamMcqAttempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamMcqAttempt_ExamAttempt_ExamAttemptId",
                        column: x => x.ExamAttemptId,
                        principalTable: "ExamAttempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_McqOption_ExamMcqAttemptId",
                table: "McqOption",
                column: "ExamMcqAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamMcqAttempt_ExamAttemptId",
                table: "ExamMcqAttempt",
                column: "ExamAttemptId");

            migrationBuilder.AddForeignKey(
                name: "FK_McqOption_ExamMcqAttempt_ExamMcqAttemptId",
                table: "McqOption",
                column: "ExamMcqAttemptId",
                principalTable: "ExamMcqAttempt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_McqOption_ExamMcqAttempt_ExamMcqAttemptId",
                table: "McqOption");

            migrationBuilder.DropTable(
                name: "ExamMcqAttempt");

            migrationBuilder.DropTable(
                name: "ExamAttempt");

            migrationBuilder.DropIndex(
                name: "IX_McqOption_ExamMcqAttemptId",
                table: "McqOption");

            migrationBuilder.DropColumn(
                name: "ExamMcqAttemptId",
                table: "McqOption");
        }
    }
}
