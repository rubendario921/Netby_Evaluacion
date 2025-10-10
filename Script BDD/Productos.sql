use inventory
GO;

CREATE TABLE Productos (
 Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
 
 -- Campos del producto
 Codigo NVARCHAR(50) NOT NULL UNIQUE,
 Nombre NVARCHAR(200) NOT NULL,
 Descripcion NVARCHAR(MAX) NULL,
 Categoria NVARCHAR(200) NULL,
 Imagen NVARCHAR(MAX) NULL,
 Precio DECIMAL(18,2) NOT NULL,
 Stock INT NOT NULL DEFAULT 0, 
 Estado BIT NOT NULL DEFAULT 1, 

 -- Campos de auditoría 
 FechaCreacion DATETIME2 NOT NULL DEFAULT GETDATE(),
 UsuarioCreacion NVARCHAR(100) NOT NULL DEFAULT SUSER_SNAME(),
 FechaModificacion DATETIME2 NULL,
 UsuarioModificacion NVARCHAR(100) NULL,
 FechaEliminacion DATETIME2 NULL,
 UsuarioEliminacion NVARCHAR(100) NULL,
 
 -- Constraints
 CONSTRAINT CK_Producto_Precio CHECK (Precio >= 0),
 CONSTRAINT CK_Producto_Stock CHECK (Stock >= 0)
)


-- =============================================
-- Trigger para actualizar auditoría en UPDATE
-- =============================================
CREATE TRIGGER TR_Producto_AuditUpdate
ON Productos
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE p
    SET 
        FechaModificacion = GETDATE(),
        UsuarioModificacion = SUSER_SNAME()
    FROM Productos p
    INNER JOIN inserted i ON p.Id = i.Id;
END