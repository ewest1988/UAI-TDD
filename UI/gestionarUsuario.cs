using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class gestionarUsuario : Form
    {
        public BE.usuario userLogin { get; set; }
        
        List<BE.usuario> usuarios = new List<BE.usuario>();
        BE.usuario user = new BE.usuario();
        BLL.seguridad seguridad = new BLL.seguridad();
        BLL.encriptacion encriptacion = new BLL.encriptacion();
        BLL.usuario usuario = new BLL.usuario();
        BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        DataTable dtUsuarios = new DataTable();

        public gestionarUsuario()
        {

            InitializeComponent();
        }

        private void gestionarUsuario_Load(object sender, EventArgs e)
        {
            
            usuarios = usuario.listarUsuarios();

            foreach (BE.usuario user in usuarios) {

                ComboBox1.Items.Add(user.uss);
            } 
            
            ComboBox1.SelectedIndex = 0;
        }

        private void Button2_Click(object sender, EventArgs e) {

            nuevoUsuario newUser = new nuevoUsuario();
            newUser.userLogin = userLogin;
            newUser.Owner = this;
            newUser.Show();
        }

        private void Button5_Click(object sender, EventArgs e) {

            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e) {

            foreach (BE.usuario uss in usuarios) {

                if (uss.uss == ComboBox1.SelectedItem.ToString() && uss.uss != "admin") {

                    try {
                        usuario.eliminarUsuario(uss);
                        gestorDV.modificarVerificador(gestorDV.CacularDVV(usuario.listarTablaUsuarios()), "Usuario");
                        MessageBox.Show("Usuario eliminado correctamente");
                        ComboBox1.Items.Remove(ComboBox1.SelectedText);
                    }
                    catch (Exception ex) {

                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else {
                    MessageBox.Show("no se puede eliminar el usuario admin");
                }
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (BE.usuario uss in usuarios)
            {

                if (uss.uss == ComboBox1.SelectedItem.ToString())
                {

                    editUsuario editUsuario = new editUsuario();
                    editUsuario.usuarioMod = uss;
                    editUsuario.userLogin = userLogin;
                    editUsuario.Show();
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            user = usuario.obtenerUsuario(user);
            user.pass = encriptacion.Encrypt("123456");
            user.digitoVerificador = seguridad.ObtenerHash(usuario.concatenarCampos(user));

            if (usuario.modificarUsuario(user))
            {

                gestorDV.modificarVerificador(gestorDV.CacularDVV(usuario.listarTablaUsuarios()), "Usuario");
                MessageBox.Show("se ha reseteado la contraseña correctamente");
                this.Close();
            }
            else {
                MessageBox.Show("no se ha podido resetear la contraseña");
            }
        }
    }
}
