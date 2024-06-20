using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qode.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCircuitProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Circuit",
                table: "UserCircuits");

            migrationBuilder.AddColumn<string>(
                name: "CircuitOperations",
                table: "UserCircuits",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CircuitOperations",
                table: "UserCircuits");

            migrationBuilder.AddColumn<string>(
                name: "Circuit",
                table: "UserCircuits",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }
    }
}
