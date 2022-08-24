using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Data;

namespace PetStore.Infraestructure.Repository
{
    public class UsersRepository : Repository<Users>, IRepository<Users>
    {

        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
