using Microsoft.EntityFrameworkCore.Migrations;

namespace CatFish.Migrations
{
    public partial class MatchResponsesAndGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Matches",
                newName: "User2Id");

            migrationBuilder.AddColumn<string>(
                name: "User1Id",
                table: "Matches",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "User1Response",
                table: "Matches",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "User2Response",
                table: "Matches",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User1Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "User1Response",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "User2Response",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "User2Id",
                table: "Matches",
                newName: "UserId");
        }
    }
}
