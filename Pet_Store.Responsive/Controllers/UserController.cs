using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Responsive.Services.IServices;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Controllers
{
    public class UserController : Controller
    {

        readonly IConfiguration _configuration;
        readonly IUserServices _services;

        public UserController(IConfiguration configuration, IUserServices services)
        {
            _configuration = configuration;
            _services = services;
        }

        public async Task<IActionResult> Users()
        {

            var users = await _services.getUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Upsert(int? id)
        {

            Users model = new();
           
            if (id == null || id == 0)
            {
                //insert new product
                return View(model);
            }
            else
            {
                //update existing product
                model = await _services.getUserById( (int) id);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Users model)
        {

            if (ModelState.IsValid)
            {

                if (model.Id == null)
                {
                    //add
                    await _services.addUserAsync(model);
                }
                else
                {
                    //update
                    await _services.updateUserById(model);

                }
                return RedirectToAction("Inventario");

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarUsuarios(int id)
        {
            var response = await _services.deleteUserById(id);
            ViewBag.response = response;
            return RedirectToAction("Clientes");

        }


        public IActionResult Roles()
        {
            return View();
        }
        public IActionResult AgregarRol()
        {
            return View();
        }
    }
}
