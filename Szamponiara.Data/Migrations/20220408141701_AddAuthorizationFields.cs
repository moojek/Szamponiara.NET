using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szamponiara.Data.Migrations
{
    public partial class AddAuthorizationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Ingredients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Ingredients");
        }
    }
}
