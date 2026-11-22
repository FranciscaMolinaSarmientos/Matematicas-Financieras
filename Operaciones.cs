using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraAmortizacion
{
    // Todas las operaciones con la base de datos (agregar, listar, buscar, modificar y eliminar)
    internal class Operaciones
    {
        // Guarda el préstamo y su tabla; regresa el id que le asignó SQL Server
        public static int agregar(Prestamo pPre)
        {
            int id = 0;
            SqlConnection con = ConexionBas.ObtenerConexion();
            string cadena = "INSERT INTO Simulacion (metodo, monto, tasa, tipo_tasa, capitalizaciones, pagos_anio, " +
                "num_pagos, tasa_periodo, pago_inicial, total_pagado, total_intereses) " +
                "VALUES (@metodo, @monto, @tasa, @tipo, @cap, @pagos, @num, @tasaPer, @pagoIni, @total, @intereses); " +
                "SELECT SCOPE_IDENTITY();";
            SqlCommand comando = new SqlCommand(cadena, con);
            agregarParametros(comando, pPre);
            id = Convert.ToInt32(comando.ExecuteScalar());

            insertarDetalle(con, id, pPre.Tabla);
            con.Close();
            return id;
        }

        // Regresa todos los préstamos guardados (sin su tabla), del más reciente al más antiguo
        public static List<Prestamo> listar()
        {
            List<Prestamo> lista = new List<Prestamo>();
            SqlConnection con = ConexionBas.ObtenerConexion();
            string cadena = "SELECT id_simulacion, fecha, metodo, monto, tasa, tipo_tasa, capitalizaciones, " +
                "pagos_anio, num_pagos, tasa_periodo FROM Simulacion ORDER BY id_simulacion DESC";
            SqlCommand comando = new SqlCommand(cadena, con);
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(leerPrestamo(reader));
            }
            reader.Close();
            con.Close();
            return lista;
        }

        // Regresa un préstamo con su tabla de amortización
        public static Prestamo selecPrestamo(int pId)
        {
            Prestamo p = null;
            SqlConnection con = ConexionBas.ObtenerConexion();
            string cadena = "SELECT id_simulacion, fecha, metodo, monto, tasa, tipo_tasa, capitalizaciones, " +
                "pagos_anio, num_pagos, tasa_periodo FROM Simulacion WHERE id_simulacion = @id";
            SqlCommand comando = new SqlCommand(cadena, con);
            comando.Parameters.AddWithValue("@id", pId);
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.Read())
            {
                p = leerPrestamo(reader);
            }
            reader.Close();
            con.Close();

            if (p != null)
            {
                p.Tabla = detalle(pId);
            }
            return p;
        }

        // Regresa la tabla de amortización guardada de un préstamo
        public static List<FilaAmortizacion> detalle(int pId)
        {
            List<FilaAmortizacion> tabla = new List<FilaAmortizacion>();
            SqlConnection con = ConexionBas.ObtenerConexion();
            string cadena = "SELECT periodo, pago, interes, amortizacion, saldo FROM Detalle " +
                "WHERE id_simulacion = @id ORDER BY periodo";
            SqlCommand comando = new SqlCommand(cadena, con);
            comando.Parameters.AddWithValue("@id", pId);
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                FilaAmortizacion f = new FilaAmortizacion();
                f.Periodo = reader.GetInt32(0);
                f.Pago = reader.GetDecimal(1);
                f.Interes = reader.GetDecimal(2);
                f.Amortizacion = reader.GetDecimal(3);
                f.Saldo = reader.GetDecimal(4);
                tabla.Add(f);
            }
            reader.Close();
            con.Close();
            return tabla;
        }

        // Actualiza los datos del préstamo y reemplaza su tabla; regresa cuántos renglones cambió
        public static int modificar(Prestamo pPre)
        {
            int retorno = 0;
            SqlConnection con = ConexionBas.ObtenerConexion();
            string cadena = "UPDATE Simulacion SET fecha = GETDATE(), metodo = @metodo, monto = @monto, tasa = @tasa, " +
                "tipo_tasa = @tipo, capitalizaciones = @cap, pagos_anio = @pagos, num_pagos = @num, " +
                "tasa_periodo = @tasaPer, pago_inicial = @pagoIni, total_pagado = @total, " +
                "total_intereses = @intereses WHERE id_simulacion = @id";
            SqlCommand comando = new SqlCommand(cadena, con);
            agregarParametros(comando, pPre);
            comando.Parameters.AddWithValue("@id", pPre.Id);
            retorno = comando.ExecuteNonQuery();

            // Se borra la tabla anterior y se guarda la nueva
            SqlCommand borrar = new SqlCommand("DELETE FROM Detalle WHERE id_simulacion = @id", con);
            borrar.Parameters.AddWithValue("@id", pPre.Id);
            borrar.ExecuteNonQuery();
            insertarDetalle(con, pPre.Id, pPre.Tabla);

            con.Close();
            return retorno;
        }

        // Borra el préstamo (su detalle se borra solo por el ON DELETE CASCADE)
        public static int eliminar(int pId)
        {
            int retorno = 0;
            SqlConnection con = ConexionBas.ObtenerConexion();
            SqlCommand comando = new SqlCommand("DELETE FROM Simulacion WHERE id_simulacion = @id", con);
            comando.Parameters.AddWithValue("@id", pId);
            retorno = comando.ExecuteNonQuery();
            con.Close();
            return retorno;
        }

        // Parámetros que comparten agregar y modificar
        private static void agregarParametros(SqlCommand comando, Prestamo pPre)
        {
            comando.Parameters.AddWithValue("@metodo", pPre.Metodo);
            comando.Parameters.AddWithValue("@monto", pPre.Monto);
            comando.Parameters.AddWithValue("@tasa", pPre.Tasa);
            comando.Parameters.AddWithValue("@tipo", pPre.TipoTasa);
            comando.Parameters.AddWithValue("@cap", pPre.Capitalizaciones);
            comando.Parameters.AddWithValue("@pagos", pPre.PagosAnio);
            comando.Parameters.AddWithValue("@num", pPre.NumPagos);
            comando.Parameters.AddWithValue("@tasaPer", Convert.ToDecimal(pPre.TasaPeriodo));
            comando.Parameters.AddWithValue("@pagoIni", pPre.PagoInicial());
            comando.Parameters.AddWithValue("@total", pPre.TotalPagado());
            comando.Parameters.AddWithValue("@intereses", pPre.TotalIntereses());
        }

        // Guarda cada renglón de la tabla de amortización
        private static void insertarDetalle(SqlConnection con, int idSimulacion, List<FilaAmortizacion> tabla)
        {
            foreach (FilaAmortizacion f in tabla)
            {
                string cadena = "INSERT INTO Detalle (id_simulacion, periodo, pago, interes, amortizacion, saldo) " +
                    "VALUES (@id, @periodo, @pago, @interes, @amort, @saldo)";
                SqlCommand comando = new SqlCommand(cadena, con);
                comando.Parameters.AddWithValue("@id", idSimulacion);
                comando.Parameters.AddWithValue("@periodo", f.Periodo);
                comando.Parameters.AddWithValue("@pago", f.Pago);
                comando.Parameters.AddWithValue("@interes", f.Interes);
                comando.Parameters.AddWithValue("@amort", f.Amortizacion);
                comando.Parameters.AddWithValue("@saldo", f.Saldo);
                comando.ExecuteNonQuery();
            }
        }

        // Arma un Prestamo con el renglón que se está leyendo
        private static Prestamo leerPrestamo(SqlDataReader reader)
        {
            Prestamo p = new Prestamo();
            p.Id = reader.GetInt32(0);
            p.Fecha = reader.GetDateTime(1);
            p.Metodo = reader.GetString(2);
            p.Monto = reader.GetDecimal(3);
            p.Tasa = reader.GetDecimal(4);
            p.TipoTasa = reader.GetString(5);
            p.Capitalizaciones = reader.GetInt32(6);
            p.PagosAnio = reader.GetInt32(7);
            p.NumPagos = reader.GetInt32(8);
            p.TasaPeriodo = Convert.ToDouble(reader.GetDecimal(9));
            return p;
        }
    }
}
