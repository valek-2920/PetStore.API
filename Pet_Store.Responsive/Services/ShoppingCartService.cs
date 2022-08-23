using Newtonsoft.Json;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Responsive.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Services
{
    public class ShoppingCartService : IShoppingCartService
    {


        /************************* ShoppingCart Services ********************************/


        public async Task<IEnumerable<ShoppingCart>> GetShoppingCartAsync(int userId)
        {
            List<ShoppingCart> cart = new List<ShoppingCart>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44316/api/ShoppingCart/GetAll-Products?userId="+userId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        cart = JsonConvert.DeserializeObject<List<ShoppingCart>>(Response);
                    }

                }
            }
            return cart;
        }

        public async Task<ShoppingCart> GetShoppingcartByUser(int UserId)
        {

            ShoppingCart cart = new ShoppingCart();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:44316/api/ShoppingCart/GetAll-Products?userId=" + UserId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        cart = JsonConvert.DeserializeObject<ShoppingCart>(Response);
                    }
                }
            }
            return cart;
        }

        public async Task<ShoppingCart> AddShoppingCartAsync(ShoppingCart cart)
        {
            ShoppingCart postCart = new ShoppingCart();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44316/api/ShoppingCart/add-products", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    postCart = JsonConvert.DeserializeObject<ShoppingCart>(apiResponse);
                }
            }
            return postCart;
        }


        public async Task<string> deleteShoppinCartById(int Userid, int count, int ProductoID)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:44316/api/ShoppingCart/remove-product/"+Userid+"/"+count+"/"+ProductoID))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return apiResponse;
        }
    }
}
