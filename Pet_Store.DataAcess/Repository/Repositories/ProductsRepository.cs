using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Data;

namespace PetStore.Infraestructure.Repository
{
    public class ProductsRepository : Repository<Products>, IRepository<Products>
    {

        public ProductsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
