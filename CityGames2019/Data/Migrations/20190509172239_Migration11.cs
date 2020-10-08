using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CityGames2019.Data.Migrations
{
    public partial class Migration11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Games");
        }
    }
}
