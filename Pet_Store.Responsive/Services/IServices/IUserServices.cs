using Pet_Store.Domains.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Services.IServices
{
    public interface IUserServices
    {
        Task<IEnumerable<Users>> getUsersAsync();
        Task<Users> getUserById(int id);
        Task<Users> updateUserById(Users product);
        Task<Users> addUserAsync(Users product);
        Task<string> deleteUserById(int id);
    }
}
