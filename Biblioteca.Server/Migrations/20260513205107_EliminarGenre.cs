using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Server.Migrations
{
    /// <inheritdoc />
    public partial class EliminarGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genres",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genres",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
