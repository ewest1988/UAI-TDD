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

        private void gestionarUsuario_Load(object sender, EventArgs e)
        {
            idioma.idMenu = 2;
            etiquetas = gestorIdioma.listarIdioma(idioma);

            Label1.Text = etiquetas[0].etiqueta;
            Label2.Text = etiquetas[1].etiqueta;
            Button2.Text = etiquetas[2].etiqueta;
            Button1.Text = etiquetas[3].etiqueta;
            Button6.Text = etiquetas[4].etiqueta;
            Button4.Text = etiquetas[5].etiqueta;
            Button3.Text = etiquetas[6].etiqueta;
            Button5.Text = etiquetas[7].etiqueta;

            usuarios = usuario.listarUsuarios();
            foreach (BE.usuario user in usuarios) {ComboBox1.Items.Add(user.uss);} 
            ComboBox1.SelectedIndex = 0;
        }

        private void Button2_Click(object sender, EventArgs e) {

            nuevoUsuario newUser = new nuevoUsuario();
            newUser.userLogin = userLogin;
            newUser.idioma = idioma;
            newUser.Owner = this;
            newUser.Show();
        }

        private void Button5_Click(object sender, EventArgs e) {

            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e) {

            if (ComboBox1.SelectedItem.ToString() == "admin") {

                MessageBox.Show(etiquetas[10].etiqueta);
            }
            else {

                foreach (BE.usuario uss in usuarios)
                {

                    if (uss.uss == ComboBox1.SelectedItem.ToString())
                    {

                        try
                        {
                            BE.usuario usuarioDel = new BE.usuario();
                            bool del = false;
                            usuarioDel = usuario.obtenerUsuario(encriptacion.Encrypt(uss.uss));

                            foreach(var p in usuarioDel.patentes) {

                                if (gestorPatente.validarZonaDeNadie(p, usuarioDel.IdUsuario)) {

                                    del = true;
                                }
                            }

                            if (del)
                            {

                                MessageBox.Show("no se puede eliminar el usuario. Existe un permiso asignado a el solo");
                            }
                            else {

                                usuario.eliminarUsuario(uss);
                                gestorDV.modificarVerificador(gestorDV.CacularDVV(usuario.listarTablaUsuarios()), "Usuario");
                                MessageBox.Show(etiquetas[11].etiqueta);
                                ComboBox1.Items.Remove(ComboBox1.SelectedItem);
                                ComboBox1.SelectedItem = 0;

                                gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1005);
                                gestorDV.modificarVerificador(gestorDV.CacularDVV(gestorBitacora.listarTablaBitacora()), "bitacora");
                            }
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }
            }

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

                userLogin.pass = seguridad.ObtenerHash("123456");
                userLogin.digitoVerificador = seguridad.ObtenerHash(usuario.concatenarCampos(userLogin));

                if (usuario.modificarUsuario(userLogin))
                {

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
