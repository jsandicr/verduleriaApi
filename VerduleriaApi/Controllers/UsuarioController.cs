using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VerduleriaApi.Models;

namespace VerduleriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly VerduleriaContext _context;

        public UsuarioController(VerduleriaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            try
            {
                //Retorna el Ok  que es igual al 200 (Status)
                var usuarios = _context.Usuario.ToList();
                if(usuarios != null)
                {
                    return Ok(usuarios);
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
        public async Task<ActionResult<List<Usuario>>> GetUsuarioById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var usuario = _context.Usuario.Find(id);
                if (usuario == null)
                    return NotFound();
                return Ok(usuario);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            try
            {
                _context.Add(usuario);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<ActionResult<Usuario>> PutUsuario(Usuario usuario)
        {
            try
            {
                _context.Update(usuario);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Usuario>>> DeleteUsuario(int id)
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
