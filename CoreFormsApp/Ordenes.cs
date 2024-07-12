using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreFormsApp
{
    public partial class Ordenes : Form
    {
        private Core.User currentUser = new Core.User();

        public Ordenes(Core.User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
        }

        private void regresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
