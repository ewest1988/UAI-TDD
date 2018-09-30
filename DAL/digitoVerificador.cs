using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class digitoVerificador
    {
        private SQLHelper sqlHelper = new SQLHelper();

        public string Obtener_Digito(string tabla)
        {
            string digito = string.Empty;
            try
            {
                digito = sqlHelper.EjecutarScalar("SELECT Digito_Verificador FROM Digito_Verificador WHERE Tabla = '" + tabla + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return digito;
        }

        public bool modificarVerificador(string cadena, string tabla)
        {
            int respuesta = 0;

            try {

                respuesta = sqlHelper.Ejecutar("UPDATE Digito_Verificador SET Digito_Verificador = '" + cadena + "' WHERE Tabla = '" + tabla + "'", false);
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
