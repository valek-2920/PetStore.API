using Pet_Store.Domains.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Services.IServices
{
    public interface IShoppingCartService
    {

        //---------ShoppingCart----------

      
        Task<IEnumerable<ShoppingCart>> GetShoppingCartAsync(int Userid);
      
       // Task<ShoppingCart> GetShoppingcartByUser(int Userid);

        Task<ShoppingCart> AddShoppingCartAsync(ShoppingCart shoppingcart);

        
        Task<string> deleteShoppinCartById(int Userid, int count, int ProductoID);
    }
}
