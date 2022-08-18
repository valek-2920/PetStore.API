using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pet_Store.Domains.Models.InputModels;
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

        [HttpPut]
        [Route("user")]
        public IActionResult UpdateUser([FromBody] UpdateUser model)
        {
            //traemos el usuario
            var OldUser = _unityOfWork.UsersRepository.GetFirstOrDefault(x => x.UserId == model.Id);

            if (ModelState.IsValid)
            {
                //cambiamos los datos viejos del usuario con los datos nuevos
                OldUser.UserId = model.Id;
                OldUser.Name = model.Name;
                OldUser.LastName = model.LastName;
                OldUser.BirthDate = model.BirthDate;
                OldUser.Phone = model.Phone;
                OldUser.Email = model.Email;
                OldUser.Address = model.Address;

                _unityOfWork.UsersRepository.Update(OldUser);
                _unityOfWork.Save();

                return Ok(model);
            }
            return BadRequest("Error al actualizar el usuario");
        }

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
