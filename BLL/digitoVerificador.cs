using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class digitoVerificador
    {
        private seguridad seguridad = new seguridad();
        private DAL.digitoVerificador digitoverificador = new DAL.digitoVerificador();

        public string ObtenerDVV(string tabla)
        {
            return digitoverificador.Obtener_Digito(tabla);
        }

        public bool VerificadorHorizontal(string cadena, string verificador)
        {
            var hash = seguridad.ObtenerHash(cadena);
            if (verificador == hash)
                return true;
            else
                return false;
        }

        public bool VerificadorVertical(string tabla, string verificador)
        {

            var hash = seguridad.ObtenerHash(tabla);
            if (verificador == hash)
                return true;
            else
                return false;
        }

        public string CacularDVV(DataTable tabla)
        {
            StringBuilder expression = new StringBuilder();
            foreach (DataRow DR in tabla.Rows) {

                for(int i = 0; i < (DR.ItemArray.Length - 2); i++) {

                    expression.Append(DR.ItemArray[i]);
            }
        }
                                
            return seguridad.ObtenerHash(expression.ToString());
        }

        public bool Modificar_Verificador(string cadena, string tabla)
        {
            bool res = false;
            try
            {
                res = digitoverificador.Modificar_Verificador(cadena, tabla);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
