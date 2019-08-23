using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class gestionarBitacora : Form
    {
        public BE.usuario userLogin { get; set; }
        public BE.idioma idioma { get; set; }
        public List<BE.idioma> etiquetas { get; set; }

        BLL.bitacora gestorBitacora = new BLL.bitacora();
        BLL.usuario usuario = new BLL.usuario();
        List<BE.usuario> usuarios = new List<BE.usuario>();
        List<BE.evento> eventos = new List<BE.evento>();
        List<BE.criticidad> criticidades = new List<BE.criticidad>();
        List<BE.bitacora> listadoBitacora = new List<BE.bitacora>();
        BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        BLL.idioma gestorIdioma = new BLL.idioma();
        DataTable resultado = new DataTable();
        private bool sortAscending = false;

        public gestionarBitacora()
        {
            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show(etiquetas[15].etiqueta);
            }
        }

        private void gestionarBitacora_Load(object sender, EventArgs e)
        {
            DateTimePicker1.Format = DateTimePickerFormat.Custom;
            DateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            DateTimePicker2.Format = DateTimePickerFormat.Custom;
            DateTimePicker2.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            try {

                    idioma.idMenu = 6;
                    etiquetas = gestorIdioma.listarIdioma(idioma);

                    Label5.Text = etiquetas[0].etiqueta;
                    Label1.Text = etiquetas[1].etiqueta;
                    Label2.Text = etiquetas[2].etiqueta;
                    Label3.Text = etiquetas[3].etiqueta;
                    Label4.Text = etiquetas[4].etiqueta;
                    Label6.Text = etiquetas[5].etiqueta;
                    Button1.Text = etiquetas[6].etiqueta;
                    Button2.Text = etiquetas[7].etiqueta;
                    Button3.Text = etiquetas[8].etiqueta;
                    button4.Text = etiquetas[13].etiqueta;

                usuarios = usuario.listarTodos();
                    eventos = gestorBitacora.listarEventos();
                    criticidades = gestorBitacora.listarCriticidad();

                    foreach (BE.usuario user in usuarios) { ComboBox1.Items.Add(user.uss); }
                    foreach (BE.evento evento in eventos) { ComboBox2.Items.Add(evento.descripcion); }
                    foreach (BE.criticidad critico in criticidades) { ComboBox3.Items.Add(critico.descripcion); }
                }
                catch (Exception ex) {

                    MessageBox.Show(ex.Message.ToString());
                }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DataTable resultadoTabla = new DataTable();
            BE.filtroBitacora filtroBitacora = new BE.filtroBitacora();

            if (ComboBox1.SelectedItem != null) {

                foreach (BE.usuario usuario in usuarios) {

                    if (usuario.uss == ComboBox1.SelectedItem.ToString()) filtroBitacora.idUsuario = usuario.IdUsuario;
                }
                
            }

            if (ComboBox2.SelectedItem != null) {

                foreach (BE.evento evento in eventos) {

                    if (evento.descripcion == ComboBox2.SelectedItem.ToString()) filtroBitacora.idEvento = evento.idEvento;
                }
            }

            if (ComboBox3.SelectedItem != null) {

                foreach (BE.criticidad critico in criticidades) {

                    if (critico.descripcion == ComboBox3.SelectedItem.ToString()) filtroBitacora.idCriticidad = critico.idCriticidad;
                }
            }

            filtroBitacora.fecDesde = DateTimePicker1.Value;
            filtroBitacora.fecHasta = DateTimePicker2.Value;
            resultado = gestorBitacora.tablaBitacora(filtroBitacora);
            listadoBitacora = gestorBitacora.listarBitacora(filtroBitacora);
            resultado.Columns.Remove("digito_verificador");
            DataGridView1.DataSource = resultado;

            DataGridView1.Columns["ID_BITACORA"].Visible = false;
            //DataGridView1.Columns["digito_verificador"].Visible = false;
            DataGridView1.Columns["usuario"].HeaderText = etiquetas[9].etiqueta; ;
            DataGridView1.Columns["desc_evento"].HeaderText = etiquetas[10].etiqueta;
            DataGridView1.Columns["fec_evento"].HeaderText = etiquetas[11].etiqueta;
            DataGridView1.Columns["desc_criticidad"].HeaderText = etiquetas[12].etiqueta;
        }

        private void Button2_Click(object sender, EventArgs e) {

            List<int> listId = new List<int>();

            if (listadoBitacora.Count > 0) {

                foreach (BE.bitacora bitacora in listadoBitacora)
                {
                    listId.Add(bitacora.IdBitacora);
                }

                try
                {

                    gestorBitacora.eliminarBitacora(listId);
                    MessageBox.Show(etiquetas[14].etiqueta);
                    this.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sortAscending)

                DataGridView1.Sort(DataGridView1.Columns[e.ColumnIndex], System.ComponentModel.ListSortDirection.Ascending);
            else
                DataGridView1.Sort(DataGridView1.Columns[e.ColumnIndex], System.ComponentModel.ListSortDirection.Descending);
            sortAscending = !sortAscending;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string resultName = "bitacora_" + DateTime.Now.ToString("yyyy-MM-ddHHmmss") + ".pdf";

            List<String> columns = new List<string>();
            columns.Add("ID");
            columns.Add(etiquetas[9].etiqueta);
            columns.Add(etiquetas[10].etiqueta);
            columns.Add(etiquetas[11].etiqueta);
            columns.Add(etiquetas[12].etiqueta);

            if (DataGridView1.RowCount > 0) {

                new BLL.BitacoraPDF().ExportarPDFARuta("bitacora", columns, DataGridView1.Rows.Cast<Object>().ToList(), resultName);
                SendToPrinter(resultName);
            }
        }

        private void SendToPrinter(string name)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = name;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();
        }
    }
}
