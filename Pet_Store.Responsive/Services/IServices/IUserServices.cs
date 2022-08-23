using Pet_Store.Domains.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Services.IServices
{
    public interface IUserServices
    {
        Task<IEnumerable<Users>> getUsersAsync();
        Task<Users> getUserById(string id);
        Task<Users> updateUserById(Users user);
        Task<Users> addUserAsync(Users user);
        Task<string> deleteUserById(string id);
    }
}
