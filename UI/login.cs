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
        public BLL.encriptacion encriptacion = new BLL.encriptacion();
        public BLL.seguridad seguridad = new BLL.seguridad();
        public BLL.usuario usuario = new BLL.usuario();
        public BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        public BLL.bitacora gestorBitacora = new BLL.bitacora();
        public BLL.idioma gestorIdioma = new BLL.idioma();
        public List<BE.idioma> etiquetas = new List<BE.idioma>();
        BE.idioma mainIdioma = new BE.idioma();

        public login()
        {
            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show("Ventana de inicio al sistema, si tiene usuario, por favor ingreselo.", "Ayuda");
            }
        }

        private void login_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            ComboBox1.Items.Add("ES");
            ComboBox1.Items.Add("EN");

            ComboBox1.SelectedIndex = 0;
            mainIdioma.idMenu = 4;
            mainIdioma.idLanguage = 1;
            mapearIdioma();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            string hash_nuevo = gestorDV.CacularDVV(usuario.listarTablaUsuarios());
            string hash_actual = gestorDV.ObtenerDVV("Usuario");
            BE.usuario userLogin = new BE.usuario();
            userLogin.uss = encriptacion.Encrypt(txtUser.Text);
            userLogin.pass = seguridad.ObtenerHash(txtPass.Text);

            try {

                bool validarUsuario = new BLL.login().loginUser(userLogin);

                if (hash_nuevo == hash_actual)
                {
                    if (validarUsuario == true)
                    {
                        this.Hide();
                        var main = new main();
                        userLogin = usuario.obtenerUsuario(userLogin.uss);

                        if (userLogin.IdEstado == 1)
                        {

                            gestorBitacora.agregarBitacora(userLogin.IdUsuario, 5);

                            main.userLogin = userLogin;
                            main.WindowState = FormWindowState.Maximized;

                            if (ComboBox1.SelectedItem.Equals("ES"))
                            {

                                mainIdioma.idLanguage = 1;
                            }
                            else { mainIdioma.idLanguage = 2; }

                            mainIdioma.idMenu = 1;

                            List<BE.idioma> idiomas = new List<BE.idioma>();

                            idiomas = gestorIdioma.listarIdioma(mainIdioma);

                            int i = 0;

                            foreach (ToolStripMenuItem masterToolStripMenuItem in main.MenuStrip1.Items)
                            {
                                foreach (ToolStripMenuItem master in masterToolStripMenuItem.DropDownItems)
                                {
                                    master.Text = idiomas[i].etiqueta;
                                    i += 1;
                                }
                            }
                            main.MenuStrip1.Items[0].Text = idiomas[10].etiqueta;
                            main.MenuStrip1.Items[1].Text = idiomas[11].etiqueta;

                            main.idioma = mainIdioma;
                            main.Show();
                        }

                        else { MessageBox.Show("usuario Bloqueado");
                                this.Show();
                        }
                    }

                    else {
                        MessageBox.Show(etiquetas[4].etiqueta);
                    }
                }

                else {
                    MessageBox.Show(etiquetas[5].etiqueta);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void mapearIdioma() {

            etiquetas = gestorIdioma.listarIdioma(mainIdioma);

            Label1.Text = etiquetas [0].etiqueta;
            Label2.Text = etiquetas [1].etiqueta;
            Button1.Text = etiquetas [2].etiqueta;
            Label3.Text = etiquetas [3].etiqueta;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainIdioma.idMenu = 4;

            if (ComboBox1.SelectedItem.Equals("ES")) {

                mainIdioma.idLanguage = 1;
            }
            else {
                mainIdioma.idLanguage = 2;
            }

            mapearIdioma();
        }

    }
}
