using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UbicacionsController : ControllerBase
    {

        // GET: api/Ubicacions
        [Route("GetList")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ubicacion>>> GetUbicacions()
        {
            InventarioContext _context = new InventarioContext();
            if (_context.Ubicacions == null)
            {
                return NotFound();
            }
            return await _context.Ubicacions.ToListAsync();
        }

        // GET: api/Ubicacions/5
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ActionResult<Ubicacion>> GetUbicacion(int id)
        {
            InventarioContext _context = new InventarioContext();
            if (_context.Ubicacions == null)
            {
                return NotFound();
            }
            var ubicacion = await _context.Ubicacions.FindAsync(id);

            if (ubicacion == null)
            {
                return NotFound();
            }

            return ubicacion;
        }

        // PUT: api/Ubicacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("Put/{id}")]
        [HttpPost]
        public async Task<ActionResult<Ubicacion>> PutUbicacion(int id, Ubicacion ubicacion)
        {
            InventarioContext _context = new InventarioContext();
            if (id != ubicacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(ubicacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return ubicacion;


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UbicacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/Ubicacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("Set")]
        [HttpPost]
        public async Task<ActionResult<Ubicacion>> PostUbicacion(Ubicacion ubicacion)
        {
            InventarioContext _context = new InventarioContext();
            if (_context.Ubicacions == null)
            {
                return Problem("Entity set 'InventarioContext.Ubicacions'  is null.");
            }
            _context.Ubicacions.Add(ubicacion);
            await _context.SaveChangesAsync();

            return ubicacion;
        }

        // DELETE: api/Ubicacions/5
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
                var usuario = await _context.Ubicacions.FindAsync(id);
                if (usuario != null)
                {
                    _context.Ubicacions.Remove(usuario);
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

        private bool UbicacionExists(int id)
        {
            InventarioContext _context = new InventarioContext();
            return (_context.Ubicacions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
