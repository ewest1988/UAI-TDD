using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class nuevoUsuario : Form
    {
        public BE.usuario userLogin { get; set; }
        public BE.idioma idioma { get; set; }
        public List<BE.idioma> etiquetas { get; set; }

        public BLL.encriptacion encriptacion = new BLL.encriptacion();
        public BLL.bitacora gestorBitacora = new BLL.bitacora();
        public BLL.usuario usuario = new BLL.usuario();
        public BLL.seguridad seguridad = new BLL.seguridad();
        public BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        public BLL.idioma gestorIdioma = new BLL.idioma();

        public nuevoUsuario() {

            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show(etiquetas[14].etiqueta);
            }
        }

        private void nuevoUsuario_Load(object sender, EventArgs e) {

            idioma.idMenu = 5;
            etiquetas = gestorIdioma.listarIdioma(idioma);

            Label10.Text = etiquetas[0].etiqueta;
            Label11.Text = etiquetas[1].etiqueta;
            Label1.Text = etiquetas[2].etiqueta;
            Label2.Text = etiquetas[3].etiqueta;
            Label3.Text = etiquetas[4].etiqueta;
            Label4.Text = etiquetas[5].etiqueta;
            Label5.Text = etiquetas[6].etiqueta;
            Label6.Text = etiquetas[7].etiqueta;
            Label12.Text = etiquetas[8].etiqueta;
            Label8.Text = etiquetas[9].etiqueta;
            Button1.Text = etiquetas[12].etiqueta;
            Button2.Text = etiquetas[13].etiqueta;
        }

        private string concatenar(object obj) {

            string cadena = "";

            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                cadena += propertyInfo.GetValue(obj);
            }

            return cadena;
        }

        private void number_keyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8)
            {

                e.Handled = true;
                MessageBox.Show(etiquetas[15].etiqueta);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            BE.usuario nuevoUsuario = new BE.usuario();
            BLL.usuario gestorUsuario = new BLL.usuario();

            if (!gestorUsuario.IsValidEmail(TextBox5.Text))
            {

                MessageBox.Show(etiquetas[16].etiqueta);
            }

            else if (gestorUsuario.validarCorreo(TextBox5.Text))
            {

                MessageBox.Show(etiquetas[17].etiqueta);
            }

            else if (gestorUsuario.validarUsuario(TextBox8.Text))
            {

                MessageBox.Show(etiquetas[18].etiqueta);
            }

            else if (validarNulos()) {

                MessageBox.Show(etiquetas[19].etiqueta);
            }
            else {

                nuevoUsuario.uss = encriptacion.Encrypt(TextBox8.Text);
                nuevoUsuario.nombre = TextBox1.Text;
                nuevoUsuario.apellido = TextBox2.Text;
                nuevoUsuario.direccion = TextBox3.Text;
                nuevoUsuario.documento = Convert.ToInt32(TextBox4.Text);
                nuevoUsuario.telefono = Convert.ToInt32(TextBox6.Text);
                nuevoUsuario.IdEstado = 1;
                nuevoUsuario.mail = TextBox5.Text;

                nuevoUsuario = usuario.generarPassword(nuevoUsuario);

                try
                {
                    gestorUsuario.agregarUsuario(nuevoUsuario);
                    gestorDV.modificarVerificador(gestorDV.CacularDVV("Usuario"), "Usuario");

                    gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1);
                    gestorDV.modificarVerificador(gestorDV.CacularDVV(gestorBitacora.listarTablaBitacora()), "bitacora");

                    MessageBox.Show(etiquetas[20].etiqueta);
                    this.Owner.Refresh();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
        }

        public bool validarNulos() {

            if (TextBox1.Text == null ||
                TextBox1.Text.Trim() == "" ||
                TextBox2.Text == null ||
                TextBox2.Text.Trim() == "" ||
                TextBox3.Text == null ||
                TextBox3.Text.Trim() == "" ||
                TextBox4.Text == null ||
                TextBox4.Text.Trim() == "" ||
                TextBox5.Text == null ||
                TextBox5.Text.Trim() == "" ||
                TextBox6.Text == null ||
                TextBox6.Text.Trim() == "" ||
                TextBox8.Text == null ||
                TextBox8.Text.Trim() == "") return true;
            else return false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
