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
    public partial class gestionarFamilia : Form
    {
        public BE.usuario userLogin { get; set; }
        public BE.idioma idioma { get; set; }

        public BLL.bitacora gestorBitacora = new BLL.bitacora();
        public BLL.seguridad seguridad = new BLL.seguridad();
        public BLL.idioma gestorIdioma = new BLL.idioma();
        public BLL.familia gestorfamilia = new BLL.familia();
        public BLL.patente gestorPatente = new BLL.patente();
        public List<BE.familia> familias = new List<BE.familia>();

        public gestionarFamilia()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            
        }

        private void gestionarFamilia_Load(object sender, EventArgs e)
        {
            actualizarComboFamilias();
        }

        public void actualizarComboFamilias() {

            familias = gestorfamilia.listarFamilias();
            ComboBox1.Items.Clear();

            foreach (BE.familia familia in familias)
            {

                ComboBox1.Items.Add(familia.Familia);
            }
            ComboBox1.Sorted = true;

            if (ComboBox1.Items.Count > 0) {
                ComboBox1.SelectedIndex = 0;
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var editFamilia = new modificarFamilia();

            foreach (BE.familia f in familias)
            {
                if (f.Familia.Equals(ComboBox1.SelectedItem)) {

                    editFamilia.familia = f;
                    editFamilia.familia.patentes = gestorPatente.listarPatentes(f).Select(s => s.id_patente).ToList();
                }
            }
            editFamilia.userLogin = userLogin;
            editFamilia.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            nuevaFamilia nuevaFamilia = new nuevaFamilia();
            nuevaFamilia.userLogin = userLogin;
            nuevaFamilia.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            nuevaFamilia.idioma = idioma;
            nuevaFamilia.Show();
        }

        private void ChildFormClosing(object sender, FormClosingEventArgs e)
        {
            actualizarComboFamilias();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            bool del = false;
             foreach (BE.familia f in familias) {

                if (f.Familia.Equals(ComboBox1.SelectedItem)) {

                    try
                    {
                        f.patentes = gestorPatente.listarPatentes(f).Select(s => s.id_patente).ToList();
                        foreach (var p in f.patentes)

                            if (gestorPatente.validarZonaDeNadieFU(p, f.idFamilia))
                            {

                                del = true;
                            }

                        if (del)
                        {
                            MessageBox.Show("Existen usuarios con permisos unicos de esta familia");
                        }
                        else {
                            gestorfamilia.eliminarFamilia(f);
                            gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1010);
                            MessageBox.Show("Familia eliminada correctamente");
                        }

                        

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message.ToString());
                    }
                    
                }
            }

            actualizarComboFamilias();
        }
    }
}
