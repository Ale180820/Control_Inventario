using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        // GET: api/Productoes
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<ControlInventarioModel.Producto>> GetList()
        {
            InventarioContext _context = new InventarioContext();
            IEnumerable<ControlInventarioModel.Producto> productos = await _context.Productos.Select(m => new ControlInventarioModel.Producto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Marca = m.Marca,
                Tipo = m.Tipo,
                PrecioBase = m.PrecioBase,
                PrecioVenta = m.PrecioVenta,
                Descripcion = m.Descripcion
            }).ToListAsync();
            return productos;
        }

        // GET: api/Productoes/5
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ControlInventarioModel.Producto> Get(int id)
        {
            InventarioContext _context = new InventarioContext();
            ControlInventarioModel.Producto producto = await _context.Productos.Select(m => new ControlInventarioModel.Producto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Marca = m.Marca,
                Tipo = m.Tipo,
                PrecioBase = m.PrecioBase,
                PrecioVenta = m.PrecioVenta,
                Descripcion = m.Descripcion
            }).Where(m => m.Id == id).FirstOrDefaultAsync();
            return producto;
        }

        // PUT: api/Productoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("Put/{id}")]
        [HttpPost]
        public async Task<ControlInventarioModel.Producto> Put(int id, ControlInventarioModel.Producto producto)
        {
            InventarioContext _context = new InventarioContext();
            Models.Producto products = _context.Productos.FirstOrDefault(s => s.Id == id);
            if (products != null)
            {
                products.Nombre = producto.Nombre;
                products.Marca = producto.Marca;
                products.Tipo = producto.Tipo;
                products.PrecioBase = producto.PrecioBase;
                products.PrecioVenta = producto.PrecioVenta;
                products.Descripcion = producto.Descripcion;
                products.Foto = producto.GUID;
                _context.Productos.Update(products);
                await _context.SaveChangesAsync();
            }
            return producto;

        }

        // POST: api/Productoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("Set")]
        [HttpPost]
        public async Task<ControlInventarioModel.Producto> Set(ControlInventarioModel.Producto producto)
        {
            InventarioContext _context = new InventarioContext();
            Models.Producto product_Change = new Models.Producto
            {
                Nombre = producto.Nombre,
                Marca = producto.Marca,
                Tipo = producto.Tipo,
                PrecioBase = producto.PrecioBase,
                PrecioVenta = producto.PrecioVenta,
                Descripcion = producto.Descripcion,
                Foto = producto.GUID
            };
            _context.Productos.Add(product_Change);
            await _context.SaveChangesAsync();
            producto.Id = product_Change.Id;
            return producto;
        }

        // DELETE: api/Productoes/5

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
                var producto = await _context.Productos.FindAsync(id);
                if (producto != null)
                {
                    _context.Productos.Remove(producto);
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
