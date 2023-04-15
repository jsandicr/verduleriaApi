using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerduleriaApi.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPromocion_Producto_ProductoRegalaId",
                table: "TipoPromocion");

            migrationBuilder.DropIndex(
                name: "IX_TipoPromocion_ProductoRegalaId",
                table: "TipoPromocion");

            migrationBuilder.DropColumn(
                name: "ProductoRegalaId",
                table: "TipoPromocion");

            migrationBuilder.CreateIndex(
                name: "IX_TipoPromocion_IdProductoRegala",
                table: "TipoPromocion",
                column: "IdProductoRegala");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPromocion_Producto_IdProductoRegala",
                table: "TipoPromocion",
                column: "IdProductoRegala",
                principalTable: "Producto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPromocion_Producto_IdProductoRegala",
                table: "TipoPromocion");

            migrationBuilder.DropIndex(
                name: "IX_TipoPromocion_IdProductoRegala",
                table: "TipoPromocion");

            migrationBuilder.AddColumn<int>(
                name: "ProductoRegalaId",
                table: "TipoPromocion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoPromocion_ProductoRegalaId",
                table: "TipoPromocion",
                column: "ProductoRegalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPromocion_Producto_ProductoRegalaId",
                table: "TipoPromocion",
                column: "ProductoRegalaId",
                principalTable: "Producto",
                principalColumn: "Id");
        }
    }
}
