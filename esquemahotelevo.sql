
CREATE TABLE [Huesped] (
  [id_huesped] int PRIMARY KEY IDENTITY(1, 1),
  [nombres] nvarchar(255),
  [apellidos] nvarchar(255),
  [tipo_documento] nvarchar(255),
  [num_documento] nvarchar(255),
  [telefono] nvarchar(255),
  [correo] nvarchar(255),
  [fecha_creacion] datetime
)
GO

CREATE TABLE [TipoHabitacion] (
  [id_tipo_habitacion] int PRIMARY KEY IDENTITY(1, 1),
  [nombre] nvarchar(255),
  [descripcion] nvarchar(255),
  [tarifa_base] decimal
)
GO

CREATE TABLE [Habitacion] (
  [id_habitacion] int PRIMARY KEY IDENTITY(1, 1),
  [numero] nvarchar(255),
  [piso] int,
  [id_tipo_habitacion] int,
  [estado] nvarchar(255)
)
GO

CREATE TABLE [Reserva] (
  [id_reserva] int PRIMARY KEY IDENTITY(1, 1),
  [id_huesped] int,
  [id_habitacion] int,
  [id_orden_conserj] int,
  [fecha_entrada] date,
  [fecha_salida] date,
  [num_personas] int,
  [monto_total] decimal,
  [estado] nvarchar(255),
  [fecha_creacion] datetime
)
GO

CREATE TABLE [Pago] (
  [id_pago] int PRIMARY KEY IDENTITY(1, 1),
  [id_reserva] int,
  [id_orden_hospedaje] int,
  [id_orden_conserjeria] int,
  [fecha_pago] datetime,
  [monto_pagado] decimal,
  [metodo] int,
  [estado_pago] nvarchar(255)
)
GO

CREATE TABLE [metodo_pago] (
  [idmetodopago] int PRIMARY KEY IDENTITY(1, 1),
  [nom_metodopago] nvarchar(255)
)
GO

CREATE TABLE [OrdenConserjeria] (
  [id_orden_conserj] int PRIMARY KEY IDENTITY(1, 1),
  [id_personal] int,
  [id_habitacion] int,
  [fecha_inicio] datetime,
  [fecha_fin] datetime,
  [precio] decimal,
  [estado] nvarchar(255),
  [descripcion] nvarchar(255)
)
GO

CREATE TABLE [Personal] (
  [id_personal] int PRIMARY KEY IDENTITY(1, 1),
  [nombre] nvarchar(255),
  [tipo_documento] nvarchar(255),
  [num_documento] nvarchar(255),
  [email] nvarchar(255),
  [contraseña] nvarchar(255),
  [rol] nvarchar(255)
)
GO

CREATE TABLE [OrdenHospedaje] (
  [id_orden_hospedaje] int PRIMARY KEY IDENTITY(1, 1),
  [id_reserva] int,
  [id_cliente] int,
  [id_habitacion] int,
  [estado] nvarchar(255),
  [fecha_checkin] datetime,
  [fecha_checkout] datetime
)
GO

CREATE TABLE [Encuesta] (
  [id_encuesta] int PRIMARY KEY IDENTITY(1, 1),
  [nombres] nvarchar(255),
  [descripcion] nvarchar(255),
  [recomendacion] int,
  [lugar_origen] nvarchar(255),
  [motivo_viaje] varchar,
  [tiempo_estadia] nvarchar(255)
)
GO

ALTER TABLE [Habitacion] ADD FOREIGN KEY ([id_tipo_habitacion]) REFERENCES [TipoHabitacion] ([id_tipo_habitacion])
GO

ALTER TABLE [Reserva] ADD FOREIGN KEY ([id_huesped]) REFERENCES [Huesped] ([id_huesped])
GO

ALTER TABLE [Reserva] ADD FOREIGN KEY ([id_habitacion]) REFERENCES [Habitacion] ([id_habitacion])
GO

ALTER TABLE [Reserva] ADD FOREIGN KEY ([id_orden_conserj]) REFERENCES [OrdenConserjeria] ([id_orden_conserj])
GO

ALTER TABLE [Pago] ADD FOREIGN KEY ([id_reserva]) REFERENCES [Reserva] ([id_reserva])
GO

ALTER TABLE [Pago] ADD FOREIGN KEY ([id_orden_hospedaje]) REFERENCES [OrdenHospedaje] ([id_orden_hospedaje])
GO

ALTER TABLE [Pago] ADD FOREIGN KEY ([id_orden_conserjeria]) REFERENCES [OrdenConserjeria] ([id_orden_conserj])
GO

ALTER TABLE [Pago] ADD FOREIGN KEY ([metodo]) REFERENCES [metodo_pago] ([idmetodopago])
GO

ALTER TABLE [OrdenConserjeria] ADD FOREIGN KEY ([id_personal]) REFERENCES [Personal] ([id_personal])
GO

ALTER TABLE [OrdenConserjeria] ADD FOREIGN KEY ([id_habitacion]) REFERENCES [Habitacion] ([id_habitacion])
GO

ALTER TABLE [OrdenHospedaje] ADD FOREIGN KEY ([id_reserva]) REFERENCES [Reserva] ([id_reserva])
GO

ALTER TABLE [OrdenHospedaje] ADD FOREIGN KEY ([id_cliente]) REFERENCES [Huesped] ([id_huesped])
GO

ALTER TABLE [OrdenHospedaje] ADD FOREIGN KEY ([id_habitacion]) REFERENCES [Habitacion] ([id_habitacion])
GO
