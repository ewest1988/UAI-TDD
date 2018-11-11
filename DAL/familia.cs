using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class familia
    {
        SQLHelper sqlHelper = new SQLHelper();

        public List<BE.familia> listarFamilias() {

            DataTable datos = new DataTable();

            try
            {
                datos = sqlHelper.ObtenerDatos("SELECT * FROM Familia");

                return mapper(datos);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool nuevaFamilia(BE.familia familia) {

            int respuesta = 0;

            try
            {
                respuesta = sqlHelper.Ejecutar("INSERT INTO FAMILIA VALUES ('" + familia.Familia + "')", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public bool asignarFamiliaUsuario(BE.familia familia, BE.usuario usuario) {

            int respuesta = 0;

            try
            {
                respuesta = sqlHelper.Ejecutar("INSERT INTO USUARIO_FAMILIA VALUES (" + usuario.IdUsuario + "," + familia.idFamilia+ ")", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public List<BE.usuario> listarUsuariosFamilia(BE.familia f)
        {
            List<BE.usuario> usuariosFamilia = new List<BE.usuario>();
            DataTable datos = new DataTable();

            try
            {
                datos = sqlHelper.ObtenerDatos("select id_usuario from usuario_familia "
                                             + "where id_familia = " + f.idFamilia);

                foreach (DataRow reg in datos.Rows)
                {
                    BE.usuario usuario = new BE.usuario();
                    usuario.IdUsuario = Convert.ToInt32(reg["id_usuario"]);

                    usuariosFamilia.Add(usuario);
                }
            }
             
            catch (Exception ex)
            {
                throw ex;

            }

            return usuariosFamilia;
        }

        public bool verificarUsuariosFamilia(BE.familia f) {

            bool respuesta = false;
            DataTable datos = new DataTable();

            try
            {
                datos = sqlHelper.ObtenerDatos("select * from usuario_familia "
                                             + "where id_familia = " + f.idFamilia);

                if (datos.Rows.Count > 0) {
                    respuesta = true;
                }
            }

            catch (Exception ex)
            {
                throw ex;
                
            }

            return respuesta;
        }
        

        public bool eliminarFamiliasUsuario(BE.usuario usuario)
        {

            int respuesta = 0;

            try {

                respuesta = sqlHelper.Ejecutar("DELETE FROM USUARIO_FAMILIA WHERE ID_USUARIO = " + usuario.IdUsuario, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public bool eliminarFamilia(BE.familia familia)
        {

            int respuesta = 0;

            try
            {
                eliminarPatenteFamilia(familia);
                respuesta = sqlHelper.Ejecutar("DELETE FROM FAMILIA WHERE ID_FAMILIA = " + familia.idFamilia, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public bool eliminarPatenteFamilia(BE.familia familia) {

            int respuesta = 0;

            try
            {
                respuesta = sqlHelper.Ejecutar("DELETE FROM PATENTE_FAMILIA WHERE ID_FAMILIA = " + familia.idFamilia, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public bool eliminarPatentesUsuario(BE.familia familia, BE.usuario us) {

            int respuesta = 0;

            try
            {
                respuesta = sqlHelper.Ejecutar("delete from usuario_patente "
                                             + "where id_usuario = " + us.IdUsuario
                                             + " and id_patente in (select id_patente from Patente_Familia where id_familia = " + familia.idFamilia + ")", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public bool modificarPatenteFamilia(BE.patente patente, BE.familia familia)
        {
            int respuesta = 0;

            try
            {
                respuesta = sqlHelper.Ejecutar("INSERT INTO PATENTE_FAMILIA VALUES (" + patente.id_patente + "," + familia.idFamilia + ")", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public List<int> obtenerPatentesFamilia(BE.familia familia) {

            List<int> patentes = new List<int>();
            DataTable datos = new DataTable();

            try
            {
                datos = sqlHelper.ObtenerDatos("select id_patente from patente_familia "
                                             + "where id_familia = " + familia.idFamilia);

                foreach (DataRow reg in datos.Rows)
                {
                    
                    patentes.Add(Convert.ToInt32(reg["id_patente"]));
                }
            }

            catch (Exception ex)
            {
                throw ex;

            }

            return patentes;

        }

        public List<BE.familia> mapper(DataTable tablaFamilia) {

            List<BE.familia> familias = new List<BE.familia>();

            if (tablaFamilia.Rows.Count > 0)
            {
                foreach (DataRow reg in tablaFamilia.Rows)
                {
                    BE.familia familia = new BE.familia();
                    familia.idFamilia = Convert.ToInt32(reg["id_familia"]);
                    familia.Familia = reg["desc_familia"].ToString();

                    familias.Add(familia);
                }
            }

            return familias;
        }
    }
}
