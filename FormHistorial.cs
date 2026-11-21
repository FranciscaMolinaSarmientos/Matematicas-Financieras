// VENTANA 2: historial guardado. Arriba la lista de cálculos; abajo el cuadro del seleccionado.
// Permite Editar (abre la Ventana 1 en modo edición) y Eliminar.
class FormHistorial : Form
{
    readonly DataGridView gridLista = new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, MultiSelect = false,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    readonly DataGridView gridDetalle = new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    readonly Button btnEditar = new() { Text = "Editar", AutoSize = true };
    readonly Button btnEliminar = new() { Text = "Eliminar", AutoSize = true };
    readonly Button btnRefrescar = new() { Text = "Refrescar", AutoSize = true };

    public FormHistorial()
    {
        Text = "Historial de cálculos";
        Size = new Size(760, 600);
        StartPosition = FormStartPosition.CenterParent;

        var botones = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(8) };
        botones.Controls.AddRange(new Control[] { btnEditar, btnEliminar, btnRefrescar });

        var raiz = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        raiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
        raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
        raiz.Controls.Add(botones, 0, 0);
        raiz.Controls.Add(gridLista, 0, 1);
        raiz.Controls.Add(gridDetalle, 0, 2);
        Controls.Add(raiz);

        gridLista.SelectionChanged += (_, _) => MostrarDetalle();
        btnRefrescar.Click += (_, _) => Cargar();
        btnEditar.Click += (_, _) => Editar();
        btnEliminar.Click += (_, _) => Eliminar();
        Load += (_, _) => Cargar();
    }

    long? IdSeleccionado => gridLista.CurrentRow?.DataBoundItem is Resumen r ? r.Id : null;

    // READ (lista)
    void Cargar()
    {
        try
        {
            gridLista.DataSource = App.Bd.ListarResumen();
            FormatoGrid.Moneda(gridLista);
            if (gridLista.Columns["Fecha"] != null) gridLista.Columns["Fecha"]!.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            MostrarDetalle();
        }
        catch (Exception) { MessageBox.Show("No se pudo leer la base de datos."); }
    }

    // READ (detalle del cálculo seleccionado)
    void MostrarDetalle()
    {
        if (IdSeleccionado is not long id) { gridDetalle.DataSource = null; return; }
        try
        {
            gridDetalle.DataSource = App.Bd.Detalle(id);
            FormatoGrid.Moneda(gridDetalle);
        }
        catch (Exception) { MessageBox.Show("No se pudo leer la base de datos."); }
    }

    // UPDATE: abre la Ventana 1 en modo edición y refresca al volver
    void Editar()
    {
        if (IdSeleccionado is not long id) return;
        using var f = new FormCalculadora(id);
        f.ShowDialog(this);
        Cargar();
    }

    // DELETE con confirmación
    void Eliminar()
    {
        if (IdSeleccionado is not long id) return;
        if (MessageBox.Show($"¿Eliminar el #{id}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try { App.Bd.Eliminar(id); Cargar(); }
        catch (Exception) { MessageBox.Show("No se pudo eliminar. Revisa que MySQL esté iniciado en XAMPP."); }
    }
}
