using Microsoft.AspNetCore.Mvc;

namespace Pet_Store.Responsive.Controllers
{
    public class InventarioController : Controller
    {
        public IActionResult Inventario()
        {
            return View();
        }
    }
}
