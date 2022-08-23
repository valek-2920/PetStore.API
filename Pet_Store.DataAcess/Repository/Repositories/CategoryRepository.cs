using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace  Pet_Store.Infraestructure.Repository.Repositories
{
    public class CategoryRepository : Repository<Category>, IRepository<Category>
    {
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }

    }
}