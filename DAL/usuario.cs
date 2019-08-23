using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class usuario
    {
        public DataTable listarUsuarios() {
            DataTable datos = new DataTable();

            try {
                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT * FROM Usuario WHERE ID_ESTADO <> 4");
                return datos;
            }

            catch (Exception ex) {
                throw ex;
            }
        }

        public bool actualizarIntentosFallidos(string user, int intento) {

            int respuesta = 0;

            try
            {
                respuesta = SQLHelper.GetInstance().Ejecutar("UPDATE USUARIO SET intentos_fallidos = " + intento +
                                               " WHERE USUARIO = '" + user + "'", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public DataTable listarTodos()
        {
            DataTable datos = new DataTable();

            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT * FROM Usuario");
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool cambiarEstadoUsuario(BE.usuario usuario)
        {
            int respuesta = 0;

            try
            {
                respuesta = SQLHelper.GetInstance().Ejecutar("UPDATE USUARIO SET ID_ESTADO = " + usuario.IdEstado + 
                                               " WHERE ID_USUARIO = " + usuario.IdUsuario, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public bool agregarUsuario(BE.usuario usuario) {

            int respuesta = 0;

            try {
                respuesta = SQLHelper.GetInstance().Ejecutar("INSERT INTO Usuario (usuario,contraseña,nombre,apellido,mail,documento,direccion,telefono,id_estado,digito_verificador) VALUES ('" + usuario.uss + "','" + usuario.pass + "','" + usuario.nombre + "','" + usuario.apellido + "','" + usuario.mail + "'," + usuario.documento + ",'" + usuario.direccion + "'," + usuario.telefono + "," + usuario.IdEstado + ",'" + usuario.digitoVerificador + "')", false);
            } catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }


        public int consultarIntentosFallidos(string usuario) {

            int intentos = 0;
            
            try {

                intentos = Convert.ToInt32(SQLHelper.GetInstance().EjecutarScalar("SELECT intentos_fallidos FROM Usuario WHERE usuario = '" + usuario + "' AND ID_ESTADO <> 4"));
            }
            catch (Exception ex) {

                throw ex;
            }

            return intentos;
        }

        

        public bool validarUsuario(string usuario)
        {
            string usuarioDB = null;

            try
            {
                usuarioDB = SQLHelper.GetInstance().EjecutarScalar("SELECT usuario FROM Usuario WHERE usuario = '" + usuario + "' AND ID_ESTADO <> 4");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (usuarioDB == usuario);
        }

        public bool validarLogin(string usuario, string contraseña)
        {
           string passbase = null;

            try {
                passbase = SQLHelper.GetInstance().EjecutarScalar("SELECT Clave FROM Usuario WHERE Usuario = '" + usuario + "' AND ID_ESTADO <> 4");
            }
            catch (Exception ex) {
                throw ex;
            }

            return (passbase == contraseña);
        }

        public bool validarCorreo(string email)
        {
            string emailDB = null;

            try
            {
                emailDB = SQLHelper.GetInstance().EjecutarScalar("SELECT mail FROM Usuario WHERE mail = '" + email + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (emailDB == email);
        }

        public bool validarCorreo(string email, int idUsuario)
        {
            string emailDB = "";

            try
            {
                emailDB = SQLHelper.GetInstance().EjecutarScalar("SELECT mail FROM Usuario WHERE mail = '" + email + "' AND id_usuario <> " + idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (emailDB == email);
        }

        public DataTable obtenerUsuario(BE.usuario usuario)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT * FROM Usuario WHERE id_usuario = " + usuario.IdUsuario + ";");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }

        public DataTable obtenerUsuario(string usuario)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT * FROM Usuario WHERE usuario = '" + usuario + "';");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }

        public List<int> obtenerPatentes(BE.usuario usuario)
        {
            List<int> patentes = new List<int>();
            DataTable datos = new DataTable();
            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT id_patente FROM Usuario_Patente WHERE id_usuario = " + usuario.IdUsuario);

                if (datos.Rows.Count > 0)
                {
                    foreach (DataRow reg in datos.Rows)
                    {
                         patentes.Add(Convert.ToInt32(reg["id_patente"]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return patentes;
        }

        public List<int> obtenerPatentesFamilia(BE.usuario usuario)
        {
            List<int> patentes = new List<int>();
            DataTable datos = new DataTable();
            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT pf.id_patente FROM Usuario_Familia uf " +
                                               "inner join Patente_Familia pf on uf.id_familia = pf.id_familia " +
                                               "WHERE id_usuario = " + usuario.IdUsuario);

                if (datos.Rows.Count > 0)
                {
                    foreach (DataRow reg in datos.Rows)
                    {
                        patentes.Add(Convert.ToInt32(reg["id_patente"]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return patentes;
        }

        public List<int> obtenerPatentesNegadas(BE.usuario usuario)
        {
            List<int> patentes = new List<int>();
            DataTable datos = new DataTable();
            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT id_patente FROM Usuario_Patente_Negada WHERE id_usuario = " + usuario.IdUsuario + ";");

                if (datos.Rows.Count > 0)
                {
                    foreach (DataRow reg in datos.Rows)
                    {
                        patentes.Add(Convert.ToInt32(reg["id_patente"]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return patentes;
        }

        public List<int> obtenerFamilias(BE.usuario usuario)
        {
            DataTable datos = new DataTable();
            List<int> familias = new List<int>();
            
            try
            {
                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT id_familia FROM Usuario_Familia WHERE id_usuario = " + usuario.IdUsuario + ";");

                if (datos.Rows.Count > 0)
                {
                    foreach (DataRow reg in datos.Rows)
                    {
                        familias.Add(Convert.ToInt32(reg["id_familia"]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return familias;
        }

        public string obtenerVerificador(BE.usuario usuario)
        {
            string datos = null;
            try {
                datos = SQLHelper.GetInstance().EjecutarScalar("SELECT Digito_Verificador FROM Usuario WHERE id_usuario = '" + usuario.IdUsuario + "'");
            }
            catch (Exception ex) {
                throw ex;
            }

            return datos;
        }

        public bool eliminarUsuario(int id) {

            int respuesta = 0;
            try {
                respuesta = SQLHelper.GetInstance().Ejecutar("UPDATE Usuario SET ID_ESTADO = 4 WHERE ID_Usuario = " + id, false);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (respuesta > 0) return true;
            else return false;
        }

        public bool cambiarIdioma(int usuario, int idioma)
        {
            int respuesta = 0;
            try {
                respuesta = SQLHelper.GetInstance().Ejecutar("UPDATE Usuario SET Idioma_ID = " + idioma + " WHERE ID_Usuario = " + usuario, false);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (respuesta > 0) return true;
            else return false;
        }

        public bool modificarUsuario(BE.usuario usuario)
        {
            int respuesta = 0;
            try
            {
                respuesta = SQLHelper.GetInstance().Ejecutar("UPDATE Usuario SET usuario = '" + usuario.uss +  "', contraseña =  '" + usuario.pass + "', nombre = '" + usuario.nombre + "', apellido = '" + usuario.apellido +"', mail = '" + usuario.mail + "', direccion = '" + usuario.direccion + "', telefono = " + usuario.telefono + ", id_estado = " + usuario.IdEstado + ", documento = " + usuario.documento + ", intentos_fallidos = " + usuario.intentosFallidos + ", digito_verificador = '" + usuario.digitoVerificador + "' WHERE id_usuario = " + usuario.IdUsuario, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (respuesta > 0) return true;
            else return false;
        }

        public string obtenerHash(BE.usuario usuario) {

            string concat = string.Empty;
            DataTable datos = new DataTable();
            string hash = string.Empty;
            try {

                datos = SQLHelper.GetInstance().ObtenerDatos("SELECT usuario,contraseña,nombre,apellido,mail,direccion,telefono,id_estado, intentos_fallidos FROM Usuario WHERE id_usuario = '" + usuario.IdUsuario + "'");

                foreach (DataRow row in datos.Rows) {

                    foreach (DataColumn col in datos.Columns) {
                        
                        concat += row[col].ToString();
                    }
                }
            }

            catch (Exception ex) {

                throw ex;
            }

            return concat;
        }
    }
}
