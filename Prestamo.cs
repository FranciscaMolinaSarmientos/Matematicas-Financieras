using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraAmortizacion
{
    // Un préstamo (una simulación): datos capturados y su tabla de amortización
    internal class Prestamo
    {
        //atributos
        private int id;
        private DateTime fecha;
        private string metodo;              // "Gradual" o "Constante"
        private decimal monto;
        private decimal tasa;               // Tasa anual en porcentaje, ej. 12
        private string tipoTasa;            // "Efectiva anual" o "Nominal capitalizable"
        private int capitalizaciones;       // Capitalizaciones por año (solo tasa nominal)
        private int pagosAnio;
        private int numPagos;
        private double tasaPeriodo;
        private List<FilaAmortizacion> tabla;

        public Prestamo()
        {
            tabla = new List<FilaAmortizacion>();
        }

        public Prestamo(string metodo, decimal monto, decimal tasa, string tipoTasa,
                        int capitalizaciones, int pagosAnio, int numPagos)
        {
            this.metodo = metodo;
            this.monto = monto;
            this.tasa = tasa;
            this.tipoTasa = tipoTasa;
            this.capitalizaciones = capitalizaciones;
            this.pagosAnio = pagosAnio;
            this.numPagos = numPagos;
            tabla = new List<FilaAmortizacion>();
        }

        public int Id { get => id; set => id = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Metodo { get => metodo; set => metodo = value; }
        public decimal Monto { get => monto; set => monto = value; }
        public decimal Tasa { get => tasa; set => tasa = value; }
        public string TipoTasa { get => tipoTasa; set => tipoTasa = value; }
        public int Capitalizaciones { get => capitalizaciones; set => capitalizaciones = value; }
        public int PagosAnio { get => pagosAnio; set => pagosAnio = value; }
        public int NumPagos { get => numPagos; set => numPagos = value; }
        public double TasaPeriodo { get => tasaPeriodo; set => tasaPeriodo = value; }
        public List<FilaAmortizacion> Tabla { get => tabla; set => tabla = value; }

        // Convierte la tasa anual a la tasa equivalente de cada periodo de pago
        public double CalcularTasaPeriodo()
        {
            double t = (double)tasa / 100;

            if (tipoTasa == "Efectiva anual")
            {
                tasaPeriodo = Math.Pow(1 + t, 1.0 / pagosAnio) - 1;
            }
            else
            {
                tasaPeriodo = Math.Pow(1 + t / capitalizaciones, (double)capitalizaciones / pagosAnio) - 1;
            }
            return tasaPeriodo;
        }

        // Arma la tabla de amortización con el método elegido (polimorfismo)
        public void Calcular()
        {
            MetodoAmortizacion calculo;

            if (metodo == "Gradual")
            {
                calculo = new Gradual();
            }
            else
            {
                calculo = new Constante();
            }

            CalcularTasaPeriodo();
            tabla = calculo.Calcular(monto, tasaPeriodo, numPagos);
        }

        public decimal PagoInicial()
        {
            if (tabla.Count == 0)
            {
                return 0;
            }
            return tabla[0].Pago;
        }

        public decimal TotalPagado()
        {
            decimal total = 0;
            foreach (FilaAmortizacion fila in tabla)
            {
                total = total + fila.Pago;
            }
            return total;
        }

        public decimal TotalIntereses()
        {
            decimal total = 0;
            foreach (FilaAmortizacion fila in tabla)
            {
                total = total + fila.Interes;
            }
            return total;
        }

        // Texto con el resumen del cálculo para mostrarlo en el formulario
        public string Resumen()
        {
            return String.Format("Tasa por periodo: {0:0.0000}%    Total de intereses: {1:C2}    Total pagado: {2:C2}",
                tasaPeriodo * 100, TotalIntereses(), TotalPagado());
        }
    }
}
