using Microsoft.EntityFrameworkCore.Migrations;

namespace NIPSS44.Data.Migrations
{
    public partial class _4567h : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "LegacyProjectAnswers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "LegacyProjectAnswers");
        }
    }
}
