using PetStore.DataAccess.Repository.IRepositories;
using Project_PetStore.API.DataAccess;
using Project_PetStore.API.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.DataAccess.Repository
{
    public class ProductsRepository : Repository<Products>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Products model)
        {
            _context.Products.Update(model);
        }
    }
}
