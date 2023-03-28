using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<ControlInventarioModel.Rol>> GetList()
        {
            InventarioContext _context = new InventarioContext();
            IEnumerable<ControlInventarioModel.Rol> rols = await _context.Rols.Select(m => new ControlInventarioModel.Rol
            {
                Id = m.Id,
                Nombre = m.Nombre
            }).ToListAsync();
            return rols;
        }
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ControlInventarioModel.Rol> Get(int id)
        {
            InventarioContext _context = new InventarioContext();
            ControlInventarioModel.Rol rol = await _context.Rols.Select(m => new ControlInventarioModel.Rol
            {
                Id = m.Id,
                Nombre = m.Nombre
            }).Where(m => m.Id == id).FirstOrDefaultAsync();
            return rol;
        }

        [Route("Put/{id}")]
        [HttpPost]
        public async Task<ControlInventarioModel.Rol> Put(int id, ControlInventarioModel.Rol rol)
        {
            InventarioContext _context = new InventarioContext();
            Models.Rol roles = _context.Rols.FirstOrDefault(s => s.Id == id);
            if (roles != null)
            {
                roles.Nombre = rol.Nombre;
                _context.Rols.Update(roles);
                await _context.SaveChangesAsync();
            }
            return rol;

        }

        [Route("Set")]
        [HttpPost]
        public async Task<ControlInventarioModel.Rol> Set(ControlInventarioModel.Rol Rol)
        {
            InventarioContext _context = new InventarioContext();
            Models.Rol rol_Change = new Models.Rol
            {
                Nombre = Rol.Nombre
            };
            _context.Rols.Add(rol_Change);
            await _context.SaveChangesAsync();
            Rol.Id = rol_Change.Id;
            return Rol;
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
                var Rol = await _context.Rols.FindAsync(id);
                if (Rol != null)
                {
                    _context.Rols.Remove(Rol);
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
