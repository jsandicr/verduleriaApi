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

                Carrito carrito = new Carrito();
                if (query.Count > 0)
                {
                    carrito.Id = query.FirstOrDefault().Carrito.Id;
                    carrito.IdUsuario = query.FirstOrDefault().Carrito.IdUsuario;
                    carrito.DetalleCarrito = query.FirstOrDefault().Carrito.DetalleCarrito;
                    return Ok(carrito);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> PostCarrito(int IdUsuario, int IdProducto, int cantidad)
        {
            try
            {
                if (IdUsuario != 0 && IdProducto != 0 && cantidad != 0)
                {
                    var carrito = _context.Carrito.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                    Carrito nuevoCarrito = new Carrito();
                    if (carrito == null)
                    {
                        nuevoCarrito.IdUsuario = IdUsuario;
                        _context.Add(nuevoCarrito);
                        _context.SaveChanges();
                    }

                    var producto = (from d in _context.DetalleCarrito
                                    where d.IdCarrito.Equals(carrito.Id) && d.IdProducto.Equals(IdProducto) && d.Editable == true
                                    select d).FirstOrDefault();

                    if (producto != null)
                    {
                        producto.CantidadProducto = producto.CantidadProducto + cantidad;
                    }
                    else
                    {
                        carrito = _context.Carrito.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                        DetalleCarrito detalle = new DetalleCarrito();
                        detalle.IdCarrito = carrito.Id;
                        detalle.IdProducto = IdProducto;
                        detalle.CantidadProducto = cantidad;
                        var costoProducto = _context.Producto.Where(x => x.Id == IdProducto).FirstOrDefault().Precio;
                        detalle.Costo = detalle.CantidadProducto * costoProducto;
                        detalle.Editable = true;
                        _context.Add(detalle);
                    }
                    _context.SaveChanges();

                    return Ok();
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

        [HttpGet]
        [Route("RestaCarrito")]
        public async Task<ActionResult<List<Producto>>> RestaCarrito(int IdUsuario, int IdProducto, int cantidad)
        {
            try
            {
                var carrito = _context.Carrito.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                if (carrito != null)
                {
                    var producto = (from d in _context.DetalleCarrito
                                    where d.IdCarrito.Equals(carrito.Id) && d.IdProducto.Equals(IdProducto) &&  d.Editable == true
                                    select d).FirstOrDefault();

                    if (producto != null)
                    {
                        producto.CantidadProducto = producto.CantidadProducto - cantidad;
                        if(producto.CantidadProducto <= 0)
                        {
                            _context.Remove(producto);
                        }

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
