using BLL.DTO;
using BLL.Services;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        IService<UserRoleDTO> roleService;
        DbContext context;
        public UserRoleController()
        {
            this.context = new ShopAdoContext();
            this.roleService = new UserRoleService(new UserRoleRepository(context));

        }
        

        [HttpGet]
        public ActionResult<IEnumerable<UserRoleDTO>> Get()
        {
            try
            {
                return roleService.GetAll().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
       
        [HttpGet("{id}")]
        public ActionResult<UserRoleDTO> Get(int id)
        {
            try
            {
                return roleService.Get(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

       
        [HttpPost]
        public ActionResult Post([FromBody] UserRoleDTO role)
        {
            try
            {
                if (role == null)
                {
                    return BadRequest();
                }
                else
                {
                    roleService.CreateOrUpdate(role);
                    return Ok();
                }
              
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UserRoleDTO role)
        {
            try
            {
                var local = roleService.GetAll().FirstOrDefault(x => x.RoleId == id);

                if (local != null)
                {
                    role.RoleId = id;
                    roleService.CreateOrUpdate(role);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
            
        }

       
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var deleteRole = roleService.Get(id);
                if (deleteRole == null)
                {
                    return NotFound($"Role with Id = {id} not found");
                }
                else
                {
                    roleService.Delete(deleteRole);
                    return StatusCode(StatusCodes.Status200OK);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
            

        }
    }
}
