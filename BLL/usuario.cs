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
        private DAL.familia familiaDatos = new DAL.familia();
        private patente gestorPatente = new patente();
        private familia gestorFamilia = new familia();
        private seguridad seguridad = new seguridad();
        private digitoVerificador digitoVerificador = new digitoVerificador();
        private encriptacion crypto = new encriptacion();

        public List<BE.usuario> listarUsuarios()
        {
            List<BE.usuario> usuarios = new List<BE.usuario>();
            DataTable usuariosTabla = usuarioDatos.listarUsuarios();

            if (usuariosTabla.Rows.Count > 0)
            {
                foreach (DataRow reg in usuariosTabla.Rows)
                {
                    BE.usuario usuario = new BE.usuario();
                    usuario.IdUsuario = Convert.ToInt32(reg["id_usuario"]);
                    usuario.uss = crypto.Decrypt(reg["usuario"].ToString());
                    usuario.pass = reg["contraseña"].ToString();

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
            string usuario_hash = usuarioDatos.obtenerHash(usuario);
            BE.usuario usuario_actual = obtenerUsuario(usuario);
            string verificador = usuarioDatos.obtenerVerificador(usuario);
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

        public bool resetPassword(BE.usuario usuario)
        {
            string usuario_hash = usuarioDatos.obtenerHash(usuario);
            BE.usuario usuario_actual = obtenerUsuario(usuario);
            string verificador = usuarioDatos.obtenerVerificador(usuario);
            bool res = digitoVerificador.VerificadorHorizontal(concatenarCampos(usuario_actual), verificador);

            if (res)
            {
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

        public bool validarLogin(string usuario, string contraseña)
        {
            bool res = false;
            res = usuarioDatos.validarLogin(usuario, contraseña);
            return res;
        }

        public BE.usuario obtenerUsuario(BE.usuario usuario)
        {
            BE.usuario miUsuario = new BE.usuario();
            miUsuario = mapper(usuarioDatos.obtenerUsuario(usuario));
            miUsuario.patentes = usuarioDatos.obtenerPatentes(miUsuario);
            miUsuario.patentesFamilias = usuarioDatos.obtenerPatentesFamilia(miUsuario);
            miUsuario.patentesNegadas = usuarioDatos.obtenerPatentesNegadas(miUsuario);
            miUsuario.familias = usuarioDatos.obtenerFamilias(miUsuario);

            return miUsuario;
        }

        public BE.usuario obtenerUsuario(string usuario)
        {
            BE.usuario miUsuario = new BE.usuario();
            miUsuario = mapper(usuarioDatos.obtenerUsuario(usuario));
            miUsuario.patentes = usuarioDatos.obtenerPatentes(miUsuario);
            miUsuario.patentesFamilias = usuarioDatos.obtenerPatentesFamilia(miUsuario);
            miUsuario.patentesNegadas = usuarioDatos.obtenerPatentesNegadas(miUsuario);
            miUsuario.familias = usuarioDatos.obtenerFamilias(miUsuario);

            return miUsuario;
        }

        public bool eliminarUsuario(BE.usuario usuario) {

            try {
                gestorPatente.eliminarPatentesUsuario(usuario);
                gestorPatente.eliminarPatentesNegadasUsuario(usuario);
                gestorFamilia.eliminarFamiliasUsuario(usuario);

                usuarioDatos.eliminarUsuario(usuario.IdUsuario);

                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void actualizarVerificadorTabla() {
            var hash_nuevo = digitoVerificador.CacularDVV(listarTablaUsuarios());
            digitoVerificador.modificarVerificador(hash_nuevo, "Usuario");
        }

        public BE.usuario mapper(DataTable tablaUsuario) {

            BE.usuario miUsuario = new BE.usuario();
            if (tablaUsuario.Rows.Count > 0)
            {
                foreach (DataRow reg in tablaUsuario.Rows)
                {
                    miUsuario.IdUsuario = Convert.ToInt32(reg["id_usuario"]);
                    miUsuario.uss = reg["usuario"].ToString();
                    miUsuario.pass = reg["contraseña"].ToString();
                    miUsuario.nombre = reg["nombre"].ToString();
                    miUsuario.apellido = reg["apellido"].ToString();
                    miUsuario.documento = Convert.ToInt32(reg["documento"]);
                    miUsuario.mail = reg["mail"].ToString();
                    miUsuario.direccion = reg["direccion"].ToString();
                    miUsuario.telefono = Convert.ToInt32(reg["telefono"]);
                    miUsuario.IdEstado = Convert.ToInt32(reg["id_estado"]);
                    miUsuario.digitoVerificador = reg["digito_verificador"].ToString();
                }
            }

            return miUsuario;
        }
    }
}
