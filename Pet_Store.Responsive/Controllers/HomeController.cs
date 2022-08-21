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
        private readonly ILogger<HomeController> _logger;
        readonly IInventarioServices _services;

        public HomeController(ILogger<HomeController> logger,  IInventarioServices services)
        {
            _logger = logger;
            _services = services;
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
        public ActionResult Cart()
        {

            return View();
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
