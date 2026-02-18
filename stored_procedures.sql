USE proyecto
GO

-- =============================================
-- TABLA: TipoDocumento
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertTipoDocumento
    @codigo NVARCHAR(20),
    @descripcion NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM TipoDocumento WHERE codigo = @codigo)
    BEGIN
        RAISERROR('Ya existe un tipo de documento con ese código.', 16, 1); RETURN;
    END
    INSERT INTO TipoDocumento (codigo, descripcion) VALUES (@codigo, @descripcion);
    SELECT SCOPE_IDENTITY() AS id_tipo_documento;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateTipoDocumento
    @id_tipo_documento INT,
    @codigo NVARCHAR(20),
    @descripcion NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM TipoDocumento WHERE id_tipo_documento = @id_tipo_documento)
    BEGIN
        RAISERROR('Tipo de documento no encontrado.', 16, 1); RETURN;
    END
    UPDATE TipoDocumento SET codigo = @codigo, descripcion = @descripcion
    WHERE id_tipo_documento = @id_tipo_documento;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteTipoDocumento
    @id_tipo_documento INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Huesped WHERE tipo_documento = @id_tipo_documento)
        OR EXISTS (SELECT 1 FROM Personal WHERE tipo_documento = @id_tipo_documento)
    BEGIN
        RAISERROR('No se puede eliminar: está siendo usado por Huesped o Personal.', 16, 1); RETURN;
    END
    DELETE FROM TipoDocumento WHERE id_tipo_documento = @id_tipo_documento;
END
GO

CREATE OR ALTER PROCEDURE sp_GetTipoDocumento
    @id_tipo_documento INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_tipo_documento, codigo, descripcion
    FROM TipoDocumento
    WHERE (@id_tipo_documento IS NULL OR id_tipo_documento = @id_tipo_documento);
END
GO

-- =============================================
-- TABLA: EstadoReserva
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertEstadoReserva
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO EstadoReserva (nombre) VALUES (@nombre);
    SELECT SCOPE_IDENTITY() AS id_estado_reserva;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateEstadoReserva
    @id_estado_reserva INT,
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM EstadoReserva WHERE id_estado_reserva = @id_estado_reserva)
    BEGIN
        RAISERROR('Estado de reserva no encontrado.', 16, 1); RETURN;
    END
    UPDATE EstadoReserva SET nombre = @nombre WHERE id_estado_reserva = @id_estado_reserva;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteEstadoReserva
    @id_estado_reserva INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Reserva WHERE estado = @id_estado_reserva)
    BEGIN
        RAISERROR('No se puede eliminar: está siendo usado en Reservas.', 16, 1); RETURN;
    END
    DELETE FROM EstadoReserva WHERE id_estado_reserva = @id_estado_reserva;
END
GO

CREATE OR ALTER PROCEDURE sp_GetEstadoReserva
    @id_estado_reserva INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_estado_reserva, nombre FROM EstadoReserva
    WHERE (@id_estado_reserva IS NULL OR id_estado_reserva = @id_estado_reserva);
END
GO

-- =============================================
-- TABLA: EstadoHabitacion
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertEstadoHabitacion
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO EstadoHabitacion (nombre) VALUES (@nombre);
    SELECT SCOPE_IDENTITY() AS id_estado_habitacion;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateEstadoHabitacion
    @id_estado_habitacion INT,
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM EstadoHabitacion WHERE id_estado_habitacion = @id_estado_habitacion)
    BEGIN
        RAISERROR('Estado de habitación no encontrado.', 16, 1); RETURN;
    END
    UPDATE EstadoHabitacion SET nombre = @nombre WHERE id_estado_habitacion = @id_estado_habitacion;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteEstadoHabitacion
    @id_estado_habitacion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Habitacion WHERE estado = @id_estado_habitacion)
    BEGIN
        RAISERROR('No se puede eliminar: está siendo usado en Habitaciones.', 16, 1); RETURN;
    END
    DELETE FROM EstadoHabitacion WHERE id_estado_habitacion = @id_estado_habitacion;
END
GO

CREATE OR ALTER PROCEDURE sp_GetEstadoHabitacion
    @id_estado_habitacion INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_estado_habitacion, nombre FROM EstadoHabitacion
    WHERE (@id_estado_habitacion IS NULL OR id_estado_habitacion = @id_estado_habitacion);
END
GO

-- =============================================
-- TABLA: EstadoOrdenConserjeria
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertEstadoOrdenConserjeria
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO EstadoOrdenConserjeria (nombre) VALUES (@nombre);
    SELECT SCOPE_IDENTITY() AS id_estado_orden_conserj;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateEstadoOrdenConserjeria
    @id_estado_orden_conserj INT,
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM EstadoOrdenConserjeria WHERE id_estado_orden_conserj = @id_estado_orden_conserj)
    BEGIN
        RAISERROR('Estado de orden conserjería no encontrado.', 16, 1); RETURN;
    END
    UPDATE EstadoOrdenConserjeria SET nombre = @nombre WHERE id_estado_orden_conserj = @id_estado_orden_conserj;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteEstadoOrdenConserjeria
    @id_estado_orden_conserj INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM OrdenConserjeria WHERE estado = @id_estado_orden_conserj)
    BEGIN
        RAISERROR('No se puede eliminar: está siendo usado en Órdenes de Conserjería.', 16, 1); RETURN;
    END
    DELETE FROM EstadoOrdenConserjeria WHERE id_estado_orden_conserj = @id_estado_orden_conserj;
END
GO

CREATE OR ALTER PROCEDURE sp_GetEstadoOrdenConserjeria
    @id_estado_orden_conserj INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_estado_orden_conserj, nombre FROM EstadoOrdenConserjeria
    WHERE (@id_estado_orden_conserj IS NULL OR id_estado_orden_conserj = @id_estado_orden_conserj);
END
GO

-- =============================================
-- TABLA: EstadoOrdenHospedaje
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertEstadoOrdenHospedaje
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO EstadoOrdenHospedaje (nombre) VALUES (@nombre);
    SELECT SCOPE_IDENTITY() AS id_estado_orden_hosp;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateEstadoOrdenHospedaje
    @id_estado_orden_hosp INT,
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM EstadoOrdenHospedaje WHERE id_estado_orden_hosp = @id_estado_orden_hosp)
    BEGIN
        RAISERROR('Estado de orden hospedaje no encontrado.', 16, 1); RETURN;
    END
    UPDATE EstadoOrdenHospedaje SET nombre = @nombre WHERE id_estado_orden_hosp = @id_estado_orden_hosp;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteEstadoOrdenHospedaje
    @id_estado_orden_hosp INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM OrdenHospedaje WHERE estado = @id_estado_orden_hosp)
    BEGIN
        RAISERROR('No se puede eliminar: está siendo usado en Órdenes de Hospedaje.', 16, 1); RETURN;
    END
    DELETE FROM EstadoOrdenHospedaje WHERE id_estado_orden_hosp = @id_estado_orden_hosp;
END
GO

CREATE OR ALTER PROCEDURE sp_GetEstadoOrdenHospedaje
    @id_estado_orden_hosp INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_estado_orden_hosp, nombre FROM EstadoOrdenHospedaje
    WHERE (@id_estado_orden_hosp IS NULL OR id_estado_orden_hosp = @id_estado_orden_hosp);
END
GO

-- =============================================
-- TABLA: EstadoPago
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertEstadoPago
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO EstadoPago (nombre) VALUES (@nombre);
    SELECT SCOPE_IDENTITY() AS id_estado_pago;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateEstadoPago
    @id_estado_pago INT,
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM EstadoPago WHERE id_estado_pago = @id_estado_pago)
    BEGIN
        RAISERROR('Estado de pago no encontrado.', 16, 1); RETURN;
    END
    UPDATE EstadoPago SET nombre = @nombre WHERE id_estado_pago = @id_estado_pago;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteEstadoPago
    @id_estado_pago INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Pago WHERE estado_pago = @id_estado_pago)
    BEGIN
        RAISERROR('No se puede eliminar: está siendo usado en Pagos.', 16, 1); RETURN;
    END
    DELETE FROM EstadoPago WHERE id_estado_pago = @id_estado_pago;
END
GO

CREATE OR ALTER PROCEDURE sp_GetEstadoPago
    @id_estado_pago INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_estado_pago, nombre FROM EstadoPago
    WHERE (@id_estado_pago IS NULL OR id_estado_pago = @id_estado_pago);
END
GO

-- =============================================
-- TABLA: EstadoDocumento
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertEstadoDocumento
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO EstadoDocumento (nombre) VALUES (@nombre);
    SELECT SCOPE_IDENTITY() AS id_estado_documento;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateEstadoDocumento
    @id_estado_documento INT,
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM EstadoDocumento WHERE id_estado_documento = @id_estado_documento)
    BEGIN
        RAISERROR('Estado de documento no encontrado.', 16, 1); RETURN;
    END
    UPDATE EstadoDocumento SET nombre = @nombre WHERE id_estado_documento = @id_estado_documento;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteEstadoDocumento
    @id_estado_documento INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Documento WHERE estado_documento = @id_estado_documento)
    BEGIN
        RAISERROR('No se puede eliminar: está siendo usado en Documentos.', 16, 1); RETURN;
    END
    DELETE FROM EstadoDocumento WHERE id_estado_documento = @id_estado_documento;
END
GO

CREATE OR ALTER PROCEDURE sp_GetEstadoDocumento
    @id_estado_documento INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_estado_documento, nombre FROM EstadoDocumento
    WHERE (@id_estado_documento IS NULL OR id_estado_documento = @id_estado_documento);
END
GO

-- =============================================
-- TABLA: MetodoPago
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertMetodoPago
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO MetodoPago (nombre) VALUES (@nombre);
    SELECT SCOPE_IDENTITY() AS id_metodo_pago;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateMetodoPago
    @id_metodo_pago INT,
    @nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM MetodoPago WHERE id_metodo_pago = @id_metodo_pago)
    BEGIN
        RAISERROR('Método de pago no encontrado.', 16, 1); RETURN;
    END
    UPDATE MetodoPago SET nombre = @nombre WHERE id_metodo_pago = @id_metodo_pago;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteMetodoPago
    @id_metodo_pago INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Pago WHERE metodo = @id_metodo_pago)
    BEGIN
        RAISERROR('No se puede eliminar: está siendo usado en Pagos.', 16, 1); RETURN;
    END
    DELETE FROM MetodoPago WHERE id_metodo_pago = @id_metodo_pago;
END
GO

CREATE OR ALTER PROCEDURE sp_GetMetodoPago
    @id_metodo_pago INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_metodo_pago, nombre FROM MetodoPago
    WHERE (@id_metodo_pago IS NULL OR id_metodo_pago = @id_metodo_pago);
END
GO

-- =============================================
-- TABLA: Rol
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertRol
    @nombre NVARCHAR(50),
    @descripcion NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Rol (nombre, descripcion) VALUES (@nombre, @descripcion);
    SELECT SCOPE_IDENTITY() AS id_rol;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateRol
    @id_rol INT,
    @nombre NVARCHAR(50),
    @descripcion NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Rol WHERE id_rol = @id_rol)
    BEGIN
        RAISERROR('Rol no encontrado.', 16, 1); RETURN;
    END
    UPDATE Rol SET nombre = @nombre, descripcion = @descripcion WHERE id_rol = @id_rol;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteRol
    @id_rol INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Personal WHERE id_rol = @id_rol)
    BEGIN
        RAISERROR('No se puede eliminar: está siendo usado por Personal.', 16, 1); RETURN;
    END
    DELETE FROM Rol WHERE id_rol = @id_rol;
END
GO

CREATE OR ALTER PROCEDURE sp_GetRol
    @id_rol INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_rol, nombre, descripcion FROM Rol
    WHERE (@id_rol IS NULL OR id_rol = @id_rol);
END
GO

-- =============================================
-- TABLA: TipoDocumentoCobro
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertTipoDocumentoCobro
    @nombre NVARCHAR(50),
    @descripcion NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO TipoDocumentoCobro (nombre, descripcion) VALUES (@nombre, @descripcion);
    SELECT SCOPE_IDENTITY() AS id_tipo_doc_cobro;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateTipoDocumentoCobro
    @id_tipo_doc_cobro INT,
    @nombre NVARCHAR(50),
    @descripcion NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM TipoDocumentoCobro WHERE id_tipo_doc_cobro = @id_tipo_doc_cobro)
    BEGIN
        RAISERROR('Tipo de documento de cobro no encontrado.', 16, 1); RETURN;
    END
    UPDATE TipoDocumentoCobro SET nombre = @nombre, descripcion = @descripcion
    WHERE id_tipo_doc_cobro = @id_tipo_doc_cobro;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteTipoDocumentoCobro
    @id_tipo_doc_cobro INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Documento WHERE tipo_documento = @id_tipo_doc_cobro)
    BEGIN
        RAISERROR('No se puede eliminar: está siendo usado en Documentos.', 16, 1); RETURN;
    END
    DELETE FROM TipoDocumentoCobro WHERE id_tipo_doc_cobro = @id_tipo_doc_cobro;
END
GO

CREATE OR ALTER PROCEDURE sp_GetTipoDocumentoCobro
    @id_tipo_doc_cobro INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_tipo_doc_cobro, nombre, descripcion FROM TipoDocumentoCobro
    WHERE (@id_tipo_doc_cobro IS NULL OR id_tipo_doc_cobro = @id_tipo_doc_cobro);
END
GO

-- =============================================
-- TABLA: Huesped
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertHuesped
    @nombres NVARCHAR(100),
    @apellidos NVARCHAR(100),
    @tipo_documento INT,
    @num_documento NVARCHAR(50),
    @telefono NVARCHAR(20) = NULL,
    @correo NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Huesped WHERE tipo_documento = @tipo_documento AND num_documento = @num_documento)
    BEGIN
        RAISERROR('Ya existe un huésped con ese tipo y número de documento.', 16, 1); RETURN;
    END
    INSERT INTO Huesped (nombres, apellidos, tipo_documento, num_documento, telefono, correo)
    VALUES (@nombres, @apellidos, @tipo_documento, @num_documento, @telefono, @correo);
    SELECT SCOPE_IDENTITY() AS id_huesped;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateHuesped
    @id_huesped INT,
    @nombres NVARCHAR(100),
    @apellidos NVARCHAR(100),
    @tipo_documento INT,
    @num_documento NVARCHAR(50),
    @telefono NVARCHAR(20) = NULL,
    @correo NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Huesped WHERE id_huesped = @id_huesped)
    BEGIN
        RAISERROR('Huésped no encontrado.', 16, 1); RETURN;
    END
    UPDATE Huesped
    SET nombres = @nombres, apellidos = @apellidos, tipo_documento = @tipo_documento,
        num_documento = @num_documento, telefono = @telefono, correo = @correo
    WHERE id_huesped = @id_huesped;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteHuesped
    @id_huesped INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Reserva WHERE id_huesped = @id_huesped)
    BEGIN
        RAISERROR('No se puede eliminar: el huésped tiene reservas asociadas.', 16, 1); RETURN;
    END
    DELETE FROM Huesped WHERE id_huesped = @id_huesped;
END
GO

CREATE OR ALTER PROCEDURE sp_GetHuesped
    @id_huesped INT = NULL,
    @num_documento NVARCHAR(50) = NULL,
    @nombre NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT h.id_huesped, h.nombres, h.apellidos, td.descripcion AS tipo_documento,
           h.num_documento, h.telefono, h.correo, h.fecha_creacion
    FROM Huesped h
    INNER JOIN TipoDocumento td ON h.tipo_documento = td.id_tipo_documento
    WHERE (@id_huesped IS NULL OR h.id_huesped = @id_huesped)
      AND (@num_documento IS NULL OR h.num_documento LIKE '%' + @num_documento + '%')
      AND (@nombre IS NULL OR h.nombres + ' ' + h.apellidos LIKE '%' + @nombre + '%');
END
GO

-- =============================================
-- TABLA: Personal
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertPersonal
    @nombre NVARCHAR(100),
    @tipo_documento INT,
    @num_documento NVARCHAR(50),
    @email NVARCHAR(255) = NULL,
    @password_hash VARBINARY(MAX),
    @id_rol INT,
    @activo BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Personal WHERE tipo_documento = @tipo_documento AND num_documento = @num_documento)
    BEGIN
        RAISERROR('Ya existe personal con ese tipo y número de documento.', 16, 1); RETURN;
    END
    INSERT INTO Personal (nombre, tipo_documento, num_documento, email, password_hash, id_rol, activo)
    VALUES (@nombre, @tipo_documento, @num_documento, @email, @password_hash, @id_rol, @activo);
    SELECT SCOPE_IDENTITY() AS id_personal;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdatePersonal
    @id_personal INT,
    @nombre NVARCHAR(100),
    @tipo_documento INT,
    @num_documento NVARCHAR(50),
    @email NVARCHAR(255) = NULL,
    @id_rol INT,
    @activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Personal WHERE id_personal = @id_personal)
    BEGIN
        RAISERROR('Personal no encontrado.', 16, 1); RETURN;
    END
    UPDATE Personal
    SET nombre = @nombre, tipo_documento = @tipo_documento, num_documento = @num_documento,
        email = @email, id_rol = @id_rol, activo = @activo
    WHERE id_personal = @id_personal;
END
GO

CREATE OR ALTER PROCEDURE sp_DeletePersonal
    @id_personal INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM OrdenConserjeria WHERE id_personal = @id_personal)
        OR EXISTS (SELECT 1 FROM Pago WHERE id_personal = @id_personal)
    BEGIN
        RAISERROR('No se puede eliminar: el personal tiene órdenes o pagos asociados.', 16, 1); RETURN;
    END
    -- Borrado lógico recomendado
    UPDATE Personal SET activo = 0 WHERE id_personal = @id_personal;
END
GO

CREATE OR ALTER PROCEDURE sp_GetPersonal
    @id_personal INT = NULL,
    @nombre NVARCHAR(100) = NULL,
    @id_rol INT = NULL,
    @activo BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.id_personal, p.nombre, td.descripcion AS tipo_documento,
           p.num_documento, p.email, r.nombre AS rol, p.activo, p.fecha_creacion
    FROM Personal p
    INNER JOIN TipoDocumento td ON p.tipo_documento = td.id_tipo_documento
    INNER JOIN Rol r ON p.id_rol = r.id_rol
    WHERE (@id_personal IS NULL OR p.id_personal = @id_personal)
      AND (@nombre IS NULL OR p.nombre LIKE '%' + @nombre + '%')
      AND (@id_rol IS NULL OR p.id_rol = @id_rol)
      AND (@activo IS NULL OR p.activo = @activo);
END
GO

-- =============================================
-- TABLA: TipoHabitacion
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertTipoHabitacion
    @nombre NVARCHAR(100),
    @descripcion NVARCHAR(255) = NULL,
    @capacidad_personas INT,
    @tarifa_base DECIMAL(12,2)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO TipoHabitacion (nombre, descripcion, capacidad_personas, tarifa_base)
    VALUES (@nombre, @descripcion, @capacidad_personas, @tarifa_base);
    SELECT SCOPE_IDENTITY() AS id_tipo_habitacion;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateTipoHabitacion
    @id_tipo_habitacion INT,
    @nombre NVARCHAR(100),
    @descripcion NVARCHAR(255) = NULL,
    @capacidad_personas INT,
    @tarifa_base DECIMAL(12,2)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM TipoHabitacion WHERE id_tipo_habitacion = @id_tipo_habitacion)
    BEGIN
        RAISERROR('Tipo de habitación no encontrado.', 16, 1); RETURN;
    END
    UPDATE TipoHabitacion
    SET nombre = @nombre, descripcion = @descripcion,
        capacidad_personas = @capacidad_personas, tarifa_base = @tarifa_base
    WHERE id_tipo_habitacion = @id_tipo_habitacion;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteTipoHabitacion
    @id_tipo_habitacion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Habitacion WHERE id_tipo_habitacion = @id_tipo_habitacion)
    BEGIN
        RAISERROR('No se puede eliminar: existen habitaciones de este tipo.', 16, 1); RETURN;
    END
    DELETE FROM TipoHabitacion WHERE id_tipo_habitacion = @id_tipo_habitacion;
END
GO

CREATE OR ALTER PROCEDURE sp_GetTipoHabitacion
    @id_tipo_habitacion INT = NULL,
    @nombre NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_tipo_habitacion, nombre, descripcion, capacidad_personas, tarifa_base
    FROM TipoHabitacion
    WHERE (@id_tipo_habitacion IS NULL OR id_tipo_habitacion = @id_tipo_habitacion)
      AND (@nombre IS NULL OR nombre LIKE '%' + @nombre + '%');
END
GO

-- =============================================
-- TABLA: Habitacion
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertHabitacion
    @numero NVARCHAR(20),
    @piso INT,
    @id_tipo_habitacion INT,
    @estado INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Habitacion WHERE numero = @numero AND piso = @piso)
    BEGIN
        RAISERROR('Ya existe una habitación con ese número y piso.', 16, 1); RETURN;
    END
    INSERT INTO Habitacion (numero, piso, id_tipo_habitacion, estado)
    VALUES (@numero, @piso, @id_tipo_habitacion, @estado);
    SELECT SCOPE_IDENTITY() AS id_habitacion;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateHabitacion
    @id_habitacion INT,
    @numero NVARCHAR(20),
    @piso INT,
    @id_tipo_habitacion INT,
    @estado INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Habitacion WHERE id_habitacion = @id_habitacion)
    BEGIN
        RAISERROR('Habitación no encontrada.', 16, 1); RETURN;
    END
    UPDATE Habitacion
    SET numero = @numero, piso = @piso,
        id_tipo_habitacion = @id_tipo_habitacion, estado = @estado
    WHERE id_habitacion = @id_habitacion;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteHabitacion
    @id_habitacion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Reserva WHERE id_habitacion = @id_habitacion)
        OR EXISTS (SELECT 1 FROM OrdenConserjeria WHERE id_habitacion = @id_habitacion)
    BEGIN
        RAISERROR('No se puede eliminar: la habitación tiene reservas u órdenes asociadas.', 16, 1); RETURN;
    END
    DELETE FROM Habitacion WHERE id_habitacion = @id_habitacion;
END
GO

CREATE OR ALTER PROCEDURE sp_GetHabitacion
    @id_habitacion INT = NULL,
    @piso INT = NULL,
    @estado INT = NULL,
    @id_tipo_habitacion INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT h.id_habitacion, h.numero, h.piso,
           th.nombre AS tipo_habitacion, th.tarifa_base,
           eh.nombre AS estado
    FROM Habitacion h
    INNER JOIN TipoHabitacion th ON h.id_tipo_habitacion = th.id_tipo_habitacion
    INNER JOIN EstadoHabitacion eh ON h.estado = eh.id_estado_habitacion
    WHERE (@id_habitacion IS NULL OR h.id_habitacion = @id_habitacion)
      AND (@piso IS NULL OR h.piso = @piso)
      AND (@estado IS NULL OR h.estado = @estado)
      AND (@id_tipo_habitacion IS NULL OR h.id_tipo_habitacion = @id_tipo_habitacion);
END
GO

-- =============================================
-- TABLA: Reserva
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertReserva
    @id_huesped INT,
    @id_habitacion INT,
    @fecha_entrada DATE,
    @fecha_salida DATE,
    @num_personas INT,
    @monto_total DECIMAL(12,2),
    @estado INT
AS
BEGIN
    SET NOCOUNT ON;
    IF @fecha_salida <= @fecha_entrada
    BEGIN
        RAISERROR('La fecha de salida debe ser posterior a la de entrada.', 16, 1); RETURN;
    END
    -- Verificar disponibilidad de habitación en ese rango
    IF EXISTS (
        SELECT 1 FROM Reserva
        WHERE id_habitacion = @id_habitacion
          AND estado NOT IN (SELECT id_estado_reserva FROM EstadoReserva WHERE nombre IN ('Cancelada', 'No show'))
          AND fecha_entrada < @fecha_salida
          AND fecha_salida > @fecha_entrada
    )
    BEGIN
        RAISERROR('La habitación no está disponible en ese rango de fechas.', 16, 1); RETURN;
    END
    INSERT INTO Reserva (id_huesped, id_habitacion, fecha_entrada, fecha_salida, num_personas, monto_total, estado)
    VALUES (@id_huesped, @id_habitacion, @fecha_entrada, @fecha_salida, @num_personas, @monto_total, @estado);
    SELECT SCOPE_IDENTITY() AS id_reserva;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateReserva
    @id_reserva INT,
    @id_huesped INT,
    @id_habitacion INT,
    @fecha_entrada DATE,
    @fecha_salida DATE,
    @num_personas INT,
    @monto_total DECIMAL(12,2),
    @estado INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Reserva WHERE id_reserva = @id_reserva)
    BEGIN
        RAISERROR('Reserva no encontrada.', 16, 1); RETURN;
    END
    IF @fecha_salida <= @fecha_entrada
    BEGIN
        RAISERROR('La fecha de salida debe ser posterior a la de entrada.', 16, 1); RETURN;
    END
    UPDATE Reserva
    SET id_huesped = @id_huesped, id_habitacion = @id_habitacion,
        fecha_entrada = @fecha_entrada, fecha_salida = @fecha_salida,
        num_personas = @num_personas, monto_total = @monto_total, estado = @estado
    WHERE id_reserva = @id_reserva;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteReserva
    @id_reserva INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM OrdenHospedaje WHERE id_reserva = @id_reserva)
        OR EXISTS (SELECT 1 FROM OrdenConserjeria WHERE id_reserva = @id_reserva)
        OR EXISTS (SELECT 1 FROM Documento WHERE id_reserva = @id_reserva)
    BEGIN
        RAISERROR('No se puede eliminar: la reserva tiene órdenes o documentos asociados.', 16, 1); RETURN;
    END
    DELETE FROM Reserva WHERE id_reserva = @id_reserva;
END
GO

CREATE OR ALTER PROCEDURE sp_GetReserva
    @id_reserva INT = NULL,
    @id_huesped INT = NULL,
    @id_habitacion INT = NULL,
    @estado INT = NULL,
    @fecha_entrada DATE = NULL,
    @fecha_salida DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT r.id_reserva, h.nombres + ' ' + h.apellidos AS huesped,
           hab.numero + ' P' + CAST(hab.piso AS NVARCHAR) AS habitacion,
           r.fecha_entrada, r.fecha_salida, r.num_personas,
           r.monto_total, er.nombre AS estado, r.fecha_creacion
    FROM Reserva r
    INNER JOIN Huesped h ON r.id_huesped = h.id_huesped
    INNER JOIN Habitacion hab ON r.id_habitacion = hab.id_habitacion
    INNER JOIN EstadoReserva er ON r.estado = er.id_estado_reserva
    WHERE (@id_reserva IS NULL OR r.id_reserva = @id_reserva)
      AND (@id_huesped IS NULL OR r.id_huesped = @id_huesped)
      AND (@id_habitacion IS NULL OR r.id_habitacion = @id_habitacion)
      AND (@estado IS NULL OR r.estado = @estado)
      AND (@fecha_entrada IS NULL OR r.fecha_entrada >= @fecha_entrada)
      AND (@fecha_salida IS NULL OR r.fecha_salida <= @fecha_salida);
END
GO

-- =============================================
-- TABLA: OrdenConserjeria
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertOrdenConserjeria
    @id_personal INT,
    @id_habitacion INT,
    @id_reserva INT = NULL,
    @fecha_inicio DATETIME2,
    @fecha_fin DATETIME2 = NULL,
    @precio DECIMAL(12,2),
    @estado INT,
    @descripcion NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF @fecha_fin IS NOT NULL AND @fecha_fin < @fecha_inicio
    BEGIN
        RAISERROR('La fecha de fin no puede ser anterior a la de inicio.', 16, 1); RETURN;
    END
    INSERT INTO OrdenConserjeria (id_personal, id_habitacion, id_reserva, fecha_inicio, fecha_fin, precio, estado, descripcion)
    VALUES (@id_personal, @id_habitacion, @id_reserva, @fecha_inicio, @fecha_fin, @precio, @estado, @descripcion);
    SELECT SCOPE_IDENTITY() AS id_orden_conserj;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateOrdenConserjeria
    @id_orden_conserj INT,
    @id_personal INT,
    @id_habitacion INT,
    @id_reserva INT = NULL,
    @fecha_inicio DATETIME2,
    @fecha_fin DATETIME2 = NULL,
    @precio DECIMAL(12,2),
    @estado INT,
    @descripcion NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM OrdenConserjeria WHERE id_orden_conserj = @id_orden_conserj)
    BEGIN
        RAISERROR('Orden de conserjería no encontrada.', 16, 1); RETURN;
    END
    UPDATE OrdenConserjeria
    SET id_personal = @id_personal, id_habitacion = @id_habitacion, id_reserva = @id_reserva,
        fecha_inicio = @fecha_inicio, fecha_fin = @fecha_fin, precio = @precio,
        estado = @estado, descripcion = @descripcion
    WHERE id_orden_conserj = @id_orden_conserj;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteOrdenConserjeria
    @id_orden_conserj INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Documento WHERE id_orden_conserjeria = @id_orden_conserj)
    BEGIN
        RAISERROR('No se puede eliminar: la orden tiene documentos asociados.', 16, 1); RETURN;
    END
    DELETE FROM OrdenConserjeria WHERE id_orden_conserj = @id_orden_conserj;
END
GO

CREATE OR ALTER PROCEDURE sp_GetOrdenConserjeria
    @id_orden_conserj INT = NULL,
    @id_habitacion INT = NULL,
    @id_personal INT = NULL,
    @estado INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT oc.id_orden_conserj, p.nombre AS personal,
           h.numero + ' P' + CAST(h.piso AS NVARCHAR) AS habitacion,
           oc.id_reserva, oc.fecha_inicio, oc.fecha_fin,
           oc.precio, eoc.nombre AS estado, oc.descripcion
    FROM OrdenConserjeria oc
    INNER JOIN Personal p ON oc.id_personal = p.id_personal
    INNER JOIN Habitacion h ON oc.id_habitacion = h.id_habitacion
    INNER JOIN EstadoOrdenConserjeria eoc ON oc.estado = eoc.id_estado_orden_conserj
    WHERE (@id_orden_conserj IS NULL OR oc.id_orden_conserj = @id_orden_conserj)
      AND (@id_habitacion IS NULL OR oc.id_habitacion = @id_habitacion)
      AND (@id_personal IS NULL OR oc.id_personal = @id_personal)
      AND (@estado IS NULL OR oc.estado = @estado);
END
GO

-- =============================================
-- TABLA: OrdenHospedaje
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertOrdenHospedaje
    @id_reserva INT,
    @estado INT,
    @fecha_checkin DATETIME2 = NULL,
    @fecha_checkout DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM OrdenHospedaje WHERE id_reserva = @id_reserva)
    BEGIN
        RAISERROR('Ya existe una orden de hospedaje para esa reserva.', 16, 1); RETURN;
    END
    IF @fecha_checkout IS NOT NULL AND @fecha_checkout < @fecha_checkin
    BEGIN
        RAISERROR('La fecha de checkout no puede ser anterior al checkin.', 16, 1); RETURN;
    END
    INSERT INTO OrdenHospedaje (id_reserva, estado, fecha_checkin, fecha_checkout)
    VALUES (@id_reserva, @estado, @fecha_checkin, @fecha_checkout);
    SELECT SCOPE_IDENTITY() AS id_orden_hospedaje;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateOrdenHospedaje
    @id_orden_hospedaje INT,
    @estado INT,
    @fecha_checkin DATETIME2 = NULL,
    @fecha_checkout DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM OrdenHospedaje WHERE id_orden_hospedaje = @id_orden_hospedaje)
    BEGIN
        RAISERROR('Orden de hospedaje no encontrada.', 16, 1); RETURN;
    END
    UPDATE OrdenHospedaje
    SET estado = @estado, fecha_checkin = @fecha_checkin, fecha_checkout = @fecha_checkout
    WHERE id_orden_hospedaje = @id_orden_hospedaje;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteOrdenHospedaje
    @id_orden_hospedaje INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Documento WHERE id_orden_hospedaje = @id_orden_hospedaje)
        OR EXISTS (SELECT 1 FROM Encuesta WHERE id_orden_hospedaje = @id_orden_hospedaje)
    BEGIN
        RAISERROR('No se puede eliminar: tiene documentos o encuestas asociados.', 16, 1); RETURN;
    END
    DELETE FROM OrdenHospedaje WHERE id_orden_hospedaje = @id_orden_hospedaje;
END
GO

CREATE OR ALTER PROCEDURE sp_GetOrdenHospedaje
    @id_orden_hospedaje INT = NULL,
    @id_reserva INT = NULL,
    @estado INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT oh.id_orden_hospedaje, oh.id_reserva,
           h.nombres + ' ' + h.apellidos AS huesped,
           hab.numero + ' P' + CAST(hab.piso AS NVARCHAR) AS habitacion,
           eoh.nombre AS estado, oh.fecha_checkin, oh.fecha_checkout
    FROM OrdenHospedaje oh
    INNER JOIN Reserva r ON oh.id_reserva = r.id_reserva
    INNER JOIN Huesped h ON r.id_huesped = h.id_huesped
    INNER JOIN Habitacion hab ON r.id_habitacion = hab.id_habitacion
    INNER JOIN EstadoOrdenHospedaje eoh ON oh.estado = eoh.id_estado_orden_hosp
    WHERE (@id_orden_hospedaje IS NULL OR oh.id_orden_hospedaje = @id_orden_hospedaje)
      AND (@id_reserva IS NULL OR oh.id_reserva = @id_reserva)
      AND (@estado IS NULL OR oh.estado = @estado);
END
GO

-- =============================================
-- TABLA: Documento
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertDocumento
    @numero_documento NVARCHAR(50),
    @tipo_documento INT,
    @id_reserva INT = NULL,
    @id_orden_hospedaje INT = NULL,
    @id_orden_conserjeria INT = NULL,
    @monto_total DECIMAL(12,2),
    @monto_pagado DECIMAL(12,2) = 0,
    @fecha_vencimiento DATE = NULL,
    @descripcion NVARCHAR(500) = NULL,
    @estado_documento INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @origenes INT =
        (CASE WHEN @id_reserva IS NOT NULL THEN 1 ELSE 0 END +
         CASE WHEN @id_orden_hospedaje IS NOT NULL THEN 1 ELSE 0 END +
         CASE WHEN @id_orden_conserjeria IS NOT NULL THEN 1 ELSE 0 END);
    IF @origenes <> 1
    BEGIN
        RAISERROR('El documento debe tener exactamente un origen (reserva, orden hospedaje u orden conserjería).', 16, 1); RETURN;
    END
    IF EXISTS (SELECT 1 FROM Documento WHERE numero_documento = @numero_documento)
    BEGIN
        RAISERROR('Ya existe un documento con ese número.', 16, 1); RETURN;
    END
    INSERT INTO Documento (numero_documento, tipo_documento, id_reserva, id_orden_hospedaje,
                           id_orden_conserjeria, monto_total, monto_pagado, fecha_vencimiento,
                           descripcion, estado_documento)
    VALUES (@numero_documento, @tipo_documento, @id_reserva, @id_orden_hospedaje,
            @id_orden_conserjeria, @monto_total, @monto_pagado, @fecha_vencimiento,
            @descripcion, @estado_documento);
    SELECT SCOPE_IDENTITY() AS id_documento;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateDocumento
    @id_documento INT,
    @monto_pagado DECIMAL(12,2),
    @fecha_vencimiento DATE = NULL,
    @descripcion NVARCHAR(500) = NULL,
    @estado_documento INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Documento WHERE id_documento = @id_documento)
    BEGIN
        RAISERROR('Documento no encontrado.', 16, 1); RETURN;
    END
    UPDATE Documento
    SET monto_pagado = @monto_pagado, fecha_vencimiento = @fecha_vencimiento,
        descripcion = @descripcion, estado_documento = @estado_documento
    WHERE id_documento = @id_documento;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteDocumento
    @id_documento INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Pago WHERE id_documento = @id_documento)
    BEGIN
        RAISERROR('No se puede eliminar: el documento tiene pagos registrados.', 16, 1); RETURN;
    END
    DELETE FROM Documento WHERE id_documento = @id_documento;
END
GO

CREATE OR ALTER PROCEDURE sp_GetDocumento
    @id_documento INT = NULL,
    @numero_documento NVARCHAR(50) = NULL,
    @estado_documento INT = NULL,
    @tipo_documento INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT d.id_documento, d.numero_documento, tdc.nombre AS tipo_documento,
           d.monto_total, d.monto_pagado, d.saldo_pendiente,
           d.fecha_emision, d.fecha_vencimiento,
           d.descripcion, ed.nombre AS estado,
           d.id_reserva, d.id_orden_hospedaje, d.id_orden_conserjeria
    FROM Documento d
    INNER JOIN TipoDocumentoCobro tdc ON d.tipo_documento = tdc.id_tipo_doc_cobro
    INNER JOIN EstadoDocumento ed ON d.estado_documento = ed.id_estado_documento
    WHERE (@id_documento IS NULL OR d.id_documento = @id_documento)
      AND (@numero_documento IS NULL OR d.numero_documento LIKE '%' + @numero_documento + '%')
      AND (@estado_documento IS NULL OR d.estado_documento = @estado_documento)
      AND (@tipo_documento IS NULL OR d.tipo_documento = @tipo_documento);
END
GO

-- =============================================
-- TABLA: Pago
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertPago
    @id_documento INT,
    @monto_pagado DECIMAL(12,2),
    @metodo INT,
    @estado_pago INT,
    @numero_comprobante NVARCHAR(100) = NULL,
    @numero_operacion NVARCHAR(100) = NULL,
    @observaciones NVARCHAR(500) = NULL,
    @id_personal INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @saldo DECIMAL(12,2);
    SELECT @saldo = saldo_pendiente FROM Documento WHERE id_documento = @id_documento;
    IF @saldo IS NULL
    BEGIN
        RAISERROR('Documento no encontrado.', 16, 1); RETURN;
    END
    IF @monto_pagado > @saldo
    BEGIN
        RAISERROR('El monto pagado excede el saldo pendiente del documento.', 16, 1); RETURN;
    END
    INSERT INTO Pago (id_documento, monto_pagado, metodo, estado_pago,
                      numero_comprobante, numero_operacion, observaciones, id_personal)
    VALUES (@id_documento, @monto_pagado, @metodo, @estado_pago,
            @numero_comprobante, @numero_operacion, @observaciones, @id_personal);
    -- Actualizar monto_pagado en Documento
    UPDATE Documento
    SET monto_pagado = monto_pagado + @monto_pagado
    WHERE id_documento = @id_documento;
    SELECT SCOPE_IDENTITY() AS id_pago;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdatePago
    @id_pago INT,
    @estado_pago INT,
    @numero_comprobante NVARCHAR(100) = NULL,
    @numero_operacion NVARCHAR(100) = NULL,
    @observaciones NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Pago WHERE id_pago = @id_pago)
    BEGIN
        RAISERROR('Pago no encontrado.', 16, 1); RETURN;
    END
    UPDATE Pago
    SET estado_pago = @estado_pago, numero_comprobante = @numero_comprobante,
        numero_operacion = @numero_operacion, observaciones = @observaciones
    WHERE id_pago = @id_pago;
END
GO

CREATE OR ALTER PROCEDURE sp_DeletePago
    @id_pago INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @monto DECIMAL(12,2), @id_doc INT;
    SELECT @monto = monto_pagado, @id_doc = id_documento FROM Pago WHERE id_pago = @id_pago;
    IF @monto IS NULL
    BEGIN
        RAISERROR('Pago no encontrado.', 16, 1); RETURN;
    END
    DELETE FROM Pago WHERE id_pago = @id_pago;
    -- Revertir monto en Documento
    UPDATE Documento SET monto_pagado = monto_pagado - @monto WHERE id_documento = @id_doc;
END
GO

CREATE OR ALTER PROCEDURE sp_GetPago
    @id_pago INT = NULL,
    @id_documento INT = NULL,
    @estado_pago INT = NULL,
    @metodo INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.id_pago, p.id_documento, d.numero_documento,
           p.fecha_pago, p.monto_pagado, mp.nombre AS metodo,
           ep.nombre AS estado_pago, p.numero_comprobante,
           p.numero_operacion, p.observaciones, per.nombre AS personal
    FROM Pago p
    INNER JOIN Documento d ON p.id_documento = d.id_documento
    INNER JOIN MetodoPago mp ON p.metodo = mp.id_metodo_pago
    INNER JOIN EstadoPago ep ON p.estado_pago = ep.id_estado_pago
    LEFT JOIN Personal per ON p.id_personal = per.id_personal
    WHERE (@id_pago IS NULL OR p.id_pago = @id_pago)
      AND (@id_documento IS NULL OR p.id_documento = @id_documento)
      AND (@estado_pago IS NULL OR p.estado_pago = @estado_pago)
      AND (@metodo IS NULL OR p.metodo = @metodo);
END
GO

-- =============================================
-- TABLA: Encuesta
-- =============================================

CREATE OR ALTER PROCEDURE sp_InsertEncuesta
    @id_orden_hospedaje INT,
    @descripcion NVARCHAR(500) = NULL,
    @recomendacion INT,
    @lugar_origen NVARCHAR(100) = NULL,
    @motivo_viaje NVARCHAR(100) = NULL,
    @calificacion_limpieza INT = NULL,
    @calificacion_servicio INT = NULL,
    @calificacion_ubicacion INT = NULL,
    @calificacion_precio INT = NULL,
    @comentarios NVARCHAR(1000) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Encuesta WHERE id_orden_hospedaje = @id_orden_hospedaje)
    BEGIN
        RAISERROR('Ya existe una encuesta para esta orden de hospedaje.', 16, 1); RETURN;
    END
    INSERT INTO Encuesta (id_orden_hospedaje, descripcion, recomendacion, lugar_origen, motivo_viaje,
                          calificacion_limpieza, calificacion_servicio, calificacion_ubicacion,
                          calificacion_precio, comentarios)
    VALUES (@id_orden_hospedaje, @descripcion, @recomendacion, @lugar_origen, @motivo_viaje,
            @calificacion_limpieza, @calificacion_servicio, @calificacion_ubicacion,
            @calificacion_precio, @comentarios);
    SELECT SCOPE_IDENTITY() AS id_encuesta;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateEncuesta
    @id_encuesta INT,
    @descripcion NVARCHAR(500) = NULL,
    @recomendacion INT,
    @lugar_origen NVARCHAR(100) = NULL,
    @motivo_viaje NVARCHAR(100) = NULL,
    @calificacion_limpieza INT = NULL,
    @calificacion_servicio INT = NULL,
    @calificacion_ubicacion INT = NULL,
    @calificacion_precio INT = NULL,
    @comentarios NVARCHAR(1000) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Encuesta WHERE id_encuesta = @id_encuesta)
    BEGIN
        RAISERROR('Encuesta no encontrada.', 16, 1); RETURN;
    END
    UPDATE Encuesta
    SET descripcion = @descripcion, recomendacion = @recomendacion,
        lugar_origen = @lugar_origen, motivo_viaje = @motivo_viaje,
        calificacion_limpieza = @calificacion_limpieza, calificacion_servicio = @calificacion_servicio,
        calificacion_ubicacion = @calificacion_ubicacion, calificacion_precio = @calificacion_precio,
        comentarios = @comentarios
    WHERE id_encuesta = @id_encuesta;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteEncuesta
    @id_encuesta INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Encuesta WHERE id_encuesta = @id_encuesta)
    BEGIN
        RAISERROR('Encuesta no encontrada.', 16, 1); RETURN;
    END
    DELETE FROM Encuesta WHERE id_encuesta = @id_encuesta;
END
GO

CREATE OR ALTER PROCEDURE sp_GetEncuesta
    @id_encuesta INT = NULL,
    @id_orden_hospedaje INT = NULL,
    @motivo_viaje NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT e.id_encuesta, e.id_orden_hospedaje,
           h.nombres + ' ' + h.apellidos AS huesped,
           e.recomendacion, e.lugar_origen, e.motivo_viaje,
           e.calificacion_limpieza, e.calificacion_servicio,
           e.calificacion_ubicacion, e.calificacion_precio,
           e.comentarios, e.descripcion, e.fecha_encuesta
    FROM Encuesta e
    INNER JOIN OrdenHospedaje oh ON e.id_orden_hospedaje = oh.id_orden_hospedaje
    INNER JOIN Reserva r ON oh.id_reserva = r.id_reserva
    INNER JOIN Huesped h ON r.id_huesped = h.id_huesped
    WHERE (@id_encuesta IS NULL OR e.id_encuesta = @id_encuesta)
      AND (@id_orden_hospedaje IS NULL OR e.id_orden_hospedaje = @id_orden_hospedaje)
      AND (@motivo_viaje IS NULL OR e.motivo_viaje LIKE '%' + @motivo_viaje + '%');
END
GO
