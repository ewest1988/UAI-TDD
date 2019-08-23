using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;

namespace UI
{
    public partial class gestionarUsuario : Form
    {
        public BE.usuario userLogin { get; set; }
        public BE.idioma idioma { get; set; }
        public List<BE.idioma> etiquetas { get; set; }

        List<BE.usuario> usuarios = new List<BE.usuario>();
        BLL.seguridad seguridad = new BLL.seguridad();
        BLL.encriptacion encriptacion = new BLL.encriptacion();
        BLL.usuario usuario = new BLL.usuario();
        BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        BLL.bitacora gestorBitacora = new BLL.bitacora();
        BLL.idioma gestorIdioma = new BLL.idioma();
        BLL.patente gestorPatente = new BLL.patente();
        DataTable dtUsuarios = new DataTable();

        public gestionarUsuario()
        {

            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show(etiquetas[14].etiqueta);
            }
        }

        private void gestionarUsuario_Load(object sender, EventArgs e)
        {
            Button1.Enabled = false;
            Button2.Enabled = false;
            Button3.Enabled = false;
            Button4.Enabled = false;

            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            idioma.idMenu = 2;
            etiquetas = gestorIdioma.listarIdioma(idioma);

            Label1.Text = etiquetas[0].etiqueta;
            Label2.Text = etiquetas[1].etiqueta;
            Button2.Text = etiquetas[2].etiqueta;
            Button1.Text = etiquetas[3].etiqueta;
            Button4.Text = etiquetas[5].etiqueta;
            Button3.Text = etiquetas[6].etiqueta;
            Button5.Text = etiquetas[7].etiqueta;

            toolTip1.SetToolTip(this.ComboBox1, etiquetas[16].etiqueta);
            toolTip1.SetToolTip(this.Button2, etiquetas[17].etiqueta);
            toolTip1.SetToolTip(this.Button1, etiquetas[18].etiqueta);
            toolTip1.SetToolTip(this.Button3, etiquetas[19].etiqueta);
            toolTip1.SetToolTip(this.Button4, etiquetas[20].etiqueta);
            toolTip1.SetToolTip(this.Button5, etiquetas[21].etiqueta);

            actualizarCombo();

            if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(9))
            {
                //Alta de Usuario
                Button2.Enabled = true;
            }

            if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(11))
            {
                //Modificar Usuario
                Button1.Enabled = true;
            }

            if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(10))
            {
                //Eliminar Usuario
                Button3.Enabled = true;
            }

            if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(18))
            {
                //Resetear Contraseña
                Button4.Enabled = true;
            }
        }

        private void Button2_Click(object sender, EventArgs e) {

            nuevoUsuario newUser = new nuevoUsuario();
            newUser.userLogin = userLogin;
            newUser.idioma = idioma;
            newUser.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            newUser.Owner = this;
            newUser.Show();
        }

        private void Button5_Click(object sender, EventArgs e) {

            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e) {

            foreach (BE.usuario uss in usuarios) {

                if (uss.uss == ComboBox1.SelectedItem.ToString()) {

                    try {
                        BE.usuario usuarioDel = new BE.usuario();
                        bool del = false;
                        usuarioDel = usuario.obtenerUsuario(encriptacion.Encrypt(uss.uss));

                        foreach(var p in usuarioDel.patentes) {

                            if (gestorPatente.validarZonaDeNadie(p, usuarioDel.IdUsuario)) {

                                del = true;
                            }
                        }

                        if (del) {

                            MessageBox.Show(etiquetas[15].etiqueta);
                        }
                        else {

                            usuario.eliminarUsuario(uss);
                            gestorDV.modificarVerificador(gestorDV.CacularDVV(usuario.listarTablaUsuarios()), "Usuario");
                            MessageBox.Show(etiquetas[11].etiqueta);
                            gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1005);
                            
                        }
                    } catch (Exception ex) {

                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            actualizarCombo();
        }

        private void ChildFormClosing(object sender, FormClosingEventArgs e)
        {
            actualizarCombo();
        }

        private void actualizarCombo() {

            usuarios = usuario.listarUsuarios();
            ComboBox1.Items.Clear();
            foreach (BE.usuario user in usuarios) { ComboBox1.Items.Add(user.uss); }
            ComboBox1.Sorted = true;
            ComboBox1.SelectedIndex = 0;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (BE.usuario uss in usuarios)
            {

                if (uss.uss == ComboBox1.SelectedItem.ToString())
                {

                    editUsuario editUsuario = new editUsuario();
                    editUsuario.usuarioMod = uss;
                    editUsuario.userLogin = userLogin;
                    editUsuario.idioma = idioma;
                    editUsuario.Show();
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(etiquetas[9].etiqueta + " " + ComboBox1.SelectedItem + "?", etiquetas[8].etiqueta, MessageBoxButtons.YesNo) == DialogResult.Yes) {

                BE.usuario usuarioSel = new BE.usuario();
                usuarioSel = usuario.generarPassword(ComboBox1.SelectedItem.ToString());

                //modifico el usuario en la base de datos
                if (usuario.modificarUsuario(usuarioSel))
                {
                    //actualizo el digito verificador
                    gestorDV.modificarVerificador(gestorDV.CacularDVV(usuario.listarTablaUsuarios()), "Usuario");
                    MessageBox.Show(etiquetas[13].etiqueta);
                    this.Close();
                }
                else {
                    MessageBox.Show(etiquetas[12].etiqueta);
                }
            }
        }
    }
}
