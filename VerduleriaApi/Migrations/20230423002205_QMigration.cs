using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerduleriaApi.Migrations
{
    /// <inheritdoc />
    public partial class QMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Editable",
                table: "DetalleCarrito",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Editable",
                table: "DetalleCarrito");
        }
    }
}
