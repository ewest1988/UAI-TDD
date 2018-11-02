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
    public partial class modificarFamilia : Form
    {
        public BE.usuario userLogin { get; set; }
        public BE.idioma idioma { get; set; }
        public BE.familia familia { get; set; }

        public BLL.bitacora gestorBitacora = new BLL.bitacora();
        public BLL.seguridad seguridad = new BLL.seguridad();
        public BLL.idioma gestorIdioma = new BLL.idioma();
        public BLL.familia gestorFamilia = new BLL.familia();
        public BLL.patente gestorPatente = new BLL.patente();
        List<BE.patente> patentes = new List<BE.patente>();
        List<BE.patente> patentesFamilia = new List<BE.patente>();

        public modificarFamilia()
        {
            InitializeComponent();
        }

        private void modificarFamilia_Load(object sender, EventArgs e)
        {
            patentes = gestorPatente.listarPatentes();
            patentesFamilia = gestorPatente.listarPatentes(familia);

            int i = -1;

            foreach (BE.patente p in patentes) {

                checkedListBox1.Items.Add(p.descPatente);
                i += 1;
                foreach (BE.patente pf in patentesFamilia) {

                    if (p.id_patente == pf.id_patente) {

                        checkedListBox1.SetItemChecked(i, true);                   }
                }
            } 
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            List<BE.patente> nuevasPatentes = new List<BE.patente>();

            if (checkedListBox1.CheckedItems.Count != 0)
            {
                for (int x = 0; x < checkedListBox1.CheckedItems.Count; x++) {

                    foreach (BE.patente p in patentes) {

                        if (checkedListBox1.CheckedItems[x].ToString() == p.descPatente) {

                            nuevasPatentes.Add(p);
                        }
                    }
                }
            }

            try {

                gestorFamilia.modificarFamilia(nuevasPatentes, familia);
                gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1008);
                MessageBox.Show("Patentes modificadas correctamente");
                this.Close();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
