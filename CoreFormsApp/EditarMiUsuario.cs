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
    public partial class EditarMiUsuario : Form
    {
        public string connstring = "Data Source = DESKTOP-MFFG200;Initial Catalog=CoreDB;Integrated Security=true";

        private Core.User userEditing;
        private Core.User userEdited;

        public EditarMiUsuario(Core.User userEditing, Core.User userEdited)
        {
            InitializeComponent();
            this.userEditing = userEditing;
            this.userEdited = userEdited;

            cmbRol.Items.AddRange(new object[] {
                "Administrador", // Rol 0
                "Usuario"       // Rol 1
            });

            if (userEdited.Rol != 0)
            {
                cmbRol.SelectedIndex = 1; // Establecer la opción predeterminada
                cmbRol.Enabled = false; // Bloquear el ComboBox
            }

            cmbRol.SelectedIndex = 0;

            txtUsername.Text = userEdited.Username;
            txtPass.Text = userEdited.Password;
            txtPass.PasswordChar = '*';
        }

        private void ckbPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.PasswordChar = ckbPass.Checked ? '\0' : '*';
        }

        private void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            // Mostrar el mensaje de confirmación
            var result = MessageBox.Show("¿Desea continuar con la edición del usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificar la respuesta del usuario
            if (result == DialogResult.No)
            {
                // Si el usuario elige "No", simplemente salir del método
                return;
            }

            using (var con = new SqlConnection(connstring))
            {
                con.Open();

                userEdited.Username = txtUsername.Text;
                userEdited.Password = txtPass.Text;
                userEdited.Rol = cmbRol.SelectedIndex;

                Core.User.UpdateUser(con, userEdited);
            }

            MessageBox.Show("El usuario se editó correctamente");
        }
    }
}
