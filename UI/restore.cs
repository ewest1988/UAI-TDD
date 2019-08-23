using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class restore : Form
    {
        public BE.usuario userLogin { get; set; }
        public BE.idioma idioma { get; set; }
        public List<BE.idioma> etiquetas { get; set; }
        public BLL.idioma gestorIdioma = new BLL.idioma();
        BLL.seguridad seguridad = new BLL.seguridad();
        BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        BLL.bitacora gestorBitacora = new BLL.bitacora();
        BLL.Backup backup = new BLL.Backup();

        public restore()
        {
            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show(etiquetas[0].etiqueta);
            }
        }

        private void restore_Load(object sender, EventArgs e)
        {
            idioma.idMenu = 17;
            etiquetas = gestorIdioma.listarIdioma(idioma);

            Label1.Text = etiquetas[1].etiqueta;
            button1.Text = etiquetas[2].etiqueta;
            Button2.Text = etiquetas[3].etiqueta;
            button3.Text = etiquetas[4].etiqueta;

            textBox1.Enabled = false;
            openFileDialog1.Filter = "Zip Files|*.zip";
        }

        private void Button2_Click(object sender, EventArgs e) {

            try {

                if (backup.importar(openFileDialog1.FileName))
                {

                    gestorBitacora.agregarBitacora(userLogin.IdUsuario, 4);
                    gestorDV.modificarVerificador(gestorDV.CacularDVV(gestorBitacora.listarTablaBitacora()), "bitacora");

                    MessageBox.Show(etiquetas[5].etiqueta);
                    this.Close();
                }
                else {

                    MessageBox.Show(etiquetas[6].etiqueta);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }
    }
}
