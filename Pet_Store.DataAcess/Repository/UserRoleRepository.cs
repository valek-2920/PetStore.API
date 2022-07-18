using Project_PetStore.API.DataAccess;
using Project_PetStore.API.Models.DataModels;
using System;
using PetStore.DataAccess.Repository.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pet_Store.DataAcess.Repository.IRepository;
using PetStore.DataAccess.Repository;

namespace Pet_Store.DataAcess.Repository
{
    public class UserRoleRepository : Repository<UserRoles>, IUserRoleRepository 
    {
        private readonly ApplicationDbContext _context;

        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(UserRoles model)
        {
            throw new NotImplementedException();
        }
    }
}
