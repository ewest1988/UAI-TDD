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
        public gestionarUsuario()
        {
            InitializeComponent();
        }

        private void gestionarUsuario_Load(object sender, EventArgs e)
        {
            BLL.usuario usuarios = new BLL.usuario();
            DataTable dtUsuarios = usuarios.listarTablaUsuarios();

            foreach (DataRow DR in dtUsuarios.Rows) {

                ComboBox1.Items.Add(DR["usuario"]);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var newUser = new nuevoUsuario();
            newUser.MdiParent = this;
            newUser.Show();
        }
    }
}
