using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlInventarioModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Control_Inventario.Controllers
{
    
    public class UbicacionsController : Controller
    {
        [Authorize]
        // GET: Ubicacions
        public async Task<IActionResult> Index()
        {
            var path = "Ubicacions/GetList";
            IEnumerable<Ubicacion> ubicaciones = await Functions.APIServices<IEnumerable<Ubicacion>>.Get(path);
            return View(ubicaciones);
        }

        // GET: Ubicacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var path = "Ubicacions/Get/" + id;
            Ubicacion Ubicacion = await Functions.APIServices<Ubicacion>.Get(path);
            return View(Ubicacion);
        }

        // GET: Ubicacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ubicacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ubicacion ubicacion)
        {
            var path = "Ubicacions/Set";
            Ubicacion Ubicacion1 = await Functions.APIServices<Ubicacion>.Post(ubicacion, path);
            return RedirectToAction(nameof(Index));
        }

        // GET: Ubicacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var path = "Ubicacions/Get/" + id;
            Ubicacion Ubicacion = await Functions.APIServices<Ubicacion>.Get(path);
            return View(Ubicacion);
        }

        // POST: Ubicacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ubicacion ubicacion)
        {
            if (id != ubicacion.Id)
            {
                return NotFound();
            }

            var path = "Ubicacions/Put/" + id;
            Ubicacion result = await Functions.APIServices<Ubicacion>.Post(ubicacion, path);
            return RedirectToAction(nameof(Index));
        }

        // GET: Ubicacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var path = "Ubicacions/Get/" + id;
            Ubicacion Ubicacion = await Functions.APIServices<Ubicacion>.Get(path);
            return View(Ubicacion);
        }

        // POST: Ubicacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var path = "Ubicacions/Delete/" + id;
            var result = await Functions.APIServices<GeneralResult>.Delete(path);
            return RedirectToAction(nameof(Index));
        }
    }
}
