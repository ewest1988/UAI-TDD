using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class usuario
    {
        private DAL.usuario usuarioDatos = new DAL.usuario();
        private seguridad seguridad = new seguridad();
        private digitoVerificador digitoVerificador = new digitoVerificador();
        private encriptacion crypto = new encriptacion();

        internal List<BE.usuario> Listar_Usuarios()
        {
            List<BE.usuario> usuarios = new List<BE.usuario>();
            DataTable usuariosTabla = usuarioDatos.listarUsuarios();

            if (usuariosTabla.Rows.Count > 0)
            {
                foreach (DataRow reg in usuariosTabla.Rows)
                {
                    BE.usuario usuario = new BE.usuario();
                    usuario.uss = reg["usuario"].ToString();
                    usuario.pass = reg["pass"].ToString();

                    usuarios.Add(usuario);
                }
            }

            return usuarios;
        }

        public DataTable listarTablaUsuarios()
        {
            DataTable usuariosTabla = usuarioDatos.listarUsuarios();

            return usuariosTabla;
        }

        public bool agregarUsuario(BE.usuario usuario)
        {
            string verificador = seguridad.ObtenerHash(usuario.uss + usuario.pass);
            
            return usuarioDatos.agregarUsuario(usuario);
        }

        public bool modificarUsuario(BE.usuario usuario)
        {
            string usuario_hash = usuarioDatos.obtenerHash(usuario.uss);
            BE.usuario usuario_actual = obtenerUsuario(usuario.uss);
            string verificador = usuarioDatos.obtenerVerificador(usuario.uss);
            bool res = digitoVerificador.VerificadorHorizontal(concatenarCampos(usuario_actual), verificador);

            if (res) {
                string hash_nuevo = seguridad.ObtenerHash(concatenarCampos(usuario));
                res = usuarioDatos.modificarUsuario(usuario);
                actualizarVerificadorTabla();
                return true;
            }
            else
                return false;
        }

        public string concatenarCampos(BE.usuario usuario)
        {
            return usuario.uss + usuario.pass + usuario.nombre + usuario.apellido + usuario.mail + usuario.direccion + usuario.telefono + usuario.IdEstado;
        }

        public bool validarLogin(string usuario, string hash)
        {
            bool res = false;
            string contraseña = crypto.Encrypt(hash);
            res = usuarioDatos.validarLogin(usuario, contraseña);
            return res;
        }

        public BE.usuario obtenerUsuario(string usuario)
        {
            BE.usuario miUsuario = new BE.usuario();
            DataTable datos = usuarioDatos.obtenerUsuario(usuario);

            if (datos.Rows.Count > 0)
            {
                foreach (DataRow reg in datos.Rows)
                {
                    miUsuario.uss = reg["usuario"].ToString();
                    miUsuario.pass = reg["contraseña"].ToString();
                    miUsuario.nombre = reg["nombre"].ToString();
                    miUsuario.apellido = reg["apellido"].ToString();
                    miUsuario.mail = reg["mail"].ToString();
                    miUsuario.direccion = reg["direccion"].ToString();
                    miUsuario.telefono = Convert.ToInt32(reg["telefono"]);
                    miUsuario.IdEstado = Convert.ToInt32(reg["id_estado"]);
                    miUsuario.digitoVerificador = reg["digito_verificador"].ToString();



                }   
            }

            return miUsuario;
        }

        public bool eliminarUsuario(BE.usuario usuario) {
            bool res = false;

            try {
                res = usuarioDatos.eliminarUsuario(usuario.IdUsuario);
            }
            catch (Exception ex) {
                throw ex;
            }
            return res;
        }

        //public bool Cambiar_Idioma(BE.usuario usuario, Idioma idioma)
        //{
        //    bool resultado;
        //    try
        //    {
        //        resultado = usuarioDatos.Cambiar_Idioma(usuario.Id, idioma.Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Interaction.MsgBox(ex.ToString());
        //        return false;
        //    }
        //    return resultado;
        //}

        public void actualizarVerificadorTabla() {
            var hash_nuevo = digitoVerificador.CacularDVV(listarTablaUsuarios());
            digitoVerificador.modificarVerificador(hash_nuevo, "Usuario");
        }
    }
}
