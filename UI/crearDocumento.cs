using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class crearDocumento : Form
    {
        public BE.usuario userLogin { get; set; }
        public BE.idioma idioma { get; set; }
        public List<BE.idioma> etiquetas { get; set; }

        BLL.documento gestorDocumento = new BLL.documento();
        BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();
        BLL.seguridad seguridad = new BLL.seguridad();
        BE.documento documento = new BE.documento();
        List<BE.tipoDocumento> tiposDocumentos = new List<BE.tipoDocumento>();

        public crearDocumento()
        {
            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show("Desde aqui podrá crear el documento que desee.", "Ayuda");
            }
        }

        private void crearDocumento_Load(object sender, EventArgs e)
        {
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);

            tiposDocumentos = gestorDocumento.listarTiposDocumentos();
            TextBox1.Enabled = false;

            foreach (var td in tiposDocumentos) {

                ComboBox1.Items.Add(td.descTipo);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem != null) {

                if (ComboBox1.SelectedItem.Equals("Nota Periodistica"))
                {

                    nota Nota = new nota();
                    Nota.Show();
                }
                else {
                    try
                    {
                        foreach(var t in tiposDocumentos)
                        {
                            if (t.descTipo == ComboBox1.SelectedItem)
                            {
                                documento.idTipo = t.idTipo;
                            }
                        }

                        if (gestorDocumento.guardarDocumento(documento))
                        {

                            MessageBox.Show("Documento guardado correctamente");
                            this.Close();

                        } else
                        {
                            MessageBox.Show("no se ha podido guardar el documento.");
                        }
                        
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            } 
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem.Equals("Imagen"))
            {
                openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            } else if (ComboBox1.SelectedItem.Equals("Audio"))
            {
                openFileDialog1.Filter = "All Supported Audio | *.mp3; *.wma | MP3s | *.mp3 | WMAs | *.wma";
            }
            else 
            {
                openFileDialog1.Filter = "All Videos Files | *.dat; *.wmv; *.3g2; *.3gp; *.3gp2; *.3gpp; *.amv; *.asf; *.avi; *.bin; *.cue; *.divx; *.dv; *.flv; *.gxf; *.iso; *.m1v; *.m2v; *.m2t; *.m2ts; *.m4v; " +
                  " *.mkv; *.mov; *.mp2; *.mp2v; *.mp4; *.mp4v; *.mpa; *.mpe; *.mpeg; *.mpeg1; *.mpeg2; *.mpeg4; *.mpg; *.mpv2; *.mts; *.nsv; *.nuv; *.ogg; *.ogm; *.ogv; *.ogx; *.ps; *.rec; *.rm; *.rmvb; *.tod; *.ts; *.tts; *.vob; *.vro; *.webm";
                
            }

            openFileDialog1.ShowDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = openFileDialog1.FileName;
                FileInfo fi = new FileInfo(openFileDialog1.FileName);

                documento.contenido = File.ReadAllBytes(openFileDialog1.FileName);
                documento.extension = fi.Extension;
                documento.nombre = TextBox2.Text;
                documento.usuario.IdUsuario = userLogin.IdUsuario;
                documento.digitoVerificador = seguridad.ObtenerHash(gestorDocumento.concatenarCampos(documento));
            }
        }
    }
}
