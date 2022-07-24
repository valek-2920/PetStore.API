using PetStore.DataAccess.Repository.IRepositories;
using Project_PetStore.API.DataAccess;
using Project_PetStore.API.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public OrderDetails GetOrderByUser(int userId)
        {
            var result = (from x in _context.OrderDetails
                          where x.OrderHeader.User.UserId == userId
                          select x).FirstOrDefault();

            if(result != null)
            {
                return result;
            }
            return null;
        }
    }
}