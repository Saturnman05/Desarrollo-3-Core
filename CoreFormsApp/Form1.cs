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
    public partial class Form1 : Form
    {
        private string connstring = "Data Source = DESKTOP-MFFG200;Initial Catalog=CoreDB;Integrated Security=true";;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string pass = txtContraseña.Text;
            Core.User user;

            using (var con = new SqlConnection(connstring))
            {
                user = Core.User.LogIn(con, username, pass);
            }
        }
    }
}
