using Microsoft.EntityFrameworkCore;
using ShopWebApi.DAL.Models;

namespace ShopWebApi.DAL.Repositories
{
    public class GoodRepository : GenericRepository<Good>
    {
        public GoodRepository(DbContext context) : base(context)
        {
        }
    }
}
