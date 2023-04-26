using ControlInventarioModel;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Control_Inventario.Controllers.Control_de_usuarios
{
    public class AutenticacionController : Controller
    {

        // GET: AutenticacionController/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string Name, string Lastname, string Email, string Password)
        {
            var path = "Usuario/Set";
            var encriptPass = Cripto.EncodePasswordToBase64(Password);
            Usuario usuario = new Usuario
            {
                Nombre = Name,
                Apellido = Lastname,
                Email = Email,
                Contrasena = encriptPass,
                FechaIngreso = DateTime.Now,
                RolId = 4
            };
            Usuario usuarioValidation = await Functions.APIServices<Usuario>.Post(usuario, path);
            if (usuarioValidation != null)
            {
                return View("Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            var encriptPass = Cripto.EncodePasswordToBase64(Password);
            Usuario usuario = new Usuario
            {
                Nombre = "",
                Apellido = "",
                Email = Email,
                Contrasena = encriptPass,
                RolId = 0
            };
            var path = "Usuario/Autentication";
            Usuario usuario1 = await Functions.APIServices<Usuario>.Post(usuario, path);
            HttpContext.Session.SetString("Rol", usuario1.RolId.ToString());
            if (usuario1 != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
