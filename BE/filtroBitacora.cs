using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class filtroBitacora
    {
        public int idUsuario { get; set; }
        public DateTime fecDesde { get; set; }
        public DateTime fecHasta { get; set; }
        public int idEvento { get; set; }
        public int idCriticidad { get; set; }
    }
}
