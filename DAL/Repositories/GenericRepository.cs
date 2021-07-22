using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace ShopWebApi.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        DbContext context;
        DbSet<T> dbSet;
        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = this.context.Set<T>();
        }
        public void CreateOrUpdate(T entity)=> dbSet.Update(entity);  
        public void Delete(T entity) => dbSet.Remove(entity);
        public T Get(int id)=> dbSet.Find(id);
        public IEnumerable<T> GetAll() => dbSet.AsNoTracking<T>();
        public void Save() => context.SaveChanges();
        
    }
}
