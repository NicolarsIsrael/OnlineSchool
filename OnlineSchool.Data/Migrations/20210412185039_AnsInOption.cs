using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSchool.Data.Migrations
{
    public partial class AnsInOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "McqQuestion",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "AnsId",
                table: "McqOption",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "McqQuestion");

            migrationBuilder.DropColumn(
                name: "AnsId",
                table: "McqOption");
        }
    }
}
