using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class bitacora
    {
        public int IdBitacora { get; set; }
        public string Usuario { get; set; }
        public int idUsuario { get; set; }
        public int idEvento { get; set; }
        public string evento { get; set; }
        public DateTime FecEvento { get; set; }
        public int idCriticidad { get; set; }
        public string criticidad { get; set; }
        public string DigitoVerificador { get; set; }
    }
}
