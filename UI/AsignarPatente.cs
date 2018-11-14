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
    public partial class AsignarPatente : Form
    {
        public BE.usuario usuarioMod { get; set; }
        public BE.usuario userLogin { get; set; }
        public List<BE.idioma> etiquetas { get; set; }
        public BE.idioma idioma { get; set; }

        BLL.usuario gestorUsuario = new BLL.usuario();
        BLL.patente gestorPatente = new BLL.patente();
        BLL.bitacora gestorBitacora = new BLL.bitacora();
        List<BE.patente> patentes = new List<BE.patente>();

        public AsignarPatente()
        {
            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show("En esta opción usted podrá asignarle permisos al usuario seleccionado.", "Ayuda");
            }
        }

        private void AsignarPatente_Load(object sender, EventArgs e) {

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            patentes = gestorPatente.listarPatentes();

            int i = -1;

            foreach (BE.patente p in patentes) {

                checkedListBox1.Items.Add(p.descPatente);
                i += 1;

                foreach (int pu in usuarioMod.patentes) {

                    if (p.id_patente == pu) {

                        checkedListBox1.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e) {

            var del = false;
            List<int> nuevasPatentes = new List<int>();

            if (checkedListBox1.CheckedItems.Count != 0)
            {
                for (int x = 0; x < checkedListBox1.CheckedItems.Count; x++) {

                    foreach (BE.patente p in patentes) {

                        if (checkedListBox1.CheckedItems[x].ToString() == p.descPatente) {

                            nuevasPatentes.Add(p.id_patente);
                        }
                    }
                }
            }

            try
            {
                foreach (var p in usuarioMod.patentes.Where(w => !nuevasPatentes.Contains(w)).ToList()) {

                    if (gestorPatente.validarZonaDeNadie(p, usuarioMod.IdUsuario)) {

                        del = true;
                    }
                }

                if (del) {

                    MessageBox.Show("no se pueden modificar dichas patentes. Existe un permiso asignado a el solo");
                }
                else {

                    gestorPatente.modificarPatentes(nuevasPatentes, usuarioMod);
                    usuarioMod = gestorUsuario.obtenerUsuario(usuarioMod);
                    gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1011);
                    MessageBox.Show("Patentes modificadas correctamente");
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
