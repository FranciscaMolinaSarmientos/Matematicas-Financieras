using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraAmortizacion
{
    internal static class Estatica
    {
        // Préstamo que se eligió en el Historial para pasarlo a la Calculadora
        private static Prestamo prestamoActual;

        public static Prestamo PrestamoActual { get => prestamoActual; set => prestamoActual = value; }
    }
}
