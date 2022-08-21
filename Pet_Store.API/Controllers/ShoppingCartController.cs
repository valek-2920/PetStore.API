using Microsoft.AspNetCore.Mvc;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using Pet_Store.DataAcess.Data;
using System.Collections.Generic;
using Pet_Store.DataAcess.Repository.UnitOfWork;
using Pet_Store.DataAcess.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<Category> _categoryRepository;
        readonly IRepository<Products> _productsRepository;
        readonly IRepository<Users> _usersRepository;
        readonly IRepository<ShoppingCart> _shoppingCartRepository;
        readonly ApplicationDbContext _context;


        public ShoppingCartController(IUnitOfWork<ApplicationDbContext> unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _productsRepository = _unitOfWork.Repository<Products>();
            _usersRepository = _unitOfWork.Repository<Users>();
            _shoppingCartRepository = _unitOfWork.Repository<ShoppingCart>();
            _context = context;
        }


        [HttpPost]
        [Route("add-products")]
        public IActionResult addToCart([FromBody] NewShoppingCart model)
        {
            if (ModelState.IsValid)
            {
                var getUser = _usersRepository.GetFirstOrDefault(x => x.Id == model.UserId);
                var getProduct = _productsRepository.GetFirstOrDefault(x => x.Name == model.Product);
                ShoppingCart shoppingCart = new ShoppingCart();

                if (getUser != null && getProduct != null)
                {

                    var CartExist = _shoppingCartRepository.GetFirstOrDefault(x => x.User.Id == model.UserId);
                    var products = getProductsName(model.UserId);

                    if (CartExist != null && products.Contains(model.Product))
                    {
                        //actualizar producto existente con nuevos datos
                        CartExist.Count += model.Count;
                        CartExist.Subtotal = CartExist.Count * getProduct.Price;

                        _shoppingCartRepository.Update(CartExist);
                        _unitOfWork.Save();
                        return Ok(CartExist);
                    }
                    //crear carrito al usuario
                    shoppingCart = new ShoppingCart
                    {
                        Count = model.Count,
                        Product = getProduct,
                        User = getUser,
                        Subtotal = (model.Count * getProduct.Price),
                    };

                    _shoppingCartRepository.Add(shoppingCart);
                    _unitOfWork.Save();
                    return Ok(shoppingCart);
                }
                return BadRequest("Producto o usuario no existe");
            }
            return BadRequest("Error al agregar producto al carrito");
        }


        [HttpDelete]
        [Route("remove-product")]
        public IActionResult DeleteItem(string Userid, int count, int ProductoID)
        {
            var CartExist = _shoppingCartRepository.GetFirstOrDefault(x => x.User.Id == Userid);
            var products = getProducts(Userid);
            var product = _shoppingCartRepository.GetFirstOrDefault(x => x.Product.ProductId == ProductoID);
            var getProduct = _productsRepository.GetFirstOrDefault(x => x.Name == product.Product.Name);

            if (product != null && CartExist != null)
            {

                if (CartExist.Product.ProductId == ProductoID)
                {
                    if (CartExist.Count >= 1)
                    {
                        //actualizar producto existente con nuevos datos
                        CartExist.Count = product.Count - count;
                        CartExist.Subtotal = CartExist.Count * getProduct.Price;

                        if (CartExist.Count <= 0)
                        {
                            _shoppingCartRepository.Remove(product);
                            _unitOfWork.Save();
                            return Ok($"El producto ha sido eliminado del carrito ");
                        }

                        _shoppingCartRepository.Update(CartExist);
                        _unitOfWork.Save();
                        return Ok(CartExist);
                    }


                    return Ok($"Es probable que el articulo no exista en el carrito ");
                }
            }
            else if (CartExist.Product == null)
            {

                _shoppingCartRepository.Remove(CartExist);
                _unitOfWork.Save();
                return Ok($"Se elimino el carrito ");
            }
            else if (product == null)
            {

                return BadRequest("Error al remover el producto del carrito puede que el usuario no tenga carrito o el articulo no exista");
            }

            return BadRequest("por favor intentelo de nuevo");
        }





        [HttpGet]
        [Route("GetAll-Products")]
        public IActionResult GetProducts(string userId)
        {
            List<ShoppingCart> allProductosShoppinCart = GetShoppingcartByUser(userId);
            _unitOfWork.Save();

            if (allProductosShoppinCart != null)
            {
                return Ok(allProductosShoppinCart);
            }
            return BadRequest("No hay productos en el carrito");
        }


        /*******************LINQS************************************************/

        public List<ShoppingCart> GetShoppingcartByUser(string userId)
        {
            var result = (from x in _context.ShoppingCarts
                          .Include(x => x.User)
                          .Include(x => x.Product)
                          where x.User.Id == userId
                          select x).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }


        public List<Products> getProducts(string userId)
        {
            var result = (from x in _context.ShoppingCarts where x.User.Id == userId select x.Product).ToList();

            if (result != null)
            {
                return result;
            }
            return null;
        }

        public List<string> getProductsName(string userId)
        {
            var result = (from x in _context.ShoppingCarts where x.User.Id == userId select x.Product.Name).ToList();

            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
