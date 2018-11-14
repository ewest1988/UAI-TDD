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
    public partial class crearDocumento : Form
    {
        public BE.usuario userLogin { get; set; }
        public BE.idioma idioma { get; set; }
        public List<BE.idioma> etiquetas { get; set; }

        BLL.documento gestorDocumento = new BLL.documento();
        BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        BLL.seguridad seguridad = new BLL.seguridad();
        BE.documento documento = new BE.documento();

        public crearDocumento()
        {
            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show("Desde aqui podrá crear el documento que desee.", "Ayuda");
            }
        }

        private void crearDocumento_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            List<BE.tipoDocumento> tiposDocumentos = new List<BE.tipoDocumento>();
            tiposDocumentos = gestorDocumento.listarTiposDocumentos();
            TextBox1.Enabled = false;

            foreach (var td in tiposDocumentos) {

                ComboBox1.Items.Add(td.descTipo);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem != null) {

                if (ComboBox1.SelectedItem.Equals("Nota Periodistica"))
                {

                    nota Nota = new nota();
                    Nota.Show();
                }
                else {
                    gestorDocumento.guardarDocumento(documento);
                }
            } 
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = openFileDialog1.FileName;
                FileInfo fi = new FileInfo(openFileDialog1.FileName);

                documento.contenido = File.ReadAllBytes(openFileDialog1.FileName);
                documento.extension = fi.Extension;
                documento.nombre = TextBox2.Text;
                documento.usuario.IdUsuario = userLogin.IdUsuario;
                documento.digitoVerificador = seguridad.ObtenerHash(gestorDocumento.concatenarCampos(documento));
            }
        }
    }
}
