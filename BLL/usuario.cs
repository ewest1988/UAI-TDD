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
        private DAL.usuario user = new DAL.usuario();
        private seguridad seguridad = new seguridad();
        private digitoVerificador digitoVerificador = new digitoVerificador();
        private encriptacion crypto = new encriptacion();

        internal List<BE.usuario> Listar_Usuarios()
        {
            List<BE.usuario> usuarios = new List<BE.usuario>();
            DataTable usuariosTabla = user.Listar_Usuarios();

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
            DataTable usuariosTabla = user.Listar_Usuarios();

            return usuariosTabla;
        }

        public bool Agregar_Usuario(BE.usuario usuario)
        {
            string verificador = seguridad.ObtenerHash(usuario.uss + usuario.pass);
            bool res = false;
            res = user.Agregar_Usuario(usuario);
            return res;
        }

        public bool Modificar_Usuario(BE.usuario usuario)
        {
            string usuario_hash = user.ObtenerHash(usuario.uss);
            BE.usuario usuario_actual = ObtenerUsuario(usuario.uss);
            string verificador = user.ObtenerVerificador(usuario.uss);
            bool res = digitoVerificador.VerificadorHorizontal(ConcatenarCampos(usuario_actual), verificador);

            if (res) {
                string hash_nuevo = seguridad.ObtenerHash(ConcatenarCampos(usuario));
                res = user.Modificar_Usuario(usuario);
                Actualizar_Verificador_Tabla();
                return true;
            }
            else
                return false;
        }

        public string ConcatenarCampos(BE.usuario usuario)
        {
            return usuario.uss + usuario.pass + usuario.nombre + usuario.apellido + usuario.mail + usuario.direccion + usuario.telefono + usuario.IdEstado + usuario.digitoVerificador;
        }

        public bool ValidarLogin(string usuario, string hash)
        {
            bool res = false;
            string contraseña = crypto.Encrypt(hash);
            res = user.ValidarLogin(usuario, contraseña);
            return res;
        }

        public BE.usuario ObtenerUsuario(string usuario)
        {
            BE.usuario miUsuario = new BE.usuario();
            DataTable datos = user.ObtenerUsuario(usuario);

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

        public bool Eliminar_Usuario(BE.usuario usuario) {
            bool res = false;

            try {
                res = user.Eliminar_Usuario(usuario.IdUsuario);
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
        //        resultado = user.Cambiar_Idioma(usuario.Id, idioma.Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Interaction.MsgBox(ex.ToString());
        //        return false;
        //    }
        //    return resultado;
        //}

        public void Actualizar_Verificador_Tabla() {
            var hash_nuevo = digitoVerificador.CacularDVV(listarTablaUsuarios());
            digitoVerificador.Modificar_Verificador(hash_nuevo, "Usuario");
        }
    }
}
