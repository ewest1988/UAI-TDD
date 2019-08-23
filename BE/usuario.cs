using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class usuario
    {
        public int IdUsuario { get; set; }
        public string uss { get; set; }
        public string pass { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string mail { get; set; }
        public int documento { get; set; }
        public string direccion { get; set; }
        public int telefono { get; set; }
        public int IdEstado { get; set; }
        public int intentosFallidos { get; set; }
        public List<int> patentes { get; set; }
        public List<int> patentesNegadas { get; set; }
        public List<int> familias { get; set; }
        public List<int> patentesFamilias { get; set; }
        public string digitoVerificador { get; set; }
    }
}
