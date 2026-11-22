using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CalculadoraAmortizacion
{
    // Formulario base: encabezado y botón Salir. Los demás formularios heredan de él.
    public partial class FormPlantilla : Form
    {
        public FormPlantilla()
        {
            InitializeComponent();
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Da formato de pesos a las columnas con cantidades de una tabla (lo heredan los demás formularios)
        protected void formatoMoneda(DataGridView tabla)
        {
            foreach (DataGridViewColumn columna in tabla.Columns)
            {
                if (columna.ValueType == typeof(decimal))
                {
                    columna.DefaultCellStyle.Format = "C2";
                }
            }
            ponerEncabezados(tabla);
        }

        // Cambia los nombres de las propiedades por encabezados con acentos y espacios
        private void ponerEncabezados(DataGridView tabla)
        {
            string[] propiedades = { "Id", "Metodo", "Tasa", "TipoTasa", "NumPagos", "Interes", "Amortizacion" };
            string[] encabezados = { "Número", "Método", "Tasa (%)", "Tipo de tasa", "Número de pagos", "Interés", "Amortización" };

            for (int i = 0; i < propiedades.Length; i++)
            {
                if (tabla.Columns[propiedades[i]] != null)
                {
                    tabla.Columns[propiedades[i]].HeaderText = encabezados[i];
                }
            }
        }
    }
}
