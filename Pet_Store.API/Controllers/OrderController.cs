using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pet_Store.Domains.Models.ViewModels;
using Pet_Store.Utility;
using PetStore.DataAccess.Repository.UnityOfWork;
using Project_PetStore.API.Models.DataModels;
using Stripe.Checkout;
using System.Collections.Generic;

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

        [HttpPost]
        [Route("order")]
        public IActionResult CreateOrder([FromBody] OrderViewModel model)
        {

            var GetUser = _unityOfWork.UsersRepository.GetFirstOrDefault(x => x.UserId == model.UserId);
            var getShoppingCart = _unityOfWork.ShoppingCartRepository.GetAll(x => x.User.UserId == model.UserId);
            var getProducts = _unityOfWork.ShoppingCartRepository.getProducts(model.UserId);
            OrderDetails orderDetails = new OrderDetails();
            var quantity = 0;
            var total = 0.0;


            if (GetUser != null)
            {
                //add new OrderHeader
                OrderHeader orderHeader = new OrderHeader
                {
                    User = GetUser,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    City = model.City,
                    Country = model.Country
                };

                //insert shopping cart to order details
                foreach (var item in getShoppingCart)
                {
                    quantity = item.Count;
                    total = item.Subtotal;

                    orderDetails = new OrderDetails
                    {
                        OrderHeader = orderHeader,
                        Product = getProducts,
                        Quantity = quantity +  item.Count,
                        Total = total + item.Subtotal
                    };
                }

                _unityOfWork.OrderDetailsRepository.Add(orderDetails);
                _unityOfWork.Save();
                return Ok(orderDetails);
            }

            return BadRequest("Usuario no existe");
        }

        [HttpGet]
        [Route("order")]
        public IActionResult GetOrder(int userId)
        {
            var orderDetails = _unityOfWork.OrderDetailsRepository.GetAll(x => x.OrderHeader.User.UserId == userId, includeProperties: "Users,Products");
            _unityOfWork.Save();

            if (orderDetails != null)
            {
                return Ok(orderDetails);
            }
            return BadRequest("Usuario no posee orden");
        }

        [HttpPut]
        [Route("order")]
        public IActionResult UpdateOrder(OrderViewModel model)
        {
            var oldOrderHeader = _unityOfWork.OrderHeaderRepository.GetFirstOrDefault(x => x.User.UserId == model.UserId);
            var GetUser = _unityOfWork.UsersRepository.GetFirstOrDefault(x => x.UserId == model.UserId);
            var getShoppingCart = _unityOfWork.ShoppingCartRepository.GetAll(x => x.User.UserId == model.UserId);
            var getProducts = _unityOfWork.ShoppingCartRepository.getProducts(model.UserId);
            OrderDetails orderDetails = new OrderDetails();
            var quantity = 0;
            var total = 0.0;


            if (GetUser != null)
            {
                //update OrderHeader

                oldOrderHeader.User = GetUser;
                oldOrderHeader.PhoneNumber = model.PhoneNumber;
                oldOrderHeader.Address = model.Address;
                oldOrderHeader.City = model.City;
                oldOrderHeader.Country = model.Country;
                

                _unityOfWork.OrderHeaderRepository.Update(oldOrderHeader);
                _unityOfWork.Save();

                //insert shopping cart to order details
                foreach (var item in getShoppingCart)
                {
                    quantity = item.Count;
                    total = item.Subtotal;

                    orderDetails = new OrderDetails
                    {
                        OrderHeader = oldOrderHeader,
                        Product = getProducts,
                        Quantity = quantity + item.Count,
                        Total = total + item.Subtotal
                    };
                }

                return Ok(orderDetails);
            }

            return BadRequest("Usuario no existe");
        }


            [HttpDelete]
        [Route("order")]
        public IActionResult DeleteOrder(int userId)
        {
            var orderDetails = _unityOfWork.OrderDetailsRepository.GetFirstOrDefault(x => x.OrderHeader.User.UserId == userId);

            if (orderDetails != null)
            {
                _unityOfWork.OrderDetailsRepository.Remove(orderDetails);
                _unityOfWork.Save();

                return Ok("La orden del usuario ha sido eliminada");
            }
            return BadRequest("Usuario no posee orden");
        }
    }
}
