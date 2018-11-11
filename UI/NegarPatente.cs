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
    public partial class NegarPatente : Form
    {
        public BE.usuario usuarioMod { get; set; }
        public BE.usuario userLogin { get; set; }
        public List<BE.idioma> etiquetas { get; set; }
        public BE.idioma idioma { get; set; }

        BLL.usuario gestorUsuario = new BLL.usuario();
        BLL.patente gestorPatente = new BLL.patente();
        BLL.bitacora gestorBitacora = new BLL.bitacora();
        List<BE.patente> patentes = new List<BE.patente>();

        public NegarPatente()
        {
            InitializeComponent();
        }

        private void AsignarPatente_Load(object sender, EventArgs e) {

            
            patentes = gestorPatente.listarPatentes();

            int i = -1;

            foreach (BE.patente p in patentes) {

                checkedListBox1.Items.Add(p.descPatente);
                i += 1;

                foreach (int pu in usuarioMod.patentesNegadas) {

                    if (p.id_patente == pu) {

                        checkedListBox1.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e) {

            List<int> nuevasPatentes = new List<int>();
            bool del = false;

            if (checkedListBox1.CheckedItems.Count != 0)
            {
                for (int x = 0; x < checkedListBox1.CheckedItems.Count; x++)
                {

                    foreach (BE.patente p in patentes) {

                        if (checkedListBox1.CheckedItems[x].ToString() == p.descPatente) {

                            nuevasPatentes.Add(p.id_patente);
                        }
                    }
                }
            }

            try {

                foreach (var p in usuarioMod.patentes.Where(w => nuevasPatentes.Contains(w)).ToList())
                {

                    if (gestorPatente.validarZonaDeNadiePN(p, usuarioMod.IdUsuario))
                    {

                        del = true;
                    }
                }

                if (del)
                {

                    MessageBox.Show("no se pueden negar dichas patentes. Existe un permiso asignado a el solo");
                }
                else {

                    gestorPatente.modificarPatentesNegadas(nuevasPatentes, usuarioMod);
                    gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1012);
                    MessageBox.Show("Patentes negadas modificadas correctamente");

                    this.Close();
                }

            }

            catch (Exception ex) {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddStock_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            this.Close();
            MessageBox.Show("Tries to close");

        }
    }
}
