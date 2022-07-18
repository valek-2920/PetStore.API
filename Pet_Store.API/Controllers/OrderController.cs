using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pet_Store.Domains.Models.ViewModels;
using PetStore.DataAccess.Repository.UnityOfWork;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork;

        public OrderController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        [Route("order")]
        public IActionResult GetOrderDetails(int orderId)
        {
            OrderViewModel orderDetails = new OrderViewModel()
            {
                OrderHeader = _unityOfWork.OrderHeaderRepository.GetFirstOrDefault(u => u.OrderId == orderId, includeProperties: "User"),
                OrderDetail = _unityOfWork.OrderDetailsRepository.GetAll(u => u.OrderId == orderId, includeProperties: "Products"),
            };

            _unityOfWork.Save();

            if (orderDetails != null)
            {
                return Ok(orderDetails);
            }
            return BadRequest("No hay orden registrada");
        }
    }
}
