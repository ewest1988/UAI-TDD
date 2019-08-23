using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class BuscarDocumento : Form
    {
        BLL.documento gestorDocumento = new BLL.documento();
        List<BE.documento> documentos = new List<BE.documento>();
        List<BE.tipoDocumento> tiposDocumentos = new List<BE.tipoDocumento>();
        BE.documento doc = new BE.documento();
        List<BE.usuario> usuarios = new List<BE.usuario>();
        BLL.usuario gestorUsuario = new BLL.usuario();
        BE.filtroDocumento filtroDoc = new BE.filtroDocumento();

        public BuscarDocumento()
        {
            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show("Funcionalidad que permite Buscar documentos, visualizarlos, relacionarlos y generar una exportación de los mismos.", "Ayuda");
            }
        }

        private void BuscarDocumento_Load(object sender, EventArgs e)
        {
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            tiposDocumentos = gestorDocumento.listarTiposDocumentos();
            usuarios = gestorUsuario.listarTodos();

            foreach (var t in tiposDocumentos)
            {
                ComboBox1.Items.Add(t.descTipo);
            }

            foreach(var u in usuarios)
            {
                comboBox2.Items.Add(u.uss);
            }
            
        }

        private void Button5_Click(object sender, EventArgs e)
        {

           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null) {

                foreach (BE.usuario usuario in usuarios) {

                    if (usuario.uss == comboBox2.SelectedItem.ToString()) filtroDoc.idUsuario = usuario.IdUsuario;
                }
            }

            if (ComboBox1.SelectedItem != null) {

                foreach (BE.tipoDocumento tipo in tiposDocumentos) {

                    if (tipo.descTipo == ComboBox1.SelectedItem.ToString()) filtroDoc.idTipo = tipo.idTipo;
                }
            }

            if(TextBox1.Text != "") {

                filtroDoc.name = TextBox1.Text;
            }

            filtroDoc.fecCreacion = DateTimePicker1.Value;

            actualizarGrilla();
        }

        private void actualizarGrilla()
        {
            documentos = gestorDocumento.listarDocumentos(filtroDoc);
            var T = documentos.Select(x => {

                string tipo = "";

                foreach (var t in tiposDocumentos)
                {
                    if (x.idTipo == t.idTipo)
                        tipo = t.descTipo;
                }

                return new
                {
                    id = x.idDocumento,
                    tipoDoc = tipo,
                    nombre = x.nombre,
                    extension = x.extension
                };
            }).ToList();

            DataGridView1.DataSource = T;

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            doc.idDocumento = Convert.ToInt32(DataGridView1.Rows[e.RowIndex].Cells["id"].Value);
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (gestorDocumento.eliminarDocumento(doc)) {
                    MessageBox.Show("Documento eliminado correctamente");
                    actualizarGrilla();
                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            doc.idDocumento = Convert.ToInt32(DataGridView1.Rows[e.RowIndex].Cells["id"].Value);
            doc = gestorDocumento.listarDocumento(doc);

            FileStream f = File.Create(doc.nombre + "." + doc.extension);
            f.Close();
            File.WriteAllBytes(doc.nombre + "." + doc.extension, doc.contenido);
            Process.Start(doc.nombre + "." + doc.extension);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (var doc in documentos)
                {
                    string filename = doc.nombre + doc.extension;
                    if (!File.Exists(filename))
                    {

                        FileStream f = File.Create(filename);
                        f.Close();
                        File.WriteAllBytes(filename, doc.contenido);
                    }

                    files.Add(filename);
                }

                MessageBox.Show("Exportado correctamente en: " + gestorDocumento.exportar(files));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
