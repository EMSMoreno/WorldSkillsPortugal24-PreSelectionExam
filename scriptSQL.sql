USE [WorldSkillsPreSelection]
GO
/****** Object:  Table [dbo].[Cinema]    Script Date: 11/09/2024 10:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cinema](
	[id_cinema] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](100) NOT NULL,
	[id_local] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cinema] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Filme]    Script Date: 11/09/2024 10:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Filme](
	[codigo_filme] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](255) NOT NULL,
	[ano] [date] NOT NULL,
	[descricao] [text] NOT NULL,
	[id_tipo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo_filme] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Local]    Script Date: 11/09/2024 10:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Local](
	[id_local] [int] NOT NULL,
	[descricao] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_local] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sala]    Script Date: 11/09/2024 10:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sala](
	[codigo_sala] [int] NOT NULL,
	[descricao] [varchar](255) NULL,
	[id_cinema] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo_sala] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessao]    Script Date: 11/09/2024 10:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessao](
	[id_sessao] [int] NOT NULL,
	[codigo_sala] [int] NULL,
	[codigo_filme] [int] NULL,
	[data] [date] NULL,
	[hora] [time](7) NULL,
	[ativa] [bit] NULL,
	[id_cinema] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_sessao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoFilme]    Script Date: 11/09/2024 10:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoFilme](
	[id_tipo] [int] IDENTITY(1,1) NOT NULL,
	[descricao] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPreferences]    Script Date: 11/09/2024 10:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPreferences](
	[UserID] [int] NOT NULL,
	[LastSelectedSala] [int] NULL,
	[LastSelectedCinema] [int] NULL,
	[LastSelectedSessao] [int] NULL,
	[LastSelectedFilme] [int] NULL,
	[LastSelectedTipoFilme] [int] NULL,
	[LastSelectedLocal] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/09/2024 10:20:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Pass] [varchar](64) NULL,
	[Salt] [varchar](24) NULL,
	[Role] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cinema]  WITH CHECK ADD FOREIGN KEY([id_local])
REFERENCES [dbo].[Local] ([id_local])
GO
ALTER TABLE [dbo].[Filme]  WITH CHECK ADD  CONSTRAINT [FK_Filme_TipoFilme] FOREIGN KEY([id_tipo])
REFERENCES [dbo].[TipoFilme] ([id_tipo])
GO
ALTER TABLE [dbo].[Filme] CHECK CONSTRAINT [FK_Filme_TipoFilme]
GO
ALTER TABLE [dbo].[Sala]  WITH CHECK ADD  CONSTRAINT [FK_Sala_Cinema] FOREIGN KEY([id_cinema])
REFERENCES [dbo].[Cinema] ([id_cinema])
GO
ALTER TABLE [dbo].[Sala] CHECK CONSTRAINT [FK_Sala_Cinema]
GO
ALTER TABLE [dbo].[Sessao]  WITH CHECK ADD FOREIGN KEY([codigo_filme])
REFERENCES [dbo].[Filme] ([codigo_filme])
GO
ALTER TABLE [dbo].[Sessao]  WITH CHECK ADD  CONSTRAINT [FK_Sessao_Sala] FOREIGN KEY([codigo_sala])
REFERENCES [dbo].[Sala] ([codigo_sala])
GO
ALTER TABLE [dbo].[Sessao] CHECK CONSTRAINT [FK_Sessao_Sala]
GO
