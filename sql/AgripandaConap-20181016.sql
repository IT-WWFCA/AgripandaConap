USE [AgripandaConap]
GO
/****** Object:  Table [dbo].[carto_visualizations]    Script Date: 10/16/2018 1:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[carto_visualizations](
	[VizID] [int] IDENTITY(1,1) NOT NULL,
	[VizCode] [nvarchar](max) NOT NULL,
	[VizLat] [float] NOT NULL,
	[VizLong] [float] NOT NULL,
	[VizZoom] [smallint] NOT NULL,
	[VizTitle] [nvarchar](150) NULL,
 CONSTRAINT [PK_maps] PRIMARY KEY CLUSTERED 
(
	[VizID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[crops]    Script Date: 10/16/2018 1:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[crops](
	[CropID] [int] IDENTITY(1,1) NOT NULL,
	[CropName] [nvarchar](250) NOT NULL,
	[CropScientificName] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_crops] PRIMARY KEY CLUSTERED 
(
	[CropID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[global_settings]    Script Date: 10/16/2018 1:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[global_settings](
	[GSID] [int] IDENTITY(1,1) NOT NULL,
	[GSName] [nvarchar](150) NOT NULL,
	[GSValue] [nvarchar](180) NOT NULL,
	[GSEdit] [tinyint] NULL,
	[GSTitle] [nvarchar](150) NULL,
	[GSDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_global_settings] PRIMARY KEY CLUSTERED 
(
	[GSID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groups]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groups](
	[GroupID] [smallint] IDENTITY(1,1) NOT NULL,
	[GroupAlias] [nvarchar](50) NULL,
	[GroupName] [nvarchar](255) NOT NULL,
	[GroupPerms] [nvarchar](100) NULL,
	[GroupCrops] [nvarchar](50) NULL,
	[GroupEdit] [tinyint] NOT NULL,
	[GroupAdmin] [tinyint] NOT NULL,
	[GroupMods] [nvarchar](150) NULL,
 CONSTRAINT [PK_user_type] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[logs]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[logs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DateStamp] [datetime] NOT NULL,
	[Note] [ntext] NOT NULL,
	[Page] [nvarchar](24) NOT NULL,
 CONSTRAINT [PK_logs_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[menus]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[menus](
	[MenuID] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](150) NOT NULL,
	[MenuCode] [nvarchar](16) NOT NULL,
	[MenuParent] [tinyint] NOT NULL,
	[MenuLink] [nvarchar](250) NULL,
	[MenuIcon] [nvarchar](64) NULL,
 CONSTRAINT [PK_menus] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mods]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mods](
	[ModID] [int] IDENTITY(1,1) NOT NULL,
	[ModName] [nvarchar](120) NOT NULL,
	[ModCode] [nvarchar](10) NOT NULL,
	[ModPos] [tinyint] NULL,
 CONSTRAINT [PK_mods] PRIMARY KEY CLUSTERED 
(
	[ModID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mods_toolbar_items]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mods_toolbar_items](
	[TBItemID] [int] IDENTITY(1,1) NOT NULL,
	[TBItemNameEN] [nvarchar](150) NOT NULL,
	[TBItemNameES] [nvarchar](150) NOT NULL,
	[TBItemCode] [nvarchar](32) NOT NULL,
	[TBItemIcon] [nvarchar](50) NOT NULL,
	[TBItemIconDis] [nvarchar](50) NULL,
	[TBItemLink] [nvarchar](max) NULL,
	[TBItemLinkType] [tinyint] NOT NULL,
	[TBItemOrder] [smallint] NOT NULL,
	[isButtonWSep] [tinyint] NOT NULL,
	[isEnabled] [tinyint] NOT NULL,
	[ItemCode] [nvarchar](32) NOT NULL,
	[AdminUsers] [nvarchar](50) NULL,
 CONSTRAINT [PK_mods_toolbar_items] PRIMARY KEY CLUSTERED 
(
	[TBItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mods_tree_items]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mods_tree_items](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](120) NOT NULL,
	[ItemCode] [nvarchar](32) NOT NULL,
	[ModID] [int] NOT NULL,
	[Ima] [nvarchar](50) NULL,
	[Imb] [nvarchar](50) NULL,
	[Imc] [nvarchar](50) NULL,
	[ItemParent] [int] NOT NULL,
	[isOpen] [tinyint] NOT NULL,
	[ItemPosition] [tinyint] NOT NULL,
	[isLink] [tinyint] NOT NULL,
	[ItemToolbar] [nvarchar](max) NULL,
	[GroupID] [int] NULL,
	[GroupIDs] [nvarchar](max) NULL,
	[isLocked] [tinyint] NULL,
	[ItemURL] [nvarchar](max) NULL,
	[ItemType] [tinyint] NOT NULL,
	[ItemPostURL] [nvarchar](max) NULL,
 CONSTRAINT [PK_mods_children] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[posts]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[posts](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[PostTitle] [nvarchar](150) NOT NULL,
	[PostCreationDate] [datetime] NOT NULL,
	[PostStartDate] [date] NULL,
	[PostEndDate] [date] NULL,
	[PostType] [nvarchar](10) NULL,
	[PostURL] [nvarchar](max) NULL,
	[PostText] [ntext] NULL,
	[UserID] [int] NOT NULL,
	[ModID] [int] NOT NULL,
	[CatID] [int] NOT NULL,
 CONSTRAINT [PK_posts] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[posts_categories]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[posts_categories](
	[CatID] [int] IDENTITY(1,1) NOT NULL,
	[CatTitle] [nvarchar](150) NOT NULL,
	[CatDescription] [nvarchar](250) NULL,
	[UserID] [int] NOT NULL,
	[ModID] [int] NOT NULL,
 CONSTRAINT [PK_posts_categories] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[scheduler]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[scheduler](
	[SchedulerID] [int] IDENTITY(1,1) NOT NULL,
	[SchedulerName] [nvarchar](120) NOT NULL,
	[ModID] [int] NOT NULL,
	[GroupIDs] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_scheduler] PRIMARY KEY CLUSTERED 
(
	[SchedulerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[scheduler_events]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[scheduler_events](
	[EventID] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[UserID] [int] NOT NULL,
	[SchedulerID] [int] NOT NULL,
 CONSTRAINT [PK_scheduler_events] PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[set_colecta_siembra_log]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[set_colecta_siembra_log](
	[CSID] [int] IDENTITY(1,1) NOT NULL,
	[COLID] [int] NOT NULL,
	[CSAreaPlaya] [nvarchar](50) NOT NULL,
	[CSFechaHoraColecta] [datetime] NOT NULL,
	[CSCuotaConservacion] [int] NULL,
	[CSDonacionNido] [int] NULL,
	[CSCompra] [int] NULL,
	[CSPatrulla] [nvarchar](50) NULL,
	[CSNumeroNido] [int] NOT NULL,
	[CSHuevosSembrados] [int] NOT NULL,
	[CSFechaHoraSiembra] [datetime] NOT NULL,
	[CSFecha] [date] NOT NULL,
	[TORID] [int] NOT NULL,
 CONSTRAINT [PK_set_colecta_siembra_log] PRIMARY KEY CLUSTERED 
(
	[CSID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[set_colectores]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[set_colectores](
	[COLID] [int] IDENTITY(1,1) NOT NULL,
	[COLNombreColector] [nvarchar](250) NOT NULL,
	[COLNoCarnet] [nchar](10) NULL,
 CONSTRAINT [PK_set_colectores] PRIMARY KEY CLUSTERED 
(
	[COLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[set_comercializacion_log]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[set_comercializacion_log](
	[CLID] [int] IDENTITY(1,1) NOT NULL,
	[COMID] [int] NOT NULL,
	[COLID] [int] NOT NULL,
	[CLAreaPlaya] [nvarchar](50) NOT NULL,
	[CLFechaHoraColecta] [datetime] NOT NULL,
	[CLHuevos] [int] NOT NULL,
	[CLNoBoleta] [nvarchar](150) NOT NULL,
	[CLFecha] [date] NOT NULL,
	[TORID] [int] NOT NULL,
 CONSTRAINT [PK_set_comercializacion_log] PRIMARY KEY CLUSTERED 
(
	[CLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[set_compra_conservacion_log]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[set_compra_conservacion_log](
	[CCID] [int] IDENTITY(1,1) NOT NULL,
	[COLID] [int] NOT NULL,
	[CCAreaPlaya] [nvarchar](50) NOT NULL,
	[CCFechaHoraColecta] [datetime] NOT NULL,
	[CCCuota] [int] NOT NULL,
	[CCCompra] [nvarchar](50) NOT NULL,
	[CCPrecioDocena] [decimal](18, 2) NOT NULL,
	[CCTotal] [decimal](18, 2) NOT NULL,
	[CCNumeroNido] [int] NOT NULL,
	[CCHuevosSembrados] [int] NOT NULL,
	[CCFechaHora] [datetime] NOT NULL,
	[CCFecha] [date] NOT NULL,
	[TORID] [int] NOT NULL,
 CONSTRAINT [PK_set_compra_conservacion_log] PRIMARY KEY CLUSTERED 
(
	[CCID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[set_compradores]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[set_compradores](
	[COMID] [int] IDENTITY(1,1) NOT NULL,
	[COMNombre] [nvarchar](250) NOT NULL,
	[COMNoCarnet] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_set_compradores] PRIMARY KEY CLUSTERED 
(
	[COMID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[set_eclosion_liberacion_log]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[set_eclosion_liberacion_log](
	[ELID] [int] IDENTITY(1,1) NOT NULL,
	[ELNidoNumero] [int] NOT NULL,
	[ELHuevosSembrados] [int] NOT NULL,
	[ELSuperficieVivos] [int] NOT NULL,
	[ELSuperficieMuertos] [int] NOT NULL,
	[ELFechaRevision] [datetime] NOT NULL,
	[ELExSinEmergerVivos] [int] NOT NULL,
	[ELExSinEmergerMuertos] [int] NOT NULL,
	[ELExEnHuevo] [int] NOT NULL,
	[ELExInfertil] [int] NOT NULL,
	[ELExNoDesarrollado] [int] NOT NULL,
	[ELExDepredado] [int] NOT NULL,
	[ELExCascaras] [int] NOT NULL,
	[ELFechaHoraLiberacion] [datetime] NOT NULL,
	[ELNeonatosLiberados] [int] NOT NULL,
	[ELFecha] [date] NOT NULL,
	[TORID] [int] NOT NULL,
 CONSTRAINT [PK_set_eclosion_liberacion_log] PRIMARY KEY CLUSTERED 
(
	[ELID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[set_tortugarios]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[set_tortugarios](
	[TORID] [int] IDENTITY(1,1) NOT NULL,
	[TORNombre] [nvarchar](250) NOT NULL,
	[TORNombreResponsable] [nvarchar](250) NOT NULL,
	[TORDepartamento] [nvarchar](250) NOT NULL,
	[TORMunicipio] [nvarchar](250) NOT NULL,
	[TORAldea] [nvarchar](250) NOT NULL,
	[TORLat] [float] NULL,
	[TORLong] [float] NULL,
 CONSTRAINT [PK_set_tortugarios] PRIMARY KEY CLUSTERED 
(
	[TORID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[system_vars]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[system_vars](
	[SVID] [int] IDENTITY(1,1) NOT NULL,
	[SVKEY] [nvarchar](50) NOT NULL,
	[SVDESC] [nvarchar](255) NULL,
	[SVContent] [nvarchar](120) NULL,
	[SVEditable] [tinyint] NOT NULL,
 CONSTRAINT [PK_system_vars] PRIMARY KEY CLUSTERED 
(
	[SVID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[trees]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[trees](
	[TreeID] [int] IDENTITY(1,1) NOT NULL,
	[TreeName] [nvarchar](250) NOT NULL,
	[TreeCode] [nvarchar](16) NULL,
 CONSTRAINT [PK_trees] PRIMARY KEY CLUSTERED 
(
	[TreeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[trees_items]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[trees_items](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](250) NOT NULL,
	[TextID] [nvarchar](150) NOT NULL,
	[Ima] [nvarchar](150) NOT NULL,
	[Imb] [nvarchar](150) NOT NULL,
	[Imc] [nvarchar](150) NOT NULL,
	[ItemParent] [int] NOT NULL,
	[ItemOpen] [tinyint] NOT NULL,
	[ItemOrder] [smallint] NULL,
	[isLink] [tinyint] NOT NULL,
	[TreeID] [int] NOT NULL,
	[ItemToolbar] [nvarchar](max) NULL,
 CONSTRAINT [PK_trees_items] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[uploads]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[uploads](
	[UFID] [int] IDENTITY(1,1) NOT NULL,
	[UFName] [nvarchar](150) NOT NULL,
	[UFCreationDate] [datetime] NULL,
	[UFMimeType] [nvarchar](50) NULL,
	[UFExtension] [nvarchar](50) NULL,
	[UFSize] [nvarchar](50) NULL,
	[UserID] [int] NOT NULL,
	[PostID] [int] NOT NULL,
 CONSTRAINT [PK_uploads] PRIMARY KEY CLUSTERED 
(
	[UFID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[GroupID] [smallint] NOT NULL,
	[UserName] [nvarchar](64) NOT NULL,
	[UserPassWD] [nvarchar](250) NOT NULL,
	[UserPassSalt] [nvarchar](32) NULL,
	[UserFirstName] [nvarchar](120) NULL,
	[UserLastName] [nvarchar](120) NULL,
	[UserEmail] [nvarchar](60) NULL,
	[UserTitle] [nvarchar](250) NULL,
	[UserWorkArea] [nvarchar](250) NULL,
	[UserOrganization] [nvarchar](250) NULL,
	[UserWorkPhone] [nvarchar](50) NULL,
	[UserCellular] [nvarchar](50) NULL,
	[UserCreationDate] [datetime] NULL,
	[UserCanAdd] [tinyint] NOT NULL,
	[UserCanEdit] [tinyint] NOT NULL,
	[UserCanDelete] [tinyint] NOT NULL,
	[UserSuperAdmin] [tinyint] NOT NULL,
	[UserLanguage] [nvarchar](8) NULL,
	[UserSkin] [nvarchar](16) NULL,
	[UserLastLogin] [datetime] NULL,
	[UserOldGroupID] [smallint] NOT NULL,
 CONSTRAINT [PK_users_1] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users_addupi_sessions]    Script Date: 10/16/2018 1:40:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_addupi_sessions](
	[SessID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[SessionID] [nvarchar](50) NOT NULL,
	[SessionDate] [datetime] NULL,
 CONSTRAINT [PK_users_addupi_sessions] PRIMARY KEY CLUSTERED 
(
	[SessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[carto_visualizations] ON 

INSERT [dbo].[carto_visualizations] ([VizID], [VizCode], [VizLat], [VizLong], [VizZoom], [VizTitle]) VALUES (2, N'd0264da6-f7fa-11e4-b9e8-0e49835281d6', 14.895974, -91.509979, 11, N'Altiplano Guatemala')
INSERT [dbo].[carto_visualizations] ([VizID], [VizCode], [VizLat], [VizLong], [VizZoom], [VizTitle]) VALUES (3, N'1fb59c68-c501-11e5-a711-0e31c9be1b51', 14.89597783, -91.50997229, 11, N'Cuenca Samalá')
INSERT [dbo].[carto_visualizations] ([VizID], [VizCode], [VizLat], [VizLong], [VizZoom], [VizTitle]) VALUES (4, N'7f2ab5a3-1608-41ec-863c-6fa1313b7a0c', 15.203808, -89.499303, 8, N'Cuenca Pasabien')
SET IDENTITY_INSERT [dbo].[carto_visualizations] OFF
SET IDENTITY_INSERT [dbo].[crops] ON 

INSERT [dbo].[crops] ([CropID], [CropName], [CropScientificName]) VALUES (1, N'Melon', N'Cucumis Melo')
INSERT [dbo].[crops] ([CropID], [CropName], [CropScientificName]) VALUES (2, N'Banana', N'Musa Acuminata')
INSERT [dbo].[crops] ([CropID], [CropName], [CropScientificName]) VALUES (3, N'Sugarcane', N'Saccharum')
INSERT [dbo].[crops] ([CropID], [CropName], [CropScientificName]) VALUES (4, N'Oil Palm', N'Elaeis Oleifera')
INSERT [dbo].[crops] ([CropID], [CropName], [CropScientificName]) VALUES (5, N'Corn', N'Zea mays')
INSERT [dbo].[crops] ([CropID], [CropName], [CropScientificName]) VALUES (6, N'Coffee', N'Coffea')
INSERT [dbo].[crops] ([CropID], [CropName], [CropScientificName]) VALUES (7, N'Cacao', N'Theobroma cacao')
INSERT [dbo].[crops] ([CropID], [CropName], [CropScientificName]) VALUES (8, N'Water', N'Water')
INSERT [dbo].[crops] ([CropID], [CropName], [CropScientificName]) VALUES (9, N'Vegetables', N'Kale')
SET IDENTITY_INSERT [dbo].[crops] OFF
SET IDENTITY_INSERT [dbo].[global_settings] ON 

INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (1, N'MainURL', N'http://www.agripanda.com', 1, N'Main URL', N'Main URL for the application.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (2, N'AddUPIServer', N'http://www.wwf-mar.org:8080/', 1, N'AddUPI URL', N'AddUPI server URL, include the port.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (3, N'AddUPIUser', N'addupi', 1, N'AddUPI User', N'User to connect to addUPI server.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (4, N'AddUPIPasswd', N'wwf2016', 1, N'AddUPI Password', N'Password of user to connect to addUPI server.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (5, N'AddUPIisActive', N'false', 1, N'Activate addUPI', N'Do you want to activate addUPI Login.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (6, N'oAuthMail', N'jlara@wwfca.org', 1, N'oAuth Email', N'Email for oAuth connection to Carto.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (7, N'oAuthPasswd', N'awedxza2!', 1, N'oAuth Password', N'Password for oAuth connection to Carto.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (8, N'oAuthConsumerKey', N'R8hZ8M56vGEk94CsPRD1bCkSi43G4718XRKfNecJ', 1, N'oAuth Consumer Key', N'Consumer key for oAuth connection to Carto.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (9, N'oAuthConsumerSecret', N'gNsKgbsqWRow5FNzwnyEIAC4d0KW29g9E97ut3TR', 1, N'oAuth Condumer Secret', N'Consumer secret for connection to Carto')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (10, N'oAuthDomain', N'wwfca', 1, N'oAuth Domain', N'The Domain used in Carto.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (11, N'SMTPFromEmail', N'it.wwfca@gmail.com', 1, N'SMTP Email', N'Email used to send emails to users.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (12, N'SMTPFromName', N'IT Manager WWF', 1, N'SMTP Name', N'The displayed name of the email.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (13, N'SMTPFromPasswd', N'rhljuve9!', 1, N'SMTP Password', N'Password of the email.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (14, N'SMTPHost', N'smtp.gmail.com', 1, N'SMTP Host', N'Host of the SMTP server.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (15, N'SMTPPort', N'587', 1, N'SMTP Port', N'Port of the SMTP server.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (16, N'MainTitle', N'Plataforma Geoespacial Agripanda', 1, N'Main System Title Name', N'Main title of the platform.')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (17, N'UserReg', N'false', 1, N'User Registration', N'Enable/disable new user registrations')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (18, N'SendAdminEmails', N'false', 1, N'Activate Admin Emails', N'Enable/disable the admin emails')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (19, N'CartoLeaflet', N'false', 1, N'Leaflet', N'Enable/disable leaflet on carto app')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (20, N'UserPendingGroup', N'2', 1, N'User Pending Group', N'Group ID for user pending for activation')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (21, N'UserBaseGroup', N'5', 1, N'User Base Group', N'Group ID for users when are activated')
INSERT [dbo].[global_settings] ([GSID], [GSName], [GSValue], [GSEdit], [GSTitle], [GSDescription]) VALUES (22, N'UserBlockedGroup', N'3', 1, N'User Blocked Group', N'Group ID for blocked users')
SET IDENTITY_INSERT [dbo].[global_settings] OFF
SET IDENTITY_INSERT [dbo].[groups] ON 

INSERT [dbo].[groups] ([GroupID], [GroupAlias], [GroupName], [GroupPerms], [GroupCrops], [GroupEdit], [GroupAdmin], [GroupMods]) VALUES (1, N'admin', N'Super Administrators', N'A1:A2:A3:A4:A5:A6:B1:B2:B3:B4:B5:B6:B7:B8:C1:C2:C3:D1:D2:D3:A7:A8:B51:B52', N'1:2:3:4', 0, 1, N'S[MAP:DBS:MET:IPM:EWA:BFO:PRM:GHR:SDO:SET:ADM]')
INSERT [dbo].[groups] ([GroupID], [GroupAlias], [GroupName], [GroupPerms], [GroupCrops], [GroupEdit], [GroupAdmin], [GroupMods]) VALUES (2, N'reguser', N'Pending Aproval Users', NULL, N'0', 0, 0, NULL)
INSERT [dbo].[groups] ([GroupID], [GroupAlias], [GroupName], [GroupPerms], [GroupCrops], [GroupEdit], [GroupAdmin], [GroupMods]) VALUES (3, N'blocked', N'Blocked User', NULL, N'0', 0, 0, NULL)
INSERT [dbo].[groups] ([GroupID], [GroupAlias], [GroupName], [GroupPerms], [GroupCrops], [GroupEdit], [GroupAdmin], [GroupMods]) VALUES (10, N'conap', N'CONAP', N'A1', N'8', 1, 0, N'S[MAP:SET]')
SET IDENTITY_INSERT [dbo].[groups] OFF
SET IDENTITY_INSERT [dbo].[menus] ON 

INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (1, N'Home', N'H', 0, N'Default.aspx', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (2, N'Manager', N'A', 0, N'#', N'icon-manager.png')
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (5, N'Farming', N'B', 0, N'#', N'icon-farming.png')
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (6, N'Reports', N'C', 0, N'#', N'icon-reports.png')
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (7, N'Research and Development', N'D', 0, N'#', N'icon-resdev.png')
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (8, N'Users', N'A1', 2, N'Manager/users.aspx', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (9, N'Permission Masks', N'A2', 2, N'Manager/pmasks.aspx', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (10, N'Locations', N'B1', 5, N'Farming/locations.aspx', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (11, N'Crops', N'A4', 2, N'Manager/crops.aspx', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (12, N'IPM', N'B5', 5, N'Farming/ipm.aspx', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (13, N'Weather', N'A5', 2, N'Manager/weather.aspx', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (14, N'EIQ Calculator', N'B5', 12, N'Farming/ipm.aspx?code=eiqcalc', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (15, N'Desease Models', N'B51', 12, N'Farming/models.aspx', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (16, N'GIS', N'A7', 2, N'Manager/gis.aspx', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (17, N'Contractors', N'A6', 2, N'Manager/contractors.aspx', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (18, N'System Variables', N'A8', 2, N'Manager/system.aspx', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (19, N'Organizations', N'A3', 2, N'#', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (20, N'Soil Management', N'B2', 5, N'#', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (21, N'Nutrition Management', N'B3', 5, N'#', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (22, N'Water Management', N'B4', 5, N'#', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (23, N'Harvest', N'B6', 5, N'Farming/harvest.aspx', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (24, N'Transportation', N'B7', 5, N'#', NULL)
INSERT [dbo].[menus] ([MenuID], [MenuName], [MenuCode], [MenuParent], [MenuLink], [MenuIcon]) VALUES (25, N'Varieties', N'B8', 5, N'Farming/varieties.aspx', NULL)
SET IDENTITY_INSERT [dbo].[menus] OFF
SET IDENTITY_INSERT [dbo].[mods] ON 

INSERT [dbo].[mods] ([ModID], [ModName], [ModCode], [ModPos]) VALUES (1, N'Databases', N'DBS', 1)
INSERT [dbo].[mods] ([ModID], [ModName], [ModCode], [ModPos]) VALUES (2, N'Maps', N'MAP', 2)
INSERT [dbo].[mods] ([ModID], [ModName], [ModCode], [ModPos]) VALUES (3, N'Weather Data', N'MET', 3)
INSERT [dbo].[mods] ([ModID], [ModName], [ModCode], [ModPos]) VALUES (4, N'Integrated Pest Management', N'IPM', 4)
INSERT [dbo].[mods] ([ModID], [ModName], [ModCode], [ModPos]) VALUES (5, N'Proyect Management', N'PRM', 7)
INSERT [dbo].[mods] ([ModID], [ModName], [ModCode], [ModPos]) VALUES (6, N'Early Warnings', N'EWA', 5)
INSERT [dbo].[mods] ([ModID], [ModName], [ModCode], [ModPos]) VALUES (7, N'Graphs and Reports', N'GHR', 8)
INSERT [dbo].[mods] ([ModID], [ModName], [ModCode], [ModPos]) VALUES (8, N'Administrator', N'ADM', 11)
INSERT [dbo].[mods] ([ModID], [ModName], [ModCode], [ModPos]) VALUES (9, N'Support Documents', N'SDO', 9)
INSERT [dbo].[mods] ([ModID], [ModName], [ModCode], [ModPos]) VALUES (10, N'Bulletins and Forecasts', N'BFO', 6)
INSERT [dbo].[mods] ([ModID], [ModName], [ModCode], [ModPos]) VALUES (11, N'Turtle Nest Statistic System', N'SET', 10)
SET IDENTITY_INSERT [dbo].[mods] OFF
SET IDENTITY_INSERT [dbo].[mods_toolbar_items] ON 

INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (1, N'Add New User', N'Agregue Nuevo Usuario', N'adduser', N'adduser.gif', N'adduser_dis.gif', N'./admin/users.aspx?code=adduser', 1, 2, 1, 1, N'users', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (2, N'View User List', N'Listado de Usuarios', N'userlist', N'userlist.gif', N'userlist_dis.gif', N'./admin/users.aspx', 1, 1, 0, 1, N'users', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (3, N'Two weeks', N'Dos semanas', N'twoweeks-ol', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=twoweeks&wsid=24', 1, 1, 0, 1, N'wsolin-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (4, N'30 days', N'Treinta días', N'month-ol', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=month&wsid=24', 1, 2, 0, 1, N'wsolin-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (5, N'90 days', N'Noventa días', N'90days-ol', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=90days&wsid=24', 1, 3, 0, 1, N'wsolin-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (6, N'Two weeks', N'Dos semanas', N'twoweeks-chi', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=twoweeks&wsid=27', 1, 1, 0, 1, N'wschi-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (7, N'Show all data', N'Todos los datos', N'showall-ol', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=showall&wsid=24', 1, 4, 1, 1, N'wsolin-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (8, N'30 days', N'Treinta días', N'month-chi', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=month&wsid=27', 1, 2, 0, 1, N'wschi-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (9, N'90 Days', N'Noventa días', N'90days-chi', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=90days&wsid=27', 1, 3, 0, 1, N'wschi-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (10, N'Two weeks', N'Dos semanas', N'twoweeks-apri', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=twoweeks&wsid=25', 1, 1, 0, 1, N'wsapri-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (11, N'30 Days', N'Treinta días', N'month-apri', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=month&wsid=25', 1, 2, 0, 1, N'wsapri-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (12, N'90 Days', N'Noventa días', N'90days-apri', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=90days&wsid=25', 1, 3, 0, 1, N'wsapri-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (13, N'Add Warning', N'Agregar Alerta', N'add-wl', N'addwl.gif', N'addwl.gif', N'./warnings/default.aspx?code=addwarning', 1, 3, 0, 0, N'wlogs', N'23')
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (14, N'Show All Warnings', N'Ver Todas las Alertas', N'viewall-wl', N'viewwl.gif', N'viewwl.gif', N'./warnings/default.aspx', 1, 2, 0, 1, N'wlogs', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (16, N'Show all data', N'Todos los datos', N'showall-chi', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=showall&wsid=27', 1, 4, 1, 1, N'wschi-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (17, N'Show all data', N'Todos los datos', N'showall-apri', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=showall&wsid=25', 1, 4, 1, 1, N'wsapri-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (18, N'Select period', N'Seleccione periodo', N'period-oli', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=period&wsid=27', 1, 5, 1, 0, N'wsolin-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (19, N'Locations', N'Ubicación', N'locguate-cs', N'wsdata.gif', N'wsdata.gif', N'https://wwfca.cartodb.com/viz/d0264da6-f7fa-11e4-b9e8-0e49835281d6/embed_map', 1, 1, 0, 1, N'guate-cs', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (20, N'Show early warnings', N'Mostrar Alertas', N'alertgt-cs', N'wsdata.gif', N'wsdata.gif', N'https://wwfca.cartodb.com/viz/1fb59c68-c501-11e5-a711-0e31c9be1b51/embed_map', 1, 2, 1, 1, N'guate-cs', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (21, N'View forecast list', N'Mostrar Listado de Pronósticos', N'forecast-list', N'wsdata.gif', N'wsdata.gif', N'./warnings/default.aspx?code=forecasts', 1, 1, 1, 1, N'forecasts', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (22, N'View Related Documents', N'Documentos Relacionados', N'reldocs-list', N'wsdata.gif', N'wsdata.gif', N'./warnings/default.aspx?code=reldocs&cid=1', 1, 1, 0, 1, N'reldocs', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (23, N'Insert New Document', N'Agregar Nuevo Documento', N'reldoc-add', N'wsdata.gif', N'wsdata.gif', N'./warnings/default.aspx?code=postreldocs', 1, 2, 1, 1, N'reldocs', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (24, N'Two weeks', N'Dose semanas', N'twoweeks-pas', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=twoweeks&wsid=31', 1, 1, 0, 1, N'wspas-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (25, N'30 days', N'Treinta días', N'month-pas', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=month&wsid=31', 1, 2, 0, 1, N'wspas-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (26, N'90 days', N'Noventa días', N'90days-pas', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=90days&wsid=31', 1, 3, 0, 1, N'wspas-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (27, N'Show all data', N'Todos los datos', N'viewall-pas', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=showall&wsid=31', 1, 4, 1, 1, N'wspas-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (28, N'Show Map', N'Mostrar Mapa', N'bgamap-sigatoka', N'map.gif', N'map_dis.gif', N'https://wwfca.cartodb.com/viz/f6c88288-1c6d-11e6-9524-0ecfd53eb7d3/embed_map', 1, 2, 1, 1, N'rundsv-bga', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (29, N'Show Map', N'Mostrar Mapa', N'banasa-sigatoka', N'wsdata.gif', N'wsdata.gif', N'https://wwfca.cartodb.com/viz/f8b8ff86-2e7a-11e6-98b5-0e3ff518bd15/embed_map', 1, 1, 1, 1, N'sig-banasamap', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (30, N'Last Day', N'Ultimo día', N'day-chiqui', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=lvldata&wsid=32&period=-1', 1, 1, 0, 1, N'wschiqui-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (31, N'Last Two Days', N'Ultimos dos días', N'twodays-chiqui', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=lvldata&wsid=32&period=-2', 1, 1, 0, 1, N'wschiqui-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (32, N'Last Week', N'Ultima semana', N'week-chiqui', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=lvldata&wsid=32&period=-7', 1, 1, 0, 1, N'wschiqui-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (33, N'Last Two Weeks', N'Ultimas dos semanas', N'twoweeks-chiqui', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=lvldata&wsid=32&period=-15', 1, 1, 0, 1, N'wschiqui-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (35, N'Last Month', N'Ultimo mes', N'month-chiqui', N'wsdata.gif', N'wsdata.gif', N'./weather/default.aspx?code=lvldata&wsid=32&period=-30', 1, 1, 1, 1, N'wschiqui-ver', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (37, N'Show calendar', N'Ver Calendario', N'cal-wl', N'wsdata.gif', N'wsdata.gif', N'./handlers/scheduler.ashx?calid=2', 2, 4, 1, 1, N'wlogs', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (38, N'Active Warnings', N'Alertas Activas', N'active-wl', N'wsdata.gif', N'wsdata.gif', N'./warnings/default.aspx?code=activewarning', 1, 1, 0, 1, N'wlogs', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (39, N'Show table', N'Mostrar tabla', N'bgalist-sigatoka', N'table.gif', N'table_dis.gif', N'./ipm/sigatoka.aspx', 1, 1, 0, 1, N'rundsv-bga', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (40, N'Insert New Datasheet', N'Agregar Ficha Técnica', N'datasheet-add', N'wsdata.gif', N'wsdata.gif', N'./ipm/sigatoka.aspx?code=addpost', 1, 2, 0, 1, N'datasheets', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (41, N'Show Categories', N'Mostrar Categorías', N'show-catsmds', N'wsdata.gif', N'wsdata.gif', N'./ipm/sigatoka.aspx?code=category', 1, 1, 0, 1, N'datasheets', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (42, N'View Group List', N'Ver Listado de Grupos', N'grouplist', N'wsdata.gif', N'wsdata.gif', N'./admin/groups.aspx', 1, 1, 0, 1, N'groups', NULL)
INSERT [dbo].[mods_toolbar_items] ([TBItemID], [TBItemNameEN], [TBItemNameES], [TBItemCode], [TBItemIcon], [TBItemIconDis], [TBItemLink], [TBItemLinkType], [TBItemOrder], [isButtonWSep], [isEnabled], [ItemCode], [AdminUsers]) VALUES (43, N'Add New Group', N'Agregar un Nuevo Grupo', N'addgroup', N'wsdata.gif', N'wsdata.gif', N'./admin/groups.aspx?code=add', 1, 2, 1, 1, N'groups', NULL)
SET IDENTITY_INSERT [dbo].[mods_toolbar_items] OFF
SET IDENTITY_INSERT [dbo].[mods_tree_items] ON 

INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (1, N'Users / Groups', N'users-groups', 8, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 1, 0, NULL, 1, N'1', 1, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (2, N'User Administration', N'users', 8, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 1, 0, 2, 1, NULL, 1, N'1', 1, N'./admin/users.aspx', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (3, N'Group Administration', N'groups', 8, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 1, 0, 3, 1, NULL, 1, N'1', 1, N'./admin/groups.aspx', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (4, N'Settings', N'settings', 8, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 4, 1, NULL, 1, N'1', 1, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (5, N'ThirdParty Apps', N'3rd-party', 8, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 5, 0, NULL, 1, N'1', 1, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (6, N'CartoDB Server', N'cartodb', 8, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 5, 0, 6, 1, NULL, 1, N'1', 1, N'https://wwfca.cartodb.com', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (7, N'Fulcrum Server', N'fulcrum', 8, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 5, 0, 7, 1, NULL, 1, N'1', 1, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (8, N'Scheduler', N'scheduler', 5, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 1, 1, NULL, 1, N'1', 1, N'./handlers/scheduler.ashx?calid=1', 2, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (9, N'Sigatoka Model BGA', N'sigatoka', 4, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 1, 0, NULL, 1, N'4', 0, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (10, N'Matrices', N'matrices', 4, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 9, 0, 2, 1, NULL, 1, N'4', 0, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (11, N'Run DSV', N'rundsv-bga', 4, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 9, 0, 3, 1, NULL, 1, N'4', 0, N'./ipm/sigatoka.aspx', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (12, N'EE Values', N'eevalues', 4, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 9, 0, 4, 1, NULL, 1, N'1', 0, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (13, N'Spore Values', N'spores', 4, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 9, 0, 5, 1, NULL, 1, N'1', 0, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (14, N'Weather Stations CP', N'weather', 8, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 8, 1, NULL, 1, N'1', 1, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (15, N'IPM Calculator', N'ipmcalc', 4, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 6, 1, NULL, 1, N'1', 0, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (16, N'System Logs', N'logs', 8, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 9, 1, NULL, 1, N'1', 1, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (17, N'Trees CP', N'trees-admin', 8, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 10, 1, NULL, 1, N'1', 1, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (18, N'First Visualization', N'map-cahsa', 2, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 1, 1, NULL, 1, N'1', 0, N'./CartoDB/default.aspx?vid=1', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (19, N'Altiplano Guatemala', N'guate-cs', 2, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 2, 1, NULL, 5, N'5:7:8', 0, N'https://wwfca.cartodb.com/viz/d0264da6-f7fa-11e4-b9e8-0e49835281d6/embed_map', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (20, N'Estación Olintepeque', N'ws-olintepeque', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 1, 0, NULL, 5, N'5:8', 0, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (21, N'Datos Climáticos', N'wsolin-ver', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 20, 0, 2, 1, NULL, 5, N'5:8', 0, N'./weather/default.aspx?code=twoweeks&wsid=24', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (22, N'Livedata', N'wsolin-livedata', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 20, 0, 3, 1, NULL, 5, N'5:8', 0, N'http://www.wwf-mar.org:8080/livedata/collection.jsf?template=weather&node=5446', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (23, N'Estacion La Lima', N'ws-lima', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 4, 1, NULL, 1, N'1', 0, N'./weather/default.aspx?code=twoweeks&wsid=24', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (24, N'Estación Chiquirichapa', N'ws-chiquirichapa', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 5, 0, NULL, 5, N'5:8', 0, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (26, N'Datos Climáticos', N'wschi-ver', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 24, 0, 6, 1, NULL, 5, N'5:8', 0, N'./weather/default.aspx?code=twoweeks&wsid=27', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (27, N'Livedata', N'wschi-livedata', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 24, 0, 7, 1, NULL, 5, N'5:8', 0, N'http://www.wwf-mar.org:8080/livedata/collection.jsf?template=weather&node=5540', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (28, N'Estación El Aprisco', N'ws-elaprisco', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 8, 0, NULL, 5, N'5:8', 0, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (29, N'Datos Climáticos', N'wsapri-ver', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 28, 0, 9, 1, NULL, 5, N'5:8', 0, N'./weather/default.aspx?code=twoweeks&wsid=25', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (30, N'Livedata', N'wsapri-livedata', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 28, 0, 10, 1, NULL, 5, N'5:8', 0, N'http://www.wwf-mar.org:8080/livedata/collection.jsf?template=weather&node=5146', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (31, N'Estación Pasajoc', N'ws-pasajoc', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 11, 0, NULL, 5, N'5:8', 0, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (32, N'Datos Climáticos', N'wspas-ver', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 31, 0, 12, 1, NULL, 5, N'5:8', 0, N'./weather/default.aspx?code=twoweeks&wsid=31', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (33, N'Altiplano de Guatemala', N'wlogs', 6, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 1, 1, NULL, 5, N'5:7:8', 0, N'./warnings/default.aspx?code=activewarning', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (34, N'Pronósticos de lluvia diaria', N'forecasts', 10, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 3, 1, NULL, 5, N'5:8', 0, N'./warnings/default.aspx?code=forecasts', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (35, N'Documentos relacionados', N'reldocs', 10, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 2, 1, NULL, 5, N'5:7:8', 0, N'./warnings/default.aspx?code=reldocs&cid=1', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (36, N'Livedata', N'wspas-livedata', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 31, 0, 13, 1, NULL, 5, N'5:8', 0, N'http://www.wwf-mar.org:8080/livedata/collection.jsf?template=weather&node=5556', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (37, N'Modelo de Sigatoka', N'sig-banasa', 4, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 7, 0, NULL, 1, N'8', 0, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (38, N'Mapa de Estaciones', N'sig-banasamap', 4, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 37, 0, 8, 1, NULL, 1, N'8', 0, N'https://wwfca.cartodb.com/viz/f8b8ff86-2e7a-11e6-98b5-0e3ff518bd15/embed_map', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (39, N'Manual de Uso de la Plataforma Climática', N'tut-gpc', 9, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 1, 1, NULL, 1, N'5:7:8', 0, N'./docs/Manual_de_uso_de_plataforma_climatica.pdf', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (40, N'Manual de Usuario AddVANTAGE PRO 6', N'tut-apro', 9, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 2, 1, NULL, 1, N'5:7:8', 0, N'./docs/Manual_de_Usuario_AddVANTAGE6.pdf', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (41, N'Estación San José Chiquilaja', N'ws-chiquilaja', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 14, 0, NULL, 5, N'5:7:8', 0, NULL, 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (42, N'Datos de nivel del rio', N'wschiqui-ver', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 41, 0, 15, 1, NULL, 5, N'5:7:8', 0, N'./weather/default.aspx?code=lvldata&wsid=32&period=-1', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (43, N'Guía de mantenimiento de estaciones climáticas en el campo', N'tut-gm', 9, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 3, 1, NULL, 1, N'5:7:8', 0, N'./docs/Mantenimiento_de_Estaciones_v2.pdf', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (44, N'Formulario para mantenimiento de estaciones climáticas', N'tut-form', 9, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 4, 1, NULL, 1, N'5:7:8', 0, N'./docs/Formulario_de_Mantenimiento_v2.pdf', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (45, N'Leaflet', N'leaflet', 2, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 3, 1, NULL, 1, N'1', 0, N'./leaflet/default.aspx', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (46, N'Carto', N'carto', 2, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 4, 1, NULL, 1, N'1', 0, N'./carto/default.aspx', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (47, N'Estación Classic', N'ws-classic', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 100, 1, NULL, 9, N'9', 0, N'./weather/default.aspx?code=twoweeks&wsid=2', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (48, N'Estación Chinook', N'ws-chinook', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 101, 1, NULL, 9, N'9', 0, N'./weather/default.aspx?code=twoweeks&wsid=12', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (49, N'Estación Omagua', N'ws-omagua', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 102, 1, NULL, 9, N'9', 0, N'./weather/default.aspx?code=twoweeks&wsid=13', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (50, N'Estación El Aprisco', N'ws-cdro', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 103, 1, NULL, 9, N'9', 0, N'./weather/default.aspx?code=twoweeks&wsid=25', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (51, N'Estación El Pastal', N'ws-pastal', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 104, 1, NULL, 9, N'9', 0, N'./weather/default.aspx?code=twoweeks&wsid=14', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (52, N'Estación Copán', N'ws-copan', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 105, 1, NULL, 9, N'9', 0, N'./weather/default.aspx?code=twoweeks&wsid=17', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (53, N'Estación Santa Bárbara', N'ws-santabarbara', 3, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 106, 1, NULL, 9, N'9', 0, N'./weather/default.aspx?code=twoweeks&wsid=18', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (54, N'Cuenca Rio Motagua', N'cuenca-motagua', 2, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 5, 1, NULL, 9, N'9', 0, N'./carto/default.aspx?vid=4', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (55, N'Material Safety Datasheets', N'datasheets', 4, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 6, 1, NULL, 1, N'4', 0, N'./ipm/sigatoka.aspx?code=category', 1, NULL)
INSERT [dbo].[mods_tree_items] ([ItemID], [ItemName], [ItemCode], [ModID], [Ima], [Imb], [Imc], [ItemParent], [isOpen], [ItemPosition], [isLink], [ItemToolbar], [GroupID], [GroupIDs], [isLocked], [ItemURL], [ItemType], [ItemPostURL]) VALUES (56, N'Colecta y siembra', N'colectasiembra', 11, N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 1, 1, NULL, 1, N'1', 0, N'./set/default.aspx?code=addcs', 1, NULL)
SET IDENTITY_INSERT [dbo].[mods_tree_items] OFF
SET IDENTITY_INSERT [dbo].[posts_categories] ON 

INSERT [dbo].[posts_categories] ([CatID], [CatTitle], [CatDescription], [UserID], [ModID]) VALUES (1, N'Documentos relacionados', N'Documentos relacionados', 1, 6)
SET IDENTITY_INSERT [dbo].[posts_categories] OFF
SET IDENTITY_INSERT [dbo].[scheduler] ON 

INSERT [dbo].[scheduler] ([SchedulerID], [SchedulerName], [ModID], [GroupIDs]) VALUES (1, N'Calendario de pruebas', 5, N'1')
INSERT [dbo].[scheduler] ([SchedulerID], [SchedulerName], [ModID], [GroupIDs]) VALUES (2, N'Calendario de Alertas Guatemala', 6, N'5')
SET IDENTITY_INSERT [dbo].[scheduler] OFF
SET IDENTITY_INSERT [dbo].[scheduler_events] ON 

INSERT [dbo].[scheduler_events] ([EventID], [StartDate], [EndDate], [Text], [Details], [UserID], [SchedulerID]) VALUES (2, CAST(N'2016-05-25T10:00:00.000' AS DateTime), CAST(N'2016-05-25T12:00:00.000' AS DateTime), N'Evento de prueba', N'Este es el texto de un evento de prueba', 1, 1)
INSERT [dbo].[scheduler_events] ([EventID], [StartDate], [EndDate], [Text], [Details], [UserID], [SchedulerID]) VALUES (3, CAST(N'2016-06-01T08:00:00.000' AS DateTime), CAST(N'2016-06-03T18:00:00.000' AS DateTime), N'Evento varios dias', N'Prueba de evento de varios dias', 1, 1)
INSERT [dbo].[scheduler_events] ([EventID], [StartDate], [EndDate], [Text], [Details], [UserID], [SchedulerID]) VALUES (12390, CAST(N'2016-11-02T08:30:00.000' AS DateTime), CAST(N'2016-11-02T09:00:00.000' AS DateTime), N'Alerta de Helada', N'Prueba de alerta', 1, 2)
INSERT [dbo].[scheduler_events] ([EventID], [StartDate], [EndDate], [Text], [Details], [UserID], [SchedulerID]) VALUES (12391, CAST(N'2016-11-02T09:00:00.000' AS DateTime), CAST(N'2016-11-02T09:30:00.000' AS DateTime), N'Alerta de Lluvia', N'Otra prueba de alerta', 1, 2)
INSERT [dbo].[scheduler_events] ([EventID], [StartDate], [EndDate], [Text], [Details], [UserID], [SchedulerID]) VALUES (12392, CAST(N'2016-11-02T08:30:00.000' AS DateTime), CAST(N'2016-11-02T09:00:00.000' AS DateTime), N'Alerta de incendio', N'Otra prueba', 1, 1)
SET IDENTITY_INSERT [dbo].[scheduler_events] OFF
SET IDENTITY_INSERT [dbo].[set_tortugarios] ON 

INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (1, N'Ocós
', N'Amigos del Bosque CONAP
', N'San Marcos
', N'Ocós
', N'Puerto Ocós
', 14.5092222, -92.19616666666667)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (2, N'Tilapa
', N'Amigos del Bosque CONAP
', N'San Marcos
', N'Ocós
', N'Tilapa
', 14.4926111, -92.170833333333334)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (3, N'Tres Cruces
', N'CONAP
', N'Retalhuleu
', N'Champerico
', N'La Barrita
', 14.4494167, -92.110833333333332)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (4, N'El Chico
', N'CONAP
', N'Retalhuleu
', N'Champerico
', N'El Chico
', 14.4094722, -92.06049999999999)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (5, N'Mar Azul
', N'Hotel Mar Azul
', N'Retalhuleu
', N'Champerico
', N'Manchón Guamuchal
', 14.3811389, -92.0235)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (6, N'Casa Aak
', N'Restaurante Siete Mares
', N'Retalhuleu
', N'Champerico
', N'Puerto Champerico
', 14.2898611, -91.912500000000009)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (7, N'La Barona Tulate
', N'Comunitario
', N'Retalhuleu
', N'San Andrés
', N'Tulate
', 14.1623333, -91.722222222222229)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (8, N'Tulate Beach
', N'Hotel Tulate Beach
', N'Retalhuleu
', N'San Andrés
', N'Tulate
', 14.1608333, -91.719861111111115)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (9, N'Conservación
', N'Hotel Iguana
', N'Retalhuleu
', N'San Andrés
', N'Tulate 
', 14.1476944, -91.70494444444445)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (10, N'Churirin
', N'Muni. Mazatenango
', N'Suchitepéquez
', N'Mazatenango
', N'Chirirín
', 14.1197222, -91.662222222222226)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (11, N'Posada Don Pancho
', N'Hotel Posada Don Pancho
', N'Suchitepéquez
', N'Mazatenango
', N'Chiquistepeque
', 14.1145, -91.6538888888889)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (12, N'Sipacate
', N'CONAP
', N'Escuintla
', N'La Gomera
', N'Puerto Sipacate
', 13.9228333, -91.1516388888889)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (13, N'El Paredón
', N'CONAP
', N'Escuintla
', N'La Gomera
', N'El Paredón
', 13.9160278, -91.07525)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (14, N'Naranjo
', N'CONAP
', N'Escuintla
', N'La Gomera
', N'El Naranjo
', 13.9141111, -91.024111111111111)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (15, N'CONAPAC
', N'CONAPAC
', N'Escuintla
', N'Puerto San José
', N'San José
', 13.9178611, -90.801083333333324)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (16, N'Conacaste
', N'Voluntario
', N'Escuintla
', N'Iztapa
', N'El Conacaste
', 13.9273056, -90.66458333333334)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (17, N'El Garitón
', N'Colegio Austriaco
', N'Santa Rosa
', N'Taxisco
', N'El Garitón
', 13.9196389, -90.605722222222212)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (18, N'Madre Vieja
', N'Zoológico La Aurora
', N'Santa Rosa
', N'Taxisco
', N'Madre Vieja
', 13.9137222, -90.572777777777773)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (19, N'El Banco
', N'Municipalidad de Taxisco
', N'Santa Rosa
', N'Taxisco
', N'El Banco
', 13.9025833, -90.525)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (20, N'Monterrico
', N'CECON CONAP
', N'Santa Rosa
', N'Taxisco
', N'Monterrico
', 13.8884722, -90.478444444444449)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (21, N'Hawái
', N'ARCAS CONAP
', N'Santa Rosa
', N'Chiquimulilla
', N'El Hawái
', 13.8681667, -90.419611111111109)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (22, N'Playa Plana
', N'Hotel Playa Plana
', N'Santa Rosa
', N'Chiquimulilla
', N'Los Limones
', 13.8595556, -90.398805555555569)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (23, N'Las Mañanitas
', N'Comunitario
', N'Santa Rosa
', N'Chiquimulilla
', N'Las Mañanitas
', 13.8550278, -90.3871388888889)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (24, N'El Rosario
', N'ARCAS CONAP
', N'Santa Rosa
', N'Chiquimulilla
', N'El Rosario
', 13.8471944, -90.3693611111111)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (25, N'Maya Jade
', N'Hotel Maya Jade
', N'Santa Rosa
', N'Chiquimulilla
', N'El Rosario
', 13.8463611, -90.367361111111109)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (26, N'El Chapetón
', N'AGEXPORT
', N'Santa Rosa
', N'Chiquimulilla
', N'El Chapetón
', 13.8306667, -90.332333333333324)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (27, N'Las Lisas
', N'AGEXPORT
', N'Santa Rosa
', N'Chiquimulilla
', N'Las Lisas
', 13.8003611, -90.262277777777783)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (28, N'Manuelita
', N'Comunitario
', N'Jutiapa
', N'Moyuta
', N'El Jiote
', 13.7811667, -90.215916666666672)
INSERT [dbo].[set_tortugarios] ([TORID], [TORNombre], [TORNombreResponsable], [TORDepartamento], [TORMunicipio], [TORAldea], [TORLat], [TORLong]) VALUES (29, N'La Barrona 
', N'CONAP
', N'Jutiapa
', N'Moyuta
', N'La Barrona
', 13.7673889, -90.185611111111115)
SET IDENTITY_INSERT [dbo].[set_tortugarios] OFF
SET IDENTITY_INSERT [dbo].[system_vars] ON 

INSERT [dbo].[system_vars] ([SVID], [SVKEY], [SVDESC], [SVContent], [SVEditable]) VALUES (4, N'soil_texture', N'Soil Bulk Density based on U.S. Texture Triangle', N'Clay:Sandy clay:Clay loam:Silty clay:Silty clay loam:Loam:Silt loam:Silt:Sandy loam:Loamy sand:Sand', 1)
INSERT [dbo].[system_vars] ([SVID], [SVKEY], [SVDESC], [SVContent], [SVEditable]) VALUES (5, N'volume_mass', N'Volume and mass measurement units', N'lb:oz:pint:gr:kg:fl oz:gal:ml:liter', 1)
INSERT [dbo].[system_vars] ([SVID], [SVKEY], [SVDESC], [SVContent], [SVEditable]) VALUES (6, N'area', N'Area measurement units', N'Acre:1000 ft. sq.:100 m. sq.:Hectare:Mz.', 1)
INSERT [dbo].[system_vars] ([SVID], [SVKEY], [SVDESC], [SVContent], [SVEditable]) VALUES (7, N'equipment_type', N'Type of equipment used', N'Wagon:Loader:Harvester:Truck', 1)
INSERT [dbo].[system_vars] ([SVID], [SVKEY], [SVDESC], [SVContent], [SVEditable]) VALUES (8, N'relational_operators', N'Operators used by models', N'Select an option:Less than:Greater than:Less than or equal:Greater than or equal', 1)
INSERT [dbo].[system_vars] ([SVID], [SVKEY], [SVDESC], [SVContent], [SVEditable]) VALUES (9, N'belize_harvest_years', N'Years', N'2006:2007:2008:2009:2010:2011:2012', 1)
SET IDENTITY_INSERT [dbo].[system_vars] OFF
SET IDENTITY_INSERT [dbo].[trees] ON 

INSERT [dbo].[trees] ([TreeID], [TreeName], [TreeCode]) VALUES (1, N'Main', NULL)
INSERT [dbo].[trees] ([TreeID], [TreeName], [TreeCode]) VALUES (2, N'Admin', NULL)
INSERT [dbo].[trees] ([TreeID], [TreeName], [TreeCode]) VALUES (3, N'Mapping', NULL)
SET IDENTITY_INSERT [dbo].[trees] OFF
SET IDENTITY_INSERT [dbo].[trees_items] ON 

INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (2, N'Users / Groups', N'users-group', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 1, 1, 0, 2, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (3, N'Sigatoka Model', N'sigatoka', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 4, 0, 2, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (5, N'User Administration', N'users', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 2, 0, 2, 1, 2, N'S[button:refresh_users:refresh_table:true;separator:sep1;button:adduser:new:true;separator:sep1;]')
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (6, N'Group Administration', N'groups', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 2, 0, 3, 1, 2, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (7, N'Matrices', N'matrices', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 3, 0, 5, 1, 2, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (8, N'Run DSV', N'rundsv', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 3, 0, 6, 1, 2, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (9, N'EE Values', N'eevalues', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 3, 0, 7, 1, 2, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (11, N'Weather Station Data', N'wsdata', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 3, 0, 9, 1, 2, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (13, N'DSI Calculation', N'rundsi', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 8, 0, 10, 0, 2, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (14, N'Sub Indices', N'subindices', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 8, 0, 11, 0, 2, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (16, N'Harvest', N'harvest', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 1, 0, 1, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (17, N'Nutrition', N'nutrition', N'nutrition.png', N'nutritionChecked.png', N'nutrition.png', -1, 0, 2, 0, 1, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (18, N'Soil Management', N'soil', N'soil.png', N'soilChecked.png', N'soil.png', -1, 0, 3, 0, 1, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (19, N'Integrated Pest Management', N'pest', N'pest.png', N'pestChecked.png', N'pest.png', -1, 0, 4, 0, 1, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (20, N'Equipment', N'equipment', N'equipment.png', N'equipmentChecked.png', N'equipment.png', -1, 0, 5, 0, 1, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (21, N'Water Management', N'water', N'water.png', N'waterChecked.png', N'water.png', -1, 0, 6, 0, 1, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (22, N'Farm Owners', N'farmers', N'farmer.png', N'farmerChecked.png', N'farmer.png', -1, 0, 7, 0, 1, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (23, N'Harvest Orange Walk', N'ow-harvest', N'list.png', N'list.png', N'list.png', 16, 0, 1, 1, 1, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (24, N'Harvest Corozal', N'cz-harvest', N'list.png', N'list.png', N'list.png', 16, 0, 2, 1, 1, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (25, N'Nutrition Orange Walk', N'ow-nutrition', N'list.png', N'list.png', N'list.png', 17, 0, 1, 1, 1, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (26, N'Nutrition Corozal', N'cz-nutrition', N'list.png', N'list.png', N'list.png', 17, 0, 2, 1, 1, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (27, N'Spore Values', N'sporevalues', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', 3, 0, 8, 1, 2, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (28, N'Harvest CGP', N'map-harvest', N'folderClosed.gif', N'folderOpen.gif', N'folderClosed.gif', -1, 0, 1, 1, 3, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (29, N'Nutrition', N'map-nutrition', N'nutrition.png', N'nutrition.png', N'nutrition.png', -1, 0, 3, 1, 3, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (30, N'Harvest CAHSA', N'map-harvest-cahsa', N'globe.png', N'globe.png', N'globbe.png', -1, 0, 2, 1, 3, NULL)
INSERT [dbo].[trees_items] ([ItemID], [Text], [TextID], [Ima], [Imb], [Imc], [ItemParent], [ItemOpen], [ItemOrder], [isLink], [TreeID], [ItemToolbar]) VALUES (31, N'IPM BSI', N'map-ipm-bsi', N'pest.png', N'pest.png', N'pest.png', -1, 0, 4, 1, 3, NULL)
SET IDENTITY_INSERT [dbo].[trees_items] OFF
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([UserID], [GroupID], [UserName], [UserPassWD], [UserPassSalt], [UserFirstName], [UserLastName], [UserEmail], [UserTitle], [UserWorkArea], [UserOrganization], [UserWorkPhone], [UserCellular], [UserCreationDate], [UserCanAdd], [UserCanEdit], [UserCanDelete], [UserSuperAdmin], [UserLanguage], [UserSkin], [UserLastLogin], [UserOldGroupID]) VALUES (3, 1, N'admin', N'', N'', N'Admin', N'Admin', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, 1, N'en-US', N'skyblue', NULL, 0)
SET IDENTITY_INSERT [dbo].[users] OFF
