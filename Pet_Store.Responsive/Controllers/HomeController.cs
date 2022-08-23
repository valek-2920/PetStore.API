using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pet_Store.Domains.Models.ViewModels;
using Pet_Store.Responsive.Services.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pet_Store.Domains.Models.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Pet_Store.Responsive.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IInventarioServices _services;
        readonly IShoppingCartService _shoppingCartService;
       readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger,  IInventarioServices services, IShoppingCartService cart, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _services = services;
            _shoppingCartService = cart;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _services.getProductsAsync();

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registro()
        {

            return View();
        }
        public async Task<IActionResult> Shop()
        {

            var products = await _services.getProductsAsync();

            return View(products);
        }

        public ActionResult About()
        {

            return View();
        }


       //-----------------ShoppingCart------------------------------


        //--Get ShoppingCart
        [HttpGet]
        public async Task<IActionResult> Cart(int userId)
        {
            var ShoppingCart = await _shoppingCartService.GetShoppingCartAsync(2);

            return View(ShoppingCart);
        }



        //--Delete porductos de shoppingCart
        [HttpPost]
        public async Task<IActionResult> EliminarProductosShopingCart(int Userid, int count, int ProductoID)
        {
            var response = await _shoppingCartService.deleteShoppinCartById(Userid, count, ProductoID);
            return RedirectToAction("cart");
        }




        //--Upsert porductos de shoppingCart

        public async Task<IActionResult> UpsertShopping(int? id, int UserId, string product)
        {
            //cambiar el parametro pot UserId
            var Shopping = await _shoppingCartService.GetShoppingcartByUser(2);
            ShoppingCartViewModel viewModel = new ShoppingCartViewModel();
            if (Shopping != null && Shopping.UserId == UserId && Shopping.Product.Name == product)
            {
       
                viewModel.ShoppingCart.Count = 1;
                return View(viewModel);
            }
            else
            {
                //update existing product
                viewModel.ShoppingCart = await _shoppingCartService.GetShoppingcartByUser(UserId);
                return View(viewModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpsertShopping([FromForm] ShoppingCartViewModel model, IFormFile? file)
        {


            if (ModelState.IsValid)
            {
                if (model.Product.ProductId == 0)
                {
                    await _shoppingCartService.AddShoppingCartAsync(model.ShoppingCart);
                }
            }
            return View(model);
        }

        public ActionResult Checkout()
        {

            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }



    }
}
