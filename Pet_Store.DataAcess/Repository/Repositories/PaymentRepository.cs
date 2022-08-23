using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Data;
using PetStore.Infraestructure.Repository;
using System.Collections.Generic;
namespace PetStore.Infraestructure.Repository.Repositories
{
    public class PaymentsRepository : Repository<Payments>, IRepository<Payments>
    {
        public PaymentsRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}