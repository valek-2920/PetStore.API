using Microsoft.EntityFrameworkCore;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Data;

namespace PetStore.Infraestructure.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IRepository<ShoppingCart>
    {

        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}