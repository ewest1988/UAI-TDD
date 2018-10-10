using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class nuevoUsuario : Form
    {
        public BE.usuario userLogin { get; set; }
        public BLL.encriptacion encriptacion = new BLL.encriptacion();
        public BLL.bitacora gestorBitacora = new BLL.bitacora();
        public BLL.usuario usuario = new BLL.usuario();
        public BLL.seguridad seguridad = new BLL.seguridad();
        public BLL.digitoVerificador gestorDV = new BLL.digitoVerificador();

        public nuevoUsuario() {

            InitializeComponent();
        }

        private void nuevoUsuario_Load(object sender, EventArgs e) {

        }

        private string concatenar(object obj) {

            string cadena = "";

            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                cadena += propertyInfo.GetValue(obj);
            }

            return cadena;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            BE.usuario nuevoUsuario = new BE.usuario();
            BLL.usuario gestorUsuario = new BLL.usuario();

            if (TextBox7.Text != TextBox9.Text) {

                MessageBox.Show("Las contraseñas no coinciden");
            } else {

                nuevoUsuario.uss = encriptacion.Encrypt(TextBox8.Text);
                nuevoUsuario.pass = seguridad.ObtenerHash(TextBox7.Text);
                nuevoUsuario.nombre = TextBox1.Text;
                nuevoUsuario.apellido = TextBox2.Text;
                nuevoUsuario.direccion = TextBox3.Text;
                nuevoUsuario.IdEstado = 1;
                nuevoUsuario.mail = TextBox5.Text;
                nuevoUsuario.digitoVerificador = seguridad.ObtenerHash(usuario.concatenarCampos(nuevoUsuario));
                
                    try
                {
                    nuevoUsuario.documento = Convert.ToInt32(TextBox4.Text);
                    nuevoUsuario.telefono = Convert.ToInt32(TextBox6.Text);
                }

                catch (Exception)
                {

                    MessageBox.Show("el telefono y el documento deben ser numericos");
                }

                try
                {
                    gestorUsuario.agregarUsuario(nuevoUsuario);
                    gestorDV.modificarVerificador(gestorDV.CacularDVV(usuario.listarTablaUsuarios()), "Usuario");

                    BE.bitacora bitacora = new BE.bitacora();
                    bitacora.idEvento = 1;
                    bitacora.idUsuario = userLogin.IdUsuario;
                    DateTime now = DateTime.Now;
                    bitacora.FecEvento = now;
                    bitacora.DigitoVerificador = seguridad.ObtenerHash(gestorBitacora.concatenarCampos(bitacora));
                    gestorBitacora.agregarBitacora(bitacora);
                    gestorDV.modificarVerificador(gestorDV.CacularDVV(gestorBitacora.listarTablaBitacora()), "bitacora");

                    MessageBox.Show("Cliente guardado correctamente");
                    this.Owner.Refresh();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString()); 
                }

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
