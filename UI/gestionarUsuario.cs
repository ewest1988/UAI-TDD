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
        List<BE.usuario> usuarios = new List<BE.usuario>();
        BLL.seguridad seguridad = new BLL.seguridad();
        BLL.encriptacion encriptacion = new BLL.encriptacion();
        BLL.usuario usuario = new BLL.usuario();
        DataTable dtUsuarios = new DataTable();

        public gestionarUsuario()
        {
            InitializeComponent();
        }

        private void gestionarUsuario_Load(object sender, EventArgs e)
        {
            dtUsuarios = usuario.listarTablaUsuarios();

            foreach (DataRow DR in dtUsuarios.Rows) {

                BE.usuario user = new BE.usuario();
                user.IdUsuario = Convert.ToInt32(DR["IdUsuario"]);
                user.nombre = DR["nombre"].ToString();
                ComboBox1.Items.Add(encriptacion.Decrypt(DR["usuario"].ToString()));
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var newUser = new nuevoUsuario();
            newUser.Show();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            foreach (BE.usuario uss in usuarios) {

                if (uss.uss = ComboBox1.SelectedText) {

                }
            }
            usuario.eliminarUsuario();
        }
    }
}
