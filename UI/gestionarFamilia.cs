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
            familias = gestorfamilia.listarFamilias();

            foreach (BE.familia familia in familias) {

                ComboBox1.Items.Add(familia.Familia);
             }
            ComboBox1.SelectedIndex = 0;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var editFamilia = new modificarFamilia();

            foreach (BE.familia f in familias)
            {
                if (f.Familia.Equals(ComboBox1.SelectedItem)) {

                    editFamilia.familia = f;
                }
            }
            editFamilia.userLogin = userLogin;
            editFamilia.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            nuevaFamilia nuevaFamilia = new nuevaFamilia();
            nuevaFamilia.userLogin = userLogin;
            nuevaFamilia.idioma = idioma;
            nuevaFamilia.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            foreach (BE.familia f in familias) {

                if (f.Familia.Equals(ComboBox1.SelectedItem)) {

                    try
                    {
                        bool bf = gestorfamilia.eliminarFamilia(f);

                        if (bf)
                        {

                            gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1010);
                            MessageBox.Show("Familia eliminada correctamente");
                        }
                        else {

                            MessageBox.Show("Existen usuarios asignados a esta familia");
                        }

                        

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message.ToString());
                    }
                    
                }
            }
        }
    }
}
