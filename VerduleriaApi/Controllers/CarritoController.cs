using Microsoft.AspNetCore.Mvc;
using VerduleriaApi.Models;

namespace VerduleriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly VerduleriaContext _context;

        public CarritoController(VerduleriaContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Carrito>>> GetCarrito(int id)
        {
            try
            {
                //Retorna el Ok  que es igual al 200 (Status)
                var carrito = _context.Carrito.Find(id);
                if(carrito != null)
                {
                    return Ok(carrito);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Producto>>> PostCarrito(int IdUsuario, int IdProducto, int cantidad)
        {
            try
            {
                //Se envia IdCarrito en 0 si no hay carrito
                var carrito = _context.Carrito.Where(x => x.IdUsuario == IdUsuario);
                if(carrito != null)
                {
                    Carrito nuevoCarrito = new Carrito();
                    nuevoCarrito.IdUsuario = IdUsuario;
                    _context.Add(nuevoCarrito);
                    DetalleCarrito detalle = new DetalleCarrito();
                    detalle.IdCarrito = carrito.First().Id;
                    detalle.IdProducto = IdProducto;
                    detalle.CantidadProducto = cantidad;
                    _context.Add(detalle);
                }
                else
                {
                    var producto = (from d in _context.DetalleCarrito
                                   where d.IdCarrito.Equals(carrito.First().Id) && d.IdProducto.Equals(IdProducto)
                                   select d).First();
                    if (producto != null)
                    {
                        producto.CantidadProducto = producto.CantidadProducto + cantidad;
                    }
                    else
                    {
                        DetalleCarrito detalle = new DetalleCarrito();
                        detalle.IdProducto = IdProducto;
                        detalle.IdCarrito = carrito.First().Id;
                        detalle.CantidadProducto = cantidad;
                        _context.Add(detalle);
                    }
                }
                
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
