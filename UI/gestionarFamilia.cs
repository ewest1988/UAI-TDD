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
        public List<BE.idioma> etiquetas { get; set; }

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

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show(etiquetas[8].etiqueta);
            }
        }

        private void gestionarFamilia_Load(object sender, EventArgs e)
        {
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            idioma.idMenu = 13;
            etiquetas = gestorIdioma.listarIdioma(idioma);

            Label1.Text = etiquetas[0].etiqueta;
            Label2.Text = etiquetas[1].etiqueta;
            Button2.Text = etiquetas[2].etiqueta;
            Button1.Text = etiquetas[3].etiqueta;
            Button3.Text = etiquetas[4].etiqueta;

            Button1.Enabled = false;
            Button2.Enabled = false;
            Button3.Enabled = false;

            if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(12))
            {
                //Alta de Familia
                Button2.Enabled = true;
            }

            if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(14))
            {
                //Modificar Familia
                Button1.Enabled = true;
            }

            if (userLogin.patentes.Union(userLogin.patentesFamilias).Except(userLogin.patentesNegadas).Contains(13))
            {
                //Eliminar Familia
                Button3.Enabled = true;
            }

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
            if (familias.Count > 0) {

                var editFamilia = new modificarFamilia();

                foreach (BE.familia f in familias)
                {
                    if (f.Familia.Equals(ComboBox1.SelectedItem))
                    {
                        editFamilia.familia = f;
                        editFamilia.familia.patentes = gestorPatente.listarPatentes(f).Select(s => s.id_patente).ToList();
                    }
                }

                editFamilia.idioma = idioma;
                editFamilia.userLogin = userLogin;
                editFamilia.Show();
            } else { MessageBox.Show(etiquetas[5].etiqueta); }
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
                            MessageBox.Show(etiquetas[6].etiqueta);
                        }
                        else {
                            gestorfamilia.eliminarFamilia(f);
                            gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1010);
                            MessageBox.Show(etiquetas[7].etiqueta);
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
