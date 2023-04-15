using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VerduleriaApi.Models;

namespace VerduleriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly VerduleriaContext _context;

        public RolController(VerduleriaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Rol>>> GetRoles()
        {
            try
            {
                //Retorna el Ok  que es igual al 200 (Status)
                var roles = _context.Rol.ToList();
                if(roles != null)
                {
                    return Ok(roles);
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
        public async Task<ActionResult<List<Rol>>> GetRolById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var rol = _context.Rol.Find(id);
                if (rol == null)
                    return NotFound();
                return Ok(rol);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
            try
            {
                _context.Add(rol);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<ActionResult<Rol>> PutRol(Rol rol)
        {
            try
            {
                //Buscar restaurante 
                _context.Update(rol);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Rol>>> DeleteRol(int id)
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
