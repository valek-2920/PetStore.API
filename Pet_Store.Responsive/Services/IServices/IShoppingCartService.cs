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

      
        Task<IEnumerable<ShoppingCart>> GetShoppingCartAsync(string Userid);
      
        Task<ShoppingCart> GetShoppingcartByUser(string UserId);

        Task<ShoppingCart> AddShoppingCartAsync(ShoppingCart shoppingcart);

        
        Task<string> deleteShoppinCartById(string Userid, int ProductoID);
    }
}
