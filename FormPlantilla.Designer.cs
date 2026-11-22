namespace CalculadoraAmortizacion
{
    partial class FormPlantilla
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
            labelTitulo = new Label();
            buttonSalir = new Button();
            SuspendLayout();
            //
            // labelTitulo
            //
            labelTitulo.BackColor = Color.DimGray;
            labelTitulo.Dock = DockStyle.Top;
            labelTitulo.Font = new Font("Lucida Sans Typewriter", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitulo.ForeColor = Color.White;
            labelTitulo.Location = new Point(0, 0);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(900, 70);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "Calculadora de Amortización";
            labelTitulo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // buttonSalir
            //
            buttonSalir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonSalir.BackColor = Color.Tomato;
            buttonSalir.FlatAppearance.BorderColor = Color.Gray;
            buttonSalir.FlatAppearance.BorderSize = 1;
            buttonSalir.FlatStyle = FlatStyle.Flat;
            buttonSalir.Font = new Font("Consolas", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonSalir.ForeColor = Color.White;
            buttonSalir.Location = new Point(760, 598);
            buttonSalir.Name = "buttonSalir";
            buttonSalir.Size = new Size(115, 40);
            buttonSalir.TabIndex = 1;
            buttonSalir.Text = "Salir";
            buttonSalir.UseVisualStyleBackColor = false;
            buttonSalir.Click += buttonSalir_Click;
            //
            // FormPlantilla
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(900, 650);
            Controls.Add(buttonSalir);
            Controls.Add(labelTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormPlantilla";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Calculadora de Amortización";
            ResumeLayout(false);
        }

        #endregion

        private Label labelTitulo;
        private Button buttonSalir;
    }
}
