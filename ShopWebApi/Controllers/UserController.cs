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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IService<UserDTO> userService;
        DbContext context;
        public UserController()
        {
            this.context = new ShopAdoContext();
            this.userService = new UserService(new UserRepository(context));

        }
        /*
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> Get()
        {
            try
            {
                return userService.GetAll().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
        {
            try
            {
                return userService.Get(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }*/

      
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UserDTO user)
        {
            try
            {
                var local = userService.GetAll().FirstOrDefault(x => x.UserId == id);
                if (local != null)
                {
                    user.UserId = id;
                    user.RoleId = 2;
                    userService.CreateOrUpdate(user);
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
                var deleteUser = userService.Get(id);
                if (userService == null)
                {
                    return NotFound($"User with Id = {id} not found");
                }
                else
                {
                    userService.Delete(deleteUser);
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
