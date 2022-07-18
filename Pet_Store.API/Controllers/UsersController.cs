using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetStore.DataAccess.Repository.UnityOfWork;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork;

        public UsersController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {
            var allUsers = _unityOfWork.UsersRepository.GetAll();
            _unityOfWork.Save();

            if (allUsers != null)
            {
                return Ok(allUsers);
            }
            return BadRequest("No existen usuarios");
        }

        [HttpGet]
        [Route("user")]
        public IActionResult GetUser(int id)
        {
            var User = _unityOfWork.UsersRepository.GetFirstOrDefault(x => x.UserId == id);
            _unityOfWork.Save();

            if (User != null)
            {
                return Ok(User);
            }
            return BadRequest("El usuario solicitado no existe");
        }

        //arreglar, tienes que usar el updateUsers models como parametro y luego usalo para actualizar el Users model
        //[HttpPut]
        //[Route("UserUpdate")]
        //public IActionResult UpdateUser([FromBody] Users model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _unityOfWork.UsersRepository.Update(model);
        //        _unityOfWork.Save();

        //        return Ok(model);
        //    }
        //    return BadRequest("Error al actualizar el usuario");
        //}

        [HttpDelete]
        [Route("user")]
        public IActionResult DeleteUser(int id)
        {
            var User = _unityOfWork.UsersRepository.GetFirstOrDefault(x => x.UserId == id);

            if (User != null)
            {
                _unityOfWork.UsersRepository.Remove(User);
                _unityOfWork.Save();

                return Ok($"El usuario ha sido eliminado");
            }
            return BadRequest($"No existe usuario con Id: {id}");
        }
    }
}
