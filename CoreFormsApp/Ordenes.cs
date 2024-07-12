using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreFormsApp
{
    public partial class Ordenes : Form
    {
        private Core.User currentUser = new Core.User();
        private static List<Core.Order> ordersList = new List<Core.Order>();
        private static int orderSelectedIndex = -1;

        // Productos
        private static List<Core.Product> productList = new List<Core.Product>();
        private static int productSelectedIndex = -1;

        public Ordenes(Core.User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            LoadOrders(cmbNumeroOrden);
        }

        public static void LoadOrders(ComboBox cmbNumeroOrden)
        {
            ordersList = GetOrders();
            cmbNumeroOrden.Items.Clear();
            foreach (var order in ordersList)
            {
                cmbNumeroOrden.Items.Add(order.OrderNumber);
            }
        }

        public static List<Core.Order> GetOrders()
        {
            List<Core.Order> newOrderList = new List<Core.Order>();

            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                newOrderList = Core.Order.ListOrders(con);
            }

            return newOrderList;
        }

        public static void LoadProducts(ComboBox cmbProductos)
        {
            productList = GetProducts();
            cmbProductos.Items.Clear();
            foreach (var product in productList)
            {
                cmbProductos.Items.Add(product.Name);
            }
        }

        public static List<Core.Product> GetProducts()
        {
            List<Core.Product> newProductList = new List<Core.Product>();

            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                newProductList = Core.Order.GetOrderProducts(con, Core.Order.GetOrder(con, orderSelectedIndex));
            }

            return newProductList;
        }

        private void regresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbNumeroOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbNumeroOrden.SelectedIndex;
            orderSelectedIndex = (int)ordersList[index].Id;
            txtPrecioTotal.Text = ordersList[index].TotalPrice.ToString();
            txtDate.Text = ordersList[index].Date.ToString();

            LoadProducts(cmbProductos);
        }

        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbNumeroOrden.SelectedIndex;
            orderSelectedIndex = (int)ordersList[index].Id;
            txtCantidadProducto.Text = ordersList[index].Quantity.ToString();
        }
    }
}
