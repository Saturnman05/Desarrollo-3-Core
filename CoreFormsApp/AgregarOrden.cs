using Core;
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
    public partial class AgregarOrden : Form
    {
        private List<OrderItem> orderItems;
        private int orders;

        private Core.User currentUser;
        private Core.Order currentOrder;
        private bool isEditMode;

        public AgregarOrden(int orders, Core.User currentUser, Core.Order order = null)
        {
            InitializeComponent();
            orderItems = new List<OrderItem>();
            this.orders = orders;

            this.currentUser = currentUser;
            this.currentOrder = order;
            this.isEditMode = order != null;

            LoadProducts();

            if (isEditMode)
            {
                LoadOrderDetails();
                lblPrecioTotal.Visible = true;
                txtTotalPrice.Visible = true;
            }
        }

        private void LoadProducts()
        {
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                List<Product> products = Product.ListProducts(con);
                cmbProductos.DataSource = products;
                cmbProductos.DisplayMember = "Name";
                cmbProductos.ValueMember = "Id";
            }
        }

        private void LoadOrderDetails()
        {
            txtNumeroOrden.Text = currentOrder.OrderNumber;
            cmbProductos.SelectedValue = currentOrder.ProductId;
            txtCantidad.Text = currentOrder.Quantity.ToString();
            txtTotalPrice.Text = currentOrder.TotalPrice.ToString("0.00");
        }

        private decimal CalculateTotalPrice(int productId, int quantity)
        {
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                Product product = Product.GetProduct(con, productId);
                return product.Price * quantity;
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                UpdateOrder();
            }
            else
            {
                AddOrder();
            }
        }

        private void AddOrder()
        {
            if (string.IsNullOrEmpty(txtNumeroOrden.Text) || orderItems.Count == 0)
            {
                MessageBox.Show("Por favor, ingrese el número de orden y agregue al menos un producto.");
                return;
            }

            int id = this.orders;

            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                foreach (var item in orderItems)
                {
                    Order order = new Order()
                    {
                        Id = ++id,
                        OrderNumber = txtNumeroOrden.Text,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        TotalPrice = CalculateTotalPrice(item.ProductId, item.Quantity),
                        Date = DateTime.Now
                    };

                    Order.AddOrder(con, order);
                }
            }

            MessageBox.Show("Orden agregada exitosamente.");
            this.Close();
        }

        private void UpdateOrder()
        {
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                currentOrder.OrderNumber = txtNumeroOrden.Text;
                currentOrder.ProductId = (int)cmbProductos.SelectedValue;
                currentOrder.Quantity = int.Parse(txtCantidad.Text);
                currentOrder.TotalPrice = decimal.Parse(txtTotalPrice.Text);
                currentOrder.Date = DateTime.Now;
                Core.Order.UpdateOrder(con, currentOrder);
            }

            MessageBox.Show("Orden actualizada exitosamente.");
            this.Close();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem == null || string.IsNullOrEmpty(txtCantidad.Text))
            {
                MessageBox.Show("Por favor, seleccione un producto y especifique la cantidad.");
                return;
            }

            int productId = (int)cmbProductos.SelectedValue;
            int quantity = int.Parse(txtCantidad.Text);

            var orderItem = new OrderItem
            {
                ProductId = productId,
                Quantity = quantity
            };

            orderItems.Add(orderItem);
            lstProductos.Items.Add($"{cmbProductos.Text} - Cantidad: {quantity}");
        }

        private void regresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
