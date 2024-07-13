using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreFormsApp
{
    public partial class Pagos : Form
    {
        // usuario
        private Core.User currentUser;

        // orden
        private Order order;
        private string numeroOrden;

        // pago
        private List<Core.Payment> pagos;

        public Pagos(Core.User currentUser, string numeroOrden, Core.Order order)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            if (this.currentUser.Rol != 0)
            {
                btnPagar.Enabled = false;
                btnUpdatePago.Enabled = false;
                btnEliminarPago.Enabled = false;
            }

            this.order = order;
            this.numeroOrden = numeroOrden;

            UpdatePagos();

            txtTotalOrden.Text = order.TotalPrice.ToString();
            txtOrderNumber.Text = numeroOrden;

            if (this.pagos.Count != 0)
            {
                txtAmount.Text = this.pagos[0].Amount.ToString();
                txtDate.Text = this.pagos[0].Date.ToString();
                btnPagar.Enabled = false;
                btnUpdatePago.Enabled = true;
                btnEliminarPago.Enabled = true;
            }
            else
            {
                MessageBox.Show("No se ha pagado la órden.");
                btnPagar.Enabled = true;
                btnUpdatePago.Enabled = false;
                btnEliminarPago.Enabled = false;
            }

            if (currentUser.Rol != 0)
            {
                btnPagar.Enabled = false;
                btnEliminarPago.Enabled = false;
                btnUpdatePago.Enabled = false;
            }
        }

        private void UpdatePagos()
        {
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                this.pagos = Core.Payment.GetPaymentByNumeroOrden(con, numeroOrden);
            }
        }

        private void regresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (txtAmount.ReadOnly)
            {
                txtAmount.ReadOnly = false;
                btnEliminarPago.Enabled = false;
                btnUpdatePago.Enabled = false;
                return;
            }

            // TODO: procesar el pago
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                Payment payment = new Payment()
                {
                    OrderId = this.order.Id,
                    OrderNumber = this.numeroOrden,
                    Amount = decimal.Parse(txtAmount.Text),
                    Date = DateTime.Now,
                    Status = "Procesado"
                };

                txtDate.Text = payment.Date.ToString();
                Payment.AddPayment(con, payment);
            }

            txtAmount.ReadOnly = true;
            btnPagar.Enabled = false;
            btnEliminarPago.Enabled = true;
            btnUpdatePago.Enabled = true;

            UpdatePagos();

            MessageBox.Show("Pago realizado exitosamente");
        }

        private void btnEliminarPago_Click(object sender, EventArgs e)
        {
            // Verificar si el usuario desea eliminar la orden
            var siNo = MessageBox.Show("¿Seguro que desea eliminar el pago? Esta acción es permanente.", "Eliminar pago", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (siNo == DialogResult.No) return;

            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                foreach (var pago in pagos)
                {
                    Payment.DeletePayment(con, pago.Id);
                }
            }

            txtAmount.Text = "";
            txtDate.Text = "";
            btnPagar.Enabled = true;
            btnUpdatePago.Enabled = false;
            btnEliminarPago.Enabled = false;

            MessageBox.Show("Se eliminó el pago.");
        }

        private void btnUpdatePago_Click(object sender, EventArgs e)
        {
            if (txtAmount.ReadOnly)
            {
                txtAmount.ReadOnly = false;
                btnPagar.Enabled = false;
                btnEliminarPago.Enabled = false;
                btnUpdatePago.Enabled = true;
                return;
            }

            // TODO: editar el pago
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                Payment payment = new Payment()
                {
                    OrderId = this.order.Id,
                    OrderNumber = this.numeroOrden,
                    Amount = decimal.Parse(txtAmount.Text),
                    Date = DateTime.Parse(txtDate.Text),
                    Status = "Procesado"
                };
                Payment.UpdatePayment(con, payment);
            }

            txtAmount.ReadOnly = true;
            btnPagar.Enabled = false;
            btnEliminarPago.Enabled = true;
            btnUpdatePago.Enabled = true;

            UpdatePagos();

            MessageBox.Show("Se editó la órden exitosamente");
        }
    }
}
