using Microsoft.AspNetCore.Mvc;
using VerduleriaApi.Models;

namespace VerduleriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly VerduleriaContext _context;

        public ProductoController(VerduleriaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            try
            {
                //Retorna el Ok  que es igual al 200 (Status)
                var restaurantes = _context.Producto.ToList();
                if(restaurantes != null)
                {
                    return Ok(restaurantes);
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
        public async Task<ActionResult<List<Producto>>> GetProductoById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var restaurante = _context.Producto.Find(id);
                if (restaurante == null)
                    return NotFound();
                return Ok(restaurante);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Producto>>> PostProducto(Producto producto)
        {
            try
            {
                _context.Add(producto);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<ActionResult<List<Producto>>> PutProducto(Producto producto)
        {
            try
            {
                //Buscar restaurante 
                _context.Update(producto);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Producto>>> DeleteProducto(int id)
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
    }
}
