using AutoMapper;
using BLL.DTO;
using ShopWebApi.DAL.Models;
using ShopWebApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
     public class UserRoleService:IService<UserRoleDTO>
    {
        IRepository<UserRole> roleRepository;
        IMapper mapper;
        public UserRoleService(IRepository<UserRole> roleRepository)
        {
            this.roleRepository = roleRepository;
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRole, UserRoleDTO>().ReverseMap();
         
            });
            mapper = new Mapper(configuration);

        }
        public UserRoleDTO Get(int id) => mapper.Map<UserRoleDTO>(roleRepository.Get(id));
        public IEnumerable<UserRoleDTO> GetAll() => mapper.Map<IEnumerable<UserRoleDTO>>(roleRepository.GetAll());
        public void CreateOrUpdate(UserRoleDTO entity)
        {
            roleRepository.CreateOrUpdate(mapper.Map<UserRole>(entity));
            roleRepository.Save();
        }
        public void Delete(UserRoleDTO entity)
        {
            var userRole = roleRepository.Get(entity.RoleId);
            roleRepository.Delete(userRole);
            roleRepository.Save();
        }
    }
}
