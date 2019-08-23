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
    public partial class datosPersonales : Form
    {
        public BE.usuario userLogin { get; set; }
        public List<BE.idioma> etiquetas { get; set; }
        public BE.idioma idioma { get; set; }

        BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        BLL.bitacora gestorBitacora = new BLL.bitacora();
        BLL.usuario gestorUsuario = new BLL.usuario();
        BLL.seguridad seguridad = new BLL.seguridad();
        BLL.encriptacion encriptacion = new BLL.encriptacion();
        BLL.idioma gestorIdioma = new BLL.idioma();

        public datosPersonales()
        {
            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show(etiquetas[10].etiqueta);
            }
        }

        private void datosPersonales_Load(object sender, EventArgs e) {

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            idioma.idMenu = 7;
            etiquetas = gestorIdioma.listarIdioma(idioma);

            Label11.Text = etiquetas[0].etiqueta;
            Label1.Text = etiquetas[1].etiqueta;
            Label2.Text = etiquetas[2].etiqueta;
            Label3.Text = etiquetas[3].etiqueta;
            Label4.Text = etiquetas[4].etiqueta;
            Label5.Text = etiquetas[5].etiqueta;
            Label6.Text = etiquetas[6].etiqueta;
            Button1.Text = etiquetas[7].etiqueta;
            Button2.Text = etiquetas[8].etiqueta;
            Button3.Text = etiquetas[9].etiqueta;

            TextBox1.Text = userLogin.nombre;
            TextBox2.Text = userLogin.apellido;
            TextBox3.Text = userLogin.direccion;
            TextBox4.Text = userLogin.documento.ToString();
            TextBox5.Text = userLogin.mail;
            TextBox6.Text = userLogin.telefono.ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            userLogin.nombre = TextBox1.Text;
            userLogin.apellido = TextBox2.Text;
            userLogin.direccion = TextBox3.Text;
            userLogin.documento = Convert.ToInt32(TextBox4.Text);
            userLogin.mail = TextBox5.Text;
            userLogin.telefono = Convert.ToInt32(TextBox6.Text);
            userLogin.digitoVerificador = seguridad.ObtenerHash(gestorUsuario.concatenarCampos(userLogin));

            try
            {
                gestorUsuario.modificarUsuario(userLogin);
                gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1006);
                MessageBox.Show(etiquetas[11].etiqueta);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            cambiarContraseña password = new cambiarContraseña();
            password.userLogin = userLogin;
            password.idioma = idioma;
            password.Show();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
