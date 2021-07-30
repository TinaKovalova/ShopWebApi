using AutoMapper;
using BLL.DTO;
using ShopWebApi.DAL.Models;
using ShopWebApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class PhotoService:IService<PhotoDTO>
    {
        IRepository<Photo> photoRepository;
        IMapper mapper;
        public PhotoService(IRepository<Photo> photoRepository)
        {
            this.photoRepository = photoRepository;
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Photo, PhotoDTO>().ReverseMap();

            });
            mapper = new Mapper(configuration);

        }
        public PhotoDTO Get(int id) => mapper.Map<PhotoDTO>(photoRepository.Get(id));
        public IEnumerable<PhotoDTO> GetAll() => mapper.Map<IEnumerable<PhotoDTO>>(photoRepository.GetAll());
        public void CreateOrUpdate(PhotoDTO entity)
        {
            photoRepository.CreateOrUpdate(mapper.Map<Photo>(entity));
            photoRepository.Save();
        }
        public void Delete(PhotoDTO entity)
        {
            var photo = photoRepository.Get(entity.PhotoId);
            photoRepository.Delete(photo);
            photoRepository.Save();
        }
    }
}
