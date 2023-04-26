using ControlInventarioModel;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
            Usuario usuarioValidation = await Functions.APIServices<Usuario>.Post<Usuario>(usuario, path);
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
            Token usuario1 = await Functions.APIServices<Usuario>.Post<Token>(usuario, path);
            if (usuario1.TokenGenerate == "")
            {
                return View();
            }
            HttpContext.Session.SetString("Rol", usuario1.Rol.ToString());
            HttpContext.Session.SetString("Nombre", usuario1.Nombre);
            HttpContext.Session.SetString("TokenGenerate", usuario1.TokenGenerate);
            var claims = new List<Claim>
                {
                    new Claim("Nombre", usuario1.Nombre),
                    new Claim("Token", usuario1.TokenGenerate)
                };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> CerrarSesion()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("HomePage", "Home");
        }
    }
}
