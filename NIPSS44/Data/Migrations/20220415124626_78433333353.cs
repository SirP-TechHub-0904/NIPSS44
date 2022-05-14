using Microsoft.EntityFrameworkCore.Migrations;

namespace NIPSS44.Data.Migrations
{
    public partial class _78433333353 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Alumnis",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Alumnis");
        }
    }
}
