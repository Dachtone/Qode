using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qode.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFavoritesRelationshipManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserCircuits_UserCircuitId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserCircuitId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserCircuitId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserFavoriteCircuits",
                columns: table => new
                {
                    UserCircuitId = table.Column<int>(type: "integer", nullable: false),
                    UserFavoritesId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteCircuits", x => new { x.UserCircuitId, x.UserFavoritesId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteCircuits_AspNetUsers_UserFavoritesId",
                        column: x => x.UserFavoritesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteCircuits_UserCircuits_UserCircuitId",
                        column: x => x.UserCircuitId,
                        principalTable: "UserCircuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteCircuits_UserFavoritesId",
                table: "UserFavoriteCircuits",
                column: "UserFavoritesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteCircuits");

            migrationBuilder.AddColumn<int>(
                name: "UserCircuitId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserCircuitId",
                table: "AspNetUsers",
                column: "UserCircuitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserCircuits_UserCircuitId",
                table: "AspNetUsers",
                column: "UserCircuitId",
                principalTable: "UserCircuits",
                principalColumn: "Id");
        }
    }
}
