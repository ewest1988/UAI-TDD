using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class familia
    {
        public DAL.familia familiaDAL = new DAL.familia();
        public BLL.patente gestorPatente = new BLL.patente();
        public DAL.patente patenteDAL = new DAL.patente();


        public List<BE.familia> listarFamilias()
        {

            return familiaDAL.listarFamilias();
        }

        public bool modificarFamilia(List<BE.patente> patentes, BE.familia familia)
        {

            try
            {
                //elimino las patentes de la familia
                familiaDAL.eliminarPatenteFamilia(familia);

                //asigno las patentes seleccionadas a la familia
                foreach (BE.patente p in patentes) {

                    familiaDAL.modificarPatenteFamilia(p, familia);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            
        }

        public bool modificarFamilias(List<BE.familia> familias, BE.usuario usuario) {

            eliminarFamiliasUsuario(usuario);

            foreach (BE.familia f in familias) {

                familiaDAL.asignarFamiliaUsuario(f, usuario);

            }

            return true;
        }

            
        

        public bool eliminarFamiliasUsuario(BE.usuario usuario)
        {
            return familiaDAL.eliminarFamiliasUsuario(usuario);
        }

        public bool nuevaFamilia(BE.familia f)
        {

            return familiaDAL.nuevaFamilia(f);
        }

        public bool eliminarFamilia(BE.familia f)
        {

            return familiaDAL.eliminarFamilia(f);
        }
    }
}
