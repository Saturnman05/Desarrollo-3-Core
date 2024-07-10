﻿using System;
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
        private string connstring = "Data Source = DESKTOP-MFFG200;Initial Catalog=CoreDB;Integrated Security=true";

        public Form1()
        {
            InitializeComponent();
            txtContraseña.PasswordChar = '*';
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string pass = txtContraseña.Text;
            Core.User user;

            using (var con = new SqlConnection(connstring))
            {
                con.Open();
                user = Core.User.LogIn(con, username, pass);
            }

            if (user.Id == null)
            {
                MessageBox.Show("Contraseña o usuario incorrecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // Redirigir a otra pantalla con los datos el usuario ingresado
        }

        private void ckbPass_CheckedChanged(object sender, EventArgs e)
        {
            txtContraseña.PasswordChar = ckbPass.Checked ? '\0' : '*';
        }
    }
}
