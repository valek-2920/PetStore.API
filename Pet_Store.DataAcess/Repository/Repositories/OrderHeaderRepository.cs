using Pet_Store.Domains.Models.DataModels;
using Pet_Store.DataAcess.Data;
using Pet_Store.DataAcess.Repository;

namespace PetStore.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IRepository<OrderHeader>
    {
        private readonly ApplicationDbContext _context;

        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderHeader model)
        {
            _context.OrderHeaders.Update(model);

        }
    }
}
