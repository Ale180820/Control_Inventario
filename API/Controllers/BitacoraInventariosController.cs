using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using ControlInventarioModel;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BitacoraInventariosController : ControllerBase
    {

        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<ControlInventarioModel.BitacoraInventario>> GetList()
        {
            InventarioContext _context = new InventarioContext();
            IEnumerable<ControlInventarioModel.BitacoraInventario> productos = await _context.BitacoraInventarios.Select(inventario => new ControlInventarioModel.BitacoraInventario
            {
                Id = inventario.Id,
                UbicacionId = inventario.UbicacionId,
                noGondola = inventario.Ubicacion.NoGondola,
                FechaIngreso = inventario.FechaIngreso,
                FechaModificacion = inventario.FechaModificacion,
                ProductoId = inventario.ProductoId,
                NombreProducto = inventario.Producto.Nombre,
                CantidadInicial = inventario.CantidadInicial,
                CantidadActual = inventario.CantidadActual,
                Disponibilidad = (bool)inventario.Disponibilidad
            }).ToListAsync();
            return productos;
        }

        // GET: api/BitacoraInventarios/5
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ControlInventarioModel.BitacoraInventario> Get(int id)
        {
            InventarioContext _context = new InventarioContext();
            ControlInventarioModel.BitacoraInventario producto = await _context.BitacoraInventarios.Select(inventario => new ControlInventarioModel.BitacoraInventario
            {
                Id = inventario.Id,
                UbicacionId = inventario.UbicacionId,
                noGondola = inventario.Ubicacion.NoGondola,
                FechaIngreso = inventario.FechaIngreso,
                FechaModificacion = inventario.FechaModificacion,
                ProductoId = inventario.ProductoId,
                NombreProducto = inventario.Producto.Nombre,
                CantidadInicial = inventario.CantidadInicial,
                CantidadActual = inventario.CantidadActual,
                Disponibilidad = (bool)inventario.Disponibilidad
            }).Where(inventario => inventario.Id == id).FirstOrDefaultAsync();
            return producto;
        }

        // PUT: api/BitacoraInventarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("Put/{id}")]
        [HttpPost]
        public async Task<ControlInventarioModel.BitacoraInventario> Set(int id, ControlInventarioModel.BitacoraInventario inventario)
        {
            InventarioContext _context = new InventarioContext();
            Models.BitacoraInventario inventarioEdit = _context.BitacoraInventarios.FirstOrDefault(s => s.Id == id);
            if (inventarioEdit != null)
            {
                inventarioEdit.UbicacionId = inventario.UbicacionId;
                inventarioEdit.FechaModificacion = inventario.FechaModificacion;
                inventarioEdit.ProductoId = inventario.ProductoId;
                inventarioEdit.CantidadActual = inventario.CantidadActual;
                inventarioEdit.Disponibilidad = inventario.Disponibilidad;
                _context.BitacoraInventarios.Update(inventarioEdit);
                await _context.SaveChangesAsync();
            }
            return inventario;
        }

        // POST: api/BitacoraInventarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("Set")]
        [HttpPost]
        public async Task<ControlInventarioModel.BitacoraInventario> Set(ControlInventarioModel.BitacoraInventario inventario)
        {
            InventarioContext _context = new InventarioContext();
            Models.BitacoraInventario inventario_Change = new Models.BitacoraInventario
            {
                UbicacionId = inventario.UbicacionId,
                FechaIngreso = inventario.FechaIngreso,
                FechaModificacion = inventario.FechaModificacion,
                ProductoId = inventario.ProductoId,
                CantidadInicial = inventario.CantidadInicial,
                CantidadActual = inventario.CantidadActual,
                Disponibilidad = inventario.Disponibilidad
            };
            _context.BitacoraInventarios.Add(inventario_Change);
            await _context.SaveChangesAsync();
            inventario.Id = inventario_Change.Id;
            return inventario;
        }

        // DELETE: api/BitacoraInventarios/5
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
                var producto = await _context.BitacoraInventarios.FindAsync(id);
                if (producto != null)
                {
                    _context.BitacoraInventarios.Remove(producto);
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
