using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class idioma
    {
        SQLHelper sqlHelper = new SQLHelper();
        public List<BE.idioma> listarIdioma(BE.idioma idioma) {

            DataTable datos = new DataTable();

            try
            {
                datos = sqlHelper.ObtenerDatos("SELECT * FROM Etiqueta WHERE ID_IDIOMA = " + idioma.idLanguage + "AND ID_MENU = " + idioma.idMenu);

                return mapper(datos);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BE.idioma> mapper(DataTable idiomaTabla) {

            List<BE.idioma> idiomas = new List<BE.idioma>();

            if (idiomaTabla.Rows.Count > 0)
            {
                foreach (DataRow reg in idiomaTabla.Rows)
                {
                    BE.idioma idioma = new BE.idioma();
                    idioma.idEtiqueta = Convert.ToInt32(reg["id_etiqueta"]);
                    idioma.etiqueta = reg["desc_etiqueta"].ToString();

                    idiomas.Add(idioma);
                }
            }

            return idiomas;
        }
    }
}
