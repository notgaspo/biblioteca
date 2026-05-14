using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Server.Migrations
{
    /// <inheritdoc />
    public partial class PuseStateConMayuscula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "state",
                table: "Books",
                newName: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Books",
                newName: "state");
        }
    }
}
