using AutoMapper;
using BLL.DTO;
using ShopWebApi.DAL.Models;
using ShopWebApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class ManufacturerService:IService<ManufacturerDTO>
    {
        IRepository<Manufacturer> repository;
        IMapper mapper;
        public ManufacturerService(IRepository<Manufacturer> repository)
        {
            this.repository = repository;
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Manufacturer, ManufacturerDTO>().ReverseMap();
            });
            mapper = new Mapper(configuration);
        }
        public ManufacturerDTO Get(int id) => mapper.Map<ManufacturerDTO>(repository.Get(id));
        public IEnumerable<ManufacturerDTO> GetAll() => mapper.Map<IEnumerable<ManufacturerDTO>>(repository.GetAll());
        public void CreateOrUpdate(ManufacturerDTO entity)
        {
            repository.CreateOrUpdate(mapper.Map<Manufacturer>(entity));
            repository.Save();
        }

        public void Delete(ManufacturerDTO entity)
        {
            try
            {
                var manufacturer = repository.Get(entity.ManufacturerId);
                repository.Delete(manufacturer);
                repository.Save();

            }
            catch (Exception)
            {

            }

        }
    }
}
