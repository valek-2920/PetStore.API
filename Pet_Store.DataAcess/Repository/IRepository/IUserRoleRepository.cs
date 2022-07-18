using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetStore.DataAccess.Repository.IRepositories;
using Project_PetStore.API.Models.DataModels;

namespace Pet_Store.DataAcess.Repository.IRepository
{
    public interface IUserRoleRepository : IRepository<UserRoles>
    {
        void Update(UserRoles model);
    }
}
