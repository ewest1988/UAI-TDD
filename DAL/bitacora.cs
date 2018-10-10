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
        SQLHelper sqlHelper = new SQLHelper();
        string qryListar = "SELECT B.ID_BITACORA, U.usuario, E.desc_evento, B.fec_evento, C.desc_criticidad, B.digito_verificador FROM BITACORA B " +
                                          "INNER JOIN Usuario U ON B.id_usuario = U.id_usuario " +
                                          "INNER JOIN Evento E ON B.id_evento = E.id_evento " +
                                          "INNER JOIN CRITICIDAD C ON B.id_criticidad = C.id_criticidad ";

        public DataTable listarBitacora() {

            return sqlHelper.ObtenerDatos(qryListar); 
        }

        public DataTable listarBitacora(BE.filtroBitacora filtro)
        {
            string filterUser = "";
            string filterEvent = "";
            string filterCritic = "";
            string filterDate = "";

            filterDate = "WHERE B.FEC_EVENTO BETWEEN '" + filtro.fecDesde.Year + "-" + filtro.fecDesde.Month + "-" + filtro.fecDesde.Day + 
                "' AND '" + filtro.fecHasta.Year + "-" + filtro.fecHasta.Month + "-" + filtro.fecHasta.Day + "' ";

            if (filtro.idUsuario != 0) filterUser = "AND B.ID_USUARIO = " + filtro.idUsuario + " ";
            if (filtro.idEvento != 0) filterEvent = "AND B.ID_EVENTO = " + filtro.idEvento + " ";
            if (filtro.idCriticidad!= 0) filterCritic = "AND B.ID_CRITICIDAD = " + filtro.idCriticidad + " ";
            return sqlHelper.ObtenerDatos(qryListar + filterDate + filterUser + filterEvent + filterCritic);
        }

        public DataTable listarTablaBitacora()
        {
            DataTable datos = new DataTable();

            try
            {
                datos = sqlHelper.ObtenerDatos("SELECT * FROM bitacora");
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

                respuesta = sqlHelper.Ejecutar("DELETE FROM Bitacora WHERE ID_BITACORA IN (" + strId.Substring(0, strId.Length - 1) + ")", false);
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

                respuesta = sqlHelper.Ejecutar("INSERT INTO BITACORA VALUES (" + bitacora.idUsuario + "," + bitacora.idEvento + ",'" +
                                                bitacora.FecEvento.Year + "-" + bitacora.FecEvento.Month + "-" + bitacora.FecEvento.Day + "',1,'" + bitacora.DigitoVerificador + "')", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (respuesta > 0) return true;
            else return false;
        }

        public DataTable listarEventos() {

            return sqlHelper.ObtenerDatos("SELECT * FROM EVENTO");
        }

        public DataTable listarCriticidad()
        {

            return sqlHelper.ObtenerDatos("SELECT * FROM CRITICIDAD");
        }

        public bool guardarEvento(BE.bitacora bitacora) {
            
              
            return true;
        }
    }
}
