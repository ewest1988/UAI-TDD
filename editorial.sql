USE [editorial]
GO
/****** Object:  User [administrador]    Script Date: 21/11/2018 05:07:25 p. m. ******/
CREATE USER [administrador] FOR LOGIN [administrador] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [administrador]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [administrador]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [administrador]
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 21/11/2018 05:07:25 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[id_bitacora] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[id_evento] [int] NOT NULL,
	[fec_evento] [datetime] NOT NULL,
	[id_criticidad] [int] NOT NULL,
	[digito_verificador] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes_Calificados]    Script Date: 21/11/2018 05:07:25 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes_Calificados](
	[CUIT] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Criticidad]    Script Date: 21/11/2018 05:07:25 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Criticidad](
	[id_criticidad] [int] IDENTITY(1,1) NOT NULL,
	[desc_criticidad] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Digito_Verificador]    Script Date: 21/11/2018 05:07:25 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Digito_Verificador](
	[Id_Digito_Verificador] [int] IDENTITY(1,1) NOT NULL,
	[Tabla] [varchar](50) NOT NULL,
	[Digito_Verificador] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Digito_Verificador] PRIMARY KEY CLUSTERED 
(
	[Id_Digito_Verificador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Documento]    Script Date: 21/11/2018 05:07:25 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documento](
	[id_documento] [int] IDENTITY(1,1) NOT NULL,
	[id_tipo] [int] NOT NULL,
	[fec_creacion] [date] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[desc_documento] [varchar](50) NOT NULL,
	[ext_documento] [varchar](5) NOT NULL,
	[cont_documento] [varbinary](max) NOT NULL,
	[digito_verificador] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Documento] PRIMARY KEY CLUSTERED 
(
	[id_documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 21/11/2018 05:07:25 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estado](
	[id_estado] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[id_criticidad] [int] NOT NULL,
 CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
(
	[id_estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Etiqueta]    Script Date: 21/11/2018 05:07:25 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Etiqueta](
	[id_etiqueta] [int] IDENTITY(1,1) NOT NULL,
	[desc_etiqueta] [varchar](50) NOT NULL,
	[id_menu] [int] NOT NULL,
	[id_idioma] [int] NOT NULL,
 CONSTRAINT [PK_Etiqueta] PRIMARY KEY CLUSTERED 
(
	[id_etiqueta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evento]    Script Date: 21/11/2018 05:07:25 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evento](
	[id_evento] [int] IDENTITY(1,1) NOT NULL,
	[desc_evento] [varchar](50) NULL,
	[id_criticidad] [int] NOT NULL,
 CONSTRAINT [PK_Evento] PRIMARY KEY CLUSTERED 
(
	[id_evento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Familia]    Script Date: 21/11/2018 05:07:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Familia](
	[id_familia] [int] IDENTITY(1,1) NOT NULL,
	[desc_familia] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Familia] PRIMARY KEY CLUSTERED 
(
	[id_familia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Idioma]    Script Date: 21/11/2018 05:07:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Idioma](
	[id_idioma] [int] IDENTITY(1,1) NOT NULL,
	[desc_idioma] [varchar](30) NULL,
 CONSTRAINT [PK_Idioma] PRIMARY KEY CLUSTERED 
(
	[id_idioma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[menu]    Script Date: 21/11/2018 05:07:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[menu](
	[id_menu] [int] IDENTITY(1,1) NOT NULL,
	[desc_menu] [varchar](50) NOT NULL,
 CONSTRAINT [PK_menu] PRIMARY KEY CLUSTERED 
(
	[id_menu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nota]    Script Date: 21/11/2018 05:07:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nota](
	[id_nota] [int] IDENTITY(1,1) NOT NULL,
	[titulo] [varchar](100) NOT NULL,
	[cuerpo] [varchar](5000) NOT NULL,
	[copete] [varchar](100) NOT NULL,
	[id_documento] [int] NOT NULL,
	[id_seccion] [int] NOT NULL,
 CONSTRAINT [PK_Nota] PRIMARY KEY CLUSTERED 
(
	[id_nota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patente]    Script Date: 21/11/2018 05:07:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patente](
	[id_patente] [int] IDENTITY(1,1) NOT NULL,
	[desc_patente] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Patente] PRIMARY KEY CLUSTERED 
(
	[id_patente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patente_Familia]    Script Date: 21/11/2018 05:07:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patente_Familia](
	[id_patente] [int] NOT NULL,
	[id_familia] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Documento]    Script Date: 21/11/2018 05:07:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Documento](
	[id_tipo_documento] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tipo_Documento] PRIMARY KEY CLUSTERED 
(
	[id_tipo_documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 21/11/2018 05:07:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](100) NOT NULL,
	[contraseña] [varchar](100) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[mail] [varchar](50) NOT NULL,
	[documento] [int] NOT NULL,
	[direccion] [varchar](50) NOT NULL,
	[telefono] [int] NULL,
	[id_estado] [int] NOT NULL,
	[digito_verificador] [varchar](100) NOT NULL,
	[intentos_fallidos] [int] NOT NULL,
 CONSTRAINT [PK_Usuario_2] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario_Familia]    Script Date: 21/11/2018 05:07:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario_Familia](
	[id_usuario] [int] NOT NULL,
	[id_familia] [int] NOT NULL,
 CONSTRAINT [PK_Usuario_Familia] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC,
	[id_familia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario_Patente]    Script Date: 21/11/2018 05:07:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario_Patente](
	[id_usuario] [int] NOT NULL,
	[id_patente] [int] NOT NULL,
 CONSTRAINT [PK_Usuario_Patente] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC,
	[id_patente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario_Patente_Negada]    Script Date: 21/11/2018 05:07:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario_Patente_Negada](
	[id_usuario] [int] NOT NULL,
	[id_patente] [int] NOT NULL,
 CONSTRAINT [PK_Usuario_Patente_Negada] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC,
	[id_patente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Estado] ADD  CONSTRAINT [DF_Estado_id_criticidad]  DEFAULT ((1)) FOR [id_criticidad]
GO
ALTER TABLE [dbo].[Evento] ADD  CONSTRAINT [DF_Evento_id_criticidad]  DEFAULT ((1)) FOR [id_criticidad]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_intentos_fallidos]  DEFAULT ((0)) FOR [intentos_fallidos]
GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_Evento] FOREIGN KEY([id_evento])
REFERENCES [dbo].[Evento] ([id_evento])
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_Evento]
GO
ALTER TABLE [dbo].[Etiqueta]  WITH CHECK ADD  CONSTRAINT [FK_Etiqueta_Idioma] FOREIGN KEY([id_idioma])
REFERENCES [dbo].[Idioma] ([id_idioma])
GO
ALTER TABLE [dbo].[Etiqueta] CHECK CONSTRAINT [FK_Etiqueta_Idioma]
GO
ALTER TABLE [dbo].[Etiqueta]  WITH CHECK ADD  CONSTRAINT [FK_Etiqueta_menu] FOREIGN KEY([id_menu])
REFERENCES [dbo].[menu] ([id_menu])
GO
ALTER TABLE [dbo].[Etiqueta] CHECK CONSTRAINT [FK_Etiqueta_menu]
GO
ALTER TABLE [dbo].[Patente_Familia]  WITH CHECK ADD  CONSTRAINT [FK_Patente_Familia_Familia] FOREIGN KEY([id_familia])
REFERENCES [dbo].[Familia] ([id_familia])
GO
ALTER TABLE [dbo].[Patente_Familia] CHECK CONSTRAINT [FK_Patente_Familia_Familia]
GO
ALTER TABLE [dbo].[Patente_Familia]  WITH CHECK ADD  CONSTRAINT [FK_Patente_Familia_Patente] FOREIGN KEY([id_patente])
REFERENCES [dbo].[Patente] ([id_patente])
GO
ALTER TABLE [dbo].[Patente_Familia] CHECK CONSTRAINT [FK_Patente_Familia_Patente]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario2_Estado] FOREIGN KEY([id_estado])
REFERENCES [dbo].[Estado] ([id_estado])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario2_Estado]
GO
ALTER TABLE [dbo].[Usuario_Familia]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Familia_Familia] FOREIGN KEY([id_familia])
REFERENCES [dbo].[Familia] ([id_familia])
GO
ALTER TABLE [dbo].[Usuario_Familia] CHECK CONSTRAINT [FK_Usuario_Familia_Familia]
GO
ALTER TABLE [dbo].[Usuario_Familia]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Familia_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Usuario_Familia] CHECK CONSTRAINT [FK_Usuario_Familia_Usuario]
GO
ALTER TABLE [dbo].[Usuario_Patente]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Patente_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Usuario_Patente] CHECK CONSTRAINT [FK_Usuario_Patente_Usuario]
GO
ALTER TABLE [dbo].[Usuario_Patente_Negada]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Patente_Negada_Patente] FOREIGN KEY([id_patente])
REFERENCES [dbo].[Patente] ([id_patente])
GO
ALTER TABLE [dbo].[Usuario_Patente_Negada] CHECK CONSTRAINT [FK_Usuario_Patente_Negada_Patente]
GO
ALTER TABLE [dbo].[Usuario_Patente_Negada]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Patente_Negada_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Usuario_Patente_Negada] CHECK CONSTRAINT [FK_Usuario_Patente_Negada_Usuario]
GO
