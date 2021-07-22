using AutoMapper;
using BLL.DTO;
using ShopWebApi.DAL.Models;
using ShopWebApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class GoodService : IService<GoodDTO>
    {
        IRepository<Good> goodRepository;
        IMapper mapper;
        public GoodService(IRepository<Good> goodRepository)
        {
            this.goodRepository = goodRepository;
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Good, GoodDTO>()
                .ForMember(dto => dto.CategoryName, x => x.MapFrom(good => good.Category.CategoryName))
                .ForMember(dto => dto.ManufacturerName, x => x.MapFrom(good => good.Manufacturer.ManufacturerName));
                cfg.CreateMap<GoodDTO, Good>();

            });
            mapper = new Mapper(configuration);

        }
        public GoodDTO Get(int id) => mapper.Map<GoodDTO>(goodRepository.Get(id));
        public IEnumerable<GoodDTO> GetAll() => mapper.Map<IEnumerable<GoodDTO>>(goodRepository.GetAll());
        public void CreateOrUpdate(GoodDTO entity)
        {
            goodRepository.CreateOrUpdate(mapper.Map<Good>(entity));
            goodRepository.Save();
        }
        public void Delete(GoodDTO entity)
        {
            var good = goodRepository.Get(entity.GoodId);
            goodRepository.Delete(good);
            goodRepository.Save();
        }
    }
}
