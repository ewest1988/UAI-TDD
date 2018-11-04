using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class patente
    {
        DAL.patente Patente = new DAL.patente();

        public List<BE.patente> listarPatentes() {

            return Patente.listarPatentes();
        }

        public List<BE.patente> listarPatentes(BE.familia familia)
        {

            return Patente.listarPatentes(familia);
        }

        public bool validarZonaDeNadie(int idPatente, int idUsuario)
        {

            return Patente.validarZonaDeNadie(idPatente, idUsuario);
        }
    }
}
