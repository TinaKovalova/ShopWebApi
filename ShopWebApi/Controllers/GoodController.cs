using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.DAL.Models;
using ShopWebApi.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ShopWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class GoodController : ControllerBase
    {

        IService<GoodDTO> goodService;
        DbContext context;

        public GoodController()
        {
            this.context = new ShopAdoContext();
            this.goodService = new GoodService( new GoodRepository(context));
        }
        [HttpGet] 
        public ActionResult<IEnumerable<GoodDTO>> Get()
        {
            return goodService.GetAll().ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<GoodDTO> Get(int id)
        {
            return goodService.Get(id);
        }
        [HttpPost]
        public void Create([FromBody] GoodDTO good)
        {
            goodService.CreateOrUpdate(good);        
        }
        [HttpPut("{id}")]
        public void Update(int id,[FromBody]GoodDTO good)
        {
            var local = goodService.GetAll().FirstOrDefault(x=>x.GoodId==id);
          
           if (local!=null)
            {
                good.GoodId = id;
                goodService.CreateOrUpdate(good);
             
            }      
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var good = goodService.Get(id);
            goodService.Delete(good);      
        }

    }
}
