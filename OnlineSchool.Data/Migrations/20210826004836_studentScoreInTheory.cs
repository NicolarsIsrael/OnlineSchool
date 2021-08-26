using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSchool.Data.Migrations
{
    public partial class studentScoreInTheory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "StudentScore",
                table: "ExamTheoryAttempt",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentScore",
                table: "ExamTheoryAttempt");
        }
    }
}
