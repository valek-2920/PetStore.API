
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pet_Store.Domains.Models.DataModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Controllers
{
    public class InventarioController : Controller
    {

        readonly IConfiguration _configuration;

        public InventarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<IActionResult> Inventario()
        {
            List<Products> products = new List<Products>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44316/api/Products/products"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        products = JsonConvert.DeserializeObject<List<Products>>(Response);
                    }

                }
            }
            return View(products);
        }

        public IActionResult AgregarProducto()
        {
            return View();
        }
        public IActionResult EditarProducto()
        {
            return View();
        }

        public async Task<IActionResult> Categorias()
        {
            List<Category> categories = new List<Category>();
            var apiUrl = _configuration.GetSection("apiUrl");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44316/api/Category/categories"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        categories = JsonConvert.DeserializeObject<List<Category>>(Response);
                    }

                }
            }
            return View(categories);
        }

        public async Task<IActionResult> AgregarCategorias(Category category)
        {
            Category postCategory = new Category();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44316/api/Category/category", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    postCategory = JsonConvert.DeserializeObject<Category>(apiResponse);
                }
            }
            return View(postCategory);
        }
    }
}
