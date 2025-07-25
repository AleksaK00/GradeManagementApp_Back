USE [GradeManagementApp]
GO
ALTER TABLE [dbo].[Grade] DROP CONSTRAINT [FK_Grade_Coodebookitem2]
GO
ALTER TABLE [dbo].[Grade] DROP CONSTRAINT [FK_Grade_Coodebookitem1]
GO
ALTER TABLE [dbo].[Grade] DROP CONSTRAINT [FK_Grade_Coodebookitem]
GO
ALTER TABLE [dbo].[Coodebookitem] DROP CONSTRAINT [FK_Coodebookitem_Codebook]
GO
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_Grade]
GO
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_Coodebookitem2]
GO
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_Coodebookitem1]
GO
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_Coodebookitem]
GO
ALTER TABLE [dbo].[Grade] DROP CONSTRAINT [DF_Grade_DatumUnosa]
GO
ALTER TABLE [dbo].[Coodebookitem] DROP CONSTRAINT [DF_Coodebookitem_DatumUnosa]
GO
ALTER TABLE [dbo].[Codebook] DROP CONSTRAINT [DF_Codebook_DatumUnosa]
GO
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [DF_Class_DatumUnosa]
GO
/****** Object:  Table [dbo].[User]    Script Date: 31.5.2025. 20:51:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Grade]    Script Date: 31.5.2025. 20:51:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Grade]') AND type in (N'U'))
DROP TABLE [dbo].[Grade]
GO
/****** Object:  Table [dbo].[Coodebookitem]    Script Date: 31.5.2025. 20:51:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Coodebookitem]') AND type in (N'U'))
DROP TABLE [dbo].[Coodebookitem]
GO
/****** Object:  Table [dbo].[Codebook]    Script Date: 31.5.2025. 20:51:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Codebook]') AND type in (N'U'))
DROP TABLE [dbo].[Codebook]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 31.5.2025. 20:51:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Class]') AND type in (N'U'))
DROP TABLE [dbo].[Class]
GO
USE [master]
GO
/****** Object:  Database [GradeManagementApp]    Script Date: 31.5.2025. 20:51:10 ******/
DROP DATABASE [GradeManagementApp]
GO
/****** Object:  Database [GradeManagementApp]    Script Date: 31.5.2025. 20:51:10 ******/
CREATE DATABASE [GradeManagementApp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GradeManagementApp', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\GradeManagementApp.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GradeManagementApp_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\GradeManagementApp_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [GradeManagementApp] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GradeManagementApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GradeManagementApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GradeManagementApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GradeManagementApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GradeManagementApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GradeManagementApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [GradeManagementApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GradeManagementApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GradeManagementApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GradeManagementApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GradeManagementApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GradeManagementApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GradeManagementApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GradeManagementApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GradeManagementApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GradeManagementApp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GradeManagementApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GradeManagementApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GradeManagementApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GradeManagementApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GradeManagementApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GradeManagementApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GradeManagementApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GradeManagementApp] SET RECOVERY FULL 
GO
ALTER DATABASE [GradeManagementApp] SET  MULTI_USER 
GO
ALTER DATABASE [GradeManagementApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GradeManagementApp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GradeManagementApp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GradeManagementApp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GradeManagementApp] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GradeManagementApp] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'GradeManagementApp', N'ON'
GO
ALTER DATABASE [GradeManagementApp] SET QUERY_STORE = ON
GO
ALTER DATABASE [GradeManagementApp] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [GradeManagementApp]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 31.5.2025. 20:51:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GradeID] [int] NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[KombinovanoOdeljenje] [bit] NOT NULL,
	[VrstaOdeljenjaID] [int] NOT NULL,
	[CelodnevnaNastava] [bit] NOT NULL,
	[IzdvojenoOdeljenje] [bit] NOT NULL,
	[NazivIzdvojeneSkole] [nvarchar](50) NULL,
	[OdeljenskiStaresina] [nvarchar](60) NOT NULL,
	[Smena] [nvarchar](50) NOT NULL,
	[JezikNastaveID] [int] NOT NULL,
	[DvojezicnoOdeljenje] [bit] NOT NULL,
	[PrviStraniJezikID] [int] NULL,
	[UkupanBrojUcenika] [int] NOT NULL,
	[BrojUcenika] [int] NOT NULL,
	[BrojUcenica] [int] NOT NULL,
	[DatumUnosa] [datetime] NOT NULL,
	[DatumIzmene] [datetime] NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Codebook]    Script Date: 31.5.2025. 20:51:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Codebook](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[DatumUnosa] [datetime] NOT NULL,
	[DatumIzmene] [datetime] NULL,
 CONSTRAINT [PK_Codebook] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coodebookitem]    Script Date: 31.5.2025. 20:51:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coodebookitem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[CoodebookID] [int] NOT NULL,
	[DatumUnosa] [datetime] NOT NULL,
	[DatumIzmene] [datetime] NULL,
 CONSTRAINT [PK_Coodebookitem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grade]    Script Date: 31.5.2025. 20:51:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SkolskaGodinaID] [int] NOT NULL,
	[RazredID] [int] NOT NULL,
	[ProgramID] [int] NOT NULL,
	[DatumUnosa] [datetime] NOT NULL,
	[DatumIzmene] [datetime] NULL,
 CONSTRAINT [PK_Grade] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 31.5.2025. 20:51:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[lozinka] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Class] ON 

INSERT [dbo].[Class] ([Id], [GradeID], [Naziv], [KombinovanoOdeljenje], [VrstaOdeljenjaID], [CelodnevnaNastava], [IzdvojenoOdeljenje], [NazivIzdvojeneSkole], [OdeljenskiStaresina], [Smena], [JezikNastaveID], [DvojezicnoOdeljenje], [PrviStraniJezikID], [UkupanBrojUcenika], [BrojUcenika], [BrojUcenica], [DatumUnosa], [DatumIzmene]) VALUES (1, 7, N'4', 0, 13, 0, 0, NULL, N'Pera Perić', N'Prva', 18, 0, NULL, 28, 15, 13, CAST(N'2025-05-22T17:03:32.337' AS DateTime), NULL)
INSERT [dbo].[Class] ([Id], [GradeID], [Naziv], [KombinovanoOdeljenje], [VrstaOdeljenjaID], [CelodnevnaNastava], [IzdvojenoOdeljenje], [NazivIzdvojeneSkole], [OdeljenskiStaresina], [Smena], [JezikNastaveID], [DvojezicnoOdeljenje], [PrviStraniJezikID], [UkupanBrojUcenika], [BrojUcenika], [BrojUcenica], [DatumUnosa], [DatumIzmene]) VALUES (11, 8, N'1', 0, 13, 0, 1, N'Skola 123', N'Mika Mikić', N'prva', 18, 0, NULL, 22, 10, 12, CAST(N'2025-05-22T19:26:57.837' AS DateTime), CAST(N'2025-05-23T13:34:24.253' AS DateTime))
INSERT [dbo].[Class] ([Id], [GradeID], [Naziv], [KombinovanoOdeljenje], [VrstaOdeljenjaID], [CelodnevnaNastava], [IzdvojenoOdeljenje], [NazivIzdvojeneSkole], [OdeljenskiStaresina], [Smena], [JezikNastaveID], [DvojezicnoOdeljenje], [PrviStraniJezikID], [UkupanBrojUcenika], [BrojUcenika], [BrojUcenica], [DatumUnosa], [DatumIzmene]) VALUES (14, 7, N'8', 1, 13, 0, 0, NULL, N'Ana Anić', N'druga', 18, 0, NULL, 33, 18, 15, CAST(N'2025-05-22T19:37:13.810' AS DateTime), NULL)
INSERT [dbo].[Class] ([Id], [GradeID], [Naziv], [KombinovanoOdeljenje], [VrstaOdeljenjaID], [CelodnevnaNastava], [IzdvojenoOdeljenje], [NazivIzdvojeneSkole], [OdeljenskiStaresina], [Smena], [JezikNastaveID], [DvojezicnoOdeljenje], [PrviStraniJezikID], [UkupanBrojUcenika], [BrojUcenika], [BrojUcenica], [DatumUnosa], [DatumIzmene]) VALUES (15, 7, N'3', 0, 13, 0, 0, NULL, N'Kolega Madjar', N'druga', 20, 1, 23, 15, 9, 6, CAST(N'2025-05-23T10:51:01.090' AS DateTime), NULL)
INSERT [dbo].[Class] ([Id], [GradeID], [Naziv], [KombinovanoOdeljenje], [VrstaOdeljenjaID], [CelodnevnaNastava], [IzdvojenoOdeljenje], [NazivIzdvojeneSkole], [OdeljenskiStaresina], [Smena], [JezikNastaveID], [DvojezicnoOdeljenje], [PrviStraniJezikID], [UkupanBrojUcenika], [BrojUcenika], [BrojUcenica], [DatumUnosa], [DatumIzmene]) VALUES (16, 8, N'2', 1, 16, 0, 0, NULL, N'Nina Ninić', N'druga', 18, 0, NULL, 21, 7, 14, CAST(N'2025-05-23T11:34:59.600' AS DateTime), NULL)
INSERT [dbo].[Class] ([Id], [GradeID], [Naziv], [KombinovanoOdeljenje], [VrstaOdeljenjaID], [CelodnevnaNastava], [IzdvojenoOdeljenje], [NazivIzdvojeneSkole], [OdeljenskiStaresina], [Smena], [JezikNastaveID], [DvojezicnoOdeljenje], [PrviStraniJezikID], [UkupanBrojUcenika], [BrojUcenika], [BrojUcenica], [DatumUnosa], [DatumIzmene]) VALUES (17, 6, N'2', 1, 13, 1, 1, N'Skola 321', N'Jova Jović', N'druga', 18, 1, 22, 28, 14, 14, CAST(N'2025-05-23T13:35:43.850' AS DateTime), CAST(N'2025-05-23T13:36:08.933' AS DateTime))
INSERT [dbo].[Class] ([Id], [GradeID], [Naziv], [KombinovanoOdeljenje], [VrstaOdeljenjaID], [CelodnevnaNastava], [IzdvojenoOdeljenje], [NazivIzdvojeneSkole], [OdeljenskiStaresina], [Smena], [JezikNastaveID], [DvojezicnoOdeljenje], [PrviStraniJezikID], [UkupanBrojUcenika], [BrojUcenika], [BrojUcenica], [DatumUnosa], [DatumIzmene]) VALUES (22, 15, N'1', 0, 13, 0, 0, NULL, N'Žika Žikić', N'prva', 18, 0, NULL, 31, 16, 15, CAST(N'2025-05-24T12:17:57.497' AS DateTime), NULL)
INSERT [dbo].[Class] ([Id], [GradeID], [Naziv], [KombinovanoOdeljenje], [VrstaOdeljenjaID], [CelodnevnaNastava], [IzdvojenoOdeljenje], [NazivIzdvojeneSkole], [OdeljenskiStaresina], [Smena], [JezikNastaveID], [DvojezicnoOdeljenje], [PrviStraniJezikID], [UkupanBrojUcenika], [BrojUcenika], [BrojUcenica], [DatumUnosa], [DatumIzmene]) VALUES (23, 15, N'2', 0, 14, 0, 1, N'Izdvojena škola 123', N'Kika Kikić', N'prva', 18, 0, NULL, 29, 16, 13, CAST(N'2025-05-24T12:18:32.727' AS DateTime), CAST(N'2025-05-24T12:18:56.627' AS DateTime))
SET IDENTITY_INSERT [dbo].[Class] OFF
GO
SET IDENTITY_INSERT [dbo].[Codebook] ON 

INSERT [dbo].[Codebook] ([Id], [Naziv], [DatumUnosa], [DatumIzmene]) VALUES (1, N'Školska godina', CAST(N'2025-05-09T11:15:34.080' AS DateTime), NULL)
INSERT [dbo].[Codebook] ([Id], [Naziv], [DatumUnosa], [DatumIzmene]) VALUES (2, N'Razred', CAST(N'2025-05-09T11:15:50.540' AS DateTime), NULL)
INSERT [dbo].[Codebook] ([Id], [Naziv], [DatumUnosa], [DatumIzmene]) VALUES (3, N'Vrsta odeljenja', CAST(N'2025-05-09T11:16:07.023' AS DateTime), NULL)
INSERT [dbo].[Codebook] ([Id], [Naziv], [DatumUnosa], [DatumIzmene]) VALUES (4, N'Jezik nastave', CAST(N'2025-05-09T11:16:13.043' AS DateTime), NULL)
INSERT [dbo].[Codebook] ([Id], [Naziv], [DatumUnosa], [DatumIzmene]) VALUES (5, N'Prvi strani jezik', CAST(N'2025-05-09T11:16:28.403' AS DateTime), NULL)
INSERT [dbo].[Codebook] ([Id], [Naziv], [DatumUnosa], [DatumIzmene]) VALUES (6, N'Program', CAST(N'2025-05-09T11:17:58.613' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Codebook] OFF
GO
SET IDENTITY_INSERT [dbo].[Coodebookitem] ON 

INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (2, N'2023/2024', 1, CAST(N'2025-05-09T11:21:36.000' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (3, N'2024/2025', 1, CAST(N'2025-05-09T11:21:43.163' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (4, N'2025/2026', 1, CAST(N'2025-05-09T11:21:48.573' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (5, N'I', 2, CAST(N'2025-05-09T11:28:56.870' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (6, N'II', 2, CAST(N'2025-05-09T11:29:01.560' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (7, N'III', 2, CAST(N'2025-05-09T11:29:06.303' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (8, N'IV', 2, CAST(N'2025-05-09T11:29:10.363' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (9, N'V', 2, CAST(N'2025-05-09T11:29:14.303' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (10, N'VI', 2, CAST(N'2025-05-09T11:29:18.430' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (11, N'VII', 2, CAST(N'2025-05-09T11:29:24.963' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (12, N'VIII', 2, CAST(N'2025-05-09T11:29:30.757' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (13, N'Opšte', 3, CAST(N'2025-05-09T11:31:55.213' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (14, N'Specijalno', 3, CAST(N'2025-05-09T11:32:03.950' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (15, N'Kombinovano', 3, CAST(N'2025-05-09T11:32:08.307' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (16, N'Muzičko', 3, CAST(N'2025-05-09T11:32:15.697' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (17, N'Tehničko', 3, CAST(N'2025-05-09T11:32:23.373' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (18, N'Srpski', 4, CAST(N'2025-05-09T11:33:00.507' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (19, N'Albanski', 4, CAST(N'2025-05-09T11:33:03.897' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (20, N'Mađarski', 4, CAST(N'2025-05-09T11:33:10.840' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (21, N'Slovački', 4, CAST(N'2025-05-09T11:33:19.893' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (22, N'Engleski', 5, CAST(N'2025-05-09T11:33:32.110' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (23, N'Francuski', 5, CAST(N'2025-05-09T11:33:39.317' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (24, N'Nemački', 5, CAST(N'2025-05-09T11:33:42.620' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (25, N'Ruski', 5, CAST(N'2025-05-09T11:33:45.600' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (26, N'Opšti program', 6, CAST(N'2025-05-09T11:33:59.807' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (27, N'IT smer', 6, CAST(N'2025-05-09T11:34:06.473' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (28, N'Sportski program', 6, CAST(N'2025-05-09T11:34:15.800' AS DateTime), NULL)
INSERT [dbo].[Coodebookitem] ([Id], [Naziv], [CoodebookID], [DatumUnosa], [DatumIzmene]) VALUES (29, N'Gimnazijski program', 6, CAST(N'2025-05-09T11:34:33.187' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Coodebookitem] OFF
GO
SET IDENTITY_INSERT [dbo].[Grade] ON 

INSERT [dbo].[Grade] ([Id], [SkolskaGodinaID], [RazredID], [ProgramID], [DatumUnosa], [DatumIzmene]) VALUES (6, 2, 7, 26, CAST(N'2025-05-21T09:39:34.247' AS DateTime), CAST(N'2025-05-21T17:48:22.333' AS DateTime))
INSERT [dbo].[Grade] ([Id], [SkolskaGodinaID], [RazredID], [ProgramID], [DatumUnosa], [DatumIzmene]) VALUES (7, 3, 11, 26, CAST(N'2025-05-21T09:47:00.637' AS DateTime), NULL)
INSERT [dbo].[Grade] ([Id], [SkolskaGodinaID], [RazredID], [ProgramID], [DatumUnosa], [DatumIzmene]) VALUES (8, 3, 9, 26, CAST(N'2025-05-21T11:44:00.813' AS DateTime), CAST(N'2025-05-22T15:49:17.510' AS DateTime))
INSERT [dbo].[Grade] ([Id], [SkolskaGodinaID], [RazredID], [ProgramID], [DatumUnosa], [DatumIzmene]) VALUES (9, 2, 9, 28, CAST(N'2025-05-21T11:48:59.717' AS DateTime), NULL)
INSERT [dbo].[Grade] ([Id], [SkolskaGodinaID], [RazredID], [ProgramID], [DatumUnosa], [DatumIzmene]) VALUES (10, 3, 10, 26, CAST(N'2025-05-21T12:02:35.197' AS DateTime), NULL)
INSERT [dbo].[Grade] ([Id], [SkolskaGodinaID], [RazredID], [ProgramID], [DatumUnosa], [DatumIzmene]) VALUES (11, 3, 12, 29, CAST(N'2025-05-21T14:08:48.797' AS DateTime), NULL)
INSERT [dbo].[Grade] ([Id], [SkolskaGodinaID], [RazredID], [ProgramID], [DatumUnosa], [DatumIzmene]) VALUES (13, 3, 10, 28, CAST(N'2025-05-22T15:34:24.630' AS DateTime), CAST(N'2025-05-22T15:34:34.497' AS DateTime))
INSERT [dbo].[Grade] ([Id], [SkolskaGodinaID], [RazredID], [ProgramID], [DatumUnosa], [DatumIzmene]) VALUES (15, 4, 12, 27, CAST(N'2025-05-24T12:17:36.790' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Grade] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id], [email], [lozinka]) VALUES (1, N'aleksa@gmail.com', N'446cd49792f9021b38581dba0705ed9ee2edb01edfdbf67ebf87c35018ccdd3b')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Class] ADD  CONSTRAINT [DF_Class_DatumUnosa]  DEFAULT (getdate()) FOR [DatumUnosa]
GO
ALTER TABLE [dbo].[Codebook] ADD  CONSTRAINT [DF_Codebook_DatumUnosa]  DEFAULT (getdate()) FOR [DatumUnosa]
GO
ALTER TABLE [dbo].[Coodebookitem] ADD  CONSTRAINT [DF_Coodebookitem_DatumUnosa]  DEFAULT (getdate()) FOR [DatumUnosa]
GO
ALTER TABLE [dbo].[Grade] ADD  CONSTRAINT [DF_Grade_DatumUnosa]  DEFAULT (getdate()) FOR [DatumUnosa]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Coodebookitem] FOREIGN KEY([VrstaOdeljenjaID])
REFERENCES [dbo].[Coodebookitem] ([Id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Coodebookitem]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Coodebookitem1] FOREIGN KEY([JezikNastaveID])
REFERENCES [dbo].[Coodebookitem] ([Id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Coodebookitem1]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Coodebookitem2] FOREIGN KEY([PrviStraniJezikID])
REFERENCES [dbo].[Coodebookitem] ([Id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Coodebookitem2]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Grade] FOREIGN KEY([GradeID])
REFERENCES [dbo].[Grade] ([Id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Grade]
GO
ALTER TABLE [dbo].[Coodebookitem]  WITH CHECK ADD  CONSTRAINT [FK_Coodebookitem_Codebook] FOREIGN KEY([CoodebookID])
REFERENCES [dbo].[Codebook] ([Id])
GO
ALTER TABLE [dbo].[Coodebookitem] CHECK CONSTRAINT [FK_Coodebookitem_Codebook]
GO
ALTER TABLE [dbo].[Grade]  WITH CHECK ADD  CONSTRAINT [FK_Grade_Coodebookitem] FOREIGN KEY([SkolskaGodinaID])
REFERENCES [dbo].[Coodebookitem] ([Id])
GO
ALTER TABLE [dbo].[Grade] CHECK CONSTRAINT [FK_Grade_Coodebookitem]
GO
ALTER TABLE [dbo].[Grade]  WITH CHECK ADD  CONSTRAINT [FK_Grade_Coodebookitem1] FOREIGN KEY([RazredID])
REFERENCES [dbo].[Coodebookitem] ([Id])
GO
ALTER TABLE [dbo].[Grade] CHECK CONSTRAINT [FK_Grade_Coodebookitem1]
GO
ALTER TABLE [dbo].[Grade]  WITH CHECK ADD  CONSTRAINT [FK_Grade_Coodebookitem2] FOREIGN KEY([ProgramID])
REFERENCES [dbo].[Coodebookitem] ([Id])
GO
ALTER TABLE [dbo].[Grade] CHECK CONSTRAINT [FK_Grade_Coodebookitem2]
GO
USE [master]
GO
ALTER DATABASE [GradeManagementApp] SET  READ_WRITE 
GO
