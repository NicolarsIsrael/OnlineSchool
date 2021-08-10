using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSchool.Data.Migrations
{
    public partial class studentInattempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ExamAttempt_StudentId",
                table: "ExamAttempt",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamAttempt_Student_StudentId",
                table: "ExamAttempt",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamAttempt_Student_StudentId",
                table: "ExamAttempt");

            migrationBuilder.DropIndex(
                name: "IX_ExamAttempt_StudentId",
                table: "ExamAttempt");
        }
    }
}
