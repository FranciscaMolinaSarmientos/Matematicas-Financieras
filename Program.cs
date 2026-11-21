using System.Globalization;

// Formato mexicano: punto decimal y moneda MXN
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-MX");
CultureInfo.CurrentCulture = new CultureInfo("es-MX");

ApplicationConfiguration.Initialize();

// Crea la base y las tablas si no existen (si MySQL no está iniciado, solo avisa)
try { App.Bd.Crear(); }
catch (Exception ex) { MessageBox.Show("No se pudo conectar a MySQL (¿XAMPP iniciado?):\n" + ex.Message); }

Application.Run(new FormCalculadora());   // Ventana 1