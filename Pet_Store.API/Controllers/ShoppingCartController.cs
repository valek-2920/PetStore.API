using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetStore.DataAccess.Repository;
using PetStore.DataAccess.Repository.IRepositories;
using PetStore.DataAccess.Repository.UnityOfWork;
using PetStore.Domain.Models.ViewModels;
using Project_PetStore.API.Models.DataModels;
using System;
using System.Threading.Tasks;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {

        private readonly IUnityOfWork _unityOfWork;
        public ShoppingCartController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        [HttpPost]
        [Route("add-products")]
        //[ValidateAntiForgeryToken]
        public IActionResult addToCart([FromBody] NewShoppingCart model)
        {
            if (ModelState.IsValid)
            {
                var getUser = _unityOfWork.UsersRepository.GetFirstOrDefault(x => x.UserId == model.UserId);
                var getProduct = _unityOfWork.ProductsRepository.GetFirstOrDefault(x => x.Name == model.Product);

                NewShoppingCart VShopping = new NewShoppingCart();
                ShoppingCart MShopping = new ShoppingCart();
                
                if (getUser != null && getProduct != null)
                {

                    Console.WriteLine(getUser);
                    Console.WriteLine(getProduct);

                    MShopping.Product = getProduct;
                    MShopping.User = getUser;
                    ShoppingCart shoppingCart = new ShoppingCart
                    {
                        Count = model.Count,
                        Product = getProduct,
                        User = getUser,
                        Subtotal = (model.Count * getProduct.Price),
                        Total = model.Subtotal * 1.13,
                    };
                    _unityOfWork.ShoppingCartRepository.Add(shoppingCart);
                    _unityOfWork.Save();
                    return Ok(shoppingCart);
                }
            }
            return BadRequest("Error al agregar producto al carrito");
        }


        [HttpDelete]
        [Route("remove-product")]
        public IActionResult DeleteCategory(int id)
        {
            var product = _unityOfWork.ShoppingCartRepository.GetFirstOrDefault(x => x.Product.ProductId == id);
            if (product != null)
            {
                _unityOfWork.ShoppingCartRepository.Remove(product);
                _unityOfWork.Save();
                return Ok($"El producto ha sido eliminado del carrito ");
            }
            return BadRequest("Error al remover el producto del carrito");
        }


        [HttpGet]
        [Route("GetAll-Products")]
        public IActionResult GetProducts()
        {
            var allProductosShoppinCart = _unityOfWork.ShoppingCartRepository.GetAll();
            _unityOfWork.Save();

            if (allProductosShoppinCart != null)
            {
                return Ok(allProductosShoppinCart);
            }
            return BadRequest("No hay productos en el carrito");
        }
    }
}
