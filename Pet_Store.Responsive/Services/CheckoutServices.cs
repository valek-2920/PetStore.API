using Newtonsoft.Json;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Responsive.Services.IServices;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Services
{
    public class CheckoutServices : ICheckoutServices
    {
        public async Task<Payments> addPaymentAsync(Payments payments)
        {
            Payments postPayment = new Payments();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(payments), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44316/api/Payment/payment", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    postPayment = JsonConvert.DeserializeObject<Payments>(apiResponse);
                }
            }
            return postPayment;
        }

        public async Task<OrderDetails> addProductAsync(OrderHeader product)
        {
            OrderDetails postOrder = new OrderDetails();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44316/api/Order/order", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    postOrder = JsonConvert.DeserializeObject<OrderDetails>(apiResponse);
                }
            }
            return postOrder;
        }

        public async Task<OrderDetails> getOrderByUserAsync(string userId)
        {
            OrderDetails orders = new OrderDetails();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:44316/api/Products/product?id=" + userId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        orders = JsonConvert.DeserializeObject<OrderDetails>(Response);
                    }
                }
            }
            return orders;
        }

        public async Task<List<Products>> getOrderProductsAsync(string userId)
        {
            List<Products> products = new List<Products>();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:44316/api/Order/order-products?userId=" + userId))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        products = JsonConvert.DeserializeObject<List<Products>>(Response);
                    }
                }
            }
            return products;
        }
    }
}
