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
    public class ManufacturerController : ControllerBase
    {

        IService<ManufacturerDTO> manufacturerService;
        DbContext context;

        public ManufacturerController()
        {
            this.context = new ShopAdoContext();
            this.manufacturerService = new ManufacturerService(new ManufacturerRepository(this.context));
        }
        [HttpGet]
        public ActionResult<List<ManufacturerDTO>> Get()
        {
            return manufacturerService.GetAll().ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<ManufacturerDTO> Get(int id)
        {
            return manufacturerService.Get(id);
        }
        [HttpPost]
        public void Create([FromBody] ManufacturerDTO manufacturer)
        {
            manufacturerService.CreateOrUpdate(manufacturer);
        }
        [HttpPut("{id}")]
        public void Update(int id, [FromBody] ManufacturerDTO manufacturer)
        {
            var local = manufacturerService.GetAll().FirstOrDefault(x => x.ManufacturerId == id);

            if (local != null)
            {
                manufacturer.ManufacturerId = id;
                manufacturerService.CreateOrUpdate(manufacturer);

            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var manufacturer = manufacturerService.Get(id);

            manufacturerService.Delete(manufacturer);
        }
    }
}
