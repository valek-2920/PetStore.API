using Microsoft.AspNetCore.Mvc;

namespace Pet_Store.Responsive.Controllers
{
    public class InventarioController : Controller
    {
        public IActionResult Inventario()
        {
            return View();
        }
        public IActionResult AgregarProducto()
        {
            return View();
        }
        public IActionResult EditarProducto()
        {
            return View();
        }
        public IActionResult Categorias()
        {
            return View();
        }
        public IActionResult AgregarCategorias()
        {
            return View();
        }
    }
}
