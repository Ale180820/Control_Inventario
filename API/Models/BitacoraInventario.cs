using System;
using System.Collections.Generic;

namespace API.Models;

public partial class BitacoraInventario
{
    public int Id { get; set; }

    public int UbicacionId { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int ProductoId { get; set; }

    public int? CantidadInicial { get; set; }

    public int? CantidadActual { get; set; }

    public bool? Disponibilidad { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Ubicacion Ubicacion { get; set; } = null!;
}
