using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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

        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<Carrito>> GetCarrito(int idUsuario)
        {
            try
            {

                var query = (from c in _context.Carrito
                                   join d in _context.DetalleCarrito
                                   on c.Id equals d.IdCarrito
                                   join p in _context.Producto
                                   on d.IdProducto equals p.Id
                                   where c.IdUsuario == idUsuario
                                   select new { Carrito = c, Detalle = d, Productos = p }).ToList();

                if (query != null)
                {
                    return Ok(query);
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
                var carrito = _context.Carrito.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                if(carrito == null)
                {
                    Carrito nuevoCarrito = new Carrito();
                    nuevoCarrito.IdUsuario = IdUsuario;
                    _context.Add(nuevoCarrito);
                    _context.SaveChanges();
                    //Se hace la query nuevamente para poder obtener el id del nuevo carrito
                    carrito = _context.Carrito.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                    DetalleCarrito detalle = new DetalleCarrito();
                    detalle.IdCarrito = carrito.Id;
                    detalle.IdProducto = IdProducto;
                    detalle.CantidadProducto = cantidad;
                    var costoProducto = _context.Producto.Where(x => x.Id == IdProducto).FirstOrDefault().Precio;
                    detalle.Costo = detalle.CantidadProducto * costoProducto;
                    _context.Add(detalle);
                    _context.SaveChanges();
                }
                else
                {
                    var producto = (from d in _context.DetalleCarrito
                                   where d.IdCarrito.Equals(carrito.Id) && d.IdProducto.Equals(IdProducto)
                                   select d).FirstOrDefault();
                    if (producto != null)
                    {
                        producto.CantidadProducto = producto.CantidadProducto + cantidad;

                    }
                    else
                    {
                        DetalleCarrito detalle = new DetalleCarrito();
                        detalle.IdProducto = IdProducto;
                        detalle.IdCarrito = carrito.Id;
                        detalle.CantidadProducto = cantidad;
                        var costoProducto = _context.Producto.Where(x => x.Id == IdProducto).FirstOrDefault().Precio;
                        detalle.Costo = detalle.CantidadProducto * costoProducto;
                        _context.Add(detalle);
                    }
                    _context.SaveChanges();
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
