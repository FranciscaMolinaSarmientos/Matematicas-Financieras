using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CalculadoraAmortizacion
{
    // Lista de cálculos guardados; permite ver su tabla, elegir uno para modificar y eliminar
    public partial class FormHistorial : FormPlantilla
    {
        public FormHistorial()
        {
            InitializeComponent();
        }

        private void FormHistorial_Load(object sender, EventArgs e)
        {
            cargar();
        }

        // Al cambiar de renglón se muestra la tabla de amortización de ese cálculo
        private void dataGridViewSimulaciones_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewSimulaciones.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridViewSimulaciones.CurrentRow.Cells["Id"].Value);
                dataGridViewDetalle.DataSource = Operaciones.detalle(id);
                formatoMoneda(dataGridViewDetalle);
            }
            else
            {
                dataGridViewDetalle.DataSource = null;
            }
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            if (dataGridViewSimulaciones.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridViewSimulaciones.CurrentRow.Cells["Id"].Value);
                Estatica.PrestamoActual = Operaciones.selecPrestamo(id);
                this.Close();
            }
            else
            {
                MessageBox.Show("Selecciona un cálculo");
            }
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewSimulaciones.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un cálculo");
            }
            else if (MessageBox.Show("Está seguro de eliminar el cálculo", "Confirme", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dataGridViewSimulaciones.CurrentRow.Cells["Id"].Value);
                int resultado = Operaciones.eliminar(id);
                if (resultado > 0)
                {
                    MessageBox.Show("Cálculo eliminado");
                    cargar();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar");
                }
            }
            else
            {
                MessageBox.Show("Eliminación cancelada");
            }
        }

        private void buttonRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Lee de la base de datos todos los cálculos guardados
        private void cargar()
        {
            dataGridViewSimulaciones.DataSource = null;
            dataGridViewSimulaciones.DataSource = Operaciones.listar();
            formatoMoneda(dataGridViewSimulaciones);

            // Columnas que no hace falta ver en la lista
            ocultarColumna("Capitalizaciones");
            ocultarColumna("PagosAnio");
            ocultarColumna("TasaPeriodo");
            ocultarColumna("Tabla");

            if (dataGridViewSimulaciones.Columns["Fecha"] != null)
            {
                dataGridViewSimulaciones.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }

        private void ocultarColumna(string nombre)
        {
            if (dataGridViewSimulaciones.Columns[nombre] != null)
            {
                dataGridViewSimulaciones.Columns[nombre].Visible = false;
            }
        }
    }
}
