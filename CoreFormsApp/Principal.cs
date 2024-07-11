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
        private Core.User currentUser;
        private string agregarProducto = "Agregar Producto";

        public Principal(Core.User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            
            if (currentUser.Rol != 0) 
                //usuarioToolStripMenuItem.Visible = false;

            btnCrear.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;

            LoadProductNames();
        }

        private void LoadProductNames()
        {
            List<string> productNames = GetProductNames();
            cmbProducts.Items.Clear();
            cmbProducts.Items.AddRange(productNames.ToArray());
            cmbProducts.Items.Add(agregarProducto); // Agregar opción adicional si es necesario
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
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            rtxtDesc.Clear();

            if (cmbProducts.SelectedItem.ToString() == agregarProducto && currentUser.Rol == 0)
            {
                btnCrear.Enabled = true;
                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            else if (currentUser.Rol == 0)
            {
                btnCrear.Enabled = false;
                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnCrear.Enabled = false;
                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;
            }

            int indice = cmbProducts.SelectedIndex + 1;
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();

                Core.Product product = Core.Product.GetProduct(con, indice);
                txtNombre.Text = product.Name;
                txtPrecio.Text = product.Price.ToString();
                txtStock.Text = product.Stock.ToString();
                rtxtDesc.Text = product.Description;
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            decimal precio;
            int stock;

            try
            {
                precio = decimal.Parse(txtPrecio.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al introducir el precio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                stock = int.Parse(txtStock.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al introducir la cantidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Core.Product product = new Core.Product() {
                Id = cmbProducts.Items.Count,
                Name = txtNombre.Text,
                Price = precio,
                Stock = stock,
                Description = rtxtDesc.Text
            };

            using (var con = new SqlConnection(connstring))
            {
                con.Open();
                Core.Product.AddProduct(con, product);
            }

            MessageBox.Show("Se agregó el producto correctamente");
            LoadProductNames();

            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            rtxtDesc.Clear();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            decimal precio;
            int stock;

            try
            {
                precio = decimal.Parse(txtPrecio.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al introducir el precio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                stock = int.Parse(txtStock.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al introducir la cantidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Core.Product product = new Core.Product()
            {
                Id = cmbProducts.SelectedIndex + 1,
                Name = txtNombre.Text,
                Price = precio,
                Stock = stock,
                Description = rtxtDesc.Text
            };

            using (var con = new SqlConnection(connstring))
            {
                con.Open();
                Core.Product.UpdateProduct(con, product);
            }

            MessageBox.Show("Se actualizó el producto correctamente");
            LoadProductNames();

            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            rtxtDesc.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Core.Product product = new Core.Product()
            {
                Id = cmbProducts.SelectedIndex + 1,
            };

            using (var con = new SqlConnection(connstring))
            {
                con.Open();
                Core.Product.DeleteProduct(con, product);
            }

            MessageBox.Show("Se eliminó el producto correctamente");
            LoadProductNames();

            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            rtxtDesc.Clear();
        }
    }
}
