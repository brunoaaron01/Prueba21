-- ==============================
-- 1. Lookups
-- ==============================

CREATE TABLE [TipoDocumento] (
  [id_tipo_documento] INT PRIMARY KEY IDENTITY(1,1),
  [codigo] NVARCHAR(20) NOT NULL,
  [descripcion] NVARCHAR(100) NOT NULL
);

CREATE TABLE [EstadoReserva] (
  [id_estado_reserva] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(50) NOT NULL
);

CREATE TABLE [EstadoHabitacion] (
  [id_estado_habitacion] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(50) NOT NULL
);

CREATE TABLE [MetodoPago] (
  [id_metodo_pago] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(50) NOT NULL
);

-- ==============================
-- 2. Entidades base (Huesped y Personal)
-- ==============================

CREATE TABLE [Huesped] (
  [id_huesped] INT PRIMARY KEY IDENTITY(1,1),
  [nombres] NVARCHAR(100) NOT NULL,
  [apellidos] NVARCHAR(100) NOT NULL,
  [tipo_documento] INT NOT NULL REFERENCES TipoDocumento(id_tipo_documento),
  [num_documento] NVARCHAR(50) NOT NULL,
  [telefono] NVARCHAR(20),
  [correo] NVARCHAR(255),
  [fecha_creacion] DATETIME2 DEFAULT SYSUTCDATETIME(),
  CONSTRAINT UQ_Huesped_Documento UNIQUE(tipo_documento, num_documento),
  CONSTRAINT CK_Huesped_Correo CHECK (correo IS NULL OR correo LIKE '%_@_%._%')
);

CREATE TABLE [Personal] (
  [id_personal] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(100) NOT NULL,
  [tipo_documento] INT NOT NULL REFERENCES TipoDocumento(id_tipo_documento),
  [num_documento] NVARCHAR(50) NOT NULL,
  [email] NVARCHAR(255),
  [password_hash] VARBINARY(MAX) NOT NULL, -- guardar hash seguro
  [rol] NVARCHAR(50) NOT NULL,
  CONSTRAINT UQ_Personal_Documento UNIQUE(tipo_documento, num_documento)
);

-- ==============================
-- 3. Tipos y Habitaciones
-- ==============================

CREATE TABLE [TipoHabitacion] (
  [id_tipo_habitacion] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(100) NOT NULL,
  [descripcion] NVARCHAR(255),
  [tarifa_base] DECIMAL(12,2) NOT NULL CHECK(tarifa_base >= 0)
);

CREATE TABLE [Habitacion] (
  [id_habitacion] INT PRIMARY KEY IDENTITY(1,1),
  [numero] NVARCHAR(20) NOT NULL,
  [piso] INT NOT NULL,
  [id_tipo_habitacion] INT NOT NULL REFERENCES TipoHabitacion(id_tipo_habitacion),
  [estado] INT NOT NULL REFERENCES EstadoHabitacion(id_estado_habitacion),
  CONSTRAINT UQ_Habitacion_Numero_Piso UNIQUE(numero, piso)
);

-- ==============================
-- 4. OrdenConserjeria
-- ==============================

CREATE TABLE [OrdenConserjeria] (
  [id_orden_conserj] INT PRIMARY KEY IDENTITY(1,1),
  [id_personal] INT NOT NULL REFERENCES Personal(id_personal),
  [id_habitacion] INT NOT NULL REFERENCES Habitacion(id_habitacion),
  [fecha_inicio] DATETIME2 NOT NULL,
  [fecha_fin] DATETIME2 NULL,
  [precio] DECIMAL(12,2) NOT NULL CHECK(precio >= 0),
  [estado] NVARCHAR(50) NOT NULL,
  [descripcion] NVARCHAR(500)
);

-- ==============================
-- 5. Reserva
-- ==============================

CREATE TABLE [Reserva] (
  [id_reserva] INT PRIMARY KEY IDENTITY(1,1),
  [id_huesped] INT NOT NULL REFERENCES Huesped(id_huesped),
  [id_habitacion] INT NOT NULL REFERENCES Habitacion(id_habitacion),
  [id_orden_conserj] INT NULL REFERENCES OrdenConserjeria(id_orden_conserj),
  [fecha_entrada] DATE NOT NULL,
  [fecha_salida] DATE NOT NULL,
  [num_personas] INT NOT NULL CHECK(num_personas > 0),
  [monto_total] DECIMAL(12,2) NOT NULL CHECK(monto_total >= 0),
  [estado] INT NOT NULL REFERENCES EstadoReserva(id_estado_reserva),
  [fecha_creacion] DATETIME2 DEFAULT SYSUTCDATETIME(),
  CONSTRAINT CK_Reserva_Fechas CHECK(fecha_salida > fecha_entrada)
);

-- ==============================
-- 6. OrdenHospedaje
-- ==============================

CREATE TABLE [OrdenHospedaje] (
  [id_orden_hospedaje] INT PRIMARY KEY IDENTITY(1,1),
  [id_reserva] INT NOT NULL REFERENCES Reserva(id_reserva),
  [id_cliente] INT NOT NULL REFERENCES Huesped(id_huesped),
  [id_habitacion] INT NOT NULL REFERENCES Habitacion(id_habitacion),
  [estado] NVARCHAR(50) NOT NULL,
  [fecha_checkin] DATETIME2 NULL,
  [fecha_checkout] DATETIME2 NULL
);

-- ==============================
-- 7. Pago
-- ==============================

CREATE TABLE [Pago] (
  [id_pago] INT PRIMARY KEY IDENTITY(1,1),
  [id_reserva] INT NULL REFERENCES Reserva(id_reserva),
  [id_orden_hospedaje] INT NULL REFERENCES OrdenHospedaje(id_orden_hospedaje),
  [id_orden_conserjeria] INT NULL REFERENCES OrdenConserjeria(id_orden_conserj),
  [fecha_pago] DATETIME2 DEFAULT SYSUTCDATETIME(),
  [monto_pagado] DECIMAL(12,2) CHECK(monto_pagado >= 0),
  [metodo] INT NOT NULL REFERENCES MetodoPago(id_metodo_pago),
  [estado_pago] NVARCHAR(50) NOT NULL,
  CONSTRAINT CK_Pago_UnSoloTarget CHECK (
      (CASE WHEN id_reserva IS NOT NULL THEN 1 ELSE 0 END +
       CASE WHEN id_orden_hospedaje IS NOT NULL THEN 1 ELSE 0 END +
       CASE WHEN id_orden_conserjeria IS NOT NULL THEN 1 ELSE 0 END) = 1
  )
);

-- ==============================
-- 8. Encuesta
-- ==============================

CREATE TABLE [Encuesta] (
  [id_encuesta] INT PRIMARY KEY IDENTITY(1,1),
  [nombres] NVARCHAR(100),
  [descripcion] NVARCHAR(500),
  [recomendacion] INT CHECK(recomendacion BETWEEN 1 AND 10),
  [lugar_origen] NVARCHAR(100),
  [motivo_viaje] NVARCHAR(100),
  [tiempo_estadia] NVARCHAR(50),
  [id_huesped] INT NULL REFERENCES Huesped(id_huesped)
);
