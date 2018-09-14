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
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void GestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var gesUser = new gestionarUsuario();
            gesUser.MdiParent = this;
            gesUser.Show();
        }

        private void GestionarBitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gesBitacora = new gestionarBitacora();
            gesBitacora.MdiParent = this;
            gesBitacora.Show();
        }

        private void CrearDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var crearDoc = new crearDocumento();
            crearDoc.MdiParent = this;
            crearDoc.Show();
        }

        private void BuscarDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var buscarDoc = new BuscarDocumento();
            buscarDoc.MdiParent = this;
            buscarDoc.Show();
        }

        private void AgregarEdicionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var agregarEdicion = new generarEdicion();
            agregarEdicion.MdiParent = this;
            agregarEdicion.Show();
        }

        private void GestionarFamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gestionarFamilia = new gestionarFamilia();
            gestionarFamilia.MdiParent = this;
            gestionarFamilia.Show();
        }

        private void DatosPersonalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var datosPersonales = new datosPersonales();
            datosPersonales.MdiParent = this;
            datosPersonales.Show();
        }

        private void CerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RealizarBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var backup = new Backup();
            backup.MdiParent = this;
            backup.Show();
        }

        private void RealizarRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var restore = new restore();
            restore.MdiParent = this;
            restore.Show();
        }

        private void main_Load(object sender, EventArgs e)
        {

        }
    }
}
