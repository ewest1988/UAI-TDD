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
        public BE.usuario userLogin { get; set; }
        public BLL.bitacora gestorBitacora = new BLL.bitacora();
        public BLL.seguridad seguridad = new BLL.seguridad();

        public main()
        {
            InitializeComponent();
        }

        private void GestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var gesUser = new gestionarUsuario();
            gesUser.userLogin = userLogin;
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
            BE.bitacora bitacora = new BE.bitacora();
            bitacora.idUsuario = userLogin.IdUsuario;
            bitacora.idEvento = 6;
            DateTime now = DateTime.Now;
            bitacora.FecEvento = now;
            bitacora.DigitoVerificador = seguridad.ObtenerHash(gestorBitacora.concatenarCampos(bitacora));
            gestorBitacora.agregarBitacora(bitacora);

            login login = new login();
            login.Show();
            this.Close();
        }

        private void RealizarBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var backup = new Backup();
            backup.MdiParent = this;
            backup.userLogin = userLogin;
            backup.Show();
        }

        private void RealizarRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var restore = new restore();
            restore.MdiParent = this;
            restore.userLogin = userLogin;
            restore.Show();
        }

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void HerramientasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
