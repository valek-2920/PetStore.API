using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.ViewModels;
using Pet_Store.Infraestructure.Data;
using PetStore.Infraestructure.Repository;
using PetStore.Infraestructure.Repository.UnitOfWork;
using System.Collections.Generic;
using Pet_Store.Domains.Models.InputModels;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<OrderDetails> _orderDetailsRepository;
        readonly IRepository<OrderHeader> _orderHeaderRepository;
        readonly IRepository<Users> _usersRepository;
        readonly IRepository<ShoppingCart> _shoppingCartRepository;
        readonly ApplicationDbContext _context;


        public OrderController(IUnitOfWork<ApplicationDbContext> unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _orderDetailsRepository = _unitOfWork.Repository<OrderDetails>();
            _orderHeaderRepository = _unitOfWork.Repository<OrderHeader>();
            _usersRepository = _unitOfWork.Repository<Users>();
            _shoppingCartRepository = _unitOfWork.Repository<ShoppingCart>();
            _context = context;
        }

        [HttpPost]
        [Route("order")]
        public IActionResult CreateOrder([FromBody] OrderViewModel model)
        {
            var GetUser = _usersRepository.GetFirstOrDefault(x => x.Id == model.UserId);
            var getShoppingCart = _shoppingCartRepository.GetAll(x => x.User.Id == model.UserId);
            var getProducts = (from x in _context.ShoppingCarts where x.User.Id == model.UserId select x.Product).ToList();
            var getOrderHeader = _orderHeaderRepository.GetFirstOrDefault(x => x.User.Id == model.UserId);
            var getOrderDetails = _orderDetailsRepository.GetFirstOrDefault(x => x.OrderHeader == getOrderHeader);
            var quantity = 0;
            var total = 0.0;

            if (GetUser != null && getProducts != null)
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

                            _orderDetailsRepository.Add(orderDetails);
                            _unitOfWork.Save();
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

                    _orderDetailsRepository.Update(getOrderDetails);
                    _unitOfWork.Save();
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
            var orderDetails = (from x in _context.OrderDetails
                          .Include(x => x.OrderHeader.User)
                          .Include(x => x.Product)
                                where x.OrderHeader.User.Id == userId
                                select x).ToList();

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
            var oldOrderHeader = _orderHeaderRepository.GetFirstOrDefault(x => x.User.Id == model.UserId);
            var GetUser = _usersRepository.GetFirstOrDefault(x => x.Id == model.UserId);
            var getShoppingCart = _shoppingCartRepository.GetAll(x => x.User.Id == model.UserId);
            var getProducts = (from x in _context.ShoppingCarts where x.User.Id == model.UserId select x.Product).ToList();
            OrderDetails orderDetails = new OrderDetails();
            var quantity = 0;
            var total = 0.0;


            if (GetUser != null && getProducts != null)
            {
                //update OrderHeader

                oldOrderHeader.User = GetUser;
                oldOrderHeader.PhoneNumber = GetUser.Phone;
                oldOrderHeader.Address = model.Address;
                oldOrderHeader.City = model.City;
                oldOrderHeader.Country = model.Country;


                _orderHeaderRepository.Update(oldOrderHeader);
                _unitOfWork.Save();

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
            var orderDetails = _orderDetailsRepository.GetFirstOrDefault(x => x.OrderHeader.User.Id == userId);

            if (orderDetails != null)
            {
                _orderDetailsRepository.Remove(orderDetails);
                _unitOfWork.Save();

                return Ok("La orden del usuario ha sido eliminada");
            }
            return BadRequest("Usuario no posee orden");
        }

        // [HttpGet]
        // [Route("order-products")]
        // public IActionResult getOrderProducts(int userId)
        // {
        //     var products =  _unityOfWork.ShoppingCartRepository.getProducts(userId);

        //     if (products != null)
        //     {
        //         return Ok(products);
        //     }
        //     return BadRequest("Usuario no posee orden");
        // }

    }
}
