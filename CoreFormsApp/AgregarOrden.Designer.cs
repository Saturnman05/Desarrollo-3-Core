namespace CoreFormsApp
{
    partial class AgregarOrden
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNumeroOrden = new System.Windows.Forms.TextBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.cmbProductos = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lstProductos = new System.Windows.Forms.ListBox();
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.regresarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblPrecioTotal = new System.Windows.Forms.Label();
            this.txtTotalPrice = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNumeroOrden
            // 
            this.txtNumeroOrden.Location = new System.Drawing.Point(169, 74);
            this.txtNumeroOrden.Name = "txtNumeroOrden";
            this.txtNumeroOrden.Size = new System.Drawing.Size(278, 26);
            this.txtNumeroOrden.TabIndex = 0;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(169, 137);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(278, 26);
            this.txtCantidad.TabIndex = 1;
            // 
            // cmbProductos
            // 
            this.cmbProductos.FormattingEnabled = true;
            this.cmbProductos.Location = new System.Drawing.Point(169, 191);
            this.cmbProductos.Name = "cmbProductos";
            this.cmbProductos.Size = new System.Drawing.Size(278, 28);
            this.cmbProductos.TabIndex = 2;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(569, 286);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(171, 55);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click_1);
            // 
            // lstProductos
            // 
            this.lstProductos.FormattingEnabled = true;
            this.lstProductos.ItemHeight = 20;
            this.lstProductos.Location = new System.Drawing.Point(506, 77);
            this.lstProductos.Name = "lstProductos";
            this.lstProductos.Size = new System.Drawing.Size(234, 184);
            this.lstProductos.TabIndex = 4;
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.Location = new System.Drawing.Point(378, 286);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(171, 55);
            this.btnAgregarProducto.TabIndex = 5;
            this.btnAgregarProducto.Text = "Agregar Producto";
            this.btnAgregarProducto.UseVisualStyleBackColor = true;
            this.btnAgregarProducto.Click += new System.EventHandler(this.btnAgregarProducto_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Número de orden";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Cantidad";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Producto";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regresarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 33);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // regresarToolStripMenuItem
            // 
            this.regresarToolStripMenuItem.Name = "regresarToolStripMenuItem";
            this.regresarToolStripMenuItem.Size = new System.Drawing.Size(96, 29);
            this.regresarToolStripMenuItem.Text = "Regresar";
            this.regresarToolStripMenuItem.Click += new System.EventHandler(this.regresarToolStripMenuItem_Click);
            // 
            // lblPrecioTotal
            // 
            this.lblPrecioTotal.AutoSize = true;
            this.lblPrecioTotal.Location = new System.Drawing.Point(31, 250);
            this.lblPrecioTotal.Name = "lblPrecioTotal";
            this.lblPrecioTotal.Size = new System.Drawing.Size(88, 20);
            this.lblPrecioTotal.TabIndex = 12;
            this.lblPrecioTotal.Text = "Precio total";
            this.lblPrecioTotal.Visible = false;
            // 
            // txtTotalPrice
            // 
            this.txtTotalPrice.Location = new System.Drawing.Point(169, 247);
            this.txtTotalPrice.Name = "txtTotalPrice";
            this.txtTotalPrice.Size = new System.Drawing.Size(278, 26);
            this.txtTotalPrice.TabIndex = 11;
            this.txtTotalPrice.Visible = false;
            // 
            // AgregarOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblPrecioTotal);
            this.Controls.Add(this.txtTotalPrice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAgregarProducto);
            this.Controls.Add(this.lstProductos);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.cmbProductos);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.txtNumeroOrden);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AgregarOrden";
            this.Text = "AgregarOrden";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNumeroOrden;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.ComboBox cmbProductos;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ListBox lstProductos;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem regresarToolStripMenuItem;
        private System.Windows.Forms.Label lblPrecioTotal;
        private System.Windows.Forms.TextBox txtTotalPrice;
    }
}