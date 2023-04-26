using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MySqlX.XDevAPI.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<ControlInventarioModel.Usuario>> GetList()
        {
            InventarioContext _context = new InventarioContext();
            IEnumerable<ControlInventarioModel.Usuario> usuarios = await _context.Usuarios.Include(c => c.Rol).Select(m => new ControlInventarioModel.Usuario
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Apellido = m.Apellido,
                Email = m.Email,
                Contrasena = m.Contrasena,
                FechaEgreso = m.FechaEgreso,
                FechaIngreso = m.FechaIngreso,
                RolId = m.RolId,
                Rol = m.Rol.Nombre
            }).ToListAsync();
            return usuarios;
        }

        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ControlInventarioModel.Usuario> Get(int id)
        {
            InventarioContext _context = new InventarioContext();
            ControlInventarioModel.Usuario usuario = await _context.Usuarios.Select(m => new ControlInventarioModel.Usuario
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Apellido = m.Apellido,
                Email = m.Email,
                Contrasena = m.Contrasena,
                FechaEgreso = m.FechaEgreso,
                FechaIngreso = m.FechaIngreso,
                RolId = m.RolId
            }).Where(m => m.Id == id).FirstOrDefaultAsync();
            return usuario;
        }

        [Route("Put/{id}")]
        [HttpPost]
        public async Task<ControlInventarioModel.Usuario> Put(int id, ControlInventarioModel.Usuario usuario)
        {
            InventarioContext _context = new InventarioContext();
            Models.Usuario usuarios = _context.Usuarios.FirstOrDefault(s => s.Id == id);
            if (usuarios != null)
            {
                usuarios.Nombre = usuario.Nombre;
                usuarios.Apellido = usuario.Apellido;
                usuarios.Email = usuario.Email;
                usuarios.Contrasena = usuario.Contrasena;
                usuarios.FechaEgreso = usuario.FechaEgreso;
                usuarios.RolId = usuario.RolId;
                _context.Usuarios.Update(usuarios);
                await _context.SaveChangesAsync();
            }
            return usuario;

        }

        [Route("Set")]
        [HttpPost]
        public async Task<ControlInventarioModel.Usuario> Set(ControlInventarioModel.Usuario usuario)
        {
            InventarioContext _context = new InventarioContext();
            Models.Usuario usuario_Change = new Models.Usuario
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Contrasena = usuario.Contrasena,
                FechaIngreso = usuario.FechaIngreso,
                RolId = usuario.RolId
            };
            _context.Usuarios.Add(usuario_Change);
            await _context.SaveChangesAsync();
            usuario.Id = usuario_Change.Id;
            return usuario;
        }
        [Route("Autentication")]
        [HttpPost]
        public async Task<ControlInventarioModel.Token> Autentication(ControlInventarioModel.Usuario usuario)
        {
            InventarioContext _context = new InventarioContext();
            var findUser = await _context.Usuarios.Where(u => u.Email == usuario.Email && u.Contrasena == usuario.Contrasena).FirstOrDefaultAsync();
            if (findUser != null)
            {
                return new ControlInventarioModel.Token
                {
                    Id = findUser.Id,
                    Nombre = findUser.Nombre,
                    Rol = findUser.RolId,
                    TokenGenerate = CustomTokenJWT(findUser.Nombre)
                };
            }
            return new ControlInventarioModel.Token
            {
                Id = 0,
                Nombre = "",
                Rol = 0,
                TokenGenerate = ""
            };

        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ControlInventarioModel.GeneralResult> Delete(int id)
        {
            var result = new ControlInventarioModel.GeneralResult
            {
                Result = false
            };
            try
            {
                InventarioContext _context = new InventarioContext();
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario != null)
                {
                    _context.Usuarios.Remove(usuario);
                }
                await _context.SaveChangesAsync();
                result.Result = true;
                return (result);
            }
            catch (Exception)
            {
                return (result);
                throw;
            }
        }

        private string CustomTokenJWT(string Nombre)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!)
            );
            var _signingCredentials = new SigningCredentials(
                _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
            );
            var _Header = new JwtHeader(_signingCredentials);
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, Nombre)
            };
            var _Payload = new JwtPayload(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: _Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(2)
            );
            var _Token = new JwtSecurityToken(_Header, _Payload);
            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}
