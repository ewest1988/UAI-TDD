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
        public List<BE.patente> listarPatentes() {

            DataTable datos = new DataTable();

            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT * FROM Patente;");

                return mapper(datos);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable listarTablaUsuariosPatentes()
        {

            DataTable datos = new DataTable();

            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT * FROM Usuario_Patente;");

                return datos;
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
                datos = SQLHelper.GetInstance().ObtenerDatos("select p.* from patente p " +
                                               "inner join patente_familia pf on p.id_patente = pf.id_patente " +
                                               "where pf.id_familia = " + familia.idFamilia);

                return mapper(datos);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool validarZonaDeNadieFU(int p, int f)
        {

            DataTable datos = new DataTable();

            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("select id_patente from usuario_patente where id_patente = " + p +
                                               " union select pf.id_patente from patente_familia pf " +
                                               "inner join usuario_familia uf on pf.id_familia = uf.id_familia " +
                                               "where pf.id_patente = " + p +
                                               " and pf.id_familia <> " + f);

                if (datos.Rows.Count == 0)
                {
                    return true;
                }
                else return false;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool validarZonaDeNadie(int p, int u) {

            DataTable datos = new DataTable();

            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("select id_patente from usuario_patente where id_patente = " + p +
                                               " and id_usuario <> " + u +
                                               " union select pf.id_patente from patente_familia pf " +
                                               "inner join usuario_familia uf on pf.id_familia = uf.id_familia " +
                                               "where pf.id_patente = " + p);

                if (datos.Rows.Count == 0)
                {
                    return true;
                }
                else return false;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool validarZonaDeNadieF(int p, int f)
        {

            DataTable datos = new DataTable();

            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("select id_patente from usuario_patente " +
                                               "where id_patente = " + p +
                                               " union select id_patente from patente_familia pf " +
                                               "inner join usuario_familia uf on pf.id_familia = uf.id_familia " +
                                               "where pf.id_patente = " + p +
                                               "and pf.id_familia <> " + f);

                if (datos.Rows.Count == 0)
                {
                    return true;
                }
                else return false;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool validarZonaDeNadiePN(int p, int u)
        {

            DataTable datos = new DataTable();

            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("select id_patente from usuario_patente where id_patente = " + p +
                                               " and id_usuario <> " + u +
                                               " union select pf.id_patente from patente_familia pf " +
                                               "inner join usuario_familia uf on pf.id_familia = uf.id_familia " +
                                               "where pf.id_patente = " + p + 
                                               " and uf.id_usuario <> " + u);

                if (datos.Rows.Count == 0)
                {
                    return true;
                }
                else return false;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool eliminarPatentesUsuario(BE.usuario usuario)
        {

            int respuesta = 0;

            try
            {

                respuesta = SQLHelper.GetInstance().Ejecutar("DELETE FROM usuario_patente WHERE ID_USUARIO = " + usuario.IdUsuario, false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public bool eliminarPatentesNegadaUsuario(BE.usuario usuario)
        {

            int respuesta = 0;

            try {

                respuesta = SQLHelper.GetInstance().Ejecutar("DELETE FROM usuario_patente_negada WHERE ID_USUARIO = " + usuario.IdUsuario, false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public bool asignarPatenteUsuario(BE.patente patente, BE.usuario usuario, string dv) {

            int respuesta = 0;

            try {

                respuesta = SQLHelper.GetInstance().Ejecutar("BEGIN IF NOT EXISTS(SELECT * FROM Usuario_Patente " +
                                               "WHERE id_usuario = " + usuario.IdUsuario +
                                               " AND id_patente = " + patente.id_patente + ") " +
                                               "BEGIN INSERT INTO Usuario_Patente VALUES(" + usuario.IdUsuario + "," + patente.id_patente + ",'" + dv + "')" + 
                                               "END END", false);
            }
            catch (Exception ex) {

                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public bool eliminarPatenteUsuario(BE.usuario usuario, BE.patente patente)
        {

            int respuesta = 0;

            try {

                respuesta = SQLHelper.GetInstance().Ejecutar("DELETE FROM usuario_patente WHERE ID_USUARIO = " + usuario.IdUsuario + " AND ID_PATENTE = " + patente.id_patente, false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public bool asignarPatenteNegadaUsuario(BE.patente patente, BE.usuario usuario)
        {

            int respuesta = 0;

            try {

                respuesta = SQLHelper.GetInstance().Ejecutar("INSERT INTO usuario_patente_negada values (" + usuario.IdUsuario + "," + patente.id_patente + ")", false);
            }
            catch (Exception ex) {

                
                throw ex;
            }
            if (respuesta > 0) {

                return true;
            }

            

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
