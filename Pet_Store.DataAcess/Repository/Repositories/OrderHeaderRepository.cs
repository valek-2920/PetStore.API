using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Data;

namespace Pet_Store.Infraestructure.Repository.Repositories
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IRepository<OrderHeader>
    {

        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
