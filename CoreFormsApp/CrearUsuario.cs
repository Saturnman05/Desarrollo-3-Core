using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreFormsApp
{
    public partial class CrearUsuario : Form
    {
        public CrearUsuario()
        {
            InitializeComponent();
            
            cmbRol.Items.AddRange(new object[] {
                "Administrador", // Rol 0
                "Usuario"       // Rol 1
            });
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            // Mostrar el mensaje de confirmación
            var result = MessageBox.Show("¿Desea continuar con la creaión del usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificar la respuesta del usuario
            if (result == DialogResult.No)
            {
                // Si el usuario elige "No", simplemente salir del método
                return;
            }

            if (txtUsername.Text.Length == 0 || txtPass.Text.Length == 0 || cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Debes llenar todos los campos para continuar.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();

                var newUser = new Core.User()
                {
                    Username = txtUsername.Text,
                    Password = txtPass.Text,
                    Rol = cmbRol.SelectedIndex
                };

                Core.User.AddUser(con, newUser);
            }
        }

        private void regresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
