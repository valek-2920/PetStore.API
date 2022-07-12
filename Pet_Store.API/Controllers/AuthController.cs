using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pet_Store.API.Services;
using Pet_Store.Shared;
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

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // Para acceder a este API vamos a usar este link --> /api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterViewModel model)
        {
            


            if (ModelState.IsValid)
            {
                Users inputModel = model.user;
                //Como es un cliente aqui quemo el valor del rol para que se agregue correctamente
                model.user.RolesId = 2;

                //IMPORTANTE: de momento quemo tambien la direccion ya que solo existe una, despues hay que ver como se maneja esto ya que cada usuario digitara su propia direccion
                model.user.DirectionId = 1;

                //if (model.user.UserId == 0)
                //{
                //    Repository.Insertar(estudiante);
                //    Cartero.Enviar
                //    (
                //        new CorreoElectronico
                //        {
                //            Destinatario = estudiante.CorreoElectronico,
                //            Asunto = "Bienvenido a la Universidad Fidelitas",
                //            Cuerpo = "Se ha creado una cuenta para su persona con el correo electronico al que se ha enviado esta notificacion"
                //        }
                //    );
                //}
                //else
                //{
                //    Repository.Actualizar(estudiante);
                //}




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
                    return Ok(result); //Status code: 200 para saber si funciono

                return BadRequest(result);
            }

            return BadRequest("Algunas propiedades no son validas"); //Status code: 400 para saber si fallo
        }
    }
}
