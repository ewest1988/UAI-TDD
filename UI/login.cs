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
    public partial class login : Form
    {
        public BE.user user { get; set; }

        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            ComboBox1.Items.Add("ES");
            ComboBox1.Items.Add("EN");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            user = new BE.user();
            user.usuario = txtUser.Text;
            user.pass = txtPass.Text;
            bool validarUsuario = new BLL.login().loginUser(user);

            if (validarUsuario == true)
            {

                this.Hide();
                var main = new main();
                main.WindowState = FormWindowState.Maximized;
                main.Show();
            }
            else {
                MessageBox.Show("no se puede ingresar a la aplicacion");
            }
            
        }
    }
}
