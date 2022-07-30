using PetStore.DataAccess.Repository.IRepositories;
using Project_PetStore.API.DataAccess;
using Project_PetStore.API.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ShoppingCart model)
        {
            throw new NotImplementedException();
        }

        public List<Products> getProducts(int userId)
        {
            var result =  (from x in _context.ShoppingCarts where x.User.UserId == userId select x.Product).ToList();

            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}