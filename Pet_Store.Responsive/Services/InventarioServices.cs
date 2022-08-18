using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using Pet_Store.Responsive.Services.IServices;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Services
{
    public class InventarioServices : IInventarioServices
    {

        /************************* Products Services ********************************/

        public async Task<IEnumerable<Products>> getProductsAsync()
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
            return products;
        }

        public async Task<Products> getProductById(int id)
        {
            Products products = new Products();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:44316/api/Products/product?id=" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        products = JsonConvert.DeserializeObject<Products>(Response);
                    }
                }
            }
            return products;
        }

        public async Task<Products> updateProductById(Products product)
        {
            Products putProduct = new Products();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8);

                using (var response = await httpClient.PutAsync("https://localhost:44316/api/Products/product", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        putProduct = JsonConvert.DeserializeObject<Products>(Response);
                    }
                }
            }
            return putProduct;
        }

        public async Task<Products> addProductAsync(Products product)
        {
            Products postProduct = new Products();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8);

                using (var response = await httpClient.PostAsync("https://localhost:44316/api/Products/product", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    postProduct = JsonConvert.DeserializeObject<Products>(apiResponse);
                }
            }
            return postProduct;
        }

        public async Task<string> deleteProductById(int id)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:44316/api/Products/product?id=" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return apiResponse;
        }

        /************************* Categories Services ********************************/


        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            List<Category> categories = new List<Category>();

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
            return categories;
        }

        public async Task<Category> getCategoryById(int id)
        {
            Category category = new Category();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:44316/api/Category/category?id=" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        category = JsonConvert.DeserializeObject<Category>(Response);
                    }
                }
            }
            return category;
        }

        public async Task<Category> updateCategoryById(Category category)
        {
            Category putCategory = new Category();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8);

                using (var response = await httpClient.PutAsync("https://localhost:44316/api/Category/category", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        putCategory = JsonConvert.DeserializeObject<Category>(Response);
                    }
                }
            }
            return putCategory;
        }

        public async Task<Category> AddCategoryAsync(Category category)
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
            return postCategory;
        }

        public async Task<string> deleteCategoryById(int id)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:44316/api/Category/category?id="+id))
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
