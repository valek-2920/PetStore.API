using Microsoft.EntityFrameworkCore;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using Pet_Store.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pet_Store.DataAcess.Repository;

namespace PetStore.DataAccess.Repository
{
    public class ProductsRepository : Repository<Products>, IRepository<Products>
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
