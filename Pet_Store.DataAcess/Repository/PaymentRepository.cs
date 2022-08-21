using Pet_Store.DataAcess.Repository.IRepository;
using Pet_Store.Domains.Models.DataModels;
using Project_PetStore.API.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.DataAccess.Repository
{
    public class PaymentRepository : Repository<Payments>, IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Payments model)
        {
            _context.Payments.Update(model);

        }

    }
}