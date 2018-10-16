USE [AgripandaConap]
GO
/****** Object:  Table [dbo].[carto_visualizations]    Script Date: 10/16/2018 1:37:58 PM ******/
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
/****** Object:  Table [dbo].[crops]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[global_settings]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[groups]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[logs]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[menus]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[mods]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[mods_toolbar_items]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[mods_tree_items]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[posts]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[posts_categories]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[scheduler]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[scheduler_events]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[set_colecta_siembra_log]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[set_colectores]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[set_comercializacion_log]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[set_compra_conservacion_log]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[set_compradores]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[set_eclosion_liberacion_log]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[set_tortugarios]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[system_vars]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[trees]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[trees_items]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[uploads]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[users]    Script Date: 10/16/2018 1:37:59 PM ******/
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
/****** Object:  Table [dbo].[users_addupi_sessions]    Script Date: 10/16/2018 1:37:59 PM ******/
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
