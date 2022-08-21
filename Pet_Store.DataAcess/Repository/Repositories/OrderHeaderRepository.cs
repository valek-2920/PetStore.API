using Pet_Store.Domains.Models.DataModels;
using Pet_Store.DataAcess.Data;
using Pet_Store.DataAcess.Repository;

namespace PetStore.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IRepository<OrderHeader>
    {

        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
