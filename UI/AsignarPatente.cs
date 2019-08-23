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
        BLL.idioma gestorIdioma = new BLL.idioma();
        BLL.patente gestorPatente = new BLL.patente();
        BLL.bitacora gestorBitacora = new BLL.bitacora();
        BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        List<BE.patente> patentes = new List<BE.patente>();

        public AsignarPatente()
        {
            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show(etiquetas[0].etiqueta);
            }
        }

        private void AsignarPatente_Load(object sender, EventArgs e) {

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            idioma.idMenu = 10;
            etiquetas = gestorIdioma.listarIdioma(idioma);

            Label1.Text = etiquetas[1].etiqueta;
            Button2.Text = etiquetas[2].etiqueta;
            Button1.Text = etiquetas[3].etiqueta;
            Label2.Text = etiquetas[4].etiqueta;

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

                    MessageBox.Show(etiquetas[5].etiqueta);
                }
                else {

                    gestorPatente.modificarPatentes(nuevasPatentes, usuarioMod);
                    usuarioMod = gestorUsuario.obtenerUsuario(usuarioMod);
                    gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1011);
                    gestorDV.modificarVerificador(gestorDV.CacularDVV("Usuario_Patente"), "Usuario_Patente");
                    MessageBox.Show(etiquetas[6].etiqueta);
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
