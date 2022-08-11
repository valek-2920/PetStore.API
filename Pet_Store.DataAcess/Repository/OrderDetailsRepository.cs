using Microsoft.EntityFrameworkCore;
using Pet_Store.DataAcess.Repository.IRepository;
using Pet_Store.Domains.Models.DataModels;
using Project_PetStore.API.DataAccess;
using System.Collections.Generic;
using System.Linq;


namespace PetStore.DataAccess.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderDetails model)
        {
           _context.OrderDetails.Update(model);
        }

        public List<OrderDetails> GetOrderByUser(int userId)
        {
            var result = (from x in _context.OrderDetails
                          .Include(x => x.OrderHeader.User)
                          .Include(x => x.Product)
                          where x.OrderHeader.User.UserId == userId
                          select x).ToList();

            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}