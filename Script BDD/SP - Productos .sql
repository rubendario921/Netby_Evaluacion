- =============================================
-- Stored Procedures para operaciones CRUD con GUID
-- =============================================
use inventory

-- Insertar producto
CREATE PROCEDURE SP_Producto_Insertar
    @Codigo NVARCHAR(50),
    @Nombre NVARCHAR(200),
    @Descripcion NVARCHAR(MAX) = NULL,
    @Categoria NVARCHAR(200) = NULL,
    @Imagen NVARCHAR(MAX) = NULL,
    @Precio DECIMAL(18,2),
    @Stock INT = 0,            
    @Id UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Generar nuevo GUID
    SET @Id = NEWID();
    
    INSERT INTO Productos (Id, Codigo, Nombre, Descripcion, Categoria,  Imagen, Precio, Stock)
    VALUES (@Id, @Codigo, @Nombre, @Descripcion,@Categoria,@Imagen, @Precio, @Stock);
END
GO



-- Actualizar producto
CREATE PROCEDURE SP_Producto_Actualizar
    @Id UNIQUEIDENTIFIER,
    @Codigo NVARCHAR(50),
    @Nombre NVARCHAR(200),
    @Descripcion NVARCHAR(MAX) = NULL,
    @Categoria NVARCHAR(200) = NULL,
    @Imagen NVARCHAR(MAX) = NULL,
    @Precio DECIMAL(18,2),
    @Stock INT,
    @Estado BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Productos
    SET 
        Codigo = @Codigo,
        Nombre = @Nombre,
        Descripcion = @Descripcion,
        Categoria = @Categoria,
        Imagen = @Imagen,
        Precio = @Precio,
        Stock = @Stock,
        Estado = @Estado
    WHERE Id = @Id;
END
GO

-- Eliminar Producto
CREATE PROCEDURE SP_Producto_Eliminar
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Productos
    SET 
        Estado = 0,
        FechaEliminacion = GETDATE(),
        UsuarioEliminacion = SUSER_SNAME()
    WHERE Id = @Id;
END
GO

-- Obtener producto por ID
CREATE PROCEDURE SP_Producto_ObtenerPorId
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT * 
    FROM Productos 
    WHERE Id = @Id;
END
GO

-- Obtener producto por Código
CREATE PROCEDURE SP_Producto_ObtenerPorCodigo
    @Codigo NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT * 
    FROM Productos 
    WHERE Codigo = @Codigo;
END
GO