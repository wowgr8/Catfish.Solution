using Microsoft.EntityFrameworkCore.Migrations;

namespace CatFish.Migrations
{
    public partial class UnmatchSystemForMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Unmatched",
                table: "Matches",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unmatched",
                table: "Matches");
        }
    }
}
