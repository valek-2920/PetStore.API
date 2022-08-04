﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pet_Store.Responsive.Models;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
        public ActionResult Shop()
        {

            return View();
        }

        public ActionResult About()
        {

            return View();
        }
        public ActionResult Cart()
        {

            return View();
        }
        public ActionResult Contact()
        {

            return View();
        }
    }
}
