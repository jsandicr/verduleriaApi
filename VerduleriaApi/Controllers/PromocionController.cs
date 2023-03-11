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
        public async Task<ActionResult<List<Promocion>>> PostPromocion(Promocion promocion)
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
        public async Task<ActionResult<List<Promocion>>> PutPromocion(Promocion promocion)
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
        public async Task<ActionResult<List<Promocion>>> DeletePromocion(int id)
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
