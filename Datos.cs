using MySqlConnector;

// Una fila del cuadro de amortización (record = clase de datos inmutable)
record Fila(int Periodo, decimal Pago, decimal Interes, decimal Amortizacion, decimal Saldo);

// Un cálculo completo: datos capturados + tasa por periodo + todas las filas
record Calculo(int Metodo, decimal Monto, decimal Tasa, int Tipo, int Cap, int PagosAnio, int N, double TasaPeriodo, List<Fila> Filas);

// Clase ABSTRACTA: define QUÉ debe hacer cualquier método de amortización,
// pero cada subclase decide CÓMO lo hace
abstract class Amortizador
{
    public abstract List<Fila> Calcular(decimal monto, double i, int n);
}

// MÉTODO GRADUAL: todos los pagos son iguales (Ejemplos 1 y 2 del PDF)
// Fórmula de la renta: R = A·i / (1 − (1+i)^−N)
class Gradual : Amortizador
{
    public override List<Fila> Calcular(decimal monto, double i, int n)
    {
        // Si la tasa es 0 el pago es simplemente monto/n; si no, se usa la fórmula
        double renta = i == 0 ? (double)monto / n : (double)monto * i / (1 - Math.Pow(1 + i, -n));
        decimal R = Math.Round((decimal)renta, 2);

        var filas = new List<Fila>();
        decimal saldo = monto;

        for (int k = 1; k <= n; k++)
        {
            decimal interes = Math.Round(saldo * (decimal)i, 2);   // Interés = saldo anterior × tasa
            decimal pago = R, amort = pago - interes;              // Amortización = pago − interés

            // En el último pago se ajusta para que el saldo quede exactamente en 0
            // (corrige diferencias de centavos por redondeo)
            if (k == n) { amort = saldo; pago = amort + interes; }

            saldo -= amort;                                        // Nuevo saldo
            filas.Add(new Fila(k, pago, interes, amort, saldo));
        }
        return filas;
    }
}

// MÉTODO CONSTANTE: la amortización es igual cada periodo (Ejemplo 4 del PDF)
// Amortización = deuda / número de pagos; el pago total baja porque bajan los intereses
class Constante : Amortizador
{
    public override List<Fila> Calcular(decimal monto, double i, int n)
    {
        decimal fija = Math.Round(monto / n, 2);   // Parte de la deuda que se paga cada periodo
        var filas = new List<Fila>();
        decimal saldo = monto;

        for (int k = 1; k <= n; k++)
        {
            decimal interes = Math.Round(saldo * (decimal)i, 2);   // Interés sobre saldo insoluto
            decimal amort = k == n ? saldo : fija;                 // Último periodo: liquida lo que reste
            saldo -= amort;
            filas.Add(new Fila(k, amort + interes, interes, amort, saldo));   // Pago = amortización + interés
        }
        return filas;
    }
}

// Clase que se encarga de TODO el acceso a MySQL
class BaseDatos
{
    private readonly string _cs;                       // Cadena de conexión
    public BaseDatos(string cs) => _cs = cs;

    // Crea la base de datos y las dos tablas si no existen
    public void Crear()
    {
        // Primero se conecta SIN indicar base de datos, para poder crearla
        var csb = new MySqlConnectionStringBuilder(_cs);
        string db = csb.Database;
        csb.Database = "";
        using (var cn = new MySqlConnection(csb.ConnectionString))
        {
            cn.Open();
            new MySqlCommand($"CREATE DATABASE IF NOT EXISTS `{db}` CHARACTER SET utf8mb4", cn).ExecuteNonQuery();
        }

        // Luego se conecta a la base ya creada y crea las tablas
        using var cn2 = new MySqlConnection(_cs);
        cn2.Open();

        // Tabla "simulaciones": un renglón por cada cálculo (datos capturados y totales)
        new MySqlCommand(@"CREATE TABLE IF NOT EXISTS simulaciones (
            id BIGINT AUTO_INCREMENT PRIMARY KEY,
            fecha DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
            metodo TINYINT NOT NULL, monto DECIMAL(18,2) NOT NULL,
            tasa_porcentaje DECIMAL(9,4) NOT NULL, tipo_tasa TINYINT NOT NULL,
            capitalizaciones_anio INT NOT NULL, pagos_anio INT NOT NULL, num_pagos INT NOT NULL,
            tasa_periodo DECIMAL(12,8) NOT NULL, pago_inicial DECIMAL(18,2) NOT NULL,
            total_pagado DECIMAL(18,2) NOT NULL, total_intereses DECIMAL(18,2) NOT NULL)", cn2).ExecuteNonQuery();

        // Tabla "detalle_amortizacion": una fila por cada periodo del cuadro.
        // ON DELETE CASCADE: al borrar un cálculo se borra también su detalle.
        new MySqlCommand(@"CREATE TABLE IF NOT EXISTS detalle_amortizacion (
            id BIGINT AUTO_INCREMENT PRIMARY KEY, simulacion_id BIGINT NOT NULL, periodo INT NOT NULL,
            pago DECIMAL(18,2) NOT NULL, interes DECIMAL(18,2) NOT NULL,
            amortizacion DECIMAL(18,2) NOT NULL, saldo DECIMAL(18,2) NOT NULL,
            FOREIGN KEY (simulacion_id) REFERENCES simulaciones(id) ON DELETE CASCADE)", cn2).ExecuteNonQuery();
    }

    // CREATE: guarda el cálculo y su cuadro; regresa el id generado
    public long Guardar(Calculo c)
    {
        using var cn = new MySqlConnection(_cs);
        cn.Open();
        using var tx = cn.BeginTransaction();   // Transacción: o se guarda todo o no se guarda nada

        var cmd = new MySqlCommand(@"INSERT INTO simulaciones
            (metodo, monto, tasa_porcentaje, tipo_tasa, capitalizaciones_anio, pagos_anio, num_pagos,
             tasa_periodo, pago_inicial, total_pagado, total_intereses)
            VALUES (@m,@monto,@tasa,@tipo,@cap,@pagos,@n,@i,@pi,@tot,@int); SELECT LAST_INSERT_ID();", cn, tx);
        Parametros(cmd, c);
        long id = Convert.ToInt64(cmd.ExecuteScalar());   // id del renglón recién insertado

        InsertarDetalle(cn, tx, id, c.Filas);             // Guarda cada fila del cuadro
        tx.Commit();                                      // Confirma los cambios
        return id;
    }

    // READ: regresa los últimos 50 cálculos como líneas de texto ya formateadas
    public List<string> Listar()
    {
        var lista = new List<string>();
        using var cn = new MySqlConnection(_cs);
        cn.Open();
        var cmd = new MySqlCommand("SELECT id, fecha, metodo, monto, tasa_porcentaje, num_pagos FROM simulaciones ORDER BY id DESC LIMIT 50", cn);
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
            lista.Add($"{rd.GetInt64(0),4} {rd.GetDateTime(1):dd/MM/yyyy HH:mm} {(rd.GetInt32(2) == 1 ? "Gradual" : "Constante"),-10} " +
                      $"{rd.GetDecimal(3),16:C2} {rd.GetDecimal(4),8} {rd.GetInt32(5),6}");
        return lista;
    }

    // READ: regresa las filas del cuadro de amortización de un cálculo
    public List<Fila> Detalle(long id)
    {
        var filas = new List<Fila>();
        using var cn = new MySqlConnection(_cs);
        cn.Open();
        var cmd = new MySqlCommand("SELECT periodo, pago, interes, amortizacion, saldo FROM detalle_amortizacion WHERE simulacion_id=@id ORDER BY periodo", cn);
        cmd.Parameters.AddWithValue("@id", id);   // Parámetro: evita errores y SQL injection
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
            filas.Add(new Fila(rd.GetInt32(0), rd.GetDecimal(1), rd.GetDecimal(2), rd.GetDecimal(3), rd.GetDecimal(4)));
        return filas;
    }

    // READ (ventanas): lista los últimos 50 cálculos como objetos para el DataGridView
    public List<Resumen> ListarResumen()
    {
        var lista = new List<Resumen>();
        using var cn = new MySqlConnection(_cs);
        cn.Open();
        var cmd = new MySqlCommand("SELECT id, fecha, metodo, monto, tasa_porcentaje, num_pagos FROM simulaciones ORDER BY id DESC LIMIT 50", cn);
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
            lista.Add(new Resumen(rd.GetInt64(0), rd.GetDateTime(1), rd.GetInt32(2) == 1 ? "Gradual" : "Constante",
                                  rd.GetDecimal(3), rd.GetDecimal(4), rd.GetInt32(5)));
        return lista;
    }

    // READ (ventanas): recarga un cálculo completo para poder editarlo
    public Calculo? Obtener(long id)
    {
        using var cn = new MySqlConnection(_cs);
        cn.Open();
        var cmd = new MySqlCommand("SELECT metodo, monto, tasa_porcentaje, tipo_tasa, capitalizaciones_anio, pagos_anio, num_pagos, tasa_periodo FROM simulaciones WHERE id=@id", cn);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        if (!rd.Read()) return null;
        return new Calculo(rd.GetInt32(0), rd.GetDecimal(1), rd.GetDecimal(2), rd.GetInt32(3), rd.GetInt32(4),
                           rd.GetInt32(5), rd.GetInt32(6), (double)rd.GetDecimal(7), Detalle(id));
    }

    // UPDATE: actualiza los datos del cálculo y reemplaza todas sus filas de detalle
    public void Actualizar(long id, Calculo c)
    {
        using var cn = new MySqlConnection(_cs);
        cn.Open();
        using var tx = cn.BeginTransaction();

        var cmd = new MySqlCommand(@"UPDATE simulaciones SET fecha=NOW(), metodo=@m, monto=@monto, tasa_porcentaje=@tasa,
            tipo_tasa=@tipo, capitalizaciones_anio=@cap, pagos_anio=@pagos, num_pagos=@n, tasa_periodo=@i,
            pago_inicial=@pi, total_pagado=@tot, total_intereses=@int WHERE id=@id", cn, tx);
        Parametros(cmd, c);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();

        // Se borra el cuadro anterior y se inserta el nuevo
        var del = new MySqlCommand("DELETE FROM detalle_amortizacion WHERE simulacion_id=@id", cn, tx);
        del.Parameters.AddWithValue("@id", id);
        del.ExecuteNonQuery();

        InsertarDetalle(cn, tx, id, c.Filas);
        tx.Commit();
    }

    // DELETE: borra el cálculo; regresa true si existía
    public bool Eliminar(long id)
    {
        using var cn = new MySqlConnection(_cs);
        cn.Open();
        var cmd = new MySqlCommand("DELETE FROM simulaciones WHERE id=@id", cn);
        cmd.Parameters.AddWithValue("@id", id);
        return cmd.ExecuteNonQuery() > 0;   // ExecuteNonQuery regresa cuántos renglones se borraron
    }

    // Agrega a un comando los parámetros comunes de INSERT y UPDATE (para no repetir código)
    private static void Parametros(MySqlCommand cmd, Calculo c)
    {
        cmd.Parameters.AddWithValue("@m", c.Metodo);
        cmd.Parameters.AddWithValue("@monto", c.Monto);
        cmd.Parameters.AddWithValue("@tasa", c.Tasa);
        cmd.Parameters.AddWithValue("@tipo", c.Tipo);
        cmd.Parameters.AddWithValue("@cap", c.Cap);
        cmd.Parameters.AddWithValue("@pagos", c.PagosAnio);
        cmd.Parameters.AddWithValue("@n", c.N);
        cmd.Parameters.AddWithValue("@i", c.TasaPeriodo);
        cmd.Parameters.AddWithValue("@pi", c.Filas[0].Pago);                 // Pago del primer periodo
        cmd.Parameters.AddWithValue("@tot", c.Filas.Sum(f => f.Pago));       // Total pagado
        cmd.Parameters.AddWithValue("@int", c.Filas.Sum(f => f.Interes));    // Total de intereses
    }

    // Guarda en la tabla detalle_amortizacion cada fila del cuadro
    private static void InsertarDetalle(MySqlConnection cn, MySqlTransaction tx, long id, List<Fila> filas)
    {
        foreach (var f in filas)
        {
            var d = new MySqlCommand(@"INSERT INTO detalle_amortizacion
                (simulacion_id, periodo, pago, interes, amortizacion, saldo) VALUES (@id,@p,@pago,@int,@am,@s)", cn, tx);
            d.Parameters.AddWithValue("@id", id);
            d.Parameters.AddWithValue("@p", f.Periodo);
            d.Parameters.AddWithValue("@pago", f.Pago);
            d.Parameters.AddWithValue("@int", f.Interes);
            d.Parameters.AddWithValue("@am", f.Amortizacion);
            d.Parameters.AddWithValue("@s", f.Saldo);
            d.ExecuteNonQuery();
        }
    }
}
// Fila de la lista del historial
record Resumen(long Id, DateTime Fecha, string Metodo, decimal Monto, decimal Tasa, int Pagos);

// Una sola conexión compartida por las ventanas
static class App
{
    public static readonly BaseDatos Bd = new("Server=localhost;Port=3306;Database=amortizacion_db;User=root;Password=;");
}

// Misma lógica de PedirDatos(), pero recibiendo los valores de la ventana
static class Calculadora
{
    public static Calculo Calcular(int metodo, decimal monto, decimal tasa, int tipo, int cap, int pagosAnio, int n)
    {
        if (tipo == 1) cap = 1;
        double t = (double)tasa / 100;
        double i = tipo == 1
            ? Math.Pow(1 + t, 1.0 / pagosAnio) - 1
            : Math.Pow(1 + t / cap, (double)cap / pagosAnio) - 1;

        Amortizador a = metodo == 1 ? new Gradual() : new Constante();   // Polimorfismo
        return new Calculo(metodo, monto, tasa, tipo, cap, pagosAnio, n, i, a.Calcular(monto, i, n));
    }
}
