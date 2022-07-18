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

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var order = _context.OrderHeaders.FirstOrDefault(u => u.OrderId == id);
            if (order != null)
            {
                order.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    order.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentID(int id, string sessionId, string paymentItentId)
        {
            var orderFromDb = _context.OrderHeaders.FirstOrDefault(u => u.OrderId == id);
            orderFromDb.PaymentDate = DateTime.Now;
            orderFromDb.SessionId = sessionId;
            orderFromDb.PaymentIntentId = paymentItentId;
        }
    }
}
