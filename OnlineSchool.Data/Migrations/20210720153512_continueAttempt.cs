using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSchool.Data.Migrations
{
    public partial class continueAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ContinueAttempt",
                table: "ExamAttempt",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContinueAttempt",
                table: "ExamAttempt");
        }
    }
}
