namespace VerduleriaApi.Models
{
    public class DetalleCarrito
    {
        public int Id { get; set; }
        public int IdCarrito { get; set; }
        public int IdProducto { get; set; }
        public int CantidadProducto { get; set; }
        public Carrito? Carrito { get; set; }
        public Producto? Producto { get; set; }
    }
}
