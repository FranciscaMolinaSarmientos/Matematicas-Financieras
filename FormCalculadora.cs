using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CalculadoraAmortizacion
{
    // Captura de datos, cálculo de la tabla de amortización, guardar y modificar
    public partial class FormCalculadora : FormPlantilla
    {
        // Préstamo que se calculó y se muestra en pantalla
        private Prestamo prestamo;

        public FormCalculadora()
        {
            InitializeComponent();
            comboBoxTipoTasa.SelectedIndex = 0;
            comboBoxMetodo.SelectedIndex = 0;
        }

        // Las capitalizaciones solo aplican con tasa nominal
        private void comboBoxTipoTasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTipoTasa.Text == "Nominal capitalizable")
            {
                textBoxCapitalizaciones.Enabled = true;
            }
            else
            {
                textBoxCapitalizaciones.Text = "1";
                textBoxCapitalizaciones.Enabled = false;
            }
        }

        private void buttonCalcular_Click(object sender, EventArgs e)
        {
            string mensaje = validar();
            if (mensaje != "")
            {
                MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                prestamo = leerDatos();
                prestamo.Calcular();
                mostrar(prestamo);

                // Si se está modificando un cálculo del historial, se guarda con Modificar
                if (Estatica.PrestamoActual == null)
                {
                    buttonGuardar.Enabled = true;
                }
            }
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (prestamo == null)
            {
                MessageBox.Show("Primero calcula la tabla de amortización");
            }
            else
            {
                int id = Operaciones.agregar(prestamo);
                if (id != 0)
                {
                    MessageBox.Show(String.Format("Cálculo guardado con el número {0}", id));
                    buttonGuardar.Enabled = false;
                }
                else
                {
                    MessageBox.Show("El cálculo no pudo ser guardado");
                }
            }
        }

        private void buttonHistorial_Click(object sender, EventArgs e)
        {
            Estatica.PrestamoActual = null;
            FormHistorial frmHistorial = new FormHistorial();
            frmHistorial.ShowDialog();

            // Si en el historial se eligió un cálculo para modificar, se cargan sus datos
            if (Estatica.PrestamoActual != null)
            {
                prestamo = Estatica.PrestamoActual;
                textBoxMonto.Text = Convert.ToString(prestamo.Monto);
                textBoxTasa.Text = Convert.ToString(prestamo.Tasa);
                comboBoxTipoTasa.Text = prestamo.TipoTasa;
                textBoxCapitalizaciones.Text = Convert.ToString(prestamo.Capitalizaciones);
                textBoxPagosAnio.Text = Convert.ToString(prestamo.PagosAnio);
                textBoxNumPagos.Text = Convert.ToString(prestamo.NumPagos);
                comboBoxMetodo.Text = prestamo.Metodo;
                mostrar(prestamo);

                buttonGuardar.Enabled = false;
                buttonModificar.Enabled = true;
            }
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            string mensaje = validar();
            if (mensaje != "")
            {
                MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Se vuelve a calcular con los datos que estén en pantalla
                prestamo = leerDatos();
                prestamo.Id = Estatica.PrestamoActual.Id;
                prestamo.Calcular();
                mostrar(prestamo);

                int resultado = Operaciones.modificar(prestamo);
                if (resultado != 0)
                {
                    MessageBox.Show(String.Format("Cálculo número {0} modificado", prestamo.Id));
                    limpiar();
                }
                else
                {
                    MessageBox.Show("El cálculo no pudo ser modificado");
                }
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        // Revisa que los datos sean correctos; regresa "" si todo está bien
        private string validar()
        {
            decimal monto;
            decimal tasa;
            int capitalizaciones;
            int pagosAnio;
            int numPagos;

            if (textBoxMonto.Text == "" || textBoxTasa.Text == "" || textBoxCapitalizaciones.Text == "" ||
                textBoxPagosAnio.Text == "" || textBoxNumPagos.Text == "")
            {
                return "Llena todos los campos";
            }
            else if (!decimal.TryParse(textBoxMonto.Text, out monto) || monto <= 0)
            {
                return "Escribe un monto mayor a 0";
            }
            else if (!decimal.TryParse(textBoxTasa.Text, out tasa) || tasa <= 0)
            {
                return "Escribe una tasa mayor a 0";
            }
            else if (!int.TryParse(textBoxCapitalizaciones.Text, out capitalizaciones) || capitalizaciones <= 0)
            {
                return "Las capitalizaciones por año deben ser un número entero mayor a 0";
            }
            else if (!int.TryParse(textBoxPagosAnio.Text, out pagosAnio) || pagosAnio <= 0)
            {
                return "Los pagos por año deben ser un número entero mayor a 0";
            }
            else if (!int.TryParse(textBoxNumPagos.Text, out numPagos) || numPagos <= 0)
            {
                return "El número de pagos debe ser un número entero mayor a 0";
            }
            else
            {
                return "";
            }
        }

        // Carga todos los datos de la pantalla en un objeto Prestamo
        private Prestamo leerDatos()
        {
            Prestamo p = new Prestamo();
            p.Monto = Convert.ToDecimal(textBoxMonto.Text);
            p.Tasa = Convert.ToDecimal(textBoxTasa.Text);
            p.TipoTasa = comboBoxTipoTasa.Text;
            p.Capitalizaciones = Convert.ToInt32(textBoxCapitalizaciones.Text);
            p.PagosAnio = Convert.ToInt32(textBoxPagosAnio.Text);
            p.NumPagos = Convert.ToInt32(textBoxNumPagos.Text);
            p.Metodo = comboBoxMetodo.Text;
            return p;
        }

        // Muestra el resumen y la tabla de amortización
        private void mostrar(Prestamo p)
        {
            labelResumen.Text = p.Resumen();
            dataGridViewTabla.DataSource = null;
            dataGridViewTabla.DataSource = p.Tabla;
            formatoMoneda(dataGridViewTabla);
        }

        private void limpiar()
        {
            textBoxMonto.Clear();
            textBoxTasa.Clear();
            textBoxPagosAnio.Text = "12";
            textBoxNumPagos.Clear();
            comboBoxTipoTasa.SelectedIndex = 0;
            comboBoxMetodo.SelectedIndex = 0;
            labelResumen.Text = "";
            dataGridViewTabla.DataSource = null;
            prestamo = null;
            Estatica.PrestamoActual = null;
            buttonGuardar.Enabled = false;
            buttonModificar.Enabled = false;
        }
    }
}
