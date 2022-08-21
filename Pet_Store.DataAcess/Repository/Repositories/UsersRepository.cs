using Pet_Store.Domains.Models.DataModels;
using Pet_Store.DataAcess.Data;
using Pet_Store.DataAcess.Repository;

namespace PetStore.DataAccess.Repository
{
    public class UsersRepository : Repository<Users>, IRepository<Users>
    {

        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
