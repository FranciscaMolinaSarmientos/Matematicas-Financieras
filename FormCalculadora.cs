// VENTANA 1: captura de datos, cálculo, cuadro de amortización y guardado.
// Si recibe un id, funciona en modo "editar" (precarga los datos y actualiza en vez de crear).
class FormCalculadora : Form
{
    readonly TextBox txtMonto = new() { Width = 160 };
    readonly TextBox txtTasa = new() { Width = 160 };
    readonly ComboBox cmbTipo = new() { Width = 160, DropDownStyle = ComboBoxStyle.DropDownList };
    readonly ComboBox cmbMetodo = new() { Width = 160, DropDownStyle = ComboBoxStyle.DropDownList };
    readonly NumericUpDown numCap = new() { Width = 160, Minimum = 1, Maximum = 365, Value = 12 };
    readonly NumericUpDown numPagosAnio = new() { Width = 160, Minimum = 1, Maximum = 365, Value = 12 };
    readonly NumericUpDown numN = new() { Width = 160, Minimum = 1, Maximum = 1200, Value = 12 };
    readonly Button btnCalcular = new() { Text = "Calcular", AutoSize = true };
    readonly Button btnGuardar = new() { Text = "Guardar", AutoSize = true, Enabled = false };
    readonly Button btnHistorial = new() { Text = "Ver historial", AutoSize = true };
    readonly Label lblResumen = new() { AutoSize = true, Padding = new Padding(0, 8, 0, 8) };
    readonly DataGridView grid = new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

    Calculo? _actual;        // Último cálculo mostrado
    readonly long? _idEditar;  // null = nuevo; con valor = editando ese id

    public FormCalculadora(long? idEditar = null)
    {
        _idEditar = idEditar;
        Text = idEditar == null ? "Calculadora de amortización" : $"Editar cálculo #{idEditar}";
        Size = new Size(760, 640);
        StartPosition = FormStartPosition.CenterScreen;

        cmbTipo.Items.AddRange(new object[] { "Efectiva anual", "Nominal capitalizable" });
        cmbMetodo.Items.AddRange(new object[] { "Gradual (pagos iguales)", "Constante (amortización igual)" });
        cmbTipo.SelectedIndex = 0;
        cmbMetodo.SelectedIndex = 0;
        numCap.Enabled = false;   // Solo aplica con tasa nominal
        cmbTipo.SelectedIndexChanged += (_, _) => numCap.Enabled = cmbTipo.SelectedIndex == 1;

        // Campos de captura en dos columnas (etiqueta | control)
        var campos = new TableLayoutPanel { AutoSize = true, ColumnCount = 2, Dock = DockStyle.Top, Padding = new Padding(8) };
        void Campo(string texto, Control c)
        {
            campos.Controls.Add(new Label { Text = texto, AutoSize = true, Anchor = AnchorStyles.Left, Padding = new Padding(0, 6, 12, 6) });
            campos.Controls.Add(c);
        }
        Campo("Monto de la deuda:", txtMonto);
        Campo("Tasa de interés (%):", txtTasa);
        Campo("Tipo de tasa:", cmbTipo);
        Campo("Capitalizaciones por año:", numCap);
        Campo("Pagos por año:", numPagosAnio);
        Campo("Número de pagos:", numN);
        Campo("Método:", cmbMetodo);

        var botones = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(8, 0, 8, 0) };
        botones.Controls.AddRange(new Control[] { btnCalcular, btnGuardar, btnHistorial });

        // Diseño vertical: campos / botones / resumen / tabla (ocupa el resto)
        var raiz = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4 };
        raiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        raiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        raiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        raiz.Controls.Add(campos, 0, 0);
        raiz.Controls.Add(botones, 0, 1);
        raiz.Controls.Add(lblResumen, 0, 2);
        raiz.Controls.Add(grid, 0, 3);
        Controls.Add(raiz);

        btnCalcular.Click += (_, _) => Calcular();
        btnGuardar.Click += (_, _) => Guardar();
        btnHistorial.Click += (_, _) => { using var f = new FormHistorial(); f.ShowDialog(this); };

        if (idEditar != null)
        {
            btnGuardar.Text = "Actualizar";
            btnHistorial.Visible = false;
            Load += (_, _) => Precargar(idEditar.Value);
        }
    }

    // Lee los campos y valida; regresa false si algo es incorrecto
    bool TryLeer(out Calculo c)
    {
        c = null!;
        if (!decimal.TryParse(txtMonto.Text, out var monto) || monto <= 0) { Aviso("Escribe un monto mayor a 0."); return false; }
        if (!decimal.TryParse(txtTasa.Text, out var tasa) || tasa <= 0) { Aviso("Escribe una tasa mayor a 0."); return false; }

        c = Calculadora.Calcular(cmbMetodo.SelectedIndex + 1, monto, tasa, cmbTipo.SelectedIndex + 1,
                                 (int)numCap.Value, (int)numPagosAnio.Value, (int)numN.Value);
        return true;
    }

    void Calcular()
    {
        if (!TryLeer(out var c)) return;
        Mostrar(c);
    }

    void Mostrar(Calculo c)
    {
        _actual = c;
        lblResumen.Text = $"Tasa efectiva por periodo: {c.TasaPeriodo * 100:0.0000}%   |   " +
                          $"Total intereses: {c.Filas.Sum(f => f.Interes):C2}   |   Total pagado: {c.Filas.Sum(f => f.Pago):C2}";
        grid.DataSource = c.Filas;
        FormatoGrid.Moneda(grid);
        btnGuardar.Enabled = true;
    }

    void Guardar()
    {
        if (_actual == null) return;
        try
        {
            if (_idEditar == null)
            {
                long id = App.Bd.Guardar(_actual);
                MessageBox.Show($"Guardado con el #{id}.");
            }
            else
            {
                App.Bd.Actualizar(_idEditar.Value, _actual);
                MessageBox.Show($"Cálculo #{_idEditar} actualizado.");
                DialogResult = DialogResult.OK;   // Cierra la ventana de edición
            }
        }
        catch (Exception) { Aviso("No se guardó: revisa que MySQL esté iniciado en XAMPP."); }
    }

    // Modo editar: llena los campos con los datos guardados y muestra su cuadro
    void Precargar(long id)
    {
        try
        {
            var c = App.Bd.Obtener(id);
            if (c == null) { Aviso("No existe ese cálculo."); Close(); return; }
            txtMonto.Text = c.Monto.ToString();
            txtTasa.Text = c.Tasa.ToString();
            cmbTipo.SelectedIndex = c.Tipo - 1;
            cmbMetodo.SelectedIndex = c.Metodo - 1;
            numCap.Value = c.Cap;
            numPagosAnio.Value = c.PagosAnio;
            numN.Value = c.N;
            Mostrar(c);
        }
        catch (Exception) { Aviso("No se pudo leer la base de datos."); Close(); }
    }

    static void Aviso(string texto) => MessageBox.Show(texto, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}

// Da formato de moneda (C2) a todas las columnas decimales de una tabla
static class FormatoGrid
{
    public static void Moneda(DataGridView g)
    {
        foreach (DataGridViewColumn col in g.Columns)
            if (col.ValueType == typeof(decimal)) col.DefaultCellStyle.Format = "C2";
    }
}
