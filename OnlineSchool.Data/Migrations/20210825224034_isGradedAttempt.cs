using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSchool.Data.Migrations
{
    public partial class isGradedAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGrraded",
                table: "ExamAttempt",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGrraded",
                table: "ExamAttempt");
        }
    }
}
