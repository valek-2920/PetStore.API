using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pet_Store.DataAcess.Data;
using Pet_Store.DataAcess.Repository.UnitOfWork;
using Pet_Store.DataAcess.Repository;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<Users> _usersRepository;

        public UsersController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _usersRepository = _unitOfWork.Repository<Users>();
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {
            var allUsers = _usersRepository.GetAll();
            _unitOfWork.Save();

            if (allUsers != null)
            {
                return Ok(allUsers);
            }
            return BadRequest("No existen usuarios");
        }

        [HttpGet]
        [Route("user")]
        public IActionResult GetUser(string id)
        {
            var User = _usersRepository.GetFirstOrDefault(x => x.Id == id);
            _unitOfWork.Save();

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
            var OldUser = _usersRepository.GetFirstOrDefault(x => x.Id == model.Id);

            if (ModelState.IsValid)
            {
                //cambiamos los datos viejos del usuario con los datos nuevos
                OldUser.Id = model.Id;
                OldUser.FirstName = model.Name;
                OldUser.LastName = model.LastName;
                OldUser.BirthDate = model.BirthDate;
                OldUser.Phone = model.Phone;
                OldUser.Email = model.Email;
                OldUser.Address = model.Address;

                _usersRepository.Update(OldUser);
                _unitOfWork.Save();

                return Ok(model);
            }
            return BadRequest("Error al actualizar el usuario");
        }

        [HttpDelete]
        [Route("user")]
        public IActionResult DeleteUser(string id)
        {
            var User = _usersRepository.GetFirstOrDefault(x => x.Id == id);

            if (User != null)
            {
                _usersRepository.Remove(User);
                _unitOfWork.Save();

                return Ok($"El usuario ha sido eliminado");
            }
            return BadRequest($"No existe usuario con Id: {id}");
        }
    }
}
