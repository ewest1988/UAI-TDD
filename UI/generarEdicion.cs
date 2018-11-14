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
    public partial class generarEdicion : Form
    {
        public generarEdicion()
        {
            InitializeComponent();
        }

        public void myKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show("En esta opcion puede generar una nueva edición de la editorial.", "Ayuda");
            }
        }

        private void generarEdicion_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(myKeyDown);
        }
    }
}
