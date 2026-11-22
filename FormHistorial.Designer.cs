namespace CalculadoraAmortizacion
{
    partial class FormHistorial
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
            labelSubtitulo = new Label();
            dataGridViewSimulaciones = new DataGridView();
            buttonModificar = new Button();
            buttonEliminar = new Button();
            buttonRegresar = new Button();
            labelDetalle = new Label();
            dataGridViewDetalle = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSimulaciones).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDetalle).BeginInit();
            SuspendLayout();
            // 
            // labelSubtitulo
            // 
            labelSubtitulo.Font = new Font("Consolas", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelSubtitulo.ForeColor = Color.Black;
            labelSubtitulo.Location = new Point(0, 75);
            labelSubtitulo.Name = "labelSubtitulo";
            labelSubtitulo.Size = new Size(900, 35);
            labelSubtitulo.Text = "Historial de cálculos";
            labelSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            labelSubtitulo.TabIndex = 2;
            // 
            // dataGridViewSimulaciones
            // 
            dataGridViewSimulaciones.AllowUserToAddRows = false;
            dataGridViewSimulaciones.AllowUserToDeleteRows = false;
            dataGridViewSimulaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSimulaciones.BackgroundColor = Color.White;
            dataGridViewSimulaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSimulaciones.DefaultCellStyle.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewSimulaciones.ColumnHeadersDefaultCellStyle.Font = new Font("Consolas", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewSimulaciones.Location = new Point(30, 120);
            dataGridViewSimulaciones.MultiSelect = false;
            dataGridViewSimulaciones.Name = "dataGridViewSimulaciones";
            dataGridViewSimulaciones.ReadOnly = true;
            dataGridViewSimulaciones.RowHeadersWidth = 30;
            dataGridViewSimulaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSimulaciones.Size = new Size(640, 200);
            dataGridViewSimulaciones.TabIndex = 3;
            dataGridViewSimulaciones.SelectionChanged += dataGridViewSimulaciones_SelectionChanged;
            // 
            // buttonModificar
            // 
            buttonModificar.BackColor = Color.DimGray;
            buttonModificar.FlatAppearance.BorderColor = Color.Gray;
            buttonModificar.FlatAppearance.BorderSize = 1;
            buttonModificar.FlatStyle = FlatStyle.Flat;
            buttonModificar.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonModificar.ForeColor = Color.White;
            buttonModificar.Location = new Point(700, 120);
            buttonModificar.Name = "buttonModificar";
            buttonModificar.Size = new Size(170, 40);
            buttonModificar.Text = "Modificar";
            buttonModificar.UseVisualStyleBackColor = false;
            buttonModificar.TabIndex = 4;
            buttonModificar.Click += buttonModificar_Click;
            // 
            // buttonEliminar
            // 
            buttonEliminar.BackColor = Color.DimGray;
            buttonEliminar.FlatAppearance.BorderColor = Color.Gray;
            buttonEliminar.FlatAppearance.BorderSize = 1;
            buttonEliminar.FlatStyle = FlatStyle.Flat;
            buttonEliminar.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonEliminar.ForeColor = Color.White;
            buttonEliminar.Location = new Point(700, 170);
            buttonEliminar.Name = "buttonEliminar";
            buttonEliminar.Size = new Size(170, 40);
            buttonEliminar.Text = "Eliminar";
            buttonEliminar.UseVisualStyleBackColor = false;
            buttonEliminar.TabIndex = 5;
            buttonEliminar.Click += buttonEliminar_Click;
            // 
            // buttonRegresar
            // 
            buttonRegresar.BackColor = Color.OliveDrab;
            buttonRegresar.FlatAppearance.BorderColor = Color.Gray;
            buttonRegresar.FlatAppearance.BorderSize = 1;
            buttonRegresar.FlatStyle = FlatStyle.Flat;
            buttonRegresar.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonRegresar.ForeColor = Color.White;
            buttonRegresar.Location = new Point(700, 220);
            buttonRegresar.Name = "buttonRegresar";
            buttonRegresar.Size = new Size(170, 40);
            buttonRegresar.Text = "Regresar";
            buttonRegresar.UseVisualStyleBackColor = false;
            buttonRegresar.TabIndex = 6;
            buttonRegresar.Click += buttonRegresar_Click;
            // 
            // labelDetalle
            // 
            labelDetalle.AutoSize = true;
            labelDetalle.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelDetalle.ForeColor = Color.Black;
            labelDetalle.Location = new Point(30, 332);
            labelDetalle.Name = "labelDetalle";
            labelDetalle.Text = "Tabla de amortización del cálculo seleccionado";
            labelDetalle.TabIndex = 7;
            // 
            // dataGridViewDetalle
            // 
            dataGridViewDetalle.AllowUserToAddRows = false;
            dataGridViewDetalle.AllowUserToDeleteRows = false;
            dataGridViewDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDetalle.BackgroundColor = Color.White;
            dataGridViewDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDetalle.DefaultCellStyle.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewDetalle.ColumnHeadersDefaultCellStyle.Font = new Font("Consolas", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewDetalle.Location = new Point(30, 365);
            dataGridViewDetalle.MultiSelect = false;
            dataGridViewDetalle.Name = "dataGridViewDetalle";
            dataGridViewDetalle.ReadOnly = true;
            dataGridViewDetalle.RowHeadersWidth = 30;
            dataGridViewDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDetalle.Size = new Size(840, 223);
            dataGridViewDetalle.TabIndex = 8;
            // 
            // FormHistorial
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 650);
            Controls.Add(dataGridViewDetalle);
            Controls.Add(labelDetalle);
            Controls.Add(buttonRegresar);
            Controls.Add(buttonEliminar);
            Controls.Add(buttonModificar);
            Controls.Add(dataGridViewSimulaciones);
            Controls.Add(labelSubtitulo);
            Load += FormHistorial_Load;
            Name = "FormHistorial";
            Text = "Historial";
            ((System.ComponentModel.ISupportInitialize)dataGridViewSimulaciones).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelSubtitulo;
        private DataGridView dataGridViewSimulaciones;
        private Button buttonModificar;
        private Button buttonEliminar;
        private Button buttonRegresar;
        private Label labelDetalle;
        private DataGridView dataGridViewDetalle;
    }
}
