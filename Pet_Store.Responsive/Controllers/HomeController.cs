using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pet_Store.Domains.Models.ViewModels;
using Pet_Store.Responsive.Services.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Controllers
{
    public class HomeController : Controller
    {
        readonly ILogger<HomeController> _logger;
        readonly IInventarioServices _inventarioServices;
        readonly ICheckoutServices _checkoutServices;
        readonly IUserServices _userServices;


        public HomeController(
            ILogger<HomeController> logger,  
            IInventarioServices inventarioServices, 
            ICheckoutServices checkoutServices,
            IUserServices userServices)
        {
            _logger = logger;
            _inventarioServices = inventarioServices;
            _checkoutServices = checkoutServices;
            _userServices = userServices;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _inventarioServices.getProductsAsync();

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

            var products = await _inventarioServices.getProductsAsync();

            return View(products);
        }

        public ActionResult About()
        {

            return View();
        }
        public ActionResult Cart()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(int id)
        {

            var user = await _userServices.getUserById(id);
            var orderDetails = await _checkoutServices.getOrderByUserAsync(id);
            var products = await _checkoutServices.getOrderProductsAsync(id);

            if(user != null)
            {

                OrderPaymentViewModel viewModel = new()
                {
                    payments = new(),
                    orderHeader = new(),
                    order = orderDetails,
                    products = products
                };

                return View(viewModel);

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderPaymentViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                await _checkoutServices.addProductAsync(viewModel.orderHeader);
                await _checkoutServices.addPaymentAsync(viewModel.payments);

                return RedirectToAction("Index");
            }
            //var errors = ModelState
            //.Where(x => x.Value.Errors.Count > 0)
            //.Select(x => new { x.Key, x.Value.Errors })
            //.ToArray();
            return View(viewModel);
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}
