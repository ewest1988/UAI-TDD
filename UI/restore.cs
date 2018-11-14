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
                MessageBox.Show("Funcionalidad que permite realizar una restauración de la base de datos.", "Ayuda");
            }
        }

        private void restore_Load(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\\Program Files\\Microsoft SQL Server\\MSSQL14.SQLEXPRESS\\MSSQL\\Backup");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*editorial*"); //Getting Text files

            foreach (FileInfo file in Files) { ComboBox1.Items.Add(file); }
        }

        private void Button2_Click(object sender, EventArgs e) {

            try {

                backup.importar("editorial", "C:\\Program Files\\Microsoft SQL Server\\MSSQL14.SQLEXPRESS\\MSSQL\\Backup\\" + ComboBox1.SelectedItem);

                gestorBitacora.agregarBitacora(userLogin.IdUsuario, 4);
                gestorDV.modificarVerificador(gestorDV.CacularDVV(gestorBitacora.listarTablaBitacora()), "bitacora");

                MessageBox.Show("importacion realizada con exito");
                this.Close();

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
    }
}
