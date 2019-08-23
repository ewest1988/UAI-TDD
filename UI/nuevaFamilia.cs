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
    public partial class nuevaFamilia : Form
    {
        public BE.usuario userLogin { get; set; }
        public BE.idioma idioma { get; set; }
        public List<BE.idioma> etiquetas { get; set; }

        public BLL.bitacora gestorBitacora = new BLL.bitacora();
        public BLL.seguridad seguridad = new BLL.seguridad();
        public BLL.idioma gestorIdioma = new BLL.idioma();
        public BLL.familia gestorfamilia = new BLL.familia();

        public nuevaFamilia()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try {

                if (TextBox1.Text != "")
                {

                    BE.familia familia = new BE.familia();
                    familia.Familia = TextBox1.Text;

                    gestorfamilia.nuevaFamilia(familia);
                    gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1009);
                    MessageBox.Show(etiquetas[0].etiqueta);
                    this.Close();
                }
                else {

                    MessageBox.Show(etiquetas[1].etiqueta);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show(etiquetas[2].etiqueta);
            }
        }

        private void nuevaFamilia_Load(object sender, EventArgs e)
        {
            idioma.idMenu = 16;
            etiquetas = gestorIdioma.listarIdioma(idioma);

            Label1.Text = etiquetas[3].etiqueta;
            Button1.Text = etiquetas[4].etiqueta;
            button2.Text = etiquetas[5].etiqueta;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);
        }
    }
}
