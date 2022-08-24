
﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using Pet_Store.Domains.Models.MailModels;
using Pet_Store.Domains.Models.ViewModels;
using Pet_Store.Responsive.Services.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pet_Store.Domains.Models.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace Pet_Store.Responsive.Controllers
{
    //[Route("home")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IInventarioServices _inventarioServices;
        readonly IUserServices _userServices;
        readonly ICheckoutServices _checkoutServices;
        ICartero Cartero;
        readonly UserManager<IdentityUser> _userManager;
        readonly SignInManager<IdentityUser> _sessionManager;
        readonly RoleManager<IdentityRole> _roleManager;

        public HomeController
            (
                ILogger<HomeController> logger,  
                IInventarioServices inventarioServices, IUserServices userServices, ICheckoutServices checkoutServices,
                UserManager<IdentityUser> userManager,
                SignInManager<IdentityUser> sessionManager,
                RoleManager<IdentityRole> roleManager,
                ICartero cartero
            )
        {
            _logger = logger;
            _inventarioServices = inventarioServices;
            _checkoutServices = checkoutServices;
            _userServices = userServices;
            _userManager = userManager;
            _sessionManager = sessionManager;
            _roleManager = roleManager;
            Cartero = cartero;
>>>>>>> bd44ac13bfcf4676afab1fa061c336b73b68b541
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

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            LoginInputModel model =
                new LoginInputModel();
            return View(model);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _sessionManager.PasswordSignInAsync
                        (model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError(string.Empty, "Session could not be started!");
            }

            return View(model);
        }

        [HttpGet]
        [Route("registro")]
        public IActionResult Registro()
        {
            RegisterInputModel model =
                new RegisterInputModel();

            return View(model);
        }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> Registro(RegisterInputModel model)
        {
            if (ModelState.IsValid)
            {
                var email = model.Email;

                Users user =
                   new Users
                   {
                       FirstName = model.FirstName,
                       LastName = model.LastName,
                       Phone = model.Phone,
                       BirthDate = model.BirthDate,
                       Address =  model.Address,
                       UserName = email,
                       Email = email
                   };

                var result =
                    await _userManager.CreateAsync(user, password: model.Password);

                if (result.Succeeded)
                {

                    //Codigo para agregar al user que se esta creando a un rol automaticamente
                    var DefaultRole = _roleManager.FindByNameAsync("Cliente").Result;

                    if (DefaultRole != null)
                    {
                        IdentityResult roleresult = await _userManager.AddToRoleAsync(user, DefaultRole.Name);
                    }

                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _sessionManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [Route("checkemail")]
        public async Task<JsonResult> CheckEmail(string email)
        {
            var user =
                await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }

            return Json($"El correo {email} ya esta siendo utilizado, Intente nuevamente!");
        }

        [HttpGet]
        [Route("forgetpassword")]
        public IActionResult ForgetPassword()
        {
            ForgetPasswordInputModel model =
                new ForgetPasswordInputModel();

            return View(model);
        }


        [HttpPost]
        [Route("forgetpassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordInputModel model)
        {
            if (ModelState.IsValid)
            {
                var email = model.Email;

                var user =
                    await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var token =
                        await _userManager.GeneratePasswordResetTokenAsync(user);

                    var resetPasswordUrl =
                        string.Concat
                            (
                                Url.Action(nameof(ResetPassword)),
                                "?email=",
                                WebUtility.UrlEncode(email),
                                "&token=",
                                WebUtility.UrlEncode(token)
                            );

                    Cartero.Enviar
                        (
                            new CorreoElectronico
                            {
                                Destinatario = email,
                                Asunto = "Reestablece tu contraseña en nuestra PetStore ",
                                Cuerpo = "Haga click en el siguiente link para reestablecer su contraseña:  " + "http://localhost:64086" + resetPasswordUrl,
                            }
                        );
                }

                return View(nameof(ForgetPasswordConfirmation));
            }

            return View(model);
        }



        [Route("forgetpasswordconfirmation")]
        public IActionResult ForgetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [Route("resetpassword")]
        //[AllowAnonymous]
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError
                    (string.Empty, "Email and Token are required!");
            }

            return View();
        }

        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordInputModel model)
        {
            if (ModelState.IsValid)
            {
                var user =
                    await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    RedirectToAction("index", "home");
                }

                var result =
                    await _userManager.ResetPasswordAsync
                        (user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ResetPasswordConfirmation));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        [Route("resetpasswordconfirmation")]
        public IActionResult ResetPasswordConfirmation()
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


       //-----------------ShoppingCart------------------------------

        //--Get ShoppingCart
        [HttpGet]
        public async Task<IActionResult> Cart(int userId)
        {
            var ShoppingCart = await _shoppingCartService.GetShoppingCartAsync(userId);

            return View(ShoppingCart);
        }


        //--Delete porductos de shoppingCart
        [HttpPost]
        public async Task<IActionResult> EliminarProductosShopingCart(int Userid, int count, int ProductoID)
        {
            var response = await _shoppingCartService.deleteShoppinCartById(Userid, count, ProductoID);
            return RedirectToAction("cart");
        }

        [HttpPost]
        public async Task<IActionResult> addProductsToCart(int UserId, int ProductId)
            {

            if (ModelState.IsValid)
            {
                ShoppingCart model = new()
                {
                    ProductId = ProductId,
                    UserId = UserId
                };

                await _shoppingCartService.AddShoppingCartAsync(model);
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        public ActionResult Checkout()

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
