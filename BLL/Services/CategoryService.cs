using AutoMapper;
using BLL.DTO;
using ShopWebApi.DAL.Models;
using ShopWebApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class CategoryService:IService<CategoryDTO>
    {
        IRepository<Category> repository;
        IMapper mapper;
        public CategoryService(IRepository<Category> repository)
        {
            this.repository = repository;
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
            });
            mapper = new Mapper(configuration);
        }
        public CategoryDTO Get(int id) => mapper.Map<CategoryDTO>(repository.Get(id));
        public IEnumerable<CategoryDTO> GetAll() => mapper.Map<IEnumerable<CategoryDTO>>(repository.GetAll());
        public void CreateOrUpdate(CategoryDTO entity)
        {
            repository.CreateOrUpdate(mapper.Map<Category>(entity));
            repository.Save();
        }

        public void Delete(CategoryDTO entity)
        {
            try
            {
                var category = repository.Get(entity.CategoryId);
                repository.Delete(category);
                repository.Save();

            }
            catch (Exception)
            {

            }

        }
    }
}
