CREATE TABLE [dbo].[TB_Usuarios]
(
    [IdUsuario]       NVARCHAR(50)   NOT NULL,
    [NombreCompleto]  NVARCHAR(200)  NOT NULL,
    [Correo]          NVARCHAR(256)  NULL,
    [Clave]           NVARCHAR(256)  NOT NULL,
    [Activo]          BIT            NOT NULL CONSTRAINT [DF_TB_Usuarios_Activo] DEFAULT (1),
    [FechaCreacion]   DATETIME2 (7)  NOT NULL CONSTRAINT [DF_TB_Usuarios_FechaCreacion] DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT [PK_TB_Usuarios] PRIMARY KEY CLUSTERED ([IdUsuario] ASC)
);

GO;

CREATE UNIQUE INDEX [UX_TB_Usuarios_Correo]
    ON [dbo].[TB_Usuarios]([Correo])
    WHERE [Correo] IS NOT NULL;
