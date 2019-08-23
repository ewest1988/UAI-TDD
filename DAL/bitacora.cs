using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class bitacora
    {
        string qryListar = "SELECT B.ID_BITACORA, U.usuario, E.desc_evento, B.fec_evento, C.desc_criticidad, B.digito_verificador FROM BITACORA B " +
                                          "INNER JOIN Usuario U ON B.id_usuario = U.id_usuario " +
                                          "INNER JOIN Evento E ON B.id_evento = E.id_evento " +
                                          "INNER JOIN CRITICIDAD C ON E.id_criticidad = C.id_criticidad ";

        public DataTable listarBitacora() {

            return SQLHelper.GetInstance().ObtenerDatos(qryListar); 
        }

        public DataTable listarBitacora(BE.filtroBitacora filtro)
        {
            string filterUser = "";
            string filterEvent = "";
            string filterCritic = "";
            string filterDate = "";

            filterDate = "WHERE B.FEC_EVENTO BETWEEN '" + filtro.fecDesde.ToString("yyyy-MM-dd HH:mm:ss") + 
                "' AND '" + filtro.fecHasta.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

            if (filtro.idUsuario != 0) filterUser = "AND B.ID_USUARIO = " + filtro.idUsuario + " ";
            if (filtro.idEvento != 0) filterEvent = "AND B.ID_EVENTO = " + filtro.idEvento + " ";
            if (filtro.idCriticidad!= 0) filterCritic = "AND B.ID_CRITICIDAD = " + filtro.idCriticidad + " ";
            return SQLHelper.GetInstance().ObtenerDatos(qryListar + filterDate + filterUser + filterEvent + filterCritic);
        }

        public DataTable listarTablaBitacora()
        {
            DataTable datos = new DataTable();

            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT * FROM bitacora");
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool eliminarBitacora(List<int> lista) {

            int respuesta = 0;
            string strId = "";
            foreach (int id in lista) { strId += id.ToString() + ","; }

            try {

                respuesta = SQLHelper.GetInstance().Ejecutar("DELETE FROM Bitacora WHERE ID_BITACORA IN (" + strId.Substring(0, strId.Length - 1) + ")", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (respuesta > 0) return true;
            else return false;
        }

        public bool agregarBitacora(BE.bitacora bitacora) {

            int respuesta = 0;

            try {

                respuesta = SQLHelper.GetInstance().Ejecutar("INSERT INTO BITACORA VALUES (" + bitacora.idUsuario + "," + bitacora.idEvento + ",'" +
                                                bitacora.FecEvento.ToString("yyyy-MM-dd HH:mm:ss") + "',1,'" + bitacora.DigitoVerificador + "')", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (respuesta > 0) return true;
            else return false;
        }

        public DataTable listarEventos() {

            return SQLHelper.GetInstance().ObtenerDatos("SELECT * FROM EVENTO");
        }

        public DataTable listarCriticidad()
        {

            return SQLHelper.GetInstance().ObtenerDatos("SELECT * FROM CRITICIDAD");
        }

        public bool guardarEvento(BE.bitacora bitacora) {
            
              
            return true;
        }
    }
}
