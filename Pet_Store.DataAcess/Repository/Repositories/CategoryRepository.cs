using Pet_Store.Domains.Models.DataModels;
using Pet_Store.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Pet_Store.DataAcess.Repository;

namespace PetStore.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, IRepository<Category>
    {
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }

    }
}