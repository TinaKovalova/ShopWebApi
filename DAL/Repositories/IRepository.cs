using System.Collections.Generic;

namespace ShopWebApi.DAL.Repositories
{
    public interface IRepository<T> where T:class
    {
        void CreateOrUpdate(T entity);
        IEnumerable<T> GetAll();
        T Get(int id);
        void Delete(T entity);
        void Save();
    }
}
