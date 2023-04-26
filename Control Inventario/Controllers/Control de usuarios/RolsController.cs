using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Control_Inventario.Controllers
{
    [Authorize]
    public class RolsController : Controller
    {

        // GET: Rols
        public async Task<IActionResult> Index()
        {
            var path = "Rol/GetList";
            IEnumerable<ControlInventarioModel.Rol> Rols = await Functions.APIServices<IEnumerable<ControlInventarioModel.Rol>>.Get(path);
            return View(Rols);
        }

        // GET: Rols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var path = "Rol/Get/" + id;
            ControlInventarioModel.Rol Rol = await Functions.APIServices<ControlInventarioModel.Rol>.Get(path);
            return View(Rol);
        }

        // GET: Rols/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ControlInventarioModel.Rol rol)
        {
            var path = "Rol/Set";
            ControlInventarioModel.Rol Rol1 = await Functions.APIServices<ControlInventarioModel.Rol>.Post(rol, path);
            return RedirectToAction(nameof(Index));
        }

        // GET: Rols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var path = "Rol/Get/" + id;
            ControlInventarioModel.Rol Rol = await Functions.APIServices<ControlInventarioModel.Rol>.Get(path);
            return View(Rol);
        }

        // POST: Rols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ControlInventarioModel.Rol rol)
        {
            if (id != rol.Id)
            {
                return NotFound();
            }

            var path = "Rol/Put/" + id;
            ControlInventarioModel.Rol result = await Functions.APIServices<ControlInventarioModel.Rol>.Post(rol, path);
            return RedirectToAction(nameof(Index));
        }

        // GET: Rols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var path = "Rol/Get/" + id;
            ControlInventarioModel.Rol Rol = await Functions.APIServices<ControlInventarioModel.Rol>.Get(path);
            return View(Rol);
        }

        // POST: Rols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var path = "Rol/Delete/" + id;
            var result = await Functions.APIServices<ControlInventarioModel.GeneralResult>.Delete(path);
            return RedirectToAction(nameof(Index));
        }
    }
}
