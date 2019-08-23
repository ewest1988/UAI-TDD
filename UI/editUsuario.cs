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
        BLL.patente gestorPatente = new BLL.patente();

        public editUsuario()
        {
            InitializeComponent();
        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show(etiquetas[18].etiqueta);
            }
        }

        private void editUsuario_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

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
            Button1.Text = etiquetas[12].etiqueta;
            Button2.Text = etiquetas[13].etiqueta;
            button3.Text = etiquetas[14].etiqueta;
            button4.Text = etiquetas[15].etiqueta;
            button5.Text = etiquetas[16].etiqueta;
            button6.Text = etiquetas[17].etiqueta;

            usuarioMod = gestorUsuario.obtenerUsuario(usuarioMod);
            TextBox1.Text = usuarioMod.nombre;
            TextBox2.Text = usuarioMod.apellido;
            TextBox3.Text = usuarioMod.direccion;
            TextBox4.Text = usuarioMod.documento.ToString();
            TextBox5.Text = usuarioMod.mail;
            TextBox6.Text = usuarioMod.telefono.ToString();
            TextBox8.Text = encriptacion.Decrypt(usuarioMod.uss);

            if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(15))
            {
                //Asignar Permisos
                button3.Enabled = true;
            }

            if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(20))
            {
                //Asignar Familias
                button4.Enabled = true;
            }

            if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(21))
            {
                //Negar Permisos
                button5.Enabled = true;
            }

            if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(19))
            {
                //Bloquear/Desbloquear
                button6.Enabled = true;
            }
        }

        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8) {

                e.Handled = true;
                MessageBox.Show(etiquetas[19].etiqueta);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!TextBox8.Text.Equals(encriptacion.Decrypt(usuarioMod.uss)) && gestorUsuario.validarUsuario(TextBox8.Text))
            {

                MessageBox.Show(etiquetas[20].etiqueta);
            }

            else if (!gestorUsuario.IsValidEmail(TextBox5.Text))
            {

                MessageBox.Show(etiquetas[21].etiqueta);
            }

            else if (gestorUsuario.validarCorreo(TextBox5.Text, usuarioMod.IdUsuario))
            {

                MessageBox.Show(etiquetas[22].etiqueta);
            }

            else if (validarNulos())
            {

                MessageBox.Show(etiquetas[23].etiqueta);
            }
            else {

                usuarioMod.uss = encriptacion.Encrypt(TextBox8.Text);
                usuarioMod.nombre = TextBox1.Text;
                usuarioMod.apellido = TextBox2.Text;
                usuarioMod.direccion = TextBox3.Text;
                usuarioMod.documento = Convert.ToInt32(TextBox4.Text);
                usuarioMod.mail = TextBox5.Text;
                usuarioMod.telefono = Convert.ToInt32(TextBox6.Text);
                usuarioMod.digitoVerificador = seguridad.ObtenerHash(gestorUsuario.concatenarCampos(usuarioMod));

                if (gestorUsuario.modificarUsuario(usuarioMod)) {

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

        public bool validarNulos()
        {

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

        private void button6_Click(object sender, EventArgs e)
        {
            bool zn = false;
            if (usuarioMod.IdEstado == 1)
            {
                foreach (var p in usuarioMod.patentes.Union(usuarioMod.patentesFamilias).Except(usuarioMod.patentesNegadas)) {

                    if (gestorPatente.validarZonaDeNadie(p, usuarioMod.IdUsuario)) {

                        zn = true;
                    }
                }

                if (zn)
                {
                    MessageBox.Show("No se puede bloquear el usuario. Posee permisos unicos.");
                }
                else {

                    usuarioMod.IdEstado = 2;
                    gestorUsuario.modificarUsuario(usuarioMod);
                    MessageBox.Show("Usuario bloqueado correctamente");
                }
                
            }
            else {

                usuarioMod.IdEstado = 1;
                gestorUsuario.cambiarEstadoUsuario(usuarioMod);
                MessageBox.Show("Usuario desbloqueado correctamente");
            }
        }
    }
}
