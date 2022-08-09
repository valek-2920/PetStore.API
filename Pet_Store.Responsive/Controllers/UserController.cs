using Microsoft.AspNetCore.Mvc;

namespace Pet_Store.Responsive.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Clientes()
        {
            return View();
        }
        public IActionResult Empleados()
        {
            return View();
        }
        public IActionResult AgregarUsuarios()
        {
            return View();
        }
        public IActionResult EditarUsuarios()
        {
            return View();
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
