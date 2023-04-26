using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MySqlX.XDevAPI.Common;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
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
                usuarios.Id = usuario.Id;
                usuarios.Nombre = usuario.Nombre;
                usuarios.Apellido = usuario.Apellido;
                usuarios.Email = usuario.Email;
                usuarios.Contrasena = usuario.Contrasena;
                usuarios.FechaEgreso = usuario.FechaEgreso;
                usuarios.FechaIngreso = usuario.FechaIngreso;
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
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Contrasena = usuario.Contrasena,
                FechaEgreso = usuario.FechaEgreso,
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
        public async Task<ControlInventarioModel.Usuario> Autentication(ControlInventarioModel.Usuario usuario)
        {
            InventarioContext _context = new InventarioContext();
            var findUser = await _context.Usuarios.Where(u => u.Email == usuario.Email && u.Contrasena == usuario.Contrasena).FirstOrDefaultAsync();
            if (findUser != null)
            {
                usuario.RolId = findUser.RolId;
                usuario.Nombre = findUser.Nombre;
                usuario.Apellido = findUser.Apellido;
                return usuario;
            }
            return null;
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
    }
}
