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

        BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        BLL.bitacora gestorBitacora = new BLL.bitacora();
        BLL.usuario gestorUsuario = new BLL.usuario();
        BLL.seguridad seguridad = new BLL.seguridad();
        BLL.encriptacion encriptacion = new BLL.encriptacion(); 

        public editUsuario()
        {
            InitializeComponent();
        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void editUsuario_Load(object sender, EventArgs e)
        {
            usuarioMod = gestorUsuario.obtenerUsuario(usuarioMod);
            TextBox1.Text = usuarioMod.nombre;
            TextBox2.Text = usuarioMod.apellido;
            TextBox3.Text = usuarioMod.direccion;
            TextBox4.Text = usuarioMod.documento.ToString();
            TextBox5.Text = usuarioMod.mail;
            TextBox6.Text = usuarioMod.telefono.ToString();
            TextBox7.Text = encriptacion.Decrypt(usuarioMod.pass);
            TextBox8.Text = encriptacion.Decrypt(usuarioMod.uss);
            TextBox9.Text = encriptacion.Decrypt(usuarioMod.pass);

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8) {

                e.Handled = true;
                MessageBox.Show("solo numeros se aceptan");
            }
        }

        private void TextBox4_keyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8)
            {

                e.Handled = true;
                MessageBox.Show("solo numeros se aceptan");
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
                    usuarioMod.pass = encriptacion.Encrypt(TextBox7.Text);
                    usuarioMod.nombre = TextBox1.Text;
                    usuarioMod.apellido = TextBox2.Text;
                    usuarioMod.direccion = TextBox3.Text;
                    usuarioMod.documento = Convert.ToInt32(TextBox4.Text);
                    usuarioMod.mail = TextBox5.Text;
                    usuarioMod.telefono = Convert.ToInt32(TextBox6.Text);
                    usuarioMod.digitoVerificador = seguridad.ObtenerHash(gestorUsuario.concatenarCampos(usuarioMod));

                    if (gestorUsuario.modificarUsuario(usuarioMod)) {

                        gestorDV.modificarVerificador(gestorDV.CacularDVV(gestorUsuario.listarTablaUsuarios()), "Usuario");

                        BE.bitacora bitacora = new BE.bitacora();
                        bitacora.idUsuario = userLogin.IdUsuario;
                        bitacora.idEvento = 2;
                        DateTime now = DateTime.Now;
                        bitacora.FecEvento = now;
                        bitacora.DigitoVerificador = seguridad.ObtenerHash(gestorBitacora.concatenarCampos(bitacora));
                        gestorBitacora.agregarBitacora(bitacora);
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
    }
}
