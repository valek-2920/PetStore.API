using Microsoft.EntityFrameworkCore;
using Pet_Store.DataAcess.Repository.IRepository;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using Project_PetStore.API.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.DataAccess.Repository
{
    public class ProductsRepository : Repository<Products>, IProductRepository
    {
        readonly ApplicationDbContext _context;

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
