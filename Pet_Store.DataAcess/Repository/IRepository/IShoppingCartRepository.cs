using Pet_Store.Domains.Models.DataModels;
using System.Collections.Generic;

namespace Pet_Store.DataAcess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart model);

        List<ShoppingCart> GetShoppingcartByUser(int userId);
        
        List<Products> getProducts(int userId);

        List<string> getProductsName(int userId);
    }
}