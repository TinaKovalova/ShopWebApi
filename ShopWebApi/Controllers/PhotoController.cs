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

namespace ShopWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        IService<PhotoDTO> photoService;
        DbContext context;
        public PhotoController()
        {
            this.context = new ShopAdoContext();
            this.photoService = new PhotoService(new PhotoRepository(context));

        }

        [HttpGet]
        public ActionResult<IEnumerable<PhotoDTO>> Get()
        {
            try
            {
                return photoService.GetAll().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
       
        [HttpGet("{id}")]
        public ActionResult<PhotoDTO> Get(int id)
        {  
            try
            {
                return photoService.Get(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
     
        [HttpPost]
        public ActionResult Post([FromBody] PhotoDTO photo)
        {
            try
            {
                if (photo == null)
                {
                    return BadRequest();
                }
                else
                {
                    photoService.CreateOrUpdate(photo);
                    return Ok();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] PhotoDTO photo)
        {
            try
            {
                var local = photoService.GetAll().FirstOrDefault(x => x.PhotoId == id);
                if (local != null)
                {
                    photo.PhotoId = id;
                    photoService.CreateOrUpdate(photo);
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
                var deletePhoto = photoService.Get(id);
                if (deletePhoto == null)
                {
                    return NotFound($"Photo with Id = {id} not found");
                }
                else
                {
                    photoService.Delete(deletePhoto);
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
