﻿using System;
using System.Data;

namespace DAL
{
    public class usuario
    {
        private SQLHelper sqlHelper = new SQLHelper();
        public DataTable Listar_Usuarios() {
            DataTable datos = new DataTable();

            try {
                datos = sqlHelper.ObtenerDatos("SELECT * FROM Usuario");
                return datos;
            }

            catch (Exception ex) {
                throw ex;
            }
        }

        public bool Agregar_Usuario(BE.usuario usuario) {
            int respuesta = 0;
            try {
                respuesta = sqlHelper.Ejecutar("INSERT INTO Usuario (usuario,contraseña,nombre,apellido,mail,direccion,telefono,id_estado,digito_verificador) VALUES ('" + usuario.uss + "','" + usuario.pass + "','" + usuario.nombre + "'," + usuario.apellido + ",'" + usuario.mail + "'," + usuario.direccion + ",'" + usuario.telefono + ",'" + usuario.IdEstado + ",'" + usuario.digitoVerificador + "')", false);
            } catch (Exception ex)
            {
                throw ex;
            }
            if (respuesta > 0) return true;
            else return false;
        }

        public bool ValidarLogin(string usuario, string contraseña)
        {
           string passbase = null;

            try {
                passbase = sqlHelper.EjecutarScalar("SELECT Clave FROM Usuario WHERE Usuario = '" + usuario + "'");
            }
            catch (Exception ex) {
                throw ex;
            }

            return (passbase == contraseña);
        }

        public DataTable ObtenerUsuario(string usuario)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = sqlHelper.ObtenerDatos("SELECT * FROM Usuario WHERE Usuario = '" + usuario + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }

        public string ObtenerVerificador(string usuario)
        {
            string datos = null;
            try {
                datos = sqlHelper.EjecutarScalar("SELECT Verificador FROM Usuario WHERE Usuario = '" + usuario + "'");
            }
            catch (Exception ex) {
                throw ex;
            }

            return datos;
        }

        public bool Eliminar_Usuario(int id) {

            int respuesta = 0;
            try {
                respuesta = sqlHelper.Ejecutar("DELETE FROM Usuario WHERE ID_Usuario = " + id, false);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (respuesta > 0) return true;
            else return false;
        }

        public bool Cambiar_Idioma(int usuario, int idioma)
        {
            int respuesta = 0;
            try {
                respuesta = sqlHelper.Ejecutar("UPDATE Usuario SET Idioma_ID = " + idioma + " WHERE ID_Usuario = " + usuario, false);
            }
            catch (Exception ex) {
                throw ex;
            }

            if (respuesta > 0) return true;
            else return false;
        }

        public bool Modificar_Usuario(BE.usuario usuario)
        {
            int respuesta = 0;
            try
            {
                respuesta = sqlHelper.Ejecutar("UPDATE Usuario SET usuario = " + usuario.uss +  ", contraseña =  " + usuario.pass + ", nombre = " + usuario.nombre + ", apellido = " + usuario.apellido +", mail = " + usuario.mail + ", direccion = " + usuario.direccion + ", telefono = " + usuario.telefono + ", id_estado = " + usuario.IdEstado + ", digito_verificador = " + usuario.digitoVerificador , false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (respuesta > 0) return true;
            else return false;
        }

        public string ObtenerHash(string usuario)
        {
            DataTable datos = new DataTable();
            string hash = string.Empty;
            try
            {
                datos = sqlHelper.ObtenerDatos("SELECT ID_Usuario,Nombre,Apellido,Usuario,Familia_ID,Clave,Idioma_ID FROM Usuario WHERE Usuario = '" + usuario + "'");
                foreach (DataRow row in datos.Rows)
                {
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return hash;
        }
    }
}
