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
    public partial class Backup : Form {

        public BE.usuario usuarioMod { get; set; }
        public BE.usuario userLogin { get; set; }
        public List<BE.idioma> etiquetas { get; set; }
        public BE.idioma idioma { get; set; }

        BLL.idioma gestorIdioma = new BLL.idioma();
        BLL.seguridad seguridad = new BLL.seguridad();
        BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        BLL.bitacora gestorBitacora = new BLL.bitacora();
        BLL.Backup backup = new BLL.Backup();

        public Backup()
        {
            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show(etiquetas[4].etiqueta);
            }
        }

        private void Backup_Load(object sender, EventArgs e) {

            idioma.idMenu = 11;
            etiquetas = gestorIdioma.listarIdioma(idioma);

            Label1.Text = etiquetas[0].etiqueta;
            label2.Text = etiquetas[1].etiqueta;
            Button2.Text = etiquetas[2].etiqueta;
            button3.Text = etiquetas[3].etiqueta;

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);
            folderBrowserDialog1.ShowDialog();
            //FileName = "C:\\Program Files\\Microsoft SQL Server\\MSSQL14.SQLEXPRESS\\MSSQL\\Backup";
            textBox1.Text = folderBrowserDialog1.SelectedPath;

            for (int i = 1; i < 5; i++) {

                comboBox1.Items.Add(i);
            }

            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Button1_Click(object sender, EventArgs e) {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e) {

            timer1.Enabled = true;
            timer1.Start();
            progressBar1.Visible = true;
            backup.exportar(folderBrowserDialog1.SelectedPath, Convert.ToInt32(comboBox1.SelectedItem));

            gestorBitacora.agregarBitacora(userLogin.IdUsuario, 3);
            gestorDV.modificarVerificador(gestorDV.CacularDVV(gestorBitacora.listarTablaBitacora()), "bitacora");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
            {

                timer1.Enabled = false;
                progressBar1.Visible = false;

                MessageBox.Show(etiquetas[5].etiqueta);
                this.Close();
            }
            else {

                progressBar1.Value += 5;
            }  
        }
    }
}
