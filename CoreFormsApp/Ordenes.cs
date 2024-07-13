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

        // Ordenes
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

            if (currentUser.Rol != 0)
            {
                btnCrearOrden.Enabled = false;
                btnActualizarOrden.Enabled = false;
                btnEliminarOrden.Enabled = false;
            }
        }

        public static void LoadOrders(ComboBox cmbNumeroOrden)
        {
            ordersList = GetOrders();
            cmbNumeroOrden.Items.Clear();
            foreach (var order in ordersList)
            {
                if (!cmbNumeroOrden.Items.Contains(order.OrderNumber)) 
                {
                    cmbNumeroOrden.Items.Add(order.OrderNumber);
                }
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
            txtCantidadProducto.Text = "";

            int indexProduct = cmbProductos.SelectedIndex;
            productSelectedIndex = (int)productList[indexProduct].Id;

            string numeroOrden = ordersList[cmbProductos.SelectedIndex].OrderNumber;
            txtCantidadProducto.Text = Core.Order.GetOrderProductQuantity(productSelectedIndex, numeroOrden);
        }

        private void btnEliminarOrden_Click(object sender, EventArgs e)
        {
            if (cmbNumeroOrden.SelectedIndex == -1 || orderSelectedIndex == -1)
            {
                MessageBox.Show("Selecciona una orden para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si el usuario desea eliminar la orden
            var siNo = MessageBox.Show("¿Seguro que desea eliminar la orden? Esta acción es permanente.", "Eliminar orden", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (siNo == DialogResult.No) return;

            string orderNumber = ordersList[cmbNumeroOrden.SelectedIndex].OrderNumber;
            foreach (var order in  ordersList)
            {
                if (order.OrderNumber == orderNumber)
                {
                    using (var con = new SqlConnection(Core.Program.ConnString))
                    {
                        con.Open();
                        Core.Order.DeleteOrder(con, order.Id);
                    }
                }
            }

            txtCantidadProducto.Text = "";
            txtDate.Text = "";
            txtPrecioTotal.Text = "";
            cmbProductos.Items.Clear();
            cmbNumeroOrden.Items.Clear();

            LoadOrders(cmbNumeroOrden);

            MessageBox.Show("Se eliminó la orden correctamente.");
        }

        private void btnCrearOrden_Click(object sender, EventArgs e)
        {
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                AgregarOrden agregarOrdenForm = new AgregarOrden(Core.Order.ListOrders(con).Count, currentUser, ordersList, null);
                agregarOrdenForm.ShowDialog();
            }

            txtCantidadProducto.Text = "";
            txtDate.Text = "";
            txtPrecioTotal.Text = "";
            cmbProductos.Items.Clear();
            cmbNumeroOrden.Items.Clear();

            LoadOrders(cmbNumeroOrden);
        }

        private void btnActualizarOrden_Click(object sender, EventArgs e)
        {
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                AgregarOrden agregarOrdenForm = new AgregarOrden(Core.Order.ListOrders(con).Count, currentUser, ordersList, Core.Order.GetOrder(con, orderSelectedIndex));
                agregarOrdenForm.ShowDialog();
            }

            txtCantidadProducto.Text = "";
            txtDate.Text = "";
            txtPrecioTotal.Text = "";
            cmbProductos.Items.Clear();
            cmbNumeroOrden.Items.Clear();

            LoadOrders(cmbNumeroOrden);
        }

        private void btnPagos_Click(object sender, EventArgs e)
        {
            if (cmbNumeroOrden.SelectedIndex == -1 || orderSelectedIndex == -1)
            {
                MessageBox.Show("Elige una órden.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Pagos pagosForm = new Pagos(currentUser, ordersList[cmbNumeroOrden.SelectedIndex].OrderNumber, ordersList[cmbNumeroOrden.SelectedIndex]);
            pagosForm.Show();
            this.Hide();
            pagosForm.FormClosed += (s, args) => this.Show();
        }
    }
}
