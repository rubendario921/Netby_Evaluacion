-- =============================================
-- Stored Procedures para Transacciones
-- =============================================

-- Registrar una Compra
CREATE PROCEDURE SP_Transaccion_RegistrarCompra
    @ProductoId UNIQUEIDENTIFIER,
    @Cantidad INT,
    @PrecioUnitario DECIMAL(18,2),
    @Detalle NVARCHAR(MAX) = NULL,
    @Id UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Generar nuevo GUID
        SET @Id = NEWID();
        
        -- Calcular precio total
        DECLARE @PrecioTotal DECIMAL(18,2) = @Cantidad * @PrecioUnitario;
        
        -- Insertar transacción de compra
        INSERT INTO Transacciones (
            Id, Fecha, TipoTransaccion, ProductoId, 
            Cantidad, PrecioUnitario, PrecioTotal, Detalle
        )
        VALUES (
            @Id, GETDATE(), 'C', @ProductoId,
            @Cantidad, @PrecioUnitario, @PrecioTotal, @Detalle
        );
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- Registrar una Venta
CREATE PROCEDURE SP_Transaccion_RegistrarVenta
    @ProductoId UNIQUEIDENTIFIER,
    @Cantidad INT,
    @PrecioUnitario DECIMAL(18,2),
    @Detalle NVARCHAR(MAX) = NULL,
    @Id UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Verificar stock disponible
        DECLARE @StockActual INT;
        SELECT @StockActual = Stock FROM Producto WHERE ProductoId = @ProductoId;
        
        IF @StockActual < @Cantidad
        BEGIN
            RAISERROR ('Stock insuficiente. Stock actual: %d, Cantidad solicitada: %d', 16, 1, @StockActual, @Cantidad);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        
        -- Generar nuevo GUID
        SET @Id = NEWID();
        
        -- Calcular precio total
        DECLARE @PrecioTotal DECIMAL(18,2) = @Cantidad * @PrecioUnitario;
        
        -- Insertar transacción de venta
        INSERT INTO Transacciones (
            Id, Fecha, TipoTransaccion, ProductoId, 
            Cantidad, PrecioUnitario, PrecioTotal, Detalle
        )
        VALUES (
            @Id, GETDATE(), 'V', @ProductoId,
            @Cantidad, @PrecioUnitario, @PrecioTotal, @Detalle
        );
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- Anular una transacción
CREATE PROCEDURE SP_Transaccion_Anular
    @Id UNIQUEIDENTIFIER,
    @MotivoAnulacion NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Obtener datos de la transacción
        DECLARE @TipoTransaccion CHAR(1), @ProductoId UNIQUEIDENTIFIER, @Cantidad INT;
        
        SELECT 
            @TipoTransaccion = TipoTransaccion,
            @ProductoId = ProductoId,
            @Cantidad = Cantidad
        FROM Transacciones
        WHERE Id = @Id AND Estado = 1;
        
        IF @TipoTransaccion IS NULL
        BEGIN
            RAISERROR ('Transacción no encontrada o ya fue anulada', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        
        -- Revertir el stock
        UPDATE Productos
        SET Stock = CASE 
            WHEN @TipoTransaccion = 'C' THEN Stock - @Cantidad  -- Revertir compra
            WHEN @TipoTransaccion = 'V' THEN Stock + @Cantidad  -- Revertir venta
        END
        WHERE Id = @ProductoId;
        
        -- Marcar como anulada
        UPDATE Transacciones
        SET 
        	TipoTransaccion = 'A', --Proceso Anulado
            Estado = 0,
            FechaAnulacion = GETDATE(),
            UsuarioAnulacion = SUSER_SNAME(),
            Detalle = ISNULL(Detalle + ' | ', '') + 'ANULADA: ' + ISNULL(@MotivoAnulacion, 'Sin motivo especificado')
        WHERE Id = @Id;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO


-- Consultar transacciones
SELECT * FROM Transacciones ORDER BY Fecha DESC;

-- Ver stock actualizado de productos
SELECT ProductoId, Codigo, Nombre, Stock FROM Producto WHERE Activo = 1;

-- Ver historial de transacciones
SELECT * FROM TransaccionesHistorial ORDER BY FechaOperacion DESC;