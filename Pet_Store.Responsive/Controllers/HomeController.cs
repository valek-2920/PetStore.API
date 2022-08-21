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
            var ShoppingCart = await _shoppingCartService.GetShoppingCartAsync(7);

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

        public async Task<IActionResult> UpsertShopping(int? id, int userId)
        {
            var Cart = await _shoppingCartService.GetShoppingCartAsync(userId);

            ShoppingCartViewModel viewModel = new()
            {
                Product = new(),


                //Shoppping = Cart.Select(i => new SelectListItem
                //{
                //    Text = i.Description,
                //    Value = i.CategoryId.ToString()
                //}),
            };

            if (id == null || id == 0)
            {
                //insert new product
                return View(viewModel);
            }
            else
            {
                //update existing product
                viewModel.Product = await _services.getProductById((int)id);
                return View(viewModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpsertShopping([FromForm] ShoppingCartViewModel model, IFormFile? file)
        {


            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"img\productos");
                    var extension = Path.GetExtension(file.FileName);

            return View();
        }
        public ActionResult Checkout()
        {

            return View();
        }
        public ActionResult Contact()
        {


                    if (model.Product.Files != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, model.Product.Files.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    model.Product.Files = @"\images\products\" + fileName + extension;

                }
                if (model.Product.ProductId == 0)
                {
                    //add
                    await _shoppingCartService.AddShoppingCartAsync(model.ShoppingCart);
                }
                else
                {
                    //update
                    await _shoppingCartService.AddShoppingCartAsync(model.ShoppingCart);

                }
                return RedirectToAction("Index");
            }
            return View(model);
        }



    }
}
