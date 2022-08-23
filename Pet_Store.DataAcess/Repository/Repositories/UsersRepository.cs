using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Data;

namespace Pet_Store.Infraestructure.Repository.Repositories
{
    public class UsersRepository : Repository<Users>, IRepository<Users>
    {

        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
