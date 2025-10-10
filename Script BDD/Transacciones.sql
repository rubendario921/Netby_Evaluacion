use inventory
GO

CREATE TABLE Transacciones (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Fecha DATETIME2 NOT NULL DEFAULT GETDATE(),
    TipoTransaccion CHAR(1) NOT NULL, -- 'C' = Compra, 'V' = Venta,  'A' = Anulada
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    PrecioTotal DECIMAL(18,2) NOT NULL,
    Detalle NVARCHAR(MAX) NULL,
    Estado  BIT NOT NULL DEFAULT 1,
    -- Relación con Producto
    ProductoId UNIQUEIDENTIFIER NOT NULL,    

    -- Campos de auditoría
    FechaCreacion DATETIME2 NOT NULL DEFAULT GETDATE(),
    UsuarioCreacion NVARCHAR(100) NOT NULL DEFAULT SUSER_SNAME(),
    FechaModificacion DATETIME2 NULL,
    UsuarioModificacion NVARCHAR(100) NULL,
    FechaAnulacion DATETIME2 NULL,
    UsuarioAnulacion NVARCHAR(100) NULL,
    
    -- Constraints
    CONSTRAINT CK_Transacciones_TipoTransaccion 
        CHECK (TipoTransaccion IN ('C', 'V')),
    CONSTRAINT CK_Transacciones_Cantidad 
        CHECK (Cantidad > 0),
    CONSTRAINT CK_Transacciones_PrecioUnitario 
        CHECK (PrecioUnitario >= 0),
    CONSTRAINT CK_Transacciones_PrecioTotal 
        CHECK (PrecioTotal >= 0),
    CONSTRAINT FK_Transacciones_Producto 
        FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);
GO

-- =============================================
-- Índices para mejorar el rendimiento
-- =============================================
CREATE NONCLUSTERED INDEX IX_Transacciones_Fecha 
    ON Transacciones(Fecha DESC);

CREATE NONCLUSTERED INDEX IX_Transacciones_TipoTransaccion 
    ON Transacciones(TipoTransaccion);

CREATE NONCLUSTERED INDEX IX_Transacciones_ProductoId 
    ON Transacciones(ProductoId);

CREATE NONCLUSTERED INDEX IX_Transacciones_Estado
    ON Transacciones(Estado);

-- Índice compuesto para consultas de reportes
CREATE NONCLUSTERED INDEX IX_Transacciones_Fecha_Tipo 
    ON Transacciones(Fecha DESC, TipoTransaccion) 
    INCLUDE (PrecioTotal);
GO

-- =============================================
-- Trigger para actualizar auditoría en UPDATE
-- =============================================
CREATE TRIGGER TR_Transacciones_AuditUpdate
ON Transacciones
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE t
    SET 
        FechaModificacion = GETDATE(),
        UsuarioModificacion = SUSER_SNAME()
    FROM Transacciones t
    INNER JOIN inserted i ON t.Id = i.Id;
END
GO
