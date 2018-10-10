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

        public BE.usuario userLogin { get; set; }

        BLL.seguridad seguridad = new BLL.seguridad();
        BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        BLL.bitacora gestorBitacora = new BLL.bitacora();
        BLL.Backup backup = new BLL.Backup();

        public Backup()
        {
            InitializeComponent();
        }

        private void Backup_Load(object sender, EventArgs e) {

            saveFileDialog1.FileName = "C:\\Program Files\\Microsoft SQL Server\\MSSQL14.SQLEXPRESS\\MSSQL\\Backup";
            textBox1.Text = saveFileDialog1.FileName;
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
            saveFileDialog1.FileName += ("\\editorial" + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            backup.exportar("editorial", saveFileDialog1.FileName);

            BE.bitacora bitacora = new BE.bitacora();
            bitacora.idUsuario = userLogin.IdUsuario;
            bitacora.idEvento = 3;
            DateTime now = DateTime.Now;
            bitacora.FecEvento = now;
            bitacora.DigitoVerificador = seguridad.ObtenerHash(gestorBitacora.concatenarCampos(bitacora));
            gestorBitacora.agregarBitacora(bitacora);
            gestorDV.modificarVerificador(gestorDV.CacularDVV(gestorBitacora.listarTablaBitacora()), "bitacora");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
            {

                timer1.Enabled = false;
                progressBar1.Visible = false;

                MessageBox.Show("se ha exportado la base de datos correctamente");
                this.Close();
            }
            else {

                progressBar1.Value += 5;
            }  
        }
    }
}
