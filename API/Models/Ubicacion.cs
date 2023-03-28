using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Ubicacion
{
    public int Id { get; set; }

    public int? NoGondola { get; set; }

    public int? Nivel { get; set; }

    public virtual ICollection<BitacoraInventario> BitacoraInventarios { get; } = new List<BitacoraInventario>();
}
