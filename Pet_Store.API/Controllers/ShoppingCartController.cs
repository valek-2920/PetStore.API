using Microsoft.AspNetCore.Mvc;
using PetStore.DataAccess.Repository.UnityOfWork;
using PetStore.Domain.Models.ViewModels;
using Project_PetStore.API.DataAccess;
using Project_PetStore.API.Models.DataModels;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        IApplicationDbContext Context;
        private readonly IUnityOfWork _unityOfWork;
        // private readonly ApplicationDbContext _context;
        public ShoppingCartController(IUnityOfWork unityOfWork, IApplicationDbContext context)
        {
            _unityOfWork = unityOfWork;
            Context = context;


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

                if (getUser != null && getProduct != null)
                {
                    ShoppingCart shoppingCart = new ShoppingCart
                    {
                        Count = model.Count,
                        Product = getProduct,
                        User = getUser,
                        Subtotal = (model.Count * getProduct.Price),
                    };
                    
                    var exist = _unityOfWork.ShoppingCartRepository.GetFirstOrDefault(x => x.Product.Name == model.Product);
                    if (exist != null)
                    {
                        var result = exist.Count + model.Count;
                        var subTotal2 = exist.Subtotal + model.Subtotal;
                        shoppingCart.Count = 0;
                        model.Count = 0;
                        shoppingCart.Count = result;
                        //_unityOfWork.ShoppingCartRepository.Update(shoppingCart);
                        //_unityOfWork.ShoppingCartRepository.Update(shoppingCart.Count);

                    }
                    _unityOfWork.ShoppingCartRepository.Add(shoppingCart);
                    _unityOfWork.Save();
                    return Ok(shoppingCart);
                }
            }
            return BadRequest("Error al agregar producto al carrito");
        }


        [HttpDelete]
        [Route("remove-product")]
        public IActionResult DeleteItem(int id)
        {
            var product = _unityOfWork.ShoppingCartRepository.GetFirstOrDefault(x => x.Product.ProductId == id);
            if (product != null)
            {
                _unityOfWork.ShoppingCartRepository.Remove(product);
                _unityOfWork.Save();
                return Ok($"El producto  ha sido eliminado del carrito ");
            }
            return BadRequest("Error al remover el producto del carrito  Es posible que el producto ya no este en el carrito");
        }


        [HttpGet]
        [Route("GetAll-Products")]
        public IActionResult GetProducts(int userId)
        {
            var allProductosShoppinCart = _unityOfWork.ShoppingCartRepository.GetShoppingcartByUser(userId);
            _unityOfWork.Save();

            if (allProductosShoppinCart != null)
            {
                return Ok(allProductosShoppinCart);
            }
            return BadRequest("No hay productos en el carrito");
        }
    }
}
