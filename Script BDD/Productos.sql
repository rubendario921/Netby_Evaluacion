use inventory
GO;

CREATE TABLE Products (
 Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
 
 -- Campos del producto
 Code NVARCHAR(50) NOT NULL UNIQUE,
 Name NVARCHAR(200) NOT NULL,
 Description NVARCHAR(MAX) NULL,
 Category NVARCHAR(200) NULL,
 Image NVARCHAR(MAX) NULL,
 Price DECIMAL(18,2) NOT NULL,
 Stock INT NOT NULL DEFAULT 0, 
 Status BIT NOT NULL DEFAULT 1, 

 -- Campos de auditoría 
 CreationDate DATETIME2 NOT NULL DEFAULT GETDATE(),
 UserCreation NVARCHAR(100) NOT NULL DEFAULT SUSER_SNAME(),
 ModificationDate DATETIME2 NULL,
 UserModification NVARCHAR(100) NULL,
 DeletionDate DATETIME2 NULL,
 UserDeletion NVARCHAR(100) NULL,
 
 -- Constraints
 CONSTRAINT CK_Product_Price CHECK (Price >= 0),
 CONSTRAINT CK_Product_Stock CHECK (Stock >= 0)
)


-- =============================================
-- Trigger para actualizar auditoría en UPDATE
-- =============================================
CREATE TRIGGER TR_Product_AuditUpdate
ON Products
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE p
    SET 
        ModificationDate = GETDATE(),
        UserModification = SUSER_SNAME()
    FROM Products p
    INNER JOIN inserted i ON p.Id = i.Id;
END