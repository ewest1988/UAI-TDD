using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    
    public class patente
    {
        SQLHelper sqlHelper = new SQLHelper();

        public List<BE.patente> listarPatentes() {

            DataTable datos = new DataTable();

            try
            {
                datos = sqlHelper.ObtenerDatos("SELECT * FROM Patente;");

                return mapper(datos);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BE.patente> listarPatentes(BE.familia familia)
        {

            DataTable datos = new DataTable();

            try
            {
                datos = sqlHelper.ObtenerDatos("select p.* from patente p " +
                                               "inner join patente_familia pf on p.id_patente = pf.id_patente " +
                                               "where pf.id_familia = " + familia.idFamilia);

                return mapper(datos);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool asignarPatenteUsuario(BE.patente patente, BE.usuario usuario) {

            int respuesta = 0;

            try
            {
                respuesta = sqlHelper.Ejecutar("INSERT INTO usuario_patente values (" + usuario.IdUsuario + "," + patente.id_patente + ")", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public List<BE.patente> mapper(DataTable patenteTabla)
        {

            List<BE.patente> patentes = new List<BE.patente>();

            if (patenteTabla.Rows.Count > 0)
            {
                foreach (DataRow reg in patenteTabla.Rows)
                {
                    BE.patente patente = new BE.patente();
                    patente.id_patente = Convert.ToInt32(reg["id_patente"]);
                    patente.descPatente = reg["desc_patente"].ToString();

                    patentes.Add(patente);
                }
            }

            return patentes;
        }
    }
}
