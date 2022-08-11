using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pet_Store.Domains.Models.AuthModelsForIdentity;
using Pet_Store.Domains.Models.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.API.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);

        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);

        Task<UserManagerResponse> ForgetPasswordAsync(string email);

        Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);

    }

    public class UserService : IUserService
    {

        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;
        private IMailService _mailService;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration, IMailService mailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
        }

        //Metodo que se utiliza para registrar un usuario admin
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("El modelo registrado es nulo");

            //Confirma que al registrar ambas passwords sean identicas
            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Las contraseñas que ingreso no son iguales",
                    IsSuccess = false,
                };


            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,

            };


            var result = await _userManager.CreateAsync(identityUser, model.Password);

            //Esto se ejecuta en caso de que toda la informacion solicitada este correcta
            if (result.Succeeded)
            {
                //Generar token para confirmar correo
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["AppUrl"]}/api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

                await _mailService.SendEmailAsync(identityUser.Email, "Confirme su correo", "<h1>Bienvenido a nuestro Pet Store!</h1>" +
                    $"<p>Debe confirmar su correo para poder comenzar a comprar <a href='{url}''> haciendo click aqui </a></p>"); 
                

                //Por hacer: enviar un correo de confirmacion
                return new UserManagerResponse
                {
                    Message = "Usuario creado exitosamente",
                    IsSuccess = true,
                };
            }

            //Esto se ejecuta en caso de que haya un error y no se cree el usuario, guarda la descripcion del error en un campo "errors"
            return new UserManagerResponse
            {
                Message = "El usuario no se creo",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };


        }

        //Metodo que se utiliza para loguear un usuario
        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            //Validacion  para ver si existe un usuario con el correo que se ingresa
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "No hay un usuario creado con ese correo",
                    IsSuccess = false,
                };
            }


            //Validacion para verificar si la contraseña ingresada es correcta
            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "Contraseña Incorrecta",
                    IsSuccess = false,
                };

            //Se guardan parametros como "claims" en caso de que vayan a ser utilizados en un futuro
            var claims = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            //Generacion del token para confirmar el logueo
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };

        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            //Busca el user por id y valida si existe o no  
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "ECorreo confirmado con exito!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "El correo no ha sido confirmado",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No hay un usuario asociado a este correo",
                };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["AppUrl"]}/ResetPassword?email={email}&token={validToken}";

            await _mailService.SendEmailAsync(email, "Reestablecer contraseña", "<h1>Siga las instrucciones para reestabler su contraseña</h1>" +
                $"<p>Para reestablecer su contraseña <a href='{url}'>Haga click aqui</a></p>");

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "El URL para reestablecer la contraseña se ha enviado exitosamente al correo!"
            };
        }

        public async Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No hay un usuario asociado a este correo",
                };

            if (model.NewPassword != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Las contraseñas no coinciden",
                };

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "La contraseña se ha modificado correctamente!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                Message = "Algo salio mal",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }


    }
}
