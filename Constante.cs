using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraAmortizacion
{
    // Método constante: la amortización es igual cada periodo
    // Amortización = deuda / número de pagos; el pago baja porque bajan los intereses
    internal class Constante : MetodoAmortizacion
    {
        public override List<FilaAmortizacion> Calcular(decimal monto, double tasaPeriodo, int numPagos)
        {
            List<FilaAmortizacion> tabla = new List<FilaAmortizacion>();
            decimal amortizacionFija = Redondear(monto / numPagos);
            decimal saldo = monto;

            for (int k = 1; k <= numPagos; k++)
            {
                decimal interes = Redondear(saldo * (decimal)tasaPeriodo);   // Interés sobre saldo insoluto
                decimal amortizacion = amortizacionFija;

                // En el último periodo se liquida lo que reste
                if (k == numPagos)
                {
                    amortizacion = saldo;
                }

                saldo = saldo - amortizacion;
                tabla.Add(new FilaAmortizacion(k, amortizacion + interes, interes, amortizacion, saldo));
            }
            return tabla;
        }
    }
}
