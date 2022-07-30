using Project_PetStore.API.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.DataAccess.Repository.IRepositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart model);

        List<ShoppingCart> GetShoppingcartByUser(int userId);
        
        List<Products> getProducts(int userId);

        List<string> getProductsName(int userId);
    }
}