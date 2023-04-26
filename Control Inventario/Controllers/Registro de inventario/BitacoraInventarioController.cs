using ControlInventarioModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace Control_Inventario.Controllers.Registro_de_inventario
{
  
    public class BitacoraInventarioController : Controller
    {
        // GET: BitacoraInventarioController
        public async Task<IActionResult> Index()
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "1" && rol != "3")
            {
                return RedirectToAction("Login","Autenticacion");
            }
            var path = "BitacoraInventarios/GetList";
            IEnumerable<BitacoraInventario> inventario = await Functions.APIServices<IEnumerable<BitacoraInventario>>.Get(path);
            return View(inventario);
        }
        // GET: BitacoraInventarioController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "1" && rol != "3")
            {
                return RedirectToAction("Login", "Autenticacion");
            }
            var path = "BitacoraInventarios/Get/" + id;
            BitacoraInventario inventario = await Functions.APIServices<BitacoraInventario>.Get(path);
            return View(inventario);
        }

        // GET: BitacoraInventarioController/Create
        public async Task<IActionResult> Create()
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "1" && rol != "3")
            {
                return RedirectToAction("Login", "Autenticacion");
            }
            var path = "Producto/GetList";
            IEnumerable<Producto> productos = await Functions.APIServices<IEnumerable<Producto>>.Get(path);
            ViewData["ProductoId"] = new SelectList(productos, "Id", "Nombre");
            var pathU = "Ubicacions/GetList";
            IEnumerable<Ubicacion> ubicaciones = await Functions.APIServices<IEnumerable<Ubicacion>>.Get(pathU);
            ViewData["UbicacionId"] = new SelectList(ubicaciones, "Id", "NoGondola");
            return View();
        }

        // POST: BitacoraInventarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BitacoraInventario inventario)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "1" && rol != "3")
            {
                return RedirectToAction("Login", "Autenticacion");
            }
            var path = "BitacoraInventarios/Set";
            inventario.FechaModificacion = DateTime.Now;
            inventario.CantidadActual = inventario.CantidadInicial;
            BitacoraInventario inventarioNuevo = await Functions.APIServices<BitacoraInventario>.Post(inventario, path);

            return RedirectToAction(nameof(Index));
        }

        // GET: BitacoraInventarioController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "1" && rol != "3")
            {
                return RedirectToAction("Login", "Autenticacion");
            }
            var path = "BitacoraInventarios/Get/" + id;
            BitacoraInventario inventario = await Functions.APIServices<BitacoraInventario>.Get(path);
            var pathP = "Producto/GetList";
            IEnumerable<Producto> productos = await Functions.APIServices<IEnumerable<Producto>>.Get(pathP);
            ViewData["ProductoId"] = new SelectList(productos, "Id", "Nombre");
            var pathU = "Ubicacions/GetList";
            IEnumerable<Ubicacion> ubicaciones = await Functions.APIServices<IEnumerable<Ubicacion>>.Get(pathU);
            ViewData["UbicacionId"] = new SelectList(ubicaciones, "Id", "NoGondola");
            return View(inventario);
        }

        // POST: BitacoraInventarioController/Edit/5
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BitacoraInventario inventario)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "1" && rol != "3")
            {
                return RedirectToAction("Login", "Autenticacion");
            }
            if (id != inventario.Id)
            {
                return NotFound();
            }
            inventario.FechaModificacion = DateTime.Now;
            var path = "BitacoraInventarios/Put/" + id;
            BitacoraInventario result = await Functions.APIServices<BitacoraInventario>.Post(inventario, path);
            return RedirectToAction(nameof(Index));
        }

        // GET: BitacoraInventarioController/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var path = "BitacoraInventarios/Get/" + id;
            BitacoraInventario inventario = await Functions.APIServices<BitacoraInventario>.Get(path);
            return View(inventario);
        }

        // POST: BitacoraInventarioController/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var path = "BitacoraInventarios/Delete/" + id;
            var result = await Functions.APIServices<GeneralResult>.Delete(path);
            return RedirectToAction(nameof(Index));
        }


        //CATALOGO
        public async Task<IActionResult> Catalogo()
        {
            var path = "BitacoraInventarios/GetList";
            IEnumerable<BitacoraInventario> inventario = await Functions.APIServices<IEnumerable<BitacoraInventario>>.Get(path);
            return View(inventario);
        }

        public async Task<IActionResult> DetalleCatalogo(int? id)
        {
            var path = "Producto/Get/" + id;
            Producto producto = await Functions.APIServices<Producto>.Get(path);
            return View(producto);
        }
    }
}
