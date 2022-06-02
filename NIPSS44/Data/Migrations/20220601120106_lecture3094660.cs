using Microsoft.EntityFrameworkCore.Migrations;

namespace NIPSS44.Data.Migrations
{
    public partial class lecture3094660 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsParticipant",
                table: "Profiles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsParticipant",
                table: "Profiles");
        }
    }
}
