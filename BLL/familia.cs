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
        public DAL.patente patenteDAL = new DAL.patente();


        public List<BE.familia> listarFamilias()
        {

            return familiaDAL.listarFamilias();
        }

        public bool modificarFamilia(List<BE.patente> patentes, BE.familia familia)
        {

            try
            {
                //listo los usuarios que poseen la familia
                List<BE.usuario> usuariosFamilia = new List<BE.usuario>();
                usuariosFamilia = familiaDAL.listarUsuariosFamilia(familia);

                //por cada usuario elimino sus patentes de esta familia y le asigno las nuevas patentes
                foreach (var us in usuariosFamilia)
                {
                    familiaDAL.eliminarPatentesUsuario(familia, us);

                    foreach (var p in patentes) {

                        patenteDAL.asignarPatenteUsuario(p, us);
                    }
                }

                //elimino las patentes de la familia
                familiaDAL.eliminarPatenteFamilia(familia);

                //asigno las patentes seleccionadas a la familia
                foreach (BE.patente p in patentes) {

                    familiaDAL.modificarPatenteFamilia(p, familia);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }

        public bool nuevaFamilia(BE.familia f)
        {

            return familiaDAL.nuevaFamilia(f);
        }

        public bool eliminarFamilia(BE.familia f)
        {

            bool bf = familiaDAL.verificarUsuariosFamilia(f);

            if (bf)
            {
                return false;
                
            }
            else return familiaDAL.eliminarFamilia(f);
        }
    }
}
