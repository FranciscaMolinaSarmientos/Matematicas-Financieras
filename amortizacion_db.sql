-- Base de datos de la Calculadora de Amortización (SQL Server)
-- Ejecutar en SQL Server Management Studio en dos partes:
-- Parte 1: solo la línea CREATE DATABASE
-- Parte 2: desde USE hasta el final

-- Parte 1
CREATE DATABASE Amortizacion;

-- Parte 2
USE Amortizacion;

-- Un renglón por cada cálculo: datos capturados y totales
CREATE TABLE Simulacion (
id_simulacion INT PRIMARY KEY IDENTITY,
fecha DATETIME DEFAULT GETDATE(),
metodo VARCHAR(20),
monto DECIMAL(18,2),
tasa DECIMAL(9,4),
tipo_tasa VARCHAR(30),
capitalizaciones INT,
pagos_anio INT,
num_pagos INT,
tasa_periodo DECIMAL(12,8),
pago_inicial DECIMAL(18,2),
total_pagado DECIMAL(18,2),
total_intereses DECIMAL(18,2)
);

-- Un renglón por cada periodo de la tabla de amortización
-- ON DELETE CASCADE: al borrar una simulación se borra también su detalle
CREATE TABLE Detalle (
id_detalle INT PRIMARY KEY IDENTITY,
id_simulacion INT FOREIGN KEY REFERENCES Simulacion(id_simulacion) ON DELETE CASCADE,
periodo INT,
pago DECIMAL(18,2),
interes DECIMAL(18,2),
amortizacion DECIMAL(18,2),
saldo DECIMAL(18,2)
);
