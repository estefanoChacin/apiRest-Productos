using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Usuario
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? contrasena { get; set; }
        public string? rol { get; set; }
    }
}
