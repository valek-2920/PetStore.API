using Newtonsoft.Json;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Responsive.Services.IServices;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Services
{
    public class UsersServices : IUserServices
    {
        public Task<Users> addUserAsync(Users user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> deleteUserById(string id)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:44316/api/Users/user?id=" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return apiResponse;
        }

        public async Task<Users> getUserById(string id)
        {
            Users user = new Users();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:44316/api/Users/user?id=" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<Users>(Response);
                    }
                }
            }
            return user;
        }

        public async Task<IEnumerable<Users>> getUsersAsync()
        {
            List<Users> users = new List<Users>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44316/api/Users/users"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<List<Users>>(Response);
                    }
                }
            }
            return users;
        }

        public async Task<Users> updateUserById(Users user)
        {
            Users putUser = new Users();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44316/api/Users/user", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        putUser = JsonConvert.DeserializeObject<Users>(Response);
                    }
                }
            }
            return putUser;
        }
    }
}
