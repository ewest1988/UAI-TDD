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
    public partial class AsignarFamilia : Form
    {
        public BE.usuario usuarioMod { get; set; }
        public BE.usuario userLogin { get; set; }
        public List<BE.idioma> etiquetas { get; set; }
        public BE.idioma idioma { get; set; }

        BLL.usuario gestorUsuario = new BLL.usuario();
        BLL.patente gestorPatente = new BLL.patente();
        BLL.familia gestorFamilia = new BLL.familia();
        BLL.bitacora gestorBitacora = new BLL.bitacora();
        List<BE.familia> familias = new List<BE.familia>();

        public AsignarFamilia()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show("En esta opción usted podrá asignarle familias al usuario seleccionado.", "Ayuda");
            }
        }

        private void AsignarFamilia_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            familias = gestorFamilia.listarFamilias();

            int i = -1;

            foreach (BE.familia f in familias)
            {

                checkedListBox1.Items.Add(f.Familia);
                i += 1;

                foreach (int fa in usuarioMod.familias)
                {

                    if (f.idFamilia == fa)
                    {

                        checkedListBox1.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            List<BE.familia> nuevasFamilias = new List<BE.familia>();
            bool del = false;

            if (checkedListBox1.CheckedItems.Count != 0)
            {
                for (int x = 0; x < checkedListBox1.CheckedItems.Count; x++)
                {

                    foreach (BE.familia f in familias)
                    {

                        if (checkedListBox1.CheckedItems[x].ToString() == f.Familia)
                        {

                            nuevasFamilias.Add(f);
                        }
                    }
                }
            }

            try
            {
                foreach (var f in usuarioMod.familias
                                    .Where(w => !nuevasFamilias.Select(s => s.idFamilia).Contains(w)).ToList())
                {
                    BE.familia familia = new BE.familia();
                    familia.idFamilia = f;
                    familia.patentes = gestorPatente.listarPatentes(familia).Select(s => s.id_patente).ToList();

                    foreach (var p in familia.patentes)

                    if (gestorPatente.validarZonaDeNadieFU(p, familia.idFamilia))
                    {

                        del = true;
                    }
                }

                if (del)
                {

                    MessageBox.Show("no se pueden modificar dicha relacion Usuario-Familia. Existe un permiso unico");
                }
                else {

                    gestorFamilia.modificarFamilias(nuevasFamilias, usuarioMod);
                    usuarioMod = gestorUsuario.obtenerUsuario(usuarioMod);
                    gestorBitacora.agregarBitacora(userLogin.IdUsuario, 1011);
                    MessageBox.Show("Familias modificadas correctamente");
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
