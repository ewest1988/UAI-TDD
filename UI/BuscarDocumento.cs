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
        public BuscarDocumento()
        {
            InitializeComponent();
        }

        private void BuscarDocumento_Load(object sender, EventArgs e)
        {
            List<BE.documento> documentos = new List<BE.documento>();
            List<documentosVista> dVista = new List<documentosVista>();
            DataGridViewLinkColumn link = new DataGridViewLinkColumn();
            DataGridViewLinkCell cell = new DataGridViewLinkCell();
            List<string> ls = new List<string>();

            documentos = gestorDocumento.listarDocumentos();

            var T = documentos.Select(x => new {id = x.idTipo, nombre = x.nombre, extension = x.extension });

            foreach (var v in T) {

                documentosVista d = new documentosVista();
                d.nombre = v.nombre;
                d.extension = v.extension;
                d.id = v.id;

                dVista.Add(d);
                
                
            }

            DataGridView1.DataSource = dVista;
            DataGridView1.Columns.Add(link);     
        }

        private void Button5_Click(object sender, EventArgs e)
        {

            //int id = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells["id"].Value.ToString());
            //string nombre = DataGridView1.SelectedRows[0].Cells["nombre"].Value.ToString();
            //string extension = DataGridView1.SelectedRows[0].Cells["extension"].Value.ToString();

            BE.documento doc = new BE.documento();
            doc.idDocumento = 2;
            doc = gestorDocumento.listarDocumento(doc);

            FileStream f = File.Create("C:\\Users\\P045922\\Documents\\miprueba2.png");
            f.Close();
            File.WriteAllBytes("C:\\Users\\P045922\\Documents\\miprueba2.png", doc.contenido);
            Process.Start("C:\\Users\\P045922\\Documents\\miprueba2.png");
        }
    }
}
