USE [master]
GO
/****** Object:  Database [CloudChat1]    Script Date: 15.11.2017 21:19:37 ******/
CREATE DATABASE [CloudChat1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CloudChat', FILENAME = N'D:\Program\SQLServer\MSSQL14.MSSQLSERVER\MSSQL\DATA\CloudChat1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CloudChat_log', FILENAME = N'D:\Program\SQLServer\MSSQL14.MSSQLSERVER\MSSQL\DATA\CloudChat1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CloudChat1] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CloudChat1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CloudChat1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CloudChat1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CloudChat1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CloudChat1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CloudChat1] SET ARITHABORT OFF 
GO
ALTER DATABASE [CloudChat1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CloudChat1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CloudChat1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CloudChat1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CloudChat1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CloudChat1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CloudChat1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CloudChat1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CloudChat1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CloudChat1] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CloudChat1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CloudChat1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CloudChat1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CloudChat1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CloudChat1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CloudChat1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CloudChat1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CloudChat1] SET RECOVERY FULL 
GO
ALTER DATABASE [CloudChat1] SET  MULTI_USER 
GO
ALTER DATABASE [CloudChat1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CloudChat1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CloudChat1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CloudChat1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CloudChat1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CloudChat1] SET QUERY_STORE = OFF
GO
USE [CloudChat1]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [CloudChat1]
GO
/****** Object:  Table [dbo].[content_over_8000]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[content_over_8000](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[share_id] [uniqueidentifier] NOT NULL,
	[file_data] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_content_over_8000] PRIMARY KEY CLUSTERED 
(
	[share_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[content_under_8000]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[content_under_8000](
	[id] [int] NOT NULL,
	[share_id] [int] IDENTITY(1,1) NOT NULL,
	[file_data] [varbinary](8000) NOT NULL,
 CONSTRAINT [PK_content_under_8000] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[group_members]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[group_members](
	[group_id] [uniqueidentifier] NOT NULL,
	[member_id] [uniqueidentifier] NOT NULL,
	[allow_or_deny] [varchar](5) NOT NULL,
 CONSTRAINT [PK_group_members] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC,
	[member_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groups]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groups](
	[group_id] [uniqueidentifier] NOT NULL,
	[group_name_part] [varchar](50) NOT NULL,
	[owner_private_id] [uniqueidentifier] NOT NULL,
	[photo_fragment] [varchar](8000) NOT NULL,
	[org_private_id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_groups] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[members]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[members](
	[public_id] [uniqueidentifier] NOT NULL,
	[an] [uniqueidentifier] NOT NULL,
	[private_id] [uniqueidentifier] NOT NULL,
	[month_last_use] [date] NOT NULL,
	[org_id] [uniqueidentifier] NOT NULL,
	[description_fragment] [varchar](50) NOT NULL,
	[photo_fragment] [varbinary](8000) NOT NULL,
	[kb_bandwidth_used] [int] NOT NULL,
	[away_busy_ready] [varchar](10) NOT NULL,
 CONSTRAINT [PK_ans] PRIMARY KEY CLUSTERED 
(
	[public_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[organizations]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[organizations](
	[private_id] [uniqueidentifier] NOT NULL,
	[public_id] [uniqueidentifier] NOT NULL,
	[kb_of_credit] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shares]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shares](
	[id] [uniqueidentifier] NOT NULL,
	[member_public] [uniqueidentifier] NOT NULL,
	[owner_private] [uniqueidentifier] NOT NULL,
	[org_public] [uniqueidentifier] NOT NULL,
	[org_private] [uniqueidentifier] NOT NULL,
	[to_public] [uniqueidentifier] NOT NULL,
	[death_date] [date] NOT NULL,
	[kb_size] [int] NOT NULL,
	[file_extention] [varchar](10) NOT NULL,
	[self_one_or_group] [varchar](10) NULL,
 CONSTRAINT [PK_shares] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[groups] ADD  CONSTRAINT [DF_groups_photo_fragment]  DEFAULT ((0)) FOR [photo_fragment]
GO
ALTER TABLE [dbo].[members] ADD  CONSTRAINT [DF_people_description_fragment]  DEFAULT ((0)) FOR [description_fragment]
GO
ALTER TABLE [dbo].[members] ADD  CONSTRAINT [DF_people_photo_fragment]  DEFAULT ((0)) FOR [photo_fragment]
GO
ALTER TABLE [dbo].[members] ADD  CONSTRAINT [DF_people_kb_bandwidth_used]  DEFAULT ((0)) FOR [kb_bandwidth_used]
GO
/****** Object:  StoredProcedure [dbo].[usp_content_over_8000Delete]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_content_over_8000Delete] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[content_over_8000]
	WHERE  [id] = @id

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_content_over_8000Insert]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_content_over_8000Insert] 
    @id int,
    @share_id uniqueidentifier,
    @file_data varbinary(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[content_over_8000] ([id], [share_id], [file_data])
	SELECT @id, @share_id, @file_data
	
	-- Begin Return Select <- do not remove
	SELECT [id], [share_id], [file_data]
	FROM   [dbo].[content_over_8000]
	WHERE  [id] = @id
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_content_over_8000Select]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_content_over_8000Select] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id], [shar_id], [file_data] 
	FROM   [dbo].[content_over_8000] 
	WHERE  ([id] = @id OR @id IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_content_over_8000Update]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_content_over_8000Update] 
    @id int,
    @shar_id int,
    @file_data varbinary(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[content_over_8000]
	SET    [id] = @id, [shar_id] = @shar_id, [file_data] = @file_data
	WHERE  [id] = @id
	
	-- Begin Return Select <- do not remove
	SELECT [id], [shar_id], [file_data]
	FROM   [dbo].[content_over_8000]
	WHERE  [id] = @id	
	-- End Return Select <- do not remove

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_content_under_8000Delete]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_content_under_8000Delete] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[content_under_8000]
	WHERE  [id] = @id

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_content_under_8000Insert]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_content_under_8000Insert] 
    @id int,
    @file_data varbinary(8000)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[content_under_8000] ([id], [file_data])
	SELECT @id, @file_data
	
	-- Begin Return Select <- do not remove
	SELECT [id], [share_id], [file_data]
	FROM   [dbo].[content_under_8000]
	WHERE  [id] = @id
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_content_under_8000Select]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_content_under_8000Select] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id], [share_id], [file_data] 
	FROM   [dbo].[content_under_8000] 
	WHERE  ([id] = @id OR @id IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_content_under_8000Update]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_content_under_8000Update] 
    @id int,
    @share_id int,
    @file_data varbinary(8000)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[content_under_8000]
	SET    [id] = @id, [file_data] = @file_data
	WHERE  [id] = @id
	
	-- Begin Return Select <- do not remove
	SELECT [id], [share_id], [file_data]
	FROM   [dbo].[content_under_8000]
	WHERE  [id] = @id	
	-- End Return Select <- do not remove

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_group_membersDelete]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_group_membersDelete] 
    @group_id uniqueidentifier,
    @member_id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[group_members]
	WHERE  [group_id] = @group_id
	       AND [member_id] = @member_id

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_group_membersInsert]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_group_membersInsert] 
    @group_id uniqueidentifier,
    @member_id uniqueidentifier,
    @allow_or_deny varchar(5)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[group_members] ([group_id], [member_id], [allow_or_deny])
	SELECT @group_id, @member_id, @allow_or_deny
	
	-- Begin Return Select <- do not remove
	SELECT [group_id], [member_id], [allow_or_deny]
	FROM   [dbo].[group_members]
	WHERE  [group_id] = @group_id
	       AND [member_id] = @member_id
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_group_membersSelect]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_group_membersSelect] 
    @group_id uniqueidentifier,
    @member_id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [group_id], [member_id], [allow_or_deny] 
	FROM   [dbo].[group_members] 
	WHERE  ([group_id] = @group_id OR @group_id IS NULL) 
	       AND ([member_id] = @member_id OR @member_id IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_group_membersUpdate]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_group_membersUpdate] 
    @group_id uniqueidentifier,
    @member_id uniqueidentifier,
    @allow_or_deny varchar(5)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[group_members]
	SET    [group_id] = @group_id, [member_id] = @member_id, [allow_or_deny] = @allow_or_deny
	WHERE  [group_id] = @group_id
	       AND [member_id] = @member_id
	
	-- Begin Return Select <- do not remove
	SELECT [group_id], [member_id], [allow_or_deny]
	FROM   [dbo].[group_members]
	WHERE  [group_id] = @group_id
	       AND [member_id] = @member_id	
	-- End Return Select <- do not remove

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_groupsDelete]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_groupsDelete] 
    @group_id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[groups]
	WHERE  [group_id] = @group_id

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_groupsInsert]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_groupsInsert] 
    @group_id uniqueidentifier,
    @group_name_part varchar(50),
    @owner_private_id uniqueidentifier,
    @photo_fragment varchar(8000),
    @org_private_id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[groups] ([group_id], [group_name_part], [owner_private_id], [photo_fragment], [org_private_id])
	SELECT @group_id, @group_name_part, @owner_private_id, @photo_fragment, @org_private_id
	
	-- Begin Return Select <- do not remove
	SELECT [group_id], [group_name_part], [owner_private_id], [photo_fragment], [org_private_id]
	FROM   [dbo].[groups]
	WHERE  [group_id] = @group_id
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_groupsSelect]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_groupsSelect] 
    @group_id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [group_id], [group_name_part], [owner_private_id], [photo_fragment], [org_private_id] 
	FROM   [dbo].[groups] 
	WHERE  ([group_id] = @group_id OR @group_id IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_groupsUpdate]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_groupsUpdate] 
    @group_id uniqueidentifier,
    @group_name_part varchar(50),
    @owner_private_id uniqueidentifier,
    @photo_fragment varchar(8000),
    @org_private_id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[groups]
	SET    [group_id] = @group_id, [group_name_part] = @group_name_part, [owner_private_id] = @owner_private_id, [photo_fragment] = @photo_fragment, [org_private_id] = @org_private_id
	WHERE  [group_id] = @group_id
	
	-- Begin Return Select <- do not remove
	SELECT [group_id], [group_name_part], [owner_private_id], [photo_fragment], [org_private_id]
	FROM   [dbo].[groups]
	WHERE  [group_id] = @group_id	
	-- End Return Select <- do not remove

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_membersDelete]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_membersDelete] 
    @public_id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[members]
	WHERE  [public_id] = @public_id

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_membersInsert]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_membersInsert] 
    @public_id uniqueidentifier,
    @an uniqueidentifier,
    @private_id uniqueidentifier,
    @month_last_use date,
    @org_id uniqueidentifier,
    @description_fragment varchar(50),
    @photo_fragment varbinary(8000),
    @kb_bandwidth_used int,
    @away_busy_ready varchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[members] ([public_id], [an], [private_id], [month_last_use], [org_id], [description_fragment], [photo_fragment], [kb_bandwidth_used], [away_busy_ready])
	SELECT @public_id, @an, @private_id, @month_last_use, @org_id, @description_fragment, @photo_fragment, @kb_bandwidth_used, @away_busy_ready
	
	-- Begin Return Select <- do not remove
	SELECT [public_id], [an], [private_id], [month_last_use], [org_id], [description_fragment], [photo_fragment], [kb_bandwidth_used], [away_busy_ready]
	FROM   [dbo].[members]
	WHERE  [public_id] = @public_id
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_membersSelect]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_membersSelect] 
    @public_id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [public_id], [an], [private_id], [month_last_use], [org_id], [description_fragment], [photo_fragment], [kb_bandwidth_used], [away_busy_ready] 
	FROM   [dbo].[members] 
	WHERE  ([public_id] = @public_id OR @public_id IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_membersUpdate]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_membersUpdate] 
    @public_id uniqueidentifier,
    @an uniqueidentifier,
    @private_id uniqueidentifier,
    @month_last_use date,
    @org_id uniqueidentifier,
    @description_fragment varchar(50),
    @photo_fragment varbinary(8000),
    @kb_bandwidth_used int,
    @away_busy_ready varchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[members]
	SET    [public_id] = @public_id, [an] = @an, [private_id] = @private_id, [month_last_use] = @month_last_use, [org_id] = @org_id, [description_fragment] = @description_fragment, [photo_fragment] = @photo_fragment, [kb_bandwidth_used] = @kb_bandwidth_used, [away_busy_ready] = @away_busy_ready
	WHERE  [public_id] = @public_id
	
	-- Begin Return Select <- do not remove
	SELECT [public_id], [an], [private_id], [month_last_use], [org_id], [description_fragment], [photo_fragment], [kb_bandwidth_used], [away_busy_ready]
	FROM   [dbo].[members]
	WHERE  [public_id] = @public_id	
	-- End Return Select <- do not remove

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_sharesDelete]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_sharesDelete] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[shares]
	WHERE  [id] = @id

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_sharesInsert]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_sharesInsert] 
    @member_public uniqueidentifier,
    @owner_private uniqueidentifier,
    @org_public uniqueidentifier,
    @org_private uniqueidentifier,
    @to_public uniqueidentifier,
    @death_date date,
    @kb_size int,
    @file_extention varchar(10),
    @self_one_or_group varchar(10) = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[shares] ([member_public], [owner_private], [org_public], [org_private], [to_public], [death_date], [kb_size], [file_extention], [self_one_or_group])
	SELECT @member_public, @owner_private, @org_public, @org_private, @to_public, @death_date, @kb_size, @file_extention, @self_one_or_group
	
	-- Begin Return Select <- do not remove
	SELECT [id], [member_public], [owner_private], [org_public], [org_private], [to_public], [death_date], [kb_size], [file_extention], [self_one_or_group]
	FROM   [dbo].[shares]
	WHERE  [id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_sharesSelect]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_sharesSelect] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id], [member_public], [owner_private], [org_public], [org_private], [to_public], [death_date], [kb_size], [file_extention], [self_one_or_group] 
	FROM   [dbo].[shares] 
	WHERE  ([id] = @id OR @id IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_sharesUpdate]    Script Date: 15.11.2017 21:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_sharesUpdate] 
    @id int,
    @member_public uniqueidentifier,
    @owner_private uniqueidentifier,
    @org_public uniqueidentifier,
    @org_private uniqueidentifier,
    @to_public uniqueidentifier,
    @death_date date,
    @kb_size int,
    @file_extention varchar(10),
    @self_one_or_group varchar(10) = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[shares]
	SET    [member_public] = @member_public, [owner_private] = @owner_private, [org_public] = @org_public, [org_private] = @org_private, [to_public] = @to_public, [death_date] = @death_date, [kb_size] = @kb_size, [file_extention] = @file_extention, [self_one_or_group] = @self_one_or_group
	WHERE  [id] = @id
	
	-- Begin Return Select <- do not remove
	SELECT [id], [member_public], [owner_private], [org_public], [org_private], [to_public], [death_date], [kb_size], [file_extention], [self_one_or_group]
	FROM   [dbo].[shares]
	WHERE  [id] = @id	
	-- End Return Select <- do not remove

	COMMIT
GO
USE [master]
GO
ALTER DATABASE [CloudChat1] SET  READ_WRITE 
GO
