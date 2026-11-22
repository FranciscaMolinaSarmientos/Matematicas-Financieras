using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraAmortizacion
{
    // Un renglón de la tabla de amortización (un periodo)
    internal class FilaAmortizacion
    {
        //atributos
        private int periodo;
        private decimal pago;
        private decimal interes;
        private decimal amortizacion;
        private decimal saldo;

        public FilaAmortizacion()
        {
        }

        public FilaAmortizacion(int periodo, decimal pago, decimal interes, decimal amortizacion, decimal saldo)
        {
            this.periodo = periodo;
            this.pago = pago;
            this.interes = interes;
            this.amortizacion = amortizacion;
            this.saldo = saldo;
        }

        public int Periodo { get => periodo; set => periodo = value; }
        public decimal Pago { get => pago; set => pago = value; }
        public decimal Interes { get => interes; set => interes = value; }
        public decimal Amortizacion { get => amortizacion; set => amortizacion = value; }
        public decimal Saldo { get => saldo; set => saldo = value; }
    }
}
