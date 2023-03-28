using System.ComponentModel;

namespace ControlInventarioModel
{
    public class Producto
    {
        public int Id { get; set; }

        [DisplayName("Nombre")]
        public string? Nombre { get; set; }

        [DisplayName("Marca")]
        public string? Marca { get; set; }

        [DisplayName("Tipo")]
        public string? Tipo { get; set; }

        [DisplayName("Foto")]
        public string? Foto { get; set; }

        [DisplayName("Precio base")]
        public decimal PrecioBase { get; set; }

        [DisplayName("Precio de venta")]
        public decimal PrecioVenta { get; set; }

        [DisplayName("Descripción")]
        public string? Descripcion { get; set; }
    }
}
