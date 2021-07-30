using AutoMapper;
using BLL.DTO;
using ShopWebApi.DAL.Models;
using ShopWebApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class UserService:IService<UserDTO>
    {
        IRepository<User> userRepository;
        IMapper mapper;
        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();

            });
            mapper = new Mapper(configuration);

        }
        public UserDTO Get(int id) => mapper.Map<UserDTO>(userRepository.Get(id));
        public IEnumerable<UserDTO> GetAll() => mapper.Map<IEnumerable<UserDTO>>(userRepository.GetAll());
        public void CreateOrUpdate(UserDTO entity)
        {
            userRepository.CreateOrUpdate(mapper.Map<User>(entity));
            userRepository.Save();
        }
        public void Delete(UserDTO entity)
        {
            var user = userRepository.Get(entity.UserId);
            userRepository.Delete(user);
            userRepository.Save();
        }
    }
}
