namespace CoreFormsApp
{
    partial class Ordenes
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.regresarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbNumeroOrden = new System.Windows.Forms.ComboBox();
            this.lblNumeroOrden = new System.Windows.Forms.Label();
            this.lblProductos = new System.Windows.Forms.Label();
            this.cmbProductos = new System.Windows.Forms.ComboBox();
            this.txtCantidadProducto = new System.Windows.Forms.TextBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblPrecioTotal = new System.Windows.Forms.Label();
            this.txtPrecioTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.btnCrearOrden = new System.Windows.Forms.Button();
            this.btnActualizarOrden = new System.Windows.Forms.Button();
            this.btnEliminarOrden = new System.Windows.Forms.Button();
            this.btnPagos = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // regresarToolStripMenuItem
            // 
            this.regresarToolStripMenuItem.Name = "regresarToolStripMenuItem";
            this.regresarToolStripMenuItem.Size = new System.Drawing.Size(96, 29);
            this.regresarToolStripMenuItem.Text = "Regresar";
            this.regresarToolStripMenuItem.Click += new System.EventHandler(this.regresarToolStripMenuItem_Click);
            // 
            // cmbNumeroOrden
            // 
            this.cmbNumeroOrden.FormattingEnabled = true;
            this.cmbNumeroOrden.Location = new System.Drawing.Point(378, 76);
            this.cmbNumeroOrden.Name = "cmbNumeroOrden";
            this.cmbNumeroOrden.Size = new System.Drawing.Size(90, 28);
            this.cmbNumeroOrden.TabIndex = 1;
            this.cmbNumeroOrden.SelectedIndexChanged += new System.EventHandler(this.cmbNumeroOrden_SelectedIndexChanged);
            // 
            // lblNumeroOrden
            // 
            this.lblNumeroOrden.AutoSize = true;
            this.lblNumeroOrden.Location = new System.Drawing.Point(222, 79);
            this.lblNumeroOrden.Name = "lblNumeroOrden";
            this.lblNumeroOrden.Size = new System.Drawing.Size(132, 20);
            this.lblNumeroOrden.TabIndex = 2;
            this.lblNumeroOrden.Text = "Número de orden";
            // 
            // lblProductos
            // 
            this.lblProductos.AutoSize = true;
            this.lblProductos.Location = new System.Drawing.Point(222, 147);
            this.lblProductos.Name = "lblProductos";
            this.lblProductos.Size = new System.Drawing.Size(81, 20);
            this.lblProductos.TabIndex = 4;
            this.lblProductos.Text = "Productos";
            // 
            // cmbProductos
            // 
            this.cmbProductos.FormattingEnabled = true;
            this.cmbProductos.Location = new System.Drawing.Point(325, 144);
            this.cmbProductos.Name = "cmbProductos";
            this.cmbProductos.Size = new System.Drawing.Size(143, 28);
            this.cmbProductos.TabIndex = 3;
            this.cmbProductos.SelectedIndexChanged += new System.EventHandler(this.cmbProductos_SelectedIndexChanged);
            // 
            // txtCantidadProducto
            // 
            this.txtCantidadProducto.Location = new System.Drawing.Point(576, 144);
            this.txtCantidadProducto.Name = "txtCantidadProducto";
            this.txtCantidadProducto.ReadOnly = true;
            this.txtCantidadProducto.Size = new System.Drawing.Size(71, 26);
            this.txtCantidadProducto.TabIndex = 5;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(497, 147);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(73, 20);
            this.lblCantidad.TabIndex = 6;
            this.lblCantidad.Text = "Cantidad";
            // 
            // lblPrecioTotal
            // 
            this.lblPrecioTotal.AutoSize = true;
            this.lblPrecioTotal.Location = new System.Drawing.Point(222, 212);
            this.lblPrecioTotal.Name = "lblPrecioTotal";
            this.lblPrecioTotal.Size = new System.Drawing.Size(44, 20);
            this.lblPrecioTotal.TabIndex = 8;
            this.lblPrecioTotal.Text = "Total";
            // 
            // txtPrecioTotal
            // 
            this.txtPrecioTotal.Location = new System.Drawing.Point(325, 209);
            this.txtPrecioTotal.Name = "txtPrecioTotal";
            this.txtPrecioTotal.ReadOnly = true;
            this.txtPrecioTotal.Size = new System.Drawing.Size(143, 26);
            this.txtPrecioTotal.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(312, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "$";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 271);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Fecha";
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(325, 268);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(143, 26);
            this.txtDate.TabIndex = 10;
            // 
            // btnCrearOrden
            // 
            this.btnCrearOrden.Location = new System.Drawing.Point(81, 364);
            this.btnCrearOrden.Name = "btnCrearOrden";
            this.btnCrearOrden.Size = new System.Drawing.Size(171, 42);
            this.btnCrearOrden.TabIndex = 13;
            this.btnCrearOrden.Text = "Crear";
            this.btnCrearOrden.UseVisualStyleBackColor = true;
            this.btnCrearOrden.Click += new System.EventHandler(this.btnCrearOrden_Click);
            // 
            // btnActualizarOrden
            // 
            this.btnActualizarOrden.Location = new System.Drawing.Point(308, 364);
            this.btnActualizarOrden.Name = "btnActualizarOrden";
            this.btnActualizarOrden.Size = new System.Drawing.Size(171, 42);
            this.btnActualizarOrden.TabIndex = 14;
            this.btnActualizarOrden.Text = "Actualizar";
            this.btnActualizarOrden.UseVisualStyleBackColor = true;
            this.btnActualizarOrden.Click += new System.EventHandler(this.btnActualizarOrden_Click);
            // 
            // btnEliminarOrden
            // 
            this.btnEliminarOrden.Location = new System.Drawing.Point(530, 364);
            this.btnEliminarOrden.Name = "btnEliminarOrden";
            this.btnEliminarOrden.Size = new System.Drawing.Size(171, 42);
            this.btnEliminarOrden.TabIndex = 15;
            this.btnEliminarOrden.Text = "Eliminar";
            this.btnEliminarOrden.UseVisualStyleBackColor = true;
            this.btnEliminarOrden.Click += new System.EventHandler(this.btnEliminarOrden_Click);
            // 
            // btnPagos
            // 
            this.btnPagos.Location = new System.Drawing.Point(501, 71);
            this.btnPagos.Name = "btnPagos";
            this.btnPagos.Size = new System.Drawing.Size(99, 36);
            this.btnPagos.TabIndex = 16;
            this.btnPagos.Text = "Ver Pagos";
            this.btnPagos.UseVisualStyleBackColor = true;
            this.btnPagos.Click += new System.EventHandler(this.btnPagos_Click);
            // 
            // Ordenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPagos);
            this.Controls.Add(this.btnEliminarOrden);
            this.Controls.Add(this.btnActualizarOrden);
            this.Controls.Add(this.btnCrearOrden);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.lblPrecioTotal);
            this.Controls.Add(this.txtPrecioTotal);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.txtCantidadProducto);
            this.Controls.Add(this.lblProductos);
            this.Controls.Add(this.cmbProductos);
            this.Controls.Add(this.lblNumeroOrden);
            this.Controls.Add(this.cmbNumeroOrden);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Ordenes";
            this.Text = "Ordenes";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem regresarToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbNumeroOrden;
        private System.Windows.Forms.Label lblNumeroOrden;
        private System.Windows.Forms.Label lblProductos;
        private System.Windows.Forms.ComboBox cmbProductos;
        private System.Windows.Forms.TextBox txtCantidadProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblPrecioTotal;
        private System.Windows.Forms.TextBox txtPrecioTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Button btnCrearOrden;
        private System.Windows.Forms.Button btnActualizarOrden;
        private System.Windows.Forms.Button btnEliminarOrden;
        private System.Windows.Forms.Button btnPagos;
    }
}