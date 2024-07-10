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
    public partial class Principal : Form
    {
        private Core.User currentUser;
        public Principal(Core.User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            lblUsuario.Text = $"Bienvenido {currentUser.Username}";
            if (currentUser.Rol != 0)
            {
                usuarioToolStripMenuItem.Visible = false;
            }
        }

        private void editarMiUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir a la pantalla de editar usuario
            EditarMiUsuario formEditarUsuario = new EditarMiUsuario(currentUser, currentUser);
            formEditarUsuario.Show();
            this.Hide();
            formEditarUsuario.FormClosed += (s, args) => this.Close();
        }
    }
}
