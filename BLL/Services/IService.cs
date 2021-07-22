using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public interface IService<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void CreateOrUpdate(T entity);
        void Delete(T entity);

    }
}

