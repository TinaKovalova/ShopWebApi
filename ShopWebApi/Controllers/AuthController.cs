using BLL.DTO;
using BLL.Services;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopWebApi.DAL.Models;
using ShopWebApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ShopWebApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IService<UserDTO> userService;
        IService<UserRoleDTO> roleService;
        DbContext context;
        public AuthController()
        {
            this.context = new ShopAdoContext();
            this.userService = new UserService(new UserRepository(context));
            this.roleService = new UserRoleService(new UserRoleRepository(context));
        }

        [HttpPost("register")] 
        public ActionResult Post([FromBody] UserDTO user)
        {
            try
            {
                var isRegest = userService.GetAll().FirstOrDefault(x => x.UserLogin == user.UserLogin);
                if (user == null || user.UserLogin == null || user.PasswordHash == null || user.UserName == null)
                {
                    return BadRequest();
                }
                else if (isRegest != null)
                {
                    return BadRequest("User with this email already exists");
                }
                else
                {
                    user.RoleId = 2;
                    user.PasswordHash = HashPassword(user.PasswordHash);
                    userService.CreateOrUpdate(user);
                    return Ok();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDTO user)
        {
           if(user.UserLogin!=null && user.PasswordHash!=null && user.UserLogin!="" && user.PasswordHash !="")
            {
                var pas = HashPassword(user.PasswordHash);

                var identity = GetIdentity(user.UserLogin, pas);
                if (identity == null)
                {
                    return BadRequest(new { errorText = "Invalid username or password." });
                }
                var now = DateTime.UtcNow;

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = identity,
                    Expires = DateTime.UtcNow.AddDays(7),
                    Issuer = AuthOptions.ISSUER,
                    Audience = AuthOptions.AUDIENCE,
                    SigningCredentials = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var dataUser = userService.GetAll().FirstOrDefault(x => x.UserLogin == user.UserLogin && x.PasswordHash == user.PasswordHash);


                return Ok(new { Token = tokenHandler.WriteToken(token) });

            }
            else
            {
                return BadRequest();
            }

            
            
        }
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            UserDTO user = userService.GetAll().FirstOrDefault(x => x.UserLogin == username && x.PasswordHash == password);
            if (user != null)
            {
            
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserLogin),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString())
                    
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        
        private string HashPassword(string password)
        {
            byte[] data = Encoding.Default.GetBytes(password);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(data);
            password = Convert.ToBase64String(result);
            return password;        
        }
    }
}
