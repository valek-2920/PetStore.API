using Microsoft.AspNetCore.Mvc;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Pet_Store.Infraestructure.Data;
using Pet_Store.Infraestructure.Repository.UnitOfWork;
using Pet_Store.Infraestructure.Repository;


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
        public IActionResult addToCart([FromBody] ShoppingCart model)
        {
            if (ModelState.IsValid)
            {

                var getUser = _usersRepository.GetFirstOrDefault(x => x.Id == model.UserId);
                var getProduct = _productsRepository.GetFirstOrDefault(x => x.ProductId == model.ProductId);

                ShoppingCart shoppingCart = new ShoppingCart();

                if (getUser != null && getProduct != null)
                {

                    var CartExist = _shoppingCartRepository.GetFirstOrDefault(x => x.User.Id == model.UserId);
                    var products = (from x in _context.ShoppingCarts where x.User.Id == model.UserId select x.Product.Name).ToList();

                    if (CartExist != null && products != null && products.Contains(model.Product.Name))
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
                        Count = 1,
                        ProductId = model.ProductId,
                        Product = getProduct,
                        UserId = model.UserId,
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


        //[HttpGet]
        //[Route("shopping")]
        //public IActionResult GetShopping()
        //{
        //    var allCart = _unityOfWork.ShoppingCartRepository.GetAll();
        //    _unityOfWork.Save();

        //    if (allCart != null)
        //    {
        //        return Ok(allCart);
        //    }
        //    return BadRequest("No hay carrito");
        //}

        [HttpDelete]
        [Route("remove-product")]
        public IActionResult DeleteItem(string Userid, int ProductoID)
        {
            var CartExist = _shoppingCartRepository.GetFirstOrDefault(x => x.User.Id == Userid);

            var products = (from x in _context.ShoppingCarts
                          .Include(x => x.User)
                          .Include(x => x.Product)
                            where x.User.Id == Userid
                            select x).ToList();

            var product = _shoppingCartRepository.GetFirstOrDefault(x => x.Product.ProductId == ProductoID);
            var getProduct = _productsRepository.GetFirstOrDefault(x => x.Name == product.Product.Name);
            if (product != null && CartExist != null)
            {
                if (CartExist.Product.ProductId == ProductoID)
                {
                    if (CartExist.Count >= 1)
                    {
                        //actualizar producto existente con nuevos datos
                        CartExist.Count = product.Count - product.Count;
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
            List<ShoppingCart> allProductosShoppinCart = (from x in _context.ShoppingCarts
                          .Include(x => x.User)
                          .Include(x => x.Product)
                          where x.User.Id == userId
                          select x).ToList();

            if (allProductosShoppinCart != null)
            {
                return Ok(allProductosShoppinCart);
            }
            return BadRequest("No hay productos en el carrito");
        }

    }
}
