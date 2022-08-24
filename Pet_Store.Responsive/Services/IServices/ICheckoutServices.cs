using Pet_Store.Domains.Models.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Services.IServices
{
    public interface ICheckoutServices
    {
        Task<OrderDetails> addProductAsync(OrderHeader product);
        Task<Payments> addPaymentAsync(Payments payments);
        Task<List<OrderDetails>> getOrderByUserAsync(string userId);
        Task<List<Products>> getOrderProductsAsync(string userId);


    }
}
