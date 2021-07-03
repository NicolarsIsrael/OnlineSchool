using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSchool.Data.Migrations
{
    public partial class AnsInOption22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_McqOption_McqQuestion_QuestionId",
                table: "McqOption");

            migrationBuilder.DropIndex(
                name: "IX_McqOption_QuestionId",
                table: "McqOption");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "McqOption");

            migrationBuilder.AddColumn<int>(
                name: "McqQuestionId",
                table: "McqOption",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_McqOption_McqQuestionId",
                table: "McqOption",
                column: "McqQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_McqOption_McqQuestion_McqQuestionId",
                table: "McqOption",
                column: "McqQuestionId",
                principalTable: "McqQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_McqOption_McqQuestion_McqQuestionId",
                table: "McqOption");

            migrationBuilder.DropIndex(
                name: "IX_McqOption_McqQuestionId",
                table: "McqOption");

            migrationBuilder.DropColumn(
                name: "McqQuestionId",
                table: "McqOption");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "McqOption",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_McqOption_QuestionId",
                table: "McqOption",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_McqOption_McqQuestion_QuestionId",
                table: "McqOption",
                column: "QuestionId",
                principalTable: "McqQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
