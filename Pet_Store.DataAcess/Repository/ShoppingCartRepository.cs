using PetStore.DataAccess.Repository.IRepositories;
using Project_PetStore.API.DataAccess;
using Project_PetStore.API.Models.DataModels;
using System;

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
    }
}