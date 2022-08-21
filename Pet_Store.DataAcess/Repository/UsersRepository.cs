using Pet_Store.DataAcess.Repository.IRepository;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.DataAcess.Data;


namespace PetStore.DataAccess.Repository
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Users model)
        {
            _context.Users.Update(model);
        }
    }
}
