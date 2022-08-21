using Microsoft.AspNetCore.Mvc;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.ViewModels;
using PetStore.DataAccess.Repository.UnityOfWork;
using System.Linq;

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
            var GetUser = _unityOfWork.UsersRepository.GetFirstOrDefault(x => x.Id == model.UserId);
            var getShoppingCart = _unityOfWork.ShoppingCartRepository.GetAll(x => x.User.Id == model.UserId);
            var getProducts = _unityOfWork.ShoppingCartRepository.getProducts(model.UserId);
            var getOrderHeader = _unityOfWork.OrderHeaderRepository.GetFirstOrDefault(x => x.User.Id == model.UserId);
            var getOrderDetails = _unityOfWork.OrderDetailsRepository.GetFirstOrDefault(x => x.OrderHeader == getOrderHeader);
            var quantity = 0;
            var total = 0.0;

            if (GetUser != null)
            {
                if (getProducts.Count() >= 1)
                {
                    if (getOrderHeader == null)
                    {
                        //add new OrderHeader
                        OrderHeader orderHeader = new OrderHeader
                        {
                            User = GetUser,
                            PhoneNumber = GetUser.Phone,
                            Address = model.Address,
                            City = model.City,
                            Country = model.Country
                        };

                        OrderDetails orderDetails = new OrderDetails();

                        //insert shopping cart to order details
                        foreach (var item in getShoppingCart)
                        {
                            quantity += item.Count;
                            total += item.Subtotal;

                            orderDetails = new OrderDetails
                            {
                                OrderHeader = orderHeader,
                                Product = getProducts,
                                Quantity = quantity,
                                Total = total
                            };

                            _unityOfWork.OrderDetailsRepository.Add(orderDetails);
                            _unityOfWork.Save();
                            return Ok(orderDetails);
                        }
                    }

                    //update shopping cart to order details
                    foreach (var item in getShoppingCart)
                    {
                        quantity += item.Count;
                        total += item.Subtotal;

                        getOrderDetails.OrderHeader = getOrderHeader;
                        getOrderDetails.Product = getProducts;
                        getOrderDetails.Quantity = quantity;
                        getOrderDetails.Total = total;
                    }

                    _unityOfWork.OrderDetailsRepository.Update(getOrderDetails);
                    _unityOfWork.Save();
                    return Ok(getOrderDetails);
                }
                return BadRequest("Usuario no posee productos en el carrito");
               
            }

            return BadRequest("Usuario no existe");
        }

        [HttpGet]
        [Route("order")]
        public IActionResult GetOrder(string userId)
        {
            var orderDetails = _unityOfWork.OrderDetailsRepository.GetOrderByUser(userId);
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
            var oldOrderHeader = _unityOfWork.OrderHeaderRepository.GetFirstOrDefault(x => x.User.Id == model.UserId);
            var GetUser = _unityOfWork.UsersRepository.GetFirstOrDefault(x => x.Id == model.UserId);
            var getShoppingCart = _unityOfWork.ShoppingCartRepository.GetAll(x => x.User.Id == model.UserId);
            var getProducts = _unityOfWork.ShoppingCartRepository.getProducts(model.UserId);
            OrderDetails orderDetails = new OrderDetails();
            var quantity = 0;
            var total = 0.0;


            if (GetUser != null)
            {
                //update OrderHeader

                oldOrderHeader.User = GetUser;
                oldOrderHeader.PhoneNumber = GetUser.Phone;
                oldOrderHeader.Address = model.Address;
                oldOrderHeader.City = model.City;
                oldOrderHeader.Country = model.Country;


                _unityOfWork.OrderHeaderRepository.Update(oldOrderHeader);
                _unityOfWork.Save();

                //insert shopping cart to order details
                foreach (var item in getShoppingCart)
                {
                    quantity += item.Count;
                    total += item.Subtotal;

                    orderDetails = new OrderDetails
                    {
                        OrderHeader = oldOrderHeader,
                        Product = getProducts,
                        Quantity = quantity,
                        Total = total
                    };
                }

                return Ok(orderDetails);
            }

            return BadRequest("Usuario no existe");
        }


        [HttpDelete]
        [Route("order")]
        public IActionResult DeleteOrder(string userId)
        {
            var orderDetails = _unityOfWork.OrderDetailsRepository.GetFirstOrDefault(x => x.OrderHeader.User.Id == userId);

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
