using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraAmortizacion
{
    // Clase abstracta: dice QUÉ debe hacer cualquier método de amortización,
    // pero cada clase hija (Gradual y Constante) decide CÓMO lo hace
    internal abstract class MetodoAmortizacion
    {
        // Cada método arma su propia tabla de amortización
        public abstract List<FilaAmortizacion> Calcular(decimal monto, double tasaPeriodo, int numPagos);

        // Método normal (no abstracto) que heredan las dos clases hijas
        public decimal Redondear(decimal cantidad)
        {
            return Math.Round(cantidad, 2);
        }
    }
}
