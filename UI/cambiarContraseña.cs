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
    public partial class cambiarContraseña : Form
    {
        public BE.usuario userLogin { get; set; }
        public List<BE.idioma> etiquetas { get; set; }
        public BE.idioma idioma { get; set; }
        public BLL.encriptacion encriptacion = new BLL.encriptacion();
        public BLL.login gestorLogin = new BLL.login();
        public BLL.usuario gestorUsuario = new BLL.usuario();
        public BLL.bitacora gestorBitacora = new BLL.bitacora();
        public BLL.idioma gestorIdioma = new BLL.idioma();
        public BLL.seguridad seguridad = new BLL.seguridad();

        public cambiarContraseña()
        {
            InitializeComponent();
        }

        private void cambiarContraseña_Load(object sender, EventArgs e)
        {
            idioma.idMenu = 8;
            etiquetas = gestorIdioma.listarIdioma(idioma);

            Label1.Text = etiquetas[0].etiqueta;
            Label2.Text = etiquetas[1].etiqueta;
            Label3.Text = etiquetas[2].etiqueta;
            Button1.Text = etiquetas[3].etiqueta;
            Button2.Text = etiquetas[4].etiqueta;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string passOriginal = encriptacion.Decrypt(userLogin.pass);
            userLogin.pass = seguridad.ObtenerHash(TextBox1.Text);
            if (gestorLogin.loginUser(userLogin))
            {

                if (TextBox2.Text.Equals("") || TextBox3.Text.Equals("") || !TextBox2.Text.Equals(TextBox3.Text))
                {
                    MessageBox.Show("las contraseñas deben coincidir");
                }
                else {

                    userLogin.pass = seguridad.ObtenerHash(TextBox2.Text);
                    gestorUsuario.modificarUsuario(userLogin);
                    gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1007);
                    MessageBox.Show("Contraseña modificada correctamente");
                }
            }
            else {
                MessageBox.Show("contraseña incorrecta");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
