using ControlInventarioModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Control_Inventario.Controllers.Registro_de_inventario
{
    public class ProductoController : Controller
    {

        // GET: ProductoController
        public async Task<IActionResult> Index()
        {
            var path = "Producto/GetList";
            IEnumerable<Producto> productos = await Functions.APIServices<IEnumerable<Producto>>.Get(path);
            return View(productos);
        }

        // GET: ProductoController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            var path = "Producto/Set";
            Producto movies = await Functions.APIServices<Producto>.Post(producto, path);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var path = "Producto/Get/" + id;
            Producto producto = await Functions.APIServices<Producto>.Get(path);
            return View(producto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if(id != producto.Id)
            {
                return NotFound();
            }

            var path = "Producto/Put/" + id;
            Producto result = await Functions.APIServices<Producto>.Post(producto, path);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var path = "Producto/Get/" + id;
            Producto producto = await Functions.APIServices<Producto>.Get(path);
            return View(producto);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var path = "Producto/Delete/" + id;
            var result = await Functions.APIServices<GeneralResult>.Delete(path);
            return RedirectToAction(nameof(Index));
        }
    }
}
