using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pet_Store.Shared;
using System;
using System.Collections.Generic;
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

    }

    public class UserService : IUserService
    {

        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        //Metodo que se utiliza para registrar un usuario
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

            if(user == null)
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


    }
}
