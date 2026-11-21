-- ==========================================
-- CREACIÓN DE LA BASE DE DATOS
-- ==========================================

-- Crea una base de datos llamada amortizacion_db.
CREATE DATABASE `amortizacion_db`;

-- ==========================================
-- CREACIÓN DE LA TABLA: simulaciones
-- ==========================================

-- Esta tabla almacena la información general
-- de cada simulación financiera.

CREATE TABLE `simulaciones` (
  `id` bigint(20) NOT NULL,
  -- Identificador único de cada simulación.

  `fecha` datetime NOT NULL DEFAULT current_timestamp(),
  -- Fecha y hora en que se registra la simulación.
  -- Si no se proporciona, MySQL coloca la fecha actual.

  `metodo` tinyint(4) NOT NULL,
  -- Identifica el método de amortización utilizado.

  `monto` decimal(18,2) NOT NULL,
  -- Cantidad inicial del préstamo o capital.

  `tasa_porcentaje` decimal(9,4) NOT NULL,
  -- Tasa de interés expresada en porcentaje.

  `tipo_tasa` tinyint(4) NOT NULL,
  -- Identifica el tipo de tasa de interés.

  `capitalizaciones_anio` int(11) NOT NULL,
  -- Número de capitalizaciones de intereses al año.

  `pagos_anio` int(11) NOT NULL,
  -- Cantidad de pagos que se realizan por año.

  `num_pagos` int(11) NOT NULL,
  -- Número total de pagos del préstamo.

  `tasa_periodo` decimal(12,8) NOT NULL,
  -- Tasa de interés correspondiente a cada periodo de pago.

  `pago_inicial` decimal(18,2) NOT NULL,
  -- Importe del pago inicial o pago periódico calculado.

  `total_pagado` decimal(18,2) NOT NULL,
  -- Suma de todos los pagos realizados.

  `total_intereses` decimal(18,2) NOT NULL
  -- Total de intereses generados durante el préstamo.
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_general_ci;

-- ==========================================
-- CREACIÓN DE LA TABLA: detalle_amortizacion
-- ==========================================

-- Almacena el desglose de cada periodo
-- de la tabla de amortización.

CREATE TABLE `detalle_amortizacion` (
  `id` bigint(20) NOT NULL,
  -- Identificador único de cada registro.

  `simulacion_id` bigint(20) NOT NULL,
  -- Identificador de la simulación a la que pertenece.

  `periodo` int(11) NOT NULL,
  -- Número del pago: 1, 2, 3, 4, etc.

  `pago` decimal(18,2) NOT NULL,
  -- Cantidad total que se paga en ese periodo.

  `interes` decimal(18,2) NOT NULL,
  -- Parte del pago que corresponde a intereses.

  `amortizacion` decimal(18,2) NOT NULL,
  -- Parte del pago que reduce la deuda.

  `saldo` decimal(18,2) NOT NULL
  -- Deuda pendiente después de realizar el pago.
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_general_ci;

-- ==========================================
-- CONFIGURACIÓN DE CLAVES E ÍNDICES
-- ==========================================

-- Establece id como clave primaria de detalle_amortizacion.
ALTER TABLE `detalle_amortizacion`
  ADD PRIMARY KEY (`id`),

  -- Crea un índice para buscar los registros
  -- relacionados con una simulación.
  ADD KEY `simulacion_id` (`simulacion_id`);

-- Establece id como clave primaria de simulaciones.
ALTER TABLE `simulaciones`
  ADD PRIMARY KEY (`id`);

-- ==========================================
-- CONFIGURACIÓN AUTO_INCREMENT
-- ==========================================

-- Hace que el ID de detalle_amortizacion
-- se genere automáticamente.
ALTER TABLE `detalle_amortizacion`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT;

-- Hace que el ID de simulaciones
-- se genere automáticamente.
ALTER TABLE `simulaciones`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT;

-- ==========================================
-- RELACIÓN ENTRE LAS DOS TABLAS
-- ==========================================

-- Establece una llave foránea que relaciona
-- detalle_amortizacion con simulaciones.

ALTER TABLE `detalle_amortizacion`
  ADD CONSTRAINT `detalle_amortizacion_ibfk_1`
  FOREIGN KEY (`simulacion_id`)
  REFERENCES `simulaciones` (`id`)
  ON DELETE CASCADE;