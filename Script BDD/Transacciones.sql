use inventory
GO

CREATE TABLE Transactions(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Date DATETIME2 NOT NULL DEFAULT GETDATE(),
    TransactionType CHAR(1) NOT NULL, -- 'C' = Compra, 'V' = Venta,  'A' = Anulada
    Amount INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    PriceTotal DECIMAL(18,2) NOT NULL,
    Detail NVARCHAR(MAX) NULL,
    Status  BIT NOT NULL DEFAULT 1,
    -- Relación con Producto
    ProductId UNIQUEIDENTIFIER NOT NULL,    

    -- Campos de auditoría
    CreationDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    UserCreation NVARCHAR(100) NOT NULL DEFAULT SUSER_SNAME(),
    ModificationDate DATETIME2 NULL,
    UserModification NVARCHAR(100) NULL,
    CancellationDate DATETIME2 NULL,
    UserCancellation NVARCHAR(100) NULL,
    
    -- Constraints
    CONSTRAINT CK_Transactions_TransactionType 
        CHECK (TransactionType IN ('C', 'V')),
    CONSTRAINT CK_Transactions_Amount 
        CHECK (Amount > 0),
    CONSTRAINT CK_Transacciones_UnitPrice 
        CHECK (UnitPrice >= 0),
    CONSTRAINT CK_Transacciones_PriceTotal 
        CHECK (PriceTotal >= 0),
    CONSTRAINT FK_Transacciones_Product 
        FOREIGN KEY (ProductId) REFERENCES Products(Id)
);
GO

-- =============================================
-- Índices para mejorar el rendimiento
-- =============================================
CREATE NONCLUSTERED INDEX IX_Transacciones_Date 
    ON Transactions(Date DESC);

CREATE NONCLUSTERED INDEX IX_Transacciones_TransactionType 
    ON Transactions(TransactionType);

CREATE NONCLUSTERED INDEX IX_Transacciones_ProductId 
    ON Transactions(ProductId);

CREATE NONCLUSTERED INDEX IX_Transacciones_Status
    ON Transactions(Status);

-- Índice compuesto para consultas de reportes
CREATE NONCLUSTERED INDEX IX_Transacciones_Fecha_Tipo 
    ON Transactions(Date DESC, TransactionType) 
    INCLUDE (PrecioTotal);
GO

-- =============================================
-- Trigger para actualizar auditoría en UPDATE
-- =============================================
CREATE TRIGGER TR_Transacciones_AuditUpdate
ON Transactions
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE t
    SET 
        ModificationDate = GETDATE(),
        UserModification = SUSER_SNAME()
    FROM Transactions t
    INNER JOIN inserted i ON t.Id = i.Id;
END
GO
