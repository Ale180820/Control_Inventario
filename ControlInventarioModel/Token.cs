using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlInventarioModel
{
    public class Token
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Rol { get; set; }
        public string TokenGenerate { get; set; } = null!;
    }
}
