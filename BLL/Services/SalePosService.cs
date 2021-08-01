using AutoMapper;
using BLL.DTO;
using ShopWebApi.DAL.Models;
using ShopWebApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class SalePosService:IService<SalePosDTO>
    {
        IRepository<SalePo> positionRepository;
        IMapper mapper;
        public SalePosService(IRepository<SalePo> positionRepository)
        {
            this.positionRepository = positionRepository;
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SalePo, SalePosDTO>()
                .ForMember(dto => dto.GoodName, x => x.MapFrom(pos => pos.Good.GoodName));
                cfg.CreateMap<SalePosDTO, SalePo>();

            });
            mapper = new Mapper(configuration);

        }
        public SalePosDTO Get(int id) => mapper.Map<SalePosDTO>(positionRepository.Get(id));
        public IEnumerable<SalePosDTO> GetAll() => mapper.Map<IEnumerable<SalePosDTO>>(positionRepository.GetAll());
        public void CreateOrUpdate(SalePosDTO entity)
        {
            positionRepository.CreateOrUpdate(mapper.Map<SalePo>(entity));
            positionRepository.Save();
        }
        public void Delete(SalePosDTO entity)
        {
            var pos = positionRepository.Get(entity.SalePosId);
            positionRepository.Delete(pos);
            positionRepository.Save();
        }
    }
}
