using Microsoft.EntityFrameworkCore;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.DataAcess.Data;
using System.Collections.Generic;
using System.Linq;
using Pet_Store.DataAcess.Repository;

namespace PetStore.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IRepository<ShoppingCart>
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<ShoppingCart> GetShoppingcartByUser(string userId)
        {
            var result = (from x in _context.ShoppingCarts
                          .Include(x => x.User)
                          .Include(x => x.Product)
                          where x.User.Id == userId
                          select x).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public void Update(ShoppingCart model)
        {
            _context.ShoppingCarts.Update(model);
        }

        public List<Products> getProducts(string userId)
        {
            var result =  (from x in _context.ShoppingCarts where x.User.Id == userId select x.Product).ToList();

            if (result != null)
            {
                return result;
            }
            return null;
        }

        public List<string> getProductsName(string userId)
        {
            var result = (from x in _context.ShoppingCarts where x.User.Id == userId select x.Product.Name).ToList();

            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}