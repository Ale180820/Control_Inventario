using ControlInventarioModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;

namespace Control_Inventario.Controllers.Registro_de_inventario
{
    [Authorize]
    public class ReportesController : Controller
    {
        public async Task<IActionResult> Reporteria()
        {
            if (validacionRol())
            {
                return RedirectToAction("Login", "Autenticacion");
            }
            var path = "Producto/GetList";
            IEnumerable<Producto> productos = await Functions.APIServices<IEnumerable<Producto>>.Get(path);
            ViewData["Productos"] = productos.Count();

            var pathU = "Usuario/GetList";
            IEnumerable<Usuario> usuarios = await Functions.APIServices<IEnumerable<Usuario>>.Get(pathU);
            ViewData["Usuarios"] = usuarios.Count();
            var pathB = "BitacoraInventarios/GetList";
            IEnumerable<BitacoraInventario> inventario = await Functions.APIServices<IEnumerable<BitacoraInventario>>.Get(pathB);

            var result = productos.Join(inventario,s1 => s1.Id, s2 => s2.ProductoId, (productos,inventario) => new
            {
                id = productos.Id,
                money = productos.PrecioBase * inventario.CantidadInicial
            });
            ViewData["Inversion"] = result.Sum(x => x.money);

            return View(inventario.OrderByDescending(i => i.CantidadActual).Take(10));
        }
        public bool validacionRol()
        {
            var rol = HttpContext.Session.GetString("Rol");
            return (rol != "1" && rol != "3");
        }
    }
}
