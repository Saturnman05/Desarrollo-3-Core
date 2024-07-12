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
    public partial class Usuarios : Form
    {
        private Core.User currentUser;
        private static List<Core.User> usersList = new List<Core.User>();
        private static int userSelectedIndex = -1;

        public Usuarios(Core.User currentUser)
        {
            InitializeComponent();
            txtPass.ReadOnly = true;
            txtRol.ReadOnly = true;
            this.currentUser = currentUser;

            if (currentUser.Rol != 0)
            {
                btnCrearUsuario.Enabled = false;
                btnEditarUsuario.Enabled = false;
                btnEliminarUsuario.Enabled = false;
            }

            LoadUsers(cmbUsername);
        }

        public static void LoadUsers(ComboBox cmbUsername)
        {
            usersList = GetUsers();
            cmbUsername.Items.Clear();
            foreach (var user in usersList)
            {
                cmbUsername.Items.Add(user.Username);
            }
        }

        public static List<Core.User> GetUsers()
        {
            List<Core.User> newUserList = new List<Core.User>();

            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                string sql = "SELECT * FROM users;";
                using (var command = new SqlCommand(sql, con))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        newUserList.Add(new Core.User() { 
                            Id = int.Parse(reader["id"].ToString()),
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString(),
                            Rol = int.Parse(reader["Rol"].ToString())
                        });
                    }
                }
            }

            return newUserList;
        }

        private void cmbUsername_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbUsername.SelectedIndex;
            userSelectedIndex = (int)usersList[index].Id;
            txtPass.Text = usersList[index].Password;
            txtRol.Text = (usersList[index].Rol == 0) ? "Administrador" : "Usuario";
        }

        private void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            Core.User userEdited = new Core.User();

            
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                userEdited = Core.User.GetUser(con, userSelectedIndex);
            }
            
            if (userEdited.Id == null)
            {
                MessageBox.Show("No se pudo encontrar al usuario seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            

            EditarMiUsuario editarUsuarioForm = new EditarMiUsuario(currentUser, userEdited);
            editarUsuarioForm.Show();
            this.Hide();
            editarUsuarioForm.FormClosed += (s, args) => this.Close();
        }

        private void ckbVerPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.PasswordChar = ckbVerPass.Checked ? '\0' : '*';
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            CrearUsuario crearUsuarioForm = new CrearUsuario();
            crearUsuarioForm.Show();
            this.Hide();
            crearUsuarioForm.FormClosed += (s, args) => this.Show();
        }

        private void editarMiUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir a la pantalla de editar usuario
            EditarMiUsuario formEditarUsuario = new EditarMiUsuario(currentUser, currentUser);
            formEditarUsuario.Show();
            this.Hide();
            formEditarUsuario.FormClosed += (s, args) => this.Close();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog();
            this.Hide();
            form.FormClosed += (s, args) => this.Close();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            if (userSelectedIndex == -1)
            {
                MessageBox.Show("Debes seleccionar un usuario para elemininarlo.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var con = new SqlConnection(Core.Program.ConnString))
                {
                    con.Open();
                    Core.User.DeleteUser(con, userSelectedIndex);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al borrar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show("Se eliminó el usuario correctamente.", "Eliminación de usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtPass.Text = "";
            txtRol.Text = "";
            LoadUsers(cmbUsername);
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            LoadUsers(cmbUsername);
        }
    }
}
