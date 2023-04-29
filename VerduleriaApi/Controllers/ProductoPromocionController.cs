using Microsoft.AspNetCore.Mvc;
using VerduleriaApi.Models;

namespace VerduleriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoPromocionController : ControllerBase
    {
        private readonly VerduleriaContext _context;

        public ProductoPromocionController(VerduleriaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoPromocion>>> GetProductosPromociones()
        {
            try
            {
                var query = (from pp in _context.ProductoPromocion
                             join p in _context.Producto
                             on pp.IdProducto equals p.Id
                             join pr in _context.Promocion
                             on pp.IdPromocion equals pr.Id
                             select new { ProductoPromocion = pp, Producto = p, Promocion = pr }).ToList();

                if (query != null)
                {
                    List<ProductoPromocion> productoPromocion = new List<ProductoPromocion>();
                    foreach (var q in query)
                    {
                        productoPromocion.Add(q.ProductoPromocion);
                    }
                    return Ok(productoPromocion);
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
        public async Task<ActionResult<ProductoPromocion>> GetProductosPromocionById(int id)
        {
            try
            {
                var query = (from pp in _context.ProductoPromocion
                             join p in _context.Producto
                             on pp.IdProducto equals p.Id
                             join pr in _context.Promocion
                             on pp.IdPromocion equals pr.Id
                             where pp.Id == id
                             select new { ProductoPromocion = pp, Producto = p, Promocion = pr }).ToList();

                if (query.Count > 0)
                {
                    return Ok(query.FirstOrDefault().ProductoPromocion);
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
        public async Task<ActionResult> PostProductoPromocion(ProductoPromocion productoPromocion)
        {
            try
            {
                _context.Add(productoPromocion);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<ActionResult<Promocion>> PutProductoPromocion(ProductoPromocion productoPromocion)
        {
            try
            {
                _context.Update(productoPromocion);
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
