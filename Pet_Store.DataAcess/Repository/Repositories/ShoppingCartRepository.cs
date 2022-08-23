using Microsoft.EntityFrameworkCore;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Data;

namespace Pet_Store.Infraestructure.Repository.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IRepository<ShoppingCart>
    {

        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}