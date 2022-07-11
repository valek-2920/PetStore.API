using Project_PetStore.API.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.DataAccess.Repository.IRepositories
{
    public interface IUsersDirectionRepository : IRepository<UserDirection>
    {
        void Actualizar(UserDirection model);
    }
}
