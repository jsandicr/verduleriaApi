namespace VerduleriaApi.Models
{
    public class ProductoPromocion
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdPromocion { get; set; }
        public int CantidadProducto { get; set; }
        public Promocion? Promocion { get; set; }
        public Producto? Producto  { get; set; }
    }
}
