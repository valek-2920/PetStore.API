using Pet_Store.Domains.Models.DataModels;
using PetStore.Domain.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pet_Store.DataAcess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Products>
    {
        Task<Products> CreateProduct(NewProduct model);
        void Update(Products model);
        bool ProductExist(string name);
        Task<List<Products>> GetAllProductsAsync();
        Task<Products> GetProductAsync(int id);

    }
}
