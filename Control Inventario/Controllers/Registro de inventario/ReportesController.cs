using Microsoft.AspNetCore.Mvc;

namespace Control_Inventario.Controllers.Registro_de_inventario
{
    public class ReportesController : Controller
    {
        public IActionResult Reporteria()
        {
            return View();
        }
    }
}
