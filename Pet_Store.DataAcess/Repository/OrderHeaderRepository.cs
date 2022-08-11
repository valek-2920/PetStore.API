using Pet_Store.DataAcess.Repository.IRepository;
using Pet_Store.Domains.Models.DataModels;
using Project_PetStore.API.DataAccess;

namespace PetStore.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
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
