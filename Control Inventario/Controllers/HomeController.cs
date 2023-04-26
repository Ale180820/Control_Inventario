using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Control_Inventario.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var rol = HttpContext.Session.GetString("Rol");
            ViewBag.Rol = rol;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            return View();
        }
        public IActionResult Contacto()
        {
            if (ViewBag.Result == null)
            {
                ViewBag.Result = "";
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contacto(string email, string phone, string name, string message)
        {
            var emailMessage = new MailMessage();
            emailMessage.To.Add(new MailAddress(email)); // replace with receiver's email id     
            emailMessage.From = new MailAddress("ayalrete@hotmail.com"); // replace with sender's email id     
            emailMessage.Subject = "Comentario o sugerencia de: " + name;
           
            string body = "<html><body><h1>¡Gracias por tu comentario o sugerencia!</h1>" +
                          "<h3>Tomamos en cuenta todas las sugerencias y las utilizamos para mejorar nuestra oferta y proporcionar una experiencia aún mejor para todos nuestros clientes.</h3>" +
                          "<p><strong>Nombre:</strong> " + name + "</p><p><strong>Correo electrónico:</strong>" + email + "</p><p><strong>Teléfono:</strong>" + phone + "</p><p><strong>Mensaje:</strong>" + message + "</p>" +
                          "</body></html>";
            emailMessage.IsBodyHtml = true;
            emailMessage.Body = body;
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "ayalrete@hotmail.com", // replace with sender's email id     
                    Password = "Aylinne18#outlook" // replace with password     
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(emailMessage);
                ViewBag.Result = "Enviado";
                return View();
            }
        }

    }
}