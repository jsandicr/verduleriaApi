using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerduleriaApi.Migrations
{
    /// <inheritdoc />
    public partial class SMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Costo",
                table: "DetalleCompra",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Costo",
                table: "DetalleCompra");
        }
    }
}
