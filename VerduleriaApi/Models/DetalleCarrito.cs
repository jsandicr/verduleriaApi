namespace VerduleriaApi.Models
{
    public class DetalleCarrito
    {
        public int Id { get; set; }
        public int IdCarrito { get; set; }
        public int IdProducto { get; set; }
        public int CantidadProducto { get; set; }
        public int? Costo { get; set; }
        //Si Editable es false es porque se agrego de una promocion y no se puede modificar
        public bool Editable { get; set; }
        public Carrito? Carrito { get; set; }
        public Producto? Producto { get; set; }
    }
}
