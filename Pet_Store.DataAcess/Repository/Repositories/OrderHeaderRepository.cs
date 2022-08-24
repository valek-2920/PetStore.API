using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Data;

namespace PetStore.Infraestructure.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IRepository<OrderHeader>
    {

        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
