using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class familia
    {
        public int idFamilia { get; set; }
        public string Familia { get; set; }
        public List<patente> patentes { get; set; }

    }
}
