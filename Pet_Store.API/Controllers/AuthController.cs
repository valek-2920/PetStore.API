using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pet_Store.API.Services;
using Pet_Store.Domains.Models.AuthModelsForIdentity;
using Pet_Store.Domains.Models.ViewModels;
using PetStore.DataAccess.Repository;
using PetStore.DataAccess.Repository.IRepositories;
using PetStore.DataAccess.Repository.UnityOfWork;
using Project_PetStore.API.DataAccess;
using Project_PetStore.API.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IUserService _userService;
        private readonly IUnityOfWork _unityOfWork;
        IApplicationDbContext Context;
        private IMailService _mailService;
        private IConfiguration _configuration;

        public AuthController(IUserService userService, IUnityOfWork unityOfWork, IApplicationDbContext context, IMailService mailService, IConfiguration configuration)
        {
            _userService = userService;
            _unityOfWork = unityOfWork;
            Context = context;
            _mailService = mailService;
            _configuration = configuration;
        }

        // Para acceder a este API vamos a usar este link --> /api/auth/register-admin
        //Se utiliza para crear un usuario admin ya que se quema el ID del rol
        [HttpPost("Register-admin")]
        public async Task<IActionResult> RegisterAdminAsync([FromBody] RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                Users userCheck =
                Context.Users.Where(w => w.Email == model.Email).FirstOrDefault();
                if (userCheck != null)
                {
                    return BadRequest("Este correo ya esta en uso");
                }
                else
                {
                    Users user = new Users
                    {
                        Name = model.Name,
                        LastName = model.LastName,
                        BirthDate = model.BirthDate,
                        Phone = model.Phone,
                        Email = model.Email,
                        Password = model.Password,
                        RolesId = 1,
                        Address = model.Address

                    };

                    _unityOfWork.UsersRepository.Add(user);
                    _unityOfWork.Save();

                }

                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); //Status code: 200

                return BadRequest(result);
            }

            return BadRequest("Algunas propiedades no son validas"); //Status code: 400
        }

        // Para acceder a este API vamos a usar este link --> /api/auth/register-client
        //Se utiliza para crear un usuario admin ya que se quema el ID del rol
        [HttpPost("Register-client")]
        public async Task<IActionResult> RegisterClientAsync([FromBody] RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                Users userCheck =
                Context.Users.Where(w => w.Email == model.Email).FirstOrDefault();
                if (userCheck != null)
                {
                    return BadRequest("Este correo ya esta en uso");
                }
                else
                {
                    Users user = new Users
                    {
                        Name = model.Name,
                        LastName = model.LastName,
                        BirthDate = model.BirthDate,
                        Phone = model.Phone,
                        Email = model.Email,
                        Password = model.Password,
                        RolesId = 2,
                        Address = model.Address

                    };

                    _unityOfWork.UsersRepository.Add(user);
                    _unityOfWork.Save();

                }

                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); //Status code: 200

                return BadRequest(result);
            }

            return BadRequest("Algunas propiedades no son validas"); //Status code: 400
        }

        // Para acceder a este API vamos a usar este link --> /api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    await _mailService.SendEmailAsync(model.Email, "Nuevo Login Test", "<h1>Se ha logueado satisfactoriamente</h1><p>Nuevo ingreso a su cuenta a las " + DateTime.Now + "</p>");
                    return Ok(result); //Status code: 200
                }

                return BadRequest(result);
            }

            return BadRequest("Algunas propiedades no son validas"); //Status code: 400 para saber si fallo
        }

        // Para acceder a este API vamos a usar este link --> /api/auth/confirmemail?userid&token
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userService.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
            {
                return Redirect($"{_configuration["AppUrl"]}/ConfirmEmail.html");
            }

            return BadRequest(result);
        }

        // Para acceder a este API vamos a usar este link --> api/auth/forgetpassword
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _userService.ForgetPasswordAsync(email);

            if (result.IsSuccess)
                return Ok(result); // status code 200

            return BadRequest(result); // status code 400
        }

        // Para acceder a este API vamos a usar este link --> api/auth/resetpassword
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Algunas propiedades no son validas");
        }

    }
}
