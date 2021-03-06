USE [master]
GO
/****** Object:  Database [YiSpace]    ******/
CREATE DATABASE [YiSpace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'YiSpace', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\YiSpace.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'YiSpace_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\YiSpace_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [YiSpace] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [YiSpace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [YiSpace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [YiSpace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [YiSpace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [YiSpace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [YiSpace] SET ARITHABORT OFF 
GO
ALTER DATABASE [YiSpace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [YiSpace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [YiSpace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [YiSpace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [YiSpace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [YiSpace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [YiSpace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [YiSpace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [YiSpace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [YiSpace] SET  DISABLE_BROKER 
GO
ALTER DATABASE [YiSpace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [YiSpace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [YiSpace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [YiSpace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [YiSpace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [YiSpace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [YiSpace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [YiSpace] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [YiSpace] SET  MULTI_USER 
GO
ALTER DATABASE [YiSpace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [YiSpace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [YiSpace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [YiSpace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [YiSpace] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [YiSpace] SET QUERY_STORE = OFF
GO
USE [YiSpace]
GO
/****** Object:  Table [dbo].[TicketIssue]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketIssue](
	[ID] [bigint] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Severity] [tinyint] NOT NULL,
	[Priority] [tinyint] NOT NULL,
	[State] [tinyint] NOT NULL,
	[Summary] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[AssignUserID] [bigint] NOT NULL,
	[Creator] [bigint] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Modifier] [bigint] NULL,
	[ModifyTime] [datetime] NULL,
 CONSTRAINT [PK_TicketIssue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [bigint] NOT NULL,
	[Account] [varchar](50) NOT NULL,
	[Password] [varchar](200) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Level] [tinyint] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'種類' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TicketIssue', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'優先權' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TicketIssue', @level2type=N'COLUMN',@level2name=N'Priority'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'狀態' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TicketIssue', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摘要' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TicketIssue', @level2type=N'COLUMN',@level2name=N'Summary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TicketIssue', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被指派者ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TicketIssue', @level2type=N'COLUMN',@level2name=N'AssignUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TicketIssue', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TicketIssue', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改者ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TicketIssue', @level2type=N'COLUMN',@level2name=N'Modifier'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TicketIssue', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帳號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者名稱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者層級' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Level'
GO
USE [master]
GO
ALTER DATABASE [YiSpace] SET  READ_WRITE 
GO
USE [YiSpace]
INSERT [dbo].[User] ([ID], [Account], [Password], [Name], [Level]) VALUES (1, N'Admin', N'wcIksDzZvHtqhtd/XazkAZF2bEhc1V3EjK+ayHMzXW8=', N'AdminTest', 99)
GO
