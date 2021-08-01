using AutoMapper;
using BLL.DTO;
using ShopWebApi.DAL.Models;
using ShopWebApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class SaleService:IService<SaleDTO>
    {

        IRepository<Sale> saleRepository;
      
        IMapper mapper;
        public SaleService(IRepository<Sale> saleRepository)
        {
            this.saleRepository = saleRepository;
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sale, SaleDTO>()
               .ForMember(dto => dto.SalePos, x => x.Ignore());
                cfg.CreateMap<SaleDTO, Sale>()
                  .ForMember(pos => pos.SalePos, x => x.Ignore());
               


            });

          
            mapper = new Mapper(configuration);
    

        }
        public SaleDTO Get(int id) => mapper.Map<SaleDTO>(saleRepository.Get(id));
        public IEnumerable<SaleDTO> GetAll() => mapper.Map<IEnumerable<SaleDTO>>(saleRepository.GetAll());
        public void CreateOrUpdate(SaleDTO entity)
        {
            saleRepository.CreateOrUpdate(mapper.Map<Sale>(entity));
            saleRepository.Save();
        }
        public void Delete(SaleDTO entity)
        {
            var sale = saleRepository.Get(entity.SaleId);
            saleRepository.Delete(sale);
            saleRepository.Save();
        }
    }
}
