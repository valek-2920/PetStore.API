using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Data;

namespace Pet_Store.Infraestructure.Repository.Repositories
{
    public class ProductsRepository : Repository<Products>, IRepository<Products>
    {

        public ProductsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
