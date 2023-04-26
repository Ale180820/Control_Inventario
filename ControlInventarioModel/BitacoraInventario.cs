using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlInventarioModel
{
    public class BitacoraInventario
    {
        public int Id { get; set; }

        public int UbicacionId { get; set; }
        public int? noGondola { get; set; }
        public DateTime? FechaIngreso { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public int ProductoId { get; set; }
        public string? NombreProducto { get; set; }

        public int? CantidadInicial { get; set; }

        public int? CantidadActual { get; set; }

        public bool Disponibilidad { get; set; }
    }
}
