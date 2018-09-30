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
        public BLL.seguridad seguridad = new BLL.seguridad();
        public BLL.usuario usuario = new BLL.usuario();
        public BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();

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

            string hash_nuevo = gestorDV.CacularDVV(usuario.listarTablaUsuarios());
            string hash_actual = gestorDV.ObtenerDVV("Usuario");
            BE.usuario user = new BE.usuario();
            user.uss = seguridad.ObtenerHash(txtUser.Text);
            user.pass = seguridad.ObtenerHash(txtPass.Text);

            bool validarUsuario = new BLL.login().loginUser(user);
            if (hash_nuevo == hash_actual)
            {
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
            else {
                MessageBox.Show("Inconsistencias en el Digito Verificador Vertical");
            }
            
            
        }
    }
}
