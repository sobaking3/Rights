USE [master]
GO
/****** Object:  Database [Rights]    Script Date: 16.06.2024 14:24:33 ******/
CREATE DATABASE [Rights]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Rights', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Rights.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Rights_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Rights_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Rights] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Rights].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Rights] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Rights] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Rights] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Rights] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Rights] SET ARITHABORT OFF 
GO
ALTER DATABASE [Rights] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Rights] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Rights] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Rights] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Rights] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Rights] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Rights] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Rights] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Rights] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Rights] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Rights] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Rights] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Rights] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Rights] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Rights] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Rights] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Rights] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Rights] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Rights] SET  MULTI_USER 
GO
ALTER DATABASE [Rights] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Rights] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Rights] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Rights] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Rights] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Rights] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Rights] SET QUERY_STORE = ON
GO
ALTER DATABASE [Rights] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Rights]
GO
/****** Object:  Table [dbo].[AppealsAndComplaints]    Script Date: 16.06.2024 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppealsAndComplaints](
	[IdAppeal] [int] IDENTITY(1,1) NOT NULL,
	[IdStaff] [int] NULL,
	[Date] [date] NULL,
	[Discription] [nvarchar](300) NULL,
	[IdStatus] [int] NULL,
 CONSTRAINT [PK_AppealsAndComplaints] PRIMARY KEY CLUSTERED 
(
	[IdAppeal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Budget]    Script Date: 16.06.2024 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Budget](
	[IdBudget] [int] IDENTITY(1,1) NOT NULL,
	[IdCommitte] [int] NULL,
	[Year] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[UsageMoney] [int] NOT NULL,
	[UnUsageMoney] [int] NOT NULL,
 CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED 
(
	[IdBudget] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Committee]    Script Date: 16.06.2024 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Committee](
	[IdCommittee] [int] IDENTITY(1,1) NOT NULL,
	[NameCommittee] [nvarchar](30) NOT NULL,
	[IdStaff] [int] NOT NULL,
 CONSTRAINT [PK_Committee] PRIMARY KEY CLUSTERED 
(
	[IdCommittee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departament]    Script Date: 16.06.2024 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departament](
	[IdDepartament] [int] IDENTITY(1,1) NOT NULL,
	[NameDepartament] [nvarchar](30) NOT NULL,
	[IdStaff] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[IdDepartament] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 16.06.2024 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[IdGender] [int] IDENTITY(1,1) NOT NULL,
	[GenderName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[IdGender] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meetings]    Script Date: 16.06.2024 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meetings](
	[IdMeetings] [int] IDENTITY(1,1) NOT NULL,
	[MeetingsDate] [date] NOT NULL,
	[Discription] [nvarchar](100) NULL,
	[IdCommittee] [int] NOT NULL,
	[IdStatus] [int] NULL,
 CONSTRAINT [PK_Meetings] PRIMARY KEY CLUSTERED 
(
	[IdMeetings] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organizations]    Script Date: 16.06.2024 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[IdOrganization] [int] IDENTITY(1,1) NOT NULL,
	[NameOrganization] [nvarchar](30) NOT NULL,
	[PhoneNumber] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](180) NOT NULL,
	[IdStatus] [int] NOT NULL,
 CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED 
(
	[IdOrganization] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 16.06.2024 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[IdRole] [int] IDENTITY(1,1) NOT NULL,
	[NameRole] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocialPrograms]    Script Date: 16.06.2024 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocialPrograms](
	[IdPrograms] [int] IDENTITY(1,1) NOT NULL,
	[ProgramName] [nvarchar](50) NOT NULL,
	[ProgramDiscription] [nvarchar](300) NULL,
	[IdCommitte] [int] NOT NULL,
 CONSTRAINT [PK_SocialPrograms] PRIMARY KEY CLUSTERED 
(
	[IdPrograms] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 16.06.2024 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[IdStaff] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[MiddleName] [nvarchar](30) NOT NULL,
	[Number] [nvarchar](30) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[IdGender] [int] NOT NULL,
	[IdUser] [int] NULL,
	[WorkStartDate] [date] NOT NULL,
	[IdDepartment] [int] NULL,
	[IdCommittee] [int] NULL,
	[PhotoStaff] [varbinary](max) NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[IdStaff] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 16.06.2024 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[IdStatus] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[IdStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 16.06.2024 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](30) NOT NULL,
	[Password] [nvarchar](30) NOT NULL,
	[IdRole] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppealsAndComplaints]  WITH CHECK ADD  CONSTRAINT [FK_AppealsAndComplaints_Staff] FOREIGN KEY([IdStaff])
REFERENCES [dbo].[Staff] ([IdStaff])
GO
ALTER TABLE [dbo].[AppealsAndComplaints] CHECK CONSTRAINT [FK_AppealsAndComplaints_Staff]
GO
ALTER TABLE [dbo].[AppealsAndComplaints]  WITH CHECK ADD  CONSTRAINT [FK_AppealsAndComplaints_Status] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[Status] ([IdStatus])
GO
ALTER TABLE [dbo].[AppealsAndComplaints] CHECK CONSTRAINT [FK_AppealsAndComplaints_Status]
GO
ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_Committee] FOREIGN KEY([IdCommitte])
REFERENCES [dbo].[Committee] ([IdCommittee])
GO
ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_Committee]
GO
ALTER TABLE [dbo].[Meetings]  WITH CHECK ADD  CONSTRAINT [FK_Meetings_Committee] FOREIGN KEY([IdCommittee])
REFERENCES [dbo].[Committee] ([IdCommittee])
GO
ALTER TABLE [dbo].[Meetings] CHECK CONSTRAINT [FK_Meetings_Committee]
GO
ALTER TABLE [dbo].[Meetings]  WITH CHECK ADD  CONSTRAINT [FK_Meetings_Status] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[Status] ([IdStatus])
GO
ALTER TABLE [dbo].[Meetings] CHECK CONSTRAINT [FK_Meetings_Status]
GO
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_Status] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[Status] ([IdStatus])
GO
ALTER TABLE [dbo].[Organizations] CHECK CONSTRAINT [FK_Organizations_Status]
GO
ALTER TABLE [dbo].[SocialPrograms]  WITH CHECK ADD  CONSTRAINT [FK_SocialPrograms_Committee] FOREIGN KEY([IdCommitte])
REFERENCES [dbo].[Committee] ([IdCommittee])
GO
ALTER TABLE [dbo].[SocialPrograms] CHECK CONSTRAINT [FK_SocialPrograms_Committee]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Committee1] FOREIGN KEY([IdCommittee])
REFERENCES [dbo].[Committee] ([IdCommittee])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Committee1]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Departament] FOREIGN KEY([IdDepartment])
REFERENCES [dbo].[Departament] ([IdDepartament])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Departament]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Gender] FOREIGN KEY([IdGender])
REFERENCES [dbo].[Gender] ([IdGender])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Gender]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_User1] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([IdUser])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_User1]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([IdRole])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [Rights] SET  READ_WRITE 
GO
