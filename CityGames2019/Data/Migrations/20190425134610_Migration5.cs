using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CityGames2019.Data.Migrations
{
    public partial class Migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Groups_GroupId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_GroupId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GroupGames",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupId = table.Column<long>(nullable: false),
                    GameId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupGames_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupGames_GameId",
                table: "GroupGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupGames_GroupId",
                table: "GroupGames",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupGames");

            migrationBuilder.AddColumn<long>(
                name: "GroupId",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_GroupId",
                table: "Games",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Groups_GroupId",
                table: "Games",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
