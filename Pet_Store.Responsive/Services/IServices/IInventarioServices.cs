using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Services.IServices
{
    public interface IInventarioServices
    {
        Task<IEnumerable<Products>> getProductsAsync();
        Task<Products> getProductById(int id);
        Task<Products> updateProductById(Products product);
        Task<Products> addProductAsync(Products product);
        Task<string> deleteProductById(int id);

        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> getCategoryById(int id);
        Task<Category> AddCategoryAsync(Category category);
        Task<string> deleteCategoryById(int id);
    }
}
