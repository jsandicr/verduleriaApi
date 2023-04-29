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
        private readonly IRolModel _rolModel;

        public RolController(IRolModel rolModel)
        {
            _rolModel = rolModel;
        }

        [HttpGet]
        public async Task<ActionResult<List<Rol>>> GetRoles()
        {
            try
            {
                var roles = _rolModel.GetRoles();
                return Ok(roles);    
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
                var rol = _rolModel.GetRolById(id);
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
                var response = _rolModel.PostRol(rol);
                if (response.Id != null)
                {
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

        [HttpPut]
        public async Task<ActionResult<Rol>> PutRol(Rol rol)
        {
            try
            {
                var response = _rolModel.PutRol(rol);
                if (response.Id != null)
                {
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
