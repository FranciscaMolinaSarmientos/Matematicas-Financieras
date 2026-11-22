using System.Globalization;

namespace CalculadoraAmortizacion
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Formato mexicano: punto decimal y pesos ($)
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-MX");
            CultureInfo.CurrentCulture = new CultureInfo("es-MX");

            ApplicationConfiguration.Initialize();
            Application.Run(new FormCalculadora());
        }
    }
}
