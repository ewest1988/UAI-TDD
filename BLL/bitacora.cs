using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class bitacora {

        DAL.bitacora gestorBitacora = new DAL.bitacora();
        encriptacion encriptacion = new encriptacion();

        public List<BE.bitacora> listarBitacora() {

            return mapper(gestorBitacora.listarBitacora()); 
        }

        public List<BE.bitacora> listarBitacora(BE.filtroBitacora filtro) {

            return mapper(gestorBitacora.listarBitacora(filtro));
        }

        public DataTable listarTablaBitacora()
        {
            DataTable bitacoraTabla = gestorBitacora.listarTablaBitacora();

            return bitacoraTabla;
        }

        public bool agregarBitacora(BE.bitacora bitacora) {

            return gestorBitacora.agregarBitacora(bitacora);
        }

        public string concatenarCampos(BE.bitacora bitacora) {

            return bitacora.idUsuario.ToString() + bitacora.idEvento.ToString() + bitacora.FecEvento.Year.ToString() + "-" + bitacora.FecEvento.Month.ToString() + "-" + bitacora.FecEvento.Day.ToString();
        }

        public bool eliminarBitacora(List<int> lista) {

            return gestorBitacora.eliminarBitacora(lista);
        }

        public List<BE.evento> listarEventos() {

            List<BE.evento> eventos = new List<BE.evento>();
            DataTable tablaEventos = gestorBitacora.listarEventos();

            if (tablaEventos.Rows.Count > 0) {

                foreach (DataRow reg in tablaEventos.Rows) {
                    BE.evento evento = new BE.evento();
                    evento.idEvento = Convert.ToInt32(reg["id_evento"]);
                    evento.descripcion = reg["desc_evento"].ToString();

                    eventos.Add(evento);
                }
            }

            return eventos;
        }

        public List<BE.criticidad> listarCriticidad()
        {

            List<BE.criticidad> criticidades = new List<BE.criticidad>();
            DataTable tablaCriticidad = gestorBitacora.listarCriticidad();

            if (tablaCriticidad.Rows.Count > 0)
            {

                foreach (DataRow reg in tablaCriticidad.Rows)
                {
                    BE.criticidad criticidad = new BE.criticidad();
                    criticidad.idCriticidad = Convert.ToInt32(reg["id_criticidad"]);
                    criticidad.descripcion = reg["desc_criticidad"].ToString();

                    criticidades.Add(criticidad);
                }
            }

            return criticidades;
        }

        public List<BE.bitacora> mapper(DataTable eventosTabla) {

            List<BE.bitacora> eventos = new List<BE.bitacora>();

            if (eventosTabla.Rows.Count > 0)
            {
                foreach (DataRow reg in eventosTabla.Rows)
                {
                    BE.bitacora bitacora = new BE.bitacora();
                    bitacora.IdBitacora = Convert.ToInt32(reg["id_bitacora"]);
                    bitacora.Usuario = encriptacion.Decrypt(reg["usuario"].ToString());
                    bitacora.evento = reg["desc_evento"].ToString();
                    bitacora.FecEvento = Convert.ToDateTime(reg["fec_evento"]);
                    bitacora.criticidad = reg["desc_criticidad"].ToString();
                    bitacora.DigitoVerificador = reg["digito_verificador"].ToString();

                    eventos.Add(bitacora);
                }
            }

            return eventos;
        }
    }
}
