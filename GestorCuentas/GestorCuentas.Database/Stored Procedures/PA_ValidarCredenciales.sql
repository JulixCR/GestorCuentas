CREATE PROCEDURE [dbo].[PA_ValidarCredenciales]
    @IdUsuario NVARCHAR(50),
    @Clave     NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(1)
    FROM [dbo].[TB_Usuarios]
    WHERE [IdUsuario] = @IdUsuario
      AND [Clave] = @Clave
      AND [Activo] = 1;
END;
GO
