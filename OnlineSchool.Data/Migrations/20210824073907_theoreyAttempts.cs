using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSchool.Data.Migrations
{
    public partial class theoreyAttempts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamTheoryAttempt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateCreatedUtc = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateModifiedUtc = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QuestionNumber = table.Column<int>(nullable: false),
                    PageNumber = table.Column<int>(nullable: false),
                    TheoryQuestionId = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    Score = table.Column<decimal>(nullable: false),
                    ExamAttemptId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTheoryAttempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamTheoryAttempt_ExamAttempt_ExamAttemptId",
                        column: x => x.ExamAttemptId,
                        principalTable: "ExamAttempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamTheoryAttempt_ExamAttemptId",
                table: "ExamTheoryAttempt",
                column: "ExamAttemptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamTheoryAttempt");
        }
    }
}
