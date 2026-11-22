using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraAmortizacion
{
    // Método gradual: todos los pagos son iguales
    // Fórmula de la renta: R = A·i / (1 − (1 + i)^−n)
    internal class Gradual : MetodoAmortizacion
    {
        public override List<FilaAmortizacion> Calcular(decimal monto, double tasaPeriodo, int numPagos)
        {
            List<FilaAmortizacion> tabla = new List<FilaAmortizacion>();
            double renta;

            if (tasaPeriodo == 0)
            {
                renta = (double)monto / numPagos;
            }
            else
            {
                renta = (double)monto * tasaPeriodo / (1 - Math.Pow(1 + tasaPeriodo, -numPagos));
            }

            decimal pagoFijo = Redondear((decimal)renta);
            decimal saldo = monto;

            for (int k = 1; k <= numPagos; k++)
            {
                decimal interes = Redondear(saldo * (decimal)tasaPeriodo);   // Interés = saldo anterior × tasa
                decimal pago = pagoFijo;
                decimal amortizacion = pago - interes;                        // Amortización = pago − interés

                // En el último pago se ajustan los centavos para que el saldo quede en 0
                if (k == numPagos)
                {
                    amortizacion = saldo;
                    pago = amortizacion + interes;
                }

                saldo = saldo - amortizacion;
                tabla.Add(new FilaAmortizacion(k, pago, interes, amortizacion, saldo));
            }
            return tabla;
        }
    }
}
