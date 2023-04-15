using Microsoft.AspNetCore.Mvc;
using VerduleriaApi.Models;

namespace VerduleriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocionController : ControllerBase
    {
        private readonly VerduleriaContext _context;

        public PromocionController(VerduleriaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Promocion>>> GetPromociones()
        {
            try
            {
                //Retorna el Ok  que es igual al 200 (Status)
                var promociones = _context.Promocion.ToList();
                if(promociones != null)
                {
                    return Ok(promociones);
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

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Promocion>>> GetPromocionById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var promociones = _context.Promocion.Find(id);
                if (promociones == null)
                    return NotFound();
                return Ok(promociones);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("ProductoxPromocion")]
        public async Task<ActionResult> PostProductoxPromocion(int idProducto, int idPromocion)
        {
            try
            {
                ProductoPromocion relacion = new ProductoPromocion();
                relacion.IdProducto = idProducto;
                relacion.IdPromocion = idPromocion;
                _context.Add(relacion);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<ActionResult<Promocion>> PostPromocion(Promocion promocion)
        {
            try
            {
                _context.Add(promocion);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<ActionResult<Promocion>> PutPromocion(Promocion promocion)
        {
            try
            {
                _context.Update(promocion);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Promocion>> DeletePromocion(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("CompraPromocion")]
        public async Task<ActionResult> CompraPromocion(int IdPromocion, int IdUsuario)
        {
            try
            {
                if (IdPromocion != 0 && IdUsuario != 0)
                {
                    var query = (from p in _context.Promocion
                                 join pd in _context.ProductoPromocion
                                 on p.Id equals pd.IdPromocion
                                 join p2 in _context.Producto
                                 on pd.IdProducto equals p2.Id
                                 where p.Id == IdPromocion
                                 select new { Promocion = p, productoPromocion = pd, producto = p2 }).ToList();
                    if (query != null)
                    {
                        Promocion promocion = new Promocion();
                        promocion = query.FirstOrDefault().Promocion;
                        List<ProductoPromocion> pd = new List<ProductoPromocion>();
                        promocion.ProductoPromocion = query.FirstOrDefault().Promocion.ProductoPromocion;
                        if (promocion.ProductoPromocion != null)
                        {
                            foreach (ProductoPromocion p in query.FirstOrDefault().Promocion.ProductoPromocion)
                            {
                                if (p.ProductoCompra == true && p.PorcentajeDebita != null)
                                {
                                    var carrito = _context.Carrito.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                                    if (carrito != null)
                                    {
                                        DetalleCarrito detalle = new DetalleCarrito();
                                        detalle.IdProducto = p.IdProducto;
                                        detalle.IdCarrito = carrito.Id;
                                        detalle.CantidadProducto = p.Cantidad;
                                        var costoProducto = _context.Producto.Where(x => x.Id == p.IdProducto).FirstOrDefault().Precio;
                                        detalle.Costo = costoProducto - (costoProducto * p.PorcentajeDebita / 100);
                                        _context.Add(detalle);
                                    }
                                    else
                                    {
                                        Carrito nuevoCarrito = new Carrito();
                                        nuevoCarrito.IdUsuario = IdUsuario;
                                        _context.Add(nuevoCarrito);
                                        _context.SaveChanges();
                                        //Se hace la query nuevamente para poder obtener el id del nuevo carrito
                                        carrito = _context.Carrito.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                                        DetalleCarrito detalle = new DetalleCarrito();
                                        detalle.IdCarrito = carrito.Id;
                                        detalle.IdProducto = p.IdProducto;
                                        detalle.CantidadProducto = p.Cantidad;
                                        var costoProducto = _context.Producto.Where(x => x.Id == p.IdProducto).FirstOrDefault().Precio;
                                        detalle.Costo = costoProducto * detalle.CantidadProducto / costoProducto;
                                        _context.Add(detalle);
                                    }
                                }
                                if (p.ProductoCompra == true && p.PorcentajeDebita == null)
                                {
                                    var carrito = _context.Carrito.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                                    if (carrito != null)
                                    {
                                        DetalleCarrito detalle = new DetalleCarrito();
                                        detalle.IdProducto = p.IdProducto;
                                        detalle.IdCarrito = carrito.Id;
                                        detalle.CantidadProducto = p.Cantidad;
                                        var costoProducto = _context.Producto.Where(x => x.Id == p.IdProducto).FirstOrDefault().Precio;
                                        detalle.Costo = costoProducto;
                                        _context.Add(detalle);
                                    }
                                    else
                                    {
                                        Carrito nuevoCarrito = new Carrito();
                                        nuevoCarrito.IdUsuario = IdUsuario;
                                        _context.Add(nuevoCarrito);
                                        _context.SaveChanges();
                                        //Se hace la query nuevamente para poder obtener el id del nuevo carrito
                                        carrito = _context.Carrito.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                                        DetalleCarrito detalle = new DetalleCarrito();
                                        detalle.IdCarrito = carrito.Id;
                                        detalle.IdProducto = p.IdProducto;
                                        detalle.CantidadProducto = p.Cantidad;
                                        var costoProducto = _context.Producto.Where(x => x.Id == p.IdProducto).FirstOrDefault().Precio;
                                        detalle.Costo = costoProducto;
                                        _context.Add(detalle);
                                    }
                                }
                                if (p.ProductoCompra == false && p.PorcentajeDebita == null)
                                {
                                    Carrito nuevoCarrito = new Carrito();
                                    nuevoCarrito.IdUsuario = IdUsuario;
                                    _context.Add(nuevoCarrito);
                                    _context.SaveChanges();
                                    //Se hace la query nuevamente para poder obtener el id del nuevo carrito
                                    var carrito = _context.Carrito.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                                    DetalleCarrito detalle = new DetalleCarrito();
                                    detalle.IdCarrito = carrito.Id;
                                    detalle.IdProducto = p.IdProducto;
                                    detalle.CantidadProducto = p.Cantidad;
                                    detalle.Costo = 0;
                                    _context.Add(detalle);
                                }
                                _context.SaveChanges();
                            }
                            return Ok(query);
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
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
    }
}
