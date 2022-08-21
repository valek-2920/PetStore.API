using Microsoft.EntityFrameworkCore;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace PetStore.Infraestructure.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IRepository<OrderDetails>
    {
        public OrderDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}