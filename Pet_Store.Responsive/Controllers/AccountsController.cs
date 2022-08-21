using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using Pet_Store.Domains.Models.MailModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Controllers
{
    
    public class AccountsController : Controller
    {
        public AccountsController
            (

                UserManager<IdentityUser> userManager,
                SignInManager<IdentityUser> sessionManager,
                RoleManager<IdentityRole> roleManager,
                ICartero cartero
            )
        {
            _userManager = userManager;
            _sessionManager = sessionManager;
            _roleManager = roleManager;
            Cartero = cartero;
        }

        ICartero Cartero;
        readonly UserManager<IdentityUser> _userManager;
        readonly SignInManager<IdentityUser> _sessionManager;
        readonly RoleManager<IdentityRole> _roleManager;

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            RegisterInputModel model =
                new RegisterInputModel();

            return View(model);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterInputModel model)
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
                        UserName = email,
                        Email = email
                    };

                var result =
                    await _userManager.CreateAsync(user, password: model.Password);

                if (result.Succeeded)
                {

                    //Codigo para agregar al user que se esta creando a un rol automaticamente
                    //var DefaultRole = _roleManager.FindByNameAsync("Cliente").Result;

                    //if (DefaultRole != null)
                    //{
                    //    IdentityResult roleresult = await _userManager.AddToRoleAsync(user, DefaultRole.Name);
                    //    //await _sessionManager.SignInAsync(user, isPersistent: false);
                    //}

                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        //[HttpGet]
        //[Route("login")]
        //public IActionResult Login()
        //{
        //    LoginInputModel model =
        //        new LoginInputModel();
        //    return View(model);
        //}

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login(LoginInputModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result =
        //            await _sessionManager.PasswordSignInAsync
        //                (model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("index", "home");
        //        }

        //        ModelState.AddModelError(string.Empty, "Session could not be started!");
        //    }

        //    return View(model);
        //}

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _sessionManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


        [Route("checkemail")]
        //[AllowAnonymous]
        public async Task<JsonResult> CheckEmail(string email)
        {
            var user =
                await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }

            return Json($"Email {email} already exists, Please try again with a different email!");
        }

        //[HttpGet]
        //[Route("forgetpassword")]
        //public IActionResult ForgetPassword()
        //{
        //    ForgetPasswordInputModel model =
        //        new ForgetPasswordInputModel();

        //    return View(model);
        //}


        //[HttpPost]
        //[Route("forgetpassword")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ForgetPassword(ForgetPasswordInputModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var email = model.Email;

        //        var user =
        //            await _userManager.FindByEmailAsync(email);

        //        if (user != null)
        //        {
        //            var token =
        //                await _userManager.GeneratePasswordResetTokenAsync(user);

        //            var resetPasswordUrl =
        //                string.Concat
        //                    (
        //                        Url.Action(nameof(ResetPassword)),
        //                        "?email=",
        //                        WebUtility.UrlEncode(email),
        //                        "&token=",
        //                        WebUtility.UrlEncode(token)
        //                    );

        //            Cartero.Enviar
        //                (
        //                    new CorreoElectronico
        //                    {
        //                        Destinatario = email,
        //                        Asunto = "Passord reset for your rent-a-car account",
        //                        Cuerpo = "Please click in the following URL to reset your password:  " + "http://localhost:21879" + resetPasswordUrl,
        //                    }
        //                );
        //        }

        //        return View(nameof(ForgetPasswordConfirmation));
        //    }

        //    return View(model);
        //}



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

        //[HttpPost]
        //[Route("resetpassword")]
        ////[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword(ResetPasswordInputModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user =
        //            await _userManager.FindByEmailAsync(model.Email);

        //        if (user == null)
        //        {
        //            RedirectToAction("index", "home");
        //        }

        //        var result =
        //            await _userManager.ResetPasswordAsync
        //                (user, model.Token, model.Password);

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction(nameof(ResetPasswordConfirmation));
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }

        //    return View(model);
        //}


        [Route("resetpasswordconfirmation")]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
