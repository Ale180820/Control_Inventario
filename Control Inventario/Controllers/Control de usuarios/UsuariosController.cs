using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlInventarioModel;

namespace Control_Inventario.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var path = "Usuario/GetList";
            IEnumerable<Usuario> usuarios = await Functions.APIServices<IEnumerable<Usuario>>.Get(path);
            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var path = "Usuario/Get/" + id;
            Usuario usuario = await Functions.APIServices<Usuario>.Get(path);
            return View(usuario);
        }

        // GET: Usuarios/Create
        public async Task<IActionResult> Create()
        {
            var path = "Usuario/GetList";
            IEnumerable<Rol> roles = await Functions.APIServices<IEnumerable<Rol>>.Get(path);
            ViewData["RolId"] = new SelectList(roles, "Id", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            var path = "Usuario/Set";
            Usuario usuario1 = await Functions.APIServices<Usuario>.Post(usuario, path);
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var path = "Usuario/Get/" + id;
            Usuario usuario = await Functions.APIServices<Usuario>.Get(path);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            var path = "Usuario/Put/" + id;
            Usuario result = await Functions.APIServices<Usuario>.Post(usuario, path);
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var path = "Usuario/Get/" + id;
            Usuario usuario = await Functions.APIServices<Usuario>.Get(path);
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var path = "Usuario/Delete/" + id;
            var result = await Functions.APIServices<GeneralResult>.Delete(path);
            return RedirectToAction(nameof(Index));
        }
    }
}
