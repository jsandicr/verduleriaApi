using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VerduleriaApi.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public bool Activo { get; set; }
        public int IdTipo { get; set; }
        public TipoProducto? TipoProducto { get; set; }
        public List<ProductoPromocion>? ProductoPromocion { get; set; }
    }
}
