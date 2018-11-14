﻿using System;
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
        public BE.idioma idioma { get; set; }

        public BLL.bitacora gestorBitacora = new BLL.bitacora();
        public BLL.seguridad seguridad = new BLL.seguridad();
        public BLL.idioma gestorIdioma = new BLL.idioma();

        public main()
        {
            InitializeComponent();
        }

        private void GestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var gesUser = new gestionarUsuario();
            gesUser.userLogin = userLogin;
            gesUser.idioma = idioma;
            gesUser.MdiParent = this;

            gesUser.Show();
        }

        private void GestionarBitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gesBitacora = new gestionarBitacora();
            gesBitacora.MdiParent = this;
            gesBitacora.userLogin = userLogin;
            gesBitacora.idioma = idioma;
            gesBitacora.Show();
        }

        private void CrearDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var crearDoc = new crearDocumento();
            crearDoc.MdiParent = this;
            crearDoc.userLogin = userLogin;
            crearDoc.idioma = idioma;
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
            gestionarFamilia.userLogin = userLogin;
            gestionarFamilia.idioma = idioma;
            gestionarFamilia.Show();
        }

        private void DatosPersonalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var datosPersonales = new datosPersonales();
            datosPersonales.MdiParent = this;
            datosPersonales.userLogin = userLogin;
            datosPersonales.idioma = idioma;
            datosPersonales.Show();
        }

        private void CerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gestorBitacora.agregarBitacora(userLogin.IdUsuario, 6);

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
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            foreach (ToolStripMenuItem masterToolStripMenuItem in MenuStrip1.Items)
            {
                var items = masterToolStripMenuItem.DropDownItems;
                foreach (ToolStripMenuItem master in masterToolStripMenuItem.DropDownItems)
                {
                    if (master.Text.Equals("Gestionar Usuarios") || master.Text.Equals("Manage Users"))
                    {
                        if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(1)) master.Visible = true; else master.Visible = false;
                    }

                    if (master.Text.Equals("Gestionar Bitacora") || master.Text.Equals("Manage Log"))
                    {
                        if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(2)) master.Visible = true; else master.Visible = false;
                    }

                    if (master.Text.Equals("Crear Documento") || master.Text.Equals("Create Document"))
                    {
                        if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(3)) master.Visible = true; else master.Visible = false;
                    }

                    if (master.Text.Equals("Buscar Documento") || master.Text.Equals("Search Document"))
                    {
                        if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(4)) master.Visible = true; else master.Visible = false;
                    }

                    if (master.Text.Equals("Crear Edicion") || master.Text.Equals("Create Edition"))
                    {
                        if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(5)) master.Visible = true; else master.Visible = false;
                    }

                    if (master.Text.Equals("Gestionar Familia") || master.Text.Equals("Manage Family"))
                    {
                        if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(6)) master.Visible = true; else master.Visible = false;
                    }

                    if (master.Text.Equals("Resguardo") || master.Text.Equals("Backup"))
                    {
                        if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(6)) master.Visible = true; else master.Visible = false;
                    }

                    if (master.Text.Equals("Restaurar") || master.Text.Equals("Restore"))
                    {
                        if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(6)) master.Visible = true; else master.Visible = false;
                    }

                }
            }
         }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show("Este es el menu principal de la pantalla, desde aqui puede elegir las opciones " + 
                                "que tenga habilitadas.","Ayuda");
            }
        }

        private void HerramientasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
