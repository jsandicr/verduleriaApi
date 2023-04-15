using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerduleriaApi.Migrations
{
    /// <inheritdoc />
    public partial class TMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promocion_TipoPromocion_IdTipo",
                table: "Promocion");

            migrationBuilder.DropTable(
                name: "TipoPromocion");

            migrationBuilder.DropIndex(
                name: "IX_Promocion_IdTipo",
                table: "Promocion");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "ProductoPromocion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PorcentajeDebita",
                table: "ProductoPromocion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ProductoCompra",
                table: "ProductoPromocion",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "ProductoPromocion");

            migrationBuilder.DropColumn(
                name: "PorcentajeDebita",
                table: "ProductoPromocion");

            migrationBuilder.DropColumn(
                name: "ProductoCompra",
                table: "ProductoPromocion");

            migrationBuilder.CreateTable(
                name: "TipoPromocion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProductoRegala = table.Column<int>(type: "int", nullable: true),
                    CantidadCompra = table.Column<int>(type: "int", nullable: true),
                    CantidadRegala = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PorcentajeDebita = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPromocion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoPromocion_Producto_IdProductoRegala",
                        column: x => x.IdProductoRegala,
                        principalTable: "Producto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_IdTipo",
                table: "Promocion",
                column: "IdTipo");

            migrationBuilder.CreateIndex(
                name: "IX_TipoPromocion_IdProductoRegala",
                table: "TipoPromocion",
                column: "IdProductoRegala");

            migrationBuilder.AddForeignKey(
                name: "FK_Promocion_TipoPromocion_IdTipo",
                table: "Promocion",
                column: "IdTipo",
                principalTable: "TipoPromocion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
