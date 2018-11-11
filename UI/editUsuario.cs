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
    public partial class editUsuario : Form
   {
        public BE.usuario usuarioMod { get; set; }
        public BE.usuario userLogin { get; set; }
        public List<BE.idioma> etiquetas { get; set; }
        public BE.idioma idioma { get; set; }

        BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        BLL.bitacora gestorBitacora = new BLL.bitacora();
        BLL.usuario gestorUsuario = new BLL.usuario();
        BLL.seguridad seguridad = new BLL.seguridad();
        BLL.encriptacion encriptacion = new BLL.encriptacion();
        BLL.idioma gestorIdioma = new BLL.idioma();

        public editUsuario()
        {
            InitializeComponent();
        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void editUsuario_Load(object sender, EventArgs e)
        {
            idioma.idMenu = 3;
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
            Label7.Text = etiquetas[10].etiqueta;
            Label9.Text = etiquetas[11].etiqueta;
            Button1.Text = etiquetas[12].etiqueta;
            Button2.Text = etiquetas[13].etiqueta;

            usuarioMod = gestorUsuario.obtenerUsuario(usuarioMod);
            TextBox1.Text = usuarioMod.nombre;
            TextBox2.Text = usuarioMod.apellido;
            TextBox3.Text = usuarioMod.direccion;
            TextBox4.Text = usuarioMod.documento.ToString();
            TextBox5.Text = usuarioMod.mail;
            TextBox6.Text = usuarioMod.telefono.ToString();
            //TextBox7.Text = usuarioMod.pass;
            TextBox8.Text = encriptacion.Decrypt(usuarioMod.uss);
            //TextBox9.Text = usuarioMod.pass;
        }

        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8) {

                e.Handled = true;
                MessageBox.Show("debes escribir solo numeros");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if ((TextBox7.Text != "" || TextBox9.Text != "") && TextBox7.Text != TextBox9.Text)
            {
                MessageBox.Show("las contraseñas deben coincidir");
            }
            else {
                if (TextBox7.Text != "") {

                    usuarioMod.uss = encriptacion.Encrypt(TextBox8.Text);
                    usuarioMod.pass = seguridad.ObtenerHash(TextBox7.Text);
                    usuarioMod.nombre = TextBox1.Text;
                    usuarioMod.apellido = TextBox2.Text;
                    usuarioMod.direccion = TextBox3.Text;
                    usuarioMod.documento = Convert.ToInt32(TextBox4.Text);
                    usuarioMod.mail = TextBox5.Text;
                    usuarioMod.telefono = Convert.ToInt32(TextBox6.Text);
                    usuarioMod.digitoVerificador = seguridad.ObtenerHash(gestorUsuario.concatenarCampos(usuarioMod));

                    if (gestorUsuario.modificarUsuario(usuarioMod)) {

                        gestorDV.modificarVerificador(gestorDV.CacularDVV(gestorUsuario.listarTablaUsuarios()), "Usuario");

                        gestorBitacora.agregarBitacora(userLogin.IdUsuario, 2);
                        gestorDV.modificarVerificador(gestorDV.CacularDVV(gestorBitacora.listarTablaBitacora()), "bitacora");

                        MessageBox.Show("Cliente modificado correctamente");
                        this.Close();
                    }
                    else {
                        MessageBox.Show("no se ha podido modificar el cliente");
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var patentes = new AsignarPatente();
            patentes.userLogin = userLogin;
            patentes.usuarioMod = usuarioMod;
            patentes.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            patentes.idioma = idioma;
            patentes.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var patentesN = new NegarPatente();
            patentesN.userLogin = userLogin;
            patentesN.usuarioMod = usuarioMod;
            patentesN.idioma = idioma;
            patentesN.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            patentesN.Show();
        }

        private void ChildFormClosing(object sender, FormClosingEventArgs e)
        {
            usuarioMod = gestorUsuario.obtenerUsuario(usuarioMod);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var asignarFamilia = new AsignarFamilia();
            asignarFamilia.userLogin = userLogin;
            asignarFamilia.usuarioMod = usuarioMod;
            asignarFamilia.idioma = idioma;
            asignarFamilia.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            asignarFamilia.Show();
        }
    }
}
