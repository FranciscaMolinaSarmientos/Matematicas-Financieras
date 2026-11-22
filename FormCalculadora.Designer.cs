namespace CalculadoraAmortizacion
{
    partial class FormCalculadora
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
            labelMonto = new Label();
            textBoxMonto = new TextBox();
            labelTasa = new Label();
            textBoxTasa = new TextBox();
            labelTipoTasa = new Label();
            comboBoxTipoTasa = new ComboBox();
            labelCapitalizaciones = new Label();
            textBoxCapitalizaciones = new TextBox();
            labelPagosAnio = new Label();
            textBoxPagosAnio = new TextBox();
            labelNumPagos = new Label();
            textBoxNumPagos = new TextBox();
            labelMetodo = new Label();
            comboBoxMetodo = new ComboBox();
            buttonCalcular = new Button();
            buttonGuardar = new Button();
            buttonModificar = new Button();
            buttonHistorial = new Button();
            buttonLimpiar = new Button();
            labelResumen = new Label();
            dataGridViewTabla = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTabla).BeginInit();
            SuspendLayout();
            // 
            // labelSubtitulo
            // 
            labelSubtitulo.Font = new Font("Consolas", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelSubtitulo.ForeColor = Color.Black;
            labelSubtitulo.Location = new Point(0, 75);
            labelSubtitulo.Name = "labelSubtitulo";
            labelSubtitulo.Size = new Size(900, 35);
            labelSubtitulo.Text = "Datos del préstamo";
            labelSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            labelSubtitulo.TabIndex = 2;
            // 
            // labelMonto
            // 
            labelMonto.AutoSize = true;
            labelMonto.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelMonto.ForeColor = Color.Black;
            labelMonto.Location = new Point(30, 123);
            labelMonto.Name = "labelMonto";
            labelMonto.Text = "Monto de la deuda ($)";
            labelMonto.TabIndex = 3;
            // 
            // textBoxMonto
            // 
            textBoxMonto.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxMonto.Location = new Point(300, 120);
            textBoxMonto.Name = "textBoxMonto";
            textBoxMonto.Size = new Size(200, 29);
            textBoxMonto.TabIndex = 4;
            // 
            // labelTasa
            // 
            labelTasa.AutoSize = true;
            labelTasa.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTasa.ForeColor = Color.Black;
            labelTasa.Location = new Point(30, 161);
            labelTasa.Name = "labelTasa";
            labelTasa.Text = "Tasa de interés anual (%)";
            labelTasa.TabIndex = 5;
            // 
            // textBoxTasa
            // 
            textBoxTasa.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxTasa.Location = new Point(300, 158);
            textBoxTasa.Name = "textBoxTasa";
            textBoxTasa.Size = new Size(200, 29);
            textBoxTasa.TabIndex = 6;
            // 
            // labelTipoTasa
            // 
            labelTipoTasa.AutoSize = true;
            labelTipoTasa.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTipoTasa.ForeColor = Color.Black;
            labelTipoTasa.Location = new Point(30, 199);
            labelTipoTasa.Name = "labelTipoTasa";
            labelTipoTasa.Text = "Tipo de tasa";
            labelTipoTasa.TabIndex = 7;
            // 
            // comboBoxTipoTasa
            // 
            comboBoxTipoTasa.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTipoTasa.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxTipoTasa.FormattingEnabled = true;
            comboBoxTipoTasa.Location = new Point(300, 196);
            comboBoxTipoTasa.Name = "comboBoxTipoTasa";
            comboBoxTipoTasa.Size = new Size(260, 30);
            comboBoxTipoTasa.TabIndex = 8;
            comboBoxTipoTasa.Items.AddRange(new object[] { "Efectiva anual", "Nominal capitalizable" });
            comboBoxTipoTasa.SelectedIndexChanged += comboBoxTipoTasa_SelectedIndexChanged;
            // 
            // labelCapitalizaciones
            // 
            labelCapitalizaciones.AutoSize = true;
            labelCapitalizaciones.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelCapitalizaciones.ForeColor = Color.Black;
            labelCapitalizaciones.Location = new Point(30, 237);
            labelCapitalizaciones.Name = "labelCapitalizaciones";
            labelCapitalizaciones.Text = "Capitalizaciones por año";
            labelCapitalizaciones.TabIndex = 9;
            // 
            // textBoxCapitalizaciones
            // 
            textBoxCapitalizaciones.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxCapitalizaciones.Location = new Point(300, 234);
            textBoxCapitalizaciones.Name = "textBoxCapitalizaciones";
            textBoxCapitalizaciones.Size = new Size(200, 29);
            textBoxCapitalizaciones.Enabled = false;
            textBoxCapitalizaciones.Text = "1";
            textBoxCapitalizaciones.TabIndex = 10;
            // 
            // labelPagosAnio
            // 
            labelPagosAnio.AutoSize = true;
            labelPagosAnio.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelPagosAnio.ForeColor = Color.Black;
            labelPagosAnio.Location = new Point(30, 275);
            labelPagosAnio.Name = "labelPagosAnio";
            labelPagosAnio.Text = "Pagos por año";
            labelPagosAnio.TabIndex = 11;
            // 
            // textBoxPagosAnio
            // 
            textBoxPagosAnio.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxPagosAnio.Location = new Point(300, 272);
            textBoxPagosAnio.Name = "textBoxPagosAnio";
            textBoxPagosAnio.Size = new Size(200, 29);
            textBoxPagosAnio.Text = "12";
            textBoxPagosAnio.TabIndex = 12;
            // 
            // labelNumPagos
            // 
            labelNumPagos.AutoSize = true;
            labelNumPagos.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelNumPagos.ForeColor = Color.Black;
            labelNumPagos.Location = new Point(30, 313);
            labelNumPagos.Name = "labelNumPagos";
            labelNumPagos.Text = "Número de pagos";
            labelNumPagos.TabIndex = 13;
            // 
            // textBoxNumPagos
            // 
            textBoxNumPagos.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxNumPagos.Location = new Point(300, 310);
            textBoxNumPagos.Name = "textBoxNumPagos";
            textBoxNumPagos.Size = new Size(200, 29);
            textBoxNumPagos.TabIndex = 14;
            // 
            // labelMetodo
            // 
            labelMetodo.AutoSize = true;
            labelMetodo.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelMetodo.ForeColor = Color.Black;
            labelMetodo.Location = new Point(30, 351);
            labelMetodo.Name = "labelMetodo";
            labelMetodo.Text = "Método";
            labelMetodo.TabIndex = 15;
            // 
            // comboBoxMetodo
            // 
            comboBoxMetodo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMetodo.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxMetodo.FormattingEnabled = true;
            comboBoxMetodo.Location = new Point(300, 348);
            comboBoxMetodo.Name = "comboBoxMetodo";
            comboBoxMetodo.Size = new Size(260, 30);
            comboBoxMetodo.TabIndex = 16;
            comboBoxMetodo.Items.AddRange(new object[] { "Gradual", "Constante" });
            // 
            // buttonCalcular
            // 
            buttonCalcular.BackColor = Color.DimGray;
            buttonCalcular.FlatAppearance.BorderColor = Color.Gray;
            buttonCalcular.FlatAppearance.BorderSize = 1;
            buttonCalcular.FlatStyle = FlatStyle.Flat;
            buttonCalcular.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonCalcular.ForeColor = Color.White;
            buttonCalcular.Location = new Point(680, 120);
            buttonCalcular.Name = "buttonCalcular";
            buttonCalcular.Size = new Size(190, 40);
            buttonCalcular.Text = "Calcular";
            buttonCalcular.UseVisualStyleBackColor = false;
            buttonCalcular.TabIndex = 17;
            buttonCalcular.Click += buttonCalcular_Click;
            // 
            // buttonGuardar
            // 
            buttonGuardar.BackColor = Color.DimGray;
            buttonGuardar.FlatAppearance.BorderColor = Color.Gray;
            buttonGuardar.FlatAppearance.BorderSize = 1;
            buttonGuardar.FlatStyle = FlatStyle.Flat;
            buttonGuardar.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonGuardar.ForeColor = Color.White;
            buttonGuardar.Location = new Point(680, 170);
            buttonGuardar.Name = "buttonGuardar";
            buttonGuardar.Size = new Size(190, 40);
            buttonGuardar.Text = "Guardar";
            buttonGuardar.UseVisualStyleBackColor = false;
            buttonGuardar.Enabled = false;
            buttonGuardar.TabIndex = 18;
            buttonGuardar.Click += buttonGuardar_Click;
            // 
            // buttonModificar
            // 
            buttonModificar.BackColor = Color.DimGray;
            buttonModificar.FlatAppearance.BorderColor = Color.Gray;
            buttonModificar.FlatAppearance.BorderSize = 1;
            buttonModificar.FlatStyle = FlatStyle.Flat;
            buttonModificar.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonModificar.ForeColor = Color.White;
            buttonModificar.Location = new Point(680, 220);
            buttonModificar.Name = "buttonModificar";
            buttonModificar.Size = new Size(190, 40);
            buttonModificar.Text = "Modificar";
            buttonModificar.UseVisualStyleBackColor = false;
            buttonModificar.Enabled = false;
            buttonModificar.TabIndex = 19;
            buttonModificar.Click += buttonModificar_Click;
            // 
            // buttonHistorial
            // 
            buttonHistorial.BackColor = Color.DimGray;
            buttonHistorial.FlatAppearance.BorderColor = Color.Gray;
            buttonHistorial.FlatAppearance.BorderSize = 1;
            buttonHistorial.FlatStyle = FlatStyle.Flat;
            buttonHistorial.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonHistorial.ForeColor = Color.White;
            buttonHistorial.Location = new Point(680, 270);
            buttonHistorial.Name = "buttonHistorial";
            buttonHistorial.Size = new Size(190, 40);
            buttonHistorial.Text = "Historial";
            buttonHistorial.UseVisualStyleBackColor = false;
            buttonHistorial.TabIndex = 20;
            buttonHistorial.Click += buttonHistorial_Click;
            // 
            // buttonLimpiar
            // 
            buttonLimpiar.BackColor = Color.SteelBlue;
            buttonLimpiar.FlatAppearance.BorderColor = Color.Gray;
            buttonLimpiar.FlatAppearance.BorderSize = 1;
            buttonLimpiar.FlatStyle = FlatStyle.Flat;
            buttonLimpiar.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonLimpiar.ForeColor = Color.White;
            buttonLimpiar.Location = new Point(680, 320);
            buttonLimpiar.Name = "buttonLimpiar";
            buttonLimpiar.Size = new Size(190, 40);
            buttonLimpiar.Text = "Limpiar";
            buttonLimpiar.UseVisualStyleBackColor = false;
            buttonLimpiar.TabIndex = 21;
            buttonLimpiar.Click += buttonLimpiar_Click;
            // 
            // labelResumen
            // 
            labelResumen.AutoSize = true;
            labelResumen.Font = new Font("Consolas", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelResumen.ForeColor = Color.Black;
            labelResumen.Location = new Point(30, 392);
            labelResumen.Name = "labelResumen";
            labelResumen.Text = "";
            labelResumen.TabIndex = 22;
            // 
            // dataGridViewTabla
            // 
            dataGridViewTabla.AllowUserToAddRows = false;
            dataGridViewTabla.AllowUserToDeleteRows = false;
            dataGridViewTabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTabla.BackgroundColor = Color.White;
            dataGridViewTabla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTabla.DefaultCellStyle.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewTabla.ColumnHeadersDefaultCellStyle.Font = new Font("Consolas", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewTabla.Location = new Point(30, 420);
            dataGridViewTabla.MultiSelect = false;
            dataGridViewTabla.Name = "dataGridViewTabla";
            dataGridViewTabla.ReadOnly = true;
            dataGridViewTabla.RowHeadersWidth = 30;
            dataGridViewTabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewTabla.Size = new Size(840, 168);
            dataGridViewTabla.TabIndex = 23;
            // 
            // FormCalculadora
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 650);
            Controls.Add(dataGridViewTabla);
            Controls.Add(labelResumen);
            Controls.Add(buttonLimpiar);
            Controls.Add(buttonHistorial);
            Controls.Add(buttonModificar);
            Controls.Add(buttonGuardar);
            Controls.Add(buttonCalcular);
            Controls.Add(comboBoxMetodo);
            Controls.Add(labelMetodo);
            Controls.Add(textBoxNumPagos);
            Controls.Add(labelNumPagos);
            Controls.Add(textBoxPagosAnio);
            Controls.Add(labelPagosAnio);
            Controls.Add(textBoxCapitalizaciones);
            Controls.Add(labelCapitalizaciones);
            Controls.Add(comboBoxTipoTasa);
            Controls.Add(labelTipoTasa);
            Controls.Add(textBoxTasa);
            Controls.Add(labelTasa);
            Controls.Add(textBoxMonto);
            Controls.Add(labelMonto);
            Controls.Add(labelSubtitulo);
            Name = "FormCalculadora";
            Text = "Calculadora de Amortización";
            ((System.ComponentModel.ISupportInitialize)dataGridViewTabla).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelSubtitulo;
        private Label labelMonto;
        private TextBox textBoxMonto;
        private Label labelTasa;
        private TextBox textBoxTasa;
        private Label labelTipoTasa;
        private ComboBox comboBoxTipoTasa;
        private Label labelCapitalizaciones;
        private TextBox textBoxCapitalizaciones;
        private Label labelPagosAnio;
        private TextBox textBoxPagosAnio;
        private Label labelNumPagos;
        private TextBox textBoxNumPagos;
        private Label labelMetodo;
        private ComboBox comboBoxMetodo;
        private Button buttonCalcular;
        private Button buttonGuardar;
        private Button buttonModificar;
        private Button buttonHistorial;
        private Button buttonLimpiar;
        private Label labelResumen;
        private DataGridView dataGridViewTabla;
    }
}
