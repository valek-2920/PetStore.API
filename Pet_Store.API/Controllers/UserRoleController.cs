using Microsoft.AspNetCore.Mvc;
using PetStore.DataAccess.Repository.UnityOfWork;
using Project_PetStore.API.Models.DataModels;

namespace Pet_Store.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UserRoleController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPut]
        [Route("UserRoleUpdate")]
        public IActionResult UpdateUserRole([FromBody] UserRoles model)
        {

            if (ModelState.IsValid)
            {
                _unityOfWork.UserRoleRepository.Update(model);
                _unityOfWork.Save();

                return Ok(model);
            }
            return BadRequest("Error al actualizar el rol del usuario");
        }
    }
}
