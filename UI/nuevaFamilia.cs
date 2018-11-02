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
                    MessageBox.Show("Nueva familia creada correctamente");
                    this.Close();
                }
                else {

                    MessageBox.Show("debe escribir un nombre para la nueva Familia");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void nuevaFamilia_Load(object sender, EventArgs e)
        {

        }
    }
}
