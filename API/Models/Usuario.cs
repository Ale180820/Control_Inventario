using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public DateTime? FechaIngreso { get; set; }

    public DateTime? FechaEgreso { get; set; }

    public int RolId { get; set; }

    public virtual Rol Rol { get; set; } = null!;
}
