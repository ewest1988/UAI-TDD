using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class digitoVerificador
    {
        public string obtenerDigito(string tabla)
        {
            string digito = string.Empty;
            try
            {
                digito = SQLHelper.GetInstance().EjecutarScalar("SELECT Digito_Verificador FROM Digito_Verificador WHERE Tabla = '" + tabla + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return digito;
        }

        public DataTable listarTabla(string tabla)
        {
            DataTable datos = new DataTable();

            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT * FROM " + tabla);
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool modificarVerificador(string cadena, string tabla)
        {
            int respuesta = 0;

            try {

                respuesta = SQLHelper.GetInstance().Ejecutar("UPDATE Digito_Verificador SET Digito_Verificador = '" + cadena + "' WHERE Tabla = '" + tabla + "'", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }
    }
}
