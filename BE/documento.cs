using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class documento
    {
        public int idDocumento { get; set; }
        public int idTipo { get; set; }
        public string nombre { get; set; }
        public string extension { get; set; }
        public DateTime fechaCreacion { get; set; }
        public byte[] contenido { get; set; }
        public usuario usuario { get; set; }
        public string digitoVerificador { get; set; }

        public documento() {

            this.usuario = new usuario();
        }
    }
}
