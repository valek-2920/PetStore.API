using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Pet_Store.Domains.Models.Enum.Enum;

namespace Pet_Store.Responsive.Controllers
{
    public class AlertController : BaseController
    {
        [HttpGet]
    public ActionResult ShowAlert()
    {
        Alert(" La acción se realizó con éxito", NotificationType.success);
        return View();
    }
}
}
