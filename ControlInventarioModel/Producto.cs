using System.ComponentModel;

namespace ControlInventarioModel
{
    public class Producto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Marca { get; set; }
        public string? Tipo { get; set; }
        public string? Foto { get; set; }
        public string? GUID { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal PrecioVenta { get; set; }
        public string? Descripcion { get; set; }
    }
}
