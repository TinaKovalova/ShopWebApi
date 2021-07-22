using BLL.DTO;
using BLL.Services;
using DAL.Repositories;
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
    public class CategoryController : ControllerBase
    {

        IService<CategoryDTO> categoryService;
         DbContext context;

        public CategoryController()
        {
            this.context = new ShopAdoContext();
            this.categoryService = new CategoryService(new CategoryRepository(this.context));
        }
        [HttpGet]
        public ActionResult<List<CategoryDTO>> Get()
        {
            return categoryService.GetAll().ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<CategoryDTO> Get(int id)
        {
            return categoryService.Get(id);
        }
        [HttpPost]
        public void Create([FromBody] CategoryDTO category)
        {
            categoryService.CreateOrUpdate(category);
        }
        [HttpPut("{id}")]
        public void Update(int id, [FromBody] CategoryDTO category)
        {
            var local = categoryService.GetAll().FirstOrDefault(x => x.CategoryId == id);

            if (local != null)
            {
                category.CategoryId = id;
                categoryService.CreateOrUpdate(category);

            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var category = categoryService.Get(id);
            categoryService.Delete(category);
        }
    }
}
