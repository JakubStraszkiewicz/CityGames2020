using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CityGames2019.Data.Migrations
{
    public partial class Migration13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<long>(
                name: "PhotoId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
