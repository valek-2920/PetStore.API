using Microsoft.EntityFrameworkCore;
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

        public List<ShoppingCart> GetShoppingcartByUser(int userId)
        {
            var result = (from x in _context.ShoppingCarts
                          .Include(x => x.User)
                          .Include(x => x.Product)
                          where x.User.UserId == userId
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

        public List<Products> getProducts(int userId)
        {
            var result =  (from x in _context.ShoppingCarts where x.User.UserId == userId select x.Product).ToList();

            if (result != null)
            {
                return result;
            }
            return null;
        }

        public List<string> getProductsName(int userId)
        {
            var result = (from x in _context.ShoppingCarts where x.User.UserId == userId select x.Product.Name).ToList();

            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}