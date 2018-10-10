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
        public BLL.idioma idioma = new BLL.idioma();

        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            ComboBox1.Items.Add("ES");
            ComboBox1.Items.Add("EN");

            ComboBox1.SelectedIndex = 0;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            string hash_nuevo = gestorDV.CacularDVV(usuario.listarTablaUsuarios());
            string hash_actual = gestorDV.ObtenerDVV("Usuario");
            BE.usuario user = new BE.usuario();
            user.uss = encriptacion.Encrypt(txtUser.Text);
            user.pass = encriptacion.Encrypt(txtPass.Text);

            try {

                bool validarUsuario = new BLL.login().loginUser(user);

                if (hash_nuevo == hash_actual)
                {
                    if (validarUsuario == true)
                    {

                        this.Hide();
                        var main = new main();
                        user = usuario.obtenerUsuario(user.uss);

                        BE.bitacora bitacora = new BE.bitacora();
                        bitacora.idUsuario = user.IdUsuario;
                        bitacora.idEvento = 5; 
                        DateTime now = DateTime.Now;
                        bitacora.FecEvento = now;
                        bitacora.DigitoVerificador = seguridad.ObtenerHash(gestorBitacora.concatenarCampos(bitacora));
                        gestorBitacora.agregarBitacora(bitacora);

                        main.userLogin = user;
                        main.WindowState = FormWindowState.Maximized;

                        BE.idioma mainIdioma = new BE.idioma();

                        if (ComboBox1.SelectedItem.Equals("ES")) {

                            mainIdioma.idLanguage = 1;
                        }
                        else { mainIdioma.idLanguage = 2; }
                        
                        mainIdioma.idMenu = 1;

                        List<BE.idioma> idiomas = new List<BE.idioma>();

                        idiomas = idioma.listarIdioma(mainIdioma);

                        int i = 0;

                        foreach (ToolStripMenuItem masterToolStripMenuItem in main.MenuStrip1.Items)
                        {
                            foreach ( ToolStripMenuItem master in masterToolStripMenuItem.DropDownItems)
                            {
                                master.Text = idiomas[i].etiqueta;
                                i += 1;
                            }
                        }
                        main.MenuStrip1.Items[0].Text = idiomas[10].etiqueta;
                        main.MenuStrip1.Items[1].Text = idiomas[11].etiqueta;
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
