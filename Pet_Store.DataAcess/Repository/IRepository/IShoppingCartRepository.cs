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
      

        List<ShoppingCart> GetShoppingcartByUser(int userId);
        List<Products> getProducts(int userId);
        void Update(ShoppingCart model);
       // void Update(int count);
    }
}