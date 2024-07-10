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
    public partial class Principal : Form
    {
        public string connstring = "Data Source = DESKTOP-MFFG200;Initial Catalog=CoreDB;Integrated Security=true";
        private Core.User currentUser;

        public Principal(Core.User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            
            if (currentUser.Rol != 0)
            {
                usuarioToolStripMenuItem.Visible = false;
                btnCrear.Enabled = false;
                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;
            }

            LoadProductNames();
        }

        private void LoadProductNames()
        {
            List<string> productNames = GetProductNames();
            cmbProducts.Items.Clear();
            cmbProducts.Items.AddRange(productNames.ToArray());
            cmbProducts.Items.Add("Agregar Producto"); // Agregar opción adicional si es necesario
        }

        private List<string> GetProductNames()
        {
            List<string> productNames = new List<string>();

            using (var con = new SqlConnection(connstring))
            {
                con.Open();
                string sql = "SELECT name FROM products";

                using (var command = new SqlCommand(sql, con))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productNames.Add(reader["name"].ToString());
                        }
                    }
                }
            }

            return productNames;
        }

        private void editarMiUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir a la pantalla de editar usuario
            EditarMiUsuario formEditarUsuario = new EditarMiUsuario(currentUser, currentUser);
            formEditarUsuario.Show();
            this.Hide();
            formEditarUsuario.FormClosed += (s, args) => this.Close();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog();
            this.Hide();
            form.FormClosed += (s, args) => this.Close();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            
        }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
