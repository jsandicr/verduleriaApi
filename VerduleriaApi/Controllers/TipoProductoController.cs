using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VerduleriaApi.Models;

namespace VerduleriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProductoController : ControllerBase
    {
        private readonly VerduleriaContext _context;

        public TipoProductoController(VerduleriaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoProducto>>> GetTipos()
        {
            try
            {
                //Retorna el Ok  que es igual al 200 (Status)
                var tipos = _context.TipoProducto.ToList();
                if(tipos != null)
                {
                    return Ok(tipos);
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
        public async Task<ActionResult<List<TipoProducto>>> GetTipoById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var tipo = _context.TipoProducto.Find(id);
                if (tipo == null)
                    return NotFound();
                return Ok(tipo);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<TipoProducto>> PostTipoProducto(TipoProducto tipo)
        {
            try
            {
                _context.Add(tipo);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<ActionResult<TipoProducto>> PutTipoProducto(TipoProducto tipo)
        {
            try
            {
                //Buscar restaurante 
                _context.Update(tipo);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TipoProducto>>> DeleteTipoProducto(int id)
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
