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

        private List<Core.Order> orderList;
        private List<Core.Order> newOrderList = new List<Core.Order>();

        public AgregarOrden(int orders, Core.User currentUser, List<Core.Order> ordersList, Core.Order order = null )
        {
            InitializeComponent();
            orderItems = new List<OrderItem>();
            this.orderList = ordersList;
            this.orders = orders;

            this.currentUser = currentUser;
            this.currentOrder = order;
            this.isEditMode = order != null;

            LoadProducts();

            if (isEditMode)
            {
                LoadOrderDetails();
                this.Text = "Editar Orden";
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
                string orderNumber = this.currentOrder.OrderNumber;
                foreach (var norder in this.orderList)
                {
                    if (norder.OrderNumber == orderNumber)
                    {
                        using (var con = new SqlConnection(Core.Program.ConnString))
                        {
                            con.Open();
                            Core.Order.DeleteOrder(con, norder.Id);
                        }
                    }
                }

                AddOrder();
                MessageBox.Show("Orden actualizada exitosamente.");
            }
            else
            {
                AddOrder();
                MessageBox.Show("Orden agregada exitosamente.");
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
                decimal totalPrice = 0;

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

                    newOrderList.Add(order);

                    totalPrice += order.TotalPrice;
                }

                foreach (var oreder in newOrderList)
                {
                    oreder.TotalPrice = totalPrice;
                    Order.AddOrder(con, oreder);
                }
            }

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
