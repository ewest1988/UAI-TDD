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

        public gestionarBitacora()
        {
            InitializeComponent();
        }

        private void gestionarBitacora_Load(object sender, EventArgs e)
        {
            string hash_nuevo = gestorDV.CacularDVV(usuario.listarTablaUsuarios());
            string hash_actual = gestorDV.ObtenerDVV("Usuario");

            if (hash_actual == hash_nuevo) {

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

                    usuarios = usuario.listarUsuarios();
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
            else {

                MessageBox.Show("Inconsistencia en la tabla de bitacora");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
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

            listadoBitacora = gestorBitacora.listarBitacora(filtroBitacora);
            DataGridView1.DataSource = listadoBitacora.Select(x => new { Usuario = x.Usuario, Evento = x.evento, Fecha = x.FecEvento, Criticidad = x.criticidad }).ToList();
        }

        private void Button2_Click(object sender, EventArgs e) {

            List<int> listId = new List<int>();

            foreach (BE.bitacora bitacora in listadoBitacora) {
                listId.Add(bitacora.IdBitacora);
            }

            try {

                gestorBitacora.eliminarBitacora(listId);
                MessageBox.Show("eventos depurados correctamente");
                this.Close();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
