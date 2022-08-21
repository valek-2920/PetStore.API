using Microsoft.EntityFrameworkCore;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.DataAcess.Data;
using System.Collections.Generic;
using System.Linq;
using Pet_Store.DataAcess.Repository;

namespace PetStore.DataAccess.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IRepository<OrderDetails>
    {
        public OrderDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}