using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Qode.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCircuits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserCircuitId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserCircuits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: true),
                    Circuit = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCircuits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCircuits_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserCircuitId",
                table: "AspNetUsers",
                column: "UserCircuitId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCircuits_AuthorId",
                table: "UserCircuits",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserCircuits_UserCircuitId",
                table: "AspNetUsers",
                column: "UserCircuitId",
                principalTable: "UserCircuits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserCircuits_UserCircuitId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserCircuits");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserCircuitId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserCircuitId",
                table: "AspNetUsers");
        }
    }
}
