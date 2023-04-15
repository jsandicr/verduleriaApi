using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using VerduleriaApi.Models;

namespace VerduleriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly VerduleriaContext _context;

        public CompraController(VerduleriaContext context)
        {
            _context = context;
        }

        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<Compra>> GetCompra(int idUsuario)
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
        public async Task<ActionResult> PostCompra(int IdUsuario)
        {
            try
            {
                //Valida si hay productos en el carrito
                var query = (from c in _context.Carrito
                                join d in _context.DetalleCarrito
                                on c.Id equals d.IdCarrito
                                join p in _context.Producto
                                on d.IdProducto equals p.Id
                                where c.IdUsuario == IdUsuario
                                select new { Carrito = c, Detalle = d, Productos = p }).ToList();

                if(query.FirstOrDefault().Carrito.DetalleCarrito.Count > 0)
                {
                    Compra nuevaCompra = new Compra();
                    nuevaCompra.IdUsuario = IdUsuario;
                    nuevaCompra.Fecha = DateTime.Now;
                    _context.Add(nuevaCompra);
                    _context.SaveChanges();

                    //Hace la query para obtener el id de la compra creada
                    var compra = _context.Compra.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                    List<DetalleCompra> detalleCompra = new List<DetalleCompra>();
                    foreach (DetalleCarrito d in query.FirstOrDefault().Carrito.DetalleCarrito)
                    {
                        if(d.Producto.Cantidad > d.CantidadProducto)
                        {
                            DetalleCompra detalleC = new DetalleCompra
                            {
                                IdCompra = compra.Id,
                                IdProducto = d.IdProducto,
                                Cantidad = d.CantidadProducto
                            };
                            d.Producto.Cantidad -= d.CantidadProducto;

                            _context.Add(detalleC);
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    _context.DetalleCarrito.RemoveRange(query.FirstOrDefault().Carrito.DetalleCarrito);
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
