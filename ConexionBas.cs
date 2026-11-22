using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraAmortizacion
{
    internal class ConexionBas
    {
        // Abre la conexión a la base de datos Amortizacion de SQL Server Express
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion;
            conexion = new SqlConnection("Data Source=localhost\\SQLEXPRESS; Initial Catalog=Amortizacion; Integrated Security=True");
            conexion.Open();
            return conexion;
        }
    }
}
