create database proyecto

use proyecto

-- ==============================
-- 1. Tablas Lookup (Catálogos)
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

CREATE TABLE [EstadoOrdenConserjeria] (
  [id_estado_orden_conserj] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(50) NOT NULL
);

CREATE TABLE [EstadoOrdenHospedaje] (
  [id_estado_orden_hosp] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(50) NOT NULL
);

CREATE TABLE [EstadoPago] (
  [id_estado_pago] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(50) NOT NULL
);

CREATE TABLE [EstadoDocumento] (
  [id_estado_documento] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(50) NOT NULL
);

CREATE TABLE [MetodoPago] (
  [id_metodo_pago] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(50) NOT NULL
);

CREATE TABLE [Rol] (
  [id_rol] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(50) NOT NULL,
  [descripcion] NVARCHAR(255)
);

CREATE TABLE [TipoDocumentoCobro] (
  [id_tipo_doc_cobro] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(50) NOT NULL,
  [descripcion] NVARCHAR(255)
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
  [password_hash] VARBINARY(MAX) NOT NULL,
  [id_rol] INT NOT NULL REFERENCES Rol(id_rol),
  [activo] BIT DEFAULT 1,
  [fecha_creacion] DATETIME2 DEFAULT SYSUTCDATETIME(),
  CONSTRAINT UQ_Personal_Documento UNIQUE(tipo_documento, num_documento),
  CONSTRAINT CK_Personal_Email CHECK (email IS NULL OR email LIKE '%_@_%._%')
);

-- ==============================
-- 3. Tipos y Habitaciones
-- ==============================

CREATE TABLE [TipoHabitacion] (
  [id_tipo_habitacion] INT PRIMARY KEY IDENTITY(1,1),
  [nombre] NVARCHAR(100) NOT NULL,
  [descripcion] NVARCHAR(255),
  [capacidad_personas] INT NOT NULL CHECK(capacidad_personas > 0),
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
-- 4. Reserva
-- ==============================

CREATE TABLE [Reserva] (
  [id_reserva] INT PRIMARY KEY IDENTITY(1,1),
  [id_huesped] INT NOT NULL REFERENCES Huesped(id_huesped),
  [id_habitacion] INT NOT NULL REFERENCES Habitacion(id_habitacion),
  [fecha_entrada] DATE NOT NULL,
  [fecha_salida] DATE NOT NULL,
  [num_personas] INT NOT NULL CHECK(num_personas > 0),
  [monto_total] DECIMAL(12,2) NOT NULL CHECK(monto_total >= 0),
  [estado] INT NOT NULL REFERENCES EstadoReserva(id_estado_reserva),
  [fecha_creacion] DATETIME2 DEFAULT SYSUTCDATETIME(),
  CONSTRAINT CK_Reserva_Fechas CHECK(fecha_salida > fecha_entrada)
);

-- ==============================
-- 5. OrdenConserjeria
-- ==============================

CREATE TABLE [OrdenConserjeria] (
  [id_orden_conserj] INT PRIMARY KEY IDENTITY(1,1),
  [id_personal] INT NOT NULL REFERENCES Personal(id_personal),
  [id_habitacion] INT NOT NULL REFERENCES Habitacion(id_habitacion),
  [id_reserva] INT NULL REFERENCES Reserva(id_reserva),
  [fecha_inicio] DATETIME2 NOT NULL,
  [fecha_fin] DATETIME2 NULL,
  [precio] DECIMAL(12,2) NOT NULL CHECK(precio >= 0),
  [estado] INT NOT NULL REFERENCES EstadoOrdenConserjeria(id_estado_orden_conserj),
  [descripcion] NVARCHAR(500),
  CONSTRAINT CK_OrdenConserj_Fechas CHECK(fecha_fin IS NULL OR fecha_fin >= fecha_inicio)
);

-- ==============================
-- 6. OrdenHospedaje
-- ==============================

CREATE TABLE [OrdenHospedaje] (
  [id_orden_hospedaje] INT PRIMARY KEY IDENTITY(1,1),
  [id_reserva] INT NOT NULL REFERENCES Reserva(id_reserva),
  [estado] INT NOT NULL REFERENCES EstadoOrdenHospedaje(id_estado_orden_hosp),
  [fecha_checkin] DATETIME2 NULL,
  [fecha_checkout] DATETIME2 NULL,
  CONSTRAINT CK_OrdenHosp_Fechas CHECK(fecha_checkout IS NULL OR fecha_checkout >= fecha_checkin)
);

-- ==============================
-- 7. Documento (Tabla Intermedia)
-- ==============================

CREATE TABLE [Documento] (
  [id_documento] INT PRIMARY KEY IDENTITY(1,1),
  [numero_documento] NVARCHAR(50) NOT NULL UNIQUE,
  [tipo_documento] INT NOT NULL REFERENCES TipoDocumentoCobro(id_tipo_doc_cobro),
  
  -- Referencias a las fuentes (solo una debe estar llena)
  [id_reserva] INT NULL REFERENCES Reserva(id_reserva),
  [id_orden_hospedaje] INT NULL REFERENCES OrdenHospedaje(id_orden_hospedaje),
  [id_orden_conserjeria] INT NULL REFERENCES OrdenConserjeria(id_orden_conserj),
  
  [monto_total] DECIMAL(12,2) NOT NULL CHECK(monto_total >= 0),
  [monto_pagado] DECIMAL(12,2) DEFAULT 0 CHECK(monto_pagado >= 0),
  [saldo_pendiente] AS (monto_total - monto_pagado) PERSISTED,
  
  [fecha_emision] DATETIME2 DEFAULT SYSUTCDATETIME(),
  [fecha_vencimiento] DATE NULL,
  [descripcion] NVARCHAR(500),
  [estado_documento] INT NOT NULL REFERENCES EstadoDocumento(id_estado_documento),
  
  CONSTRAINT CK_Documento_UnSoloOrigen CHECK (
      (CASE WHEN id_reserva IS NOT NULL THEN 1 ELSE 0 END +
       CASE WHEN id_orden_hospedaje IS NOT NULL THEN 1 ELSE 0 END +
       CASE WHEN id_orden_conserjeria IS NOT NULL THEN 1 ELSE 0 END) = 1
  ),
  CONSTRAINT CK_Documento_MontoPagado CHECK(monto_pagado <= monto_total)
);

-- ==============================
-- 8. Pago (SIMPLIFICADO - solo relacionado con Documento)
-- ==============================

CREATE TABLE [Pago] (
  [id_pago] INT PRIMARY KEY IDENTITY(1,1),
  [id_documento] INT NOT NULL REFERENCES Documento(id_documento),
  
  [fecha_pago] DATETIME2 DEFAULT SYSUTCDATETIME(),
  [monto_pagado] DECIMAL(12,2) NOT NULL CHECK(monto_pagado > 0),
  [metodo] INT NOT NULL REFERENCES MetodoPago(id_metodo_pago),
  [estado_pago] INT NOT NULL REFERENCES EstadoPago(id_estado_pago),
  
  [numero_comprobante] NVARCHAR(100),
  [numero_operacion] NVARCHAR(100),
  [observaciones] NVARCHAR(500),
  
  [id_personal] INT NULL REFERENCES Personal(id_personal)
);

-- ==============================
-- 9. Encuesta
-- ==============================

CREATE TABLE [Encuesta] (
  [id_encuesta] INT PRIMARY KEY IDENTITY(1,1),
  [id_orden_hospedaje] INT NOT NULL REFERENCES OrdenHospedaje(id_orden_hospedaje),
  [descripcion] NVARCHAR(500),
  [recomendacion] INT NOT NULL CHECK(recomendacion BETWEEN 1 AND 10),
  [lugar_origen] NVARCHAR(100),
  [motivo_viaje] NVARCHAR(100),
  [calificacion_limpieza] INT CHECK(calificacion_limpieza BETWEEN 1 AND 5),
  [calificacion_servicio] INT CHECK(calificacion_servicio BETWEEN 1 AND 5),
  [calificacion_ubicacion] INT CHECK(calificacion_ubicacion BETWEEN 1 AND 5),
  [calificacion_precio] INT CHECK(calificacion_precio BETWEEN 1 AND 5),
  [comentarios] NVARCHAR(1000),
  [fecha_encuesta] DATETIME2 DEFAULT SYSUTCDATETIME(),
  CONSTRAINT UQ_Encuesta_OrdenHospedaje UNIQUE(id_orden_hospedaje)
);

-- ==============================
-- 10. Índices recomendados para performance
-- ==============================

CREATE INDEX IX_Reserva_Huesped ON Reserva(id_huesped);
CREATE INDEX IX_Reserva_Habitacion ON Reserva(id_habitacion);
CREATE INDEX IX_Reserva_Fechas ON Reserva(fecha_entrada, fecha_salida);
CREATE INDEX IX_Reserva_Estado ON Reserva(estado);

CREATE INDEX IX_OrdenHospedaje_Reserva ON OrdenHospedaje(id_reserva);
CREATE INDEX IX_OrdenHospedaje_Estado ON OrdenHospedaje(estado);

CREATE INDEX IX_OrdenConserjeria_Habitacion ON OrdenConserjeria(id_habitacion);
CREATE INDEX IX_OrdenConserjeria_Reserva ON OrdenConserjeria(id_reserva);
CREATE INDEX IX_OrdenConserjeria_Personal ON OrdenConserjeria(id_personal);
CREATE INDEX IX_OrdenConserjeria_Estado ON OrdenConserjeria(estado);

CREATE INDEX IX_Documento_TipoDoc ON Documento(tipo_documento);
CREATE INDEX IX_Documento_Reserva ON Documento(id_reserva);
CREATE INDEX IX_Documento_OrdenHospedaje ON Documento(id_orden_hospedaje);
CREATE INDEX IX_Documento_OrdenConserjeria ON Documento(id_orden_conserjeria);
CREATE INDEX IX_Documento_Estado ON Documento(estado_documento);
CREATE INDEX IX_Documento_Numero ON Documento(numero_documento);
CREATE INDEX IX_Documento_FechaEmision ON Documento(fecha_emision);

CREATE INDEX IX_Pago_Documento ON Pago(id_documento);
CREATE INDEX IX_Pago_Fecha ON Pago(fecha_pago);
CREATE INDEX IX_Pago_Metodo ON Pago(metodo);
CREATE INDEX IX_Pago_Personal ON Pago(id_personal);

CREATE INDEX IX_Encuesta_OrdenHospedaje ON Encuesta(id_orden_hospedaje);

CREATE INDEX IX_Personal_Rol ON Personal(id_rol);
CREATE INDEX IX_Personal_Activo ON Personal(activo);

CREATE INDEX IX_Habitacion_TipoHabitacion ON Habitacion(id_tipo_habitacion);
CREATE INDEX IX_Habitacion_Estado ON Habitacion(estado);

ALTER AUTHORIZATION ON DATABASE::proyecto TO sa;


---- Procedimiento para registrar un pago y actualizar el documento
--CREATE PROCEDURE sp_RegistrarPago
--    @id_documento INT,
--    @monto_pagado DECIMAL(12,2),
--    @id_metodo_pago INT,
--    @id_personal INT = NULL,
--    @numero_comprobante NVARCHAR(100) = NULL,
--    @numero_operacion NVARCHAR(100) = NULL,
--    @observaciones NVARCHAR(500) = NULL
--AS
--BEGIN
--    SET NOCOUNT ON;
--    BEGIN TRANSACTION;
    
--    BEGIN TRY
--        -- Validar que el documento existe y no está anulado
--        DECLARE @estado_actual INT, @monto_total DECIMAL(12,2), @monto_pagado_actual DECIMAL(12,2);
        
--        SELECT 
--            @estado_actual = estado_documento,
--            @monto_total = monto_total,
--            @monto_pagado_actual = monto_pagado
--        FROM Documento
--        WHERE id_documento = @id_documento;
        
--        IF @estado_actual = (SELECT id_estado_documento FROM EstadoDocumento WHERE nombre = 'Anulado')
--        BEGIN
--            RAISERROR('No se puede registrar pago en un documento anulado', 16, 1);
--            RETURN;
--        END
        
--        -- Validar que no se exceda el monto total
--        IF (@monto_pagado_actual + @monto_pagado) > @monto_total
--        BEGIN
--            RAISERROR('El monto a pagar excede el saldo pendiente', 16, 1);
--            RETURN;
--        END
        
--        -- Insertar el pago
--        INSERT INTO Pago (
--            id_documento,
--            monto_pagado,
--            metodo,
--            estado_pago,
--            numero_comprobante,
--            numero_operacion,
--            observaciones,
--            id_personal
--        )
--        VALUES (
--            @id_documento,
--            @monto_pagado,
--            @id_metodo_pago,
--            (SELECT id_estado_pago FROM EstadoPago WHERE nombre = 'Completado'),
--            @numero_comprobante,
--            @numero_operacion,
--            @observaciones,
--            @id_personal
--        );
        
--        -- Actualizar el monto pagado en el documento
--        UPDATE Documento
--        SET monto_pagado = monto_pagado + @monto_pagado,
--            estado_documento = CASE 
--                WHEN (monto_pagado + @monto_pagado) >= monto_total 
--                THEN (SELECT id_estado_documento FROM EstadoDocumento WHERE nombre = 'Pagado Total')
--                ELSE (SELECT id_estado_documento FROM EstadoDocumento WHERE nombre = 'Pagado Parcial')
--            END
--        WHERE id_documento = @id_documento;
        
--        COMMIT TRANSACTION;
        
--        SELECT 'Pago registrado exitosamente' AS Mensaje;
--    END TRY
--    BEGIN CATCH
--        ROLLBACK TRANSACTION;
--        THROW;
--    END CATCH
--END;
--GO

---- Procedimiento para obtener el estado de cuenta de un huésped
--CREATE PROCEDURE sp_EstadoCuentaHuesped
--    @id_huesped INT
--AS
--BEGIN
--    SET NOCOUNT ON;
    
--    SELECT 
--        d.numero_documento,
--        tdc.nombre AS tipo_concepto,
--        d.fecha_emision,
--        d.monto_total,
--        d.monto_pagado,
--        d.saldo_pendiente,
--        ed.nombre AS estado,
--        CASE 
--            WHEN d.id_reserva IS NOT NULL THEN 'Reserva #' + CAST(d.id_reserva AS NVARCHAR)
--            WHEN d.id_orden_hospedaje IS NOT NULL THEN 'Hospedaje #' + CAST(d.id_orden_hospedaje AS NVARCHAR)
--            WHEN d.id_orden_conserjeria IS NOT NULL THEN 'Conserjería #' + CAST(d.id_orden_conserjeria AS NVARCHAR)
--        END AS origen
--    FROM Documento d
--    INNER JOIN TipoDocumentoCobro tdc ON d.tipo_documento = tdc.id_tipo_doc_cobro
--    INNER JOIN EstadoDocumento ed ON d.estado_documento = ed.id_estado_documento
--    LEFT JOIN Reserva r ON d.id_reserva = r.id_reserva
--    LEFT JOIN OrdenHospedaje oh ON d.id_orden_hospedaje = oh.id_orden_hospedaje
--    LEFT JOIN Reserva r2 ON oh.id_reserva = r2.id_reserva
--    WHERE r.id_huesped = @id_huesped 
--       OR r2.id_huesped = @id_huesped
--    ORDER BY d.fecha_emision DESC;
--END;
--GO

---- Procedimiento para obtener reporte de documentos pendientes
--CREATE PROCEDURE sp_DocumentosPendientes
--AS
--BEGIN
--    SET NOCOUNT ON;
    
--    SELECT 
--        d.numero_documento,
--        d.fecha_emision,
--        d.fecha_vencimiento,
--        tdc.nombre AS tipo_documento,
--        h.nombres + ' ' + h.apellidos AS huesped,
--        d.monto_total,
--        d.monto_pagado,
--        d.saldo_pendiente,
--        ed.nombre AS estado,
--        DATEDIFF(DAY, GETDATE(), d.fecha_vencimiento) AS dias_vencimiento
--    FROM Documento d
--    INNER JOIN TipoDocumentoCobro tdc ON d.tipo_documento = tdc.id_tipo_doc_cobro
--    INNER JOIN EstadoDocumento ed ON d.estado_documento = ed.id_estado_documento
--    LEFT JOIN Reserva r ON d.id_reserva = r.id_reserva
--    LEFT JOIN OrdenHospedaje oh ON d.id_orden_hospedaje = oh.id_orden_hospedaje
--    LEFT JOIN Reserva r2 ON oh.id_reserva = r2.id_reserva
--    LEFT JOIN Huesped h ON COALESCE(r.id_huesped, r2.id_huesped) = h.id_huesped
--    WHERE d.estado_documento IN (
--        SELECT id_estado_documento 
--        FROM EstadoDocumento 
--        WHERE nombre IN ('Pendiente', 'Pagado Parcial', 'Vencido')
--    )
--    ORDER BY d.fecha_vencimiento ASC;
--END;
--GO