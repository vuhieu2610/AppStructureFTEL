USE [master]
GO
/****** Object:  Database [DemoStructureDB]    Script Date: 14/06/2019 11:52:55 AM ******/
CREATE DATABASE [DemoStructureDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DemoStructureDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\DemoStructureDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DemoStructureDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\DemoStructureDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DemoStructureDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DemoStructureDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DemoStructureDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DemoStructureDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DemoStructureDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DemoStructureDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DemoStructureDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DemoStructureDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DemoStructureDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DemoStructureDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DemoStructureDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DemoStructureDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DemoStructureDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DemoStructureDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DemoStructureDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DemoStructureDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DemoStructureDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DemoStructureDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DemoStructureDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DemoStructureDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DemoStructureDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DemoStructureDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DemoStructureDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DemoStructureDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DemoStructureDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DemoStructureDB] SET  MULTI_USER 
GO
ALTER DATABASE [DemoStructureDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DemoStructureDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DemoStructureDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DemoStructureDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DemoStructureDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DemoStructureDB] SET QUERY_STORE = OFF
GO
USE [DemoStructureDB]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14/06/2019 11:52:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[FullName] [nvarchar](255) NULL,
	[Age] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[USER_DELETE_SINGLE]    Script Date: 14/06/2019 11:52:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USER_DELETE_SINGLE]
	@IN_JSON  NVARCHAR(max),
	@OUT_CODE NVARCHAR(10)   OUTPUT,
	@OUT_MSG  NVARCHAR(1000) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION
	BEGIN TRY
		DELETE FROM [dbo].[Users]
		WHERE Id = JSON_VALUE(@IN_JSON, '$.Id');

		SET @OUT_CODE = '0';
		SET @OUT_MSG  = 'SUC';
		COMMIT;
	END TRY

	BEGIN CATCH
		SET @OUT_CODE = ERROR_NUMBER();
		SET @OUT_MSG  = ERROR_MESSAGE();
		ROLLBACK;
	END CATCH
	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[USER_GET_PAGING]    Script Date: 14/06/2019 11:52:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		HieuVm3
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USER_GET_PAGING]
	@IN_JSON  NVARCHAR(max),
	@OUT_TOTAL_COUNT INT OUTPUT,
	@OUT_CODE NVARCHAR(10)   OUTPUT,
	@OUT_MSG  NVARCHAR(1000) OUTPUT
AS
DECLARE @QUERY NVARCHAR(MAX) = '';
DECLARE @QUERY_TOTAL_COUNT NVARCHAR(MAX) = '';
BEGIN
	SET NOCOUNT ON;
		SET @QUERY = 'SELECT *
						FROM [dbo].[Users] AS U
						WHERE 1=1 '    + JSON_VALUE(@IN_JSON, '$.IN_WHERE')   + 
						' ORDER BY U.ID DESC ' + 
						' OFFSET '     + JSON_VALUE(@IN_JSON, '$.FromRecord') + ' ROWS ' +
						' FETCH NEXT ' + JSON_VALUE(@IN_JSON, '$.PageSize') + ' ROWS ONLY';

		SET @QUERY_TOTAL_COUNT = 'SELECT @OUT_TOTAL_COUNT = COUNT(ID)
									FROM [dbo].[Users] AS U
								    WHERE 1=1 '  + JSON_VALUE(@IN_JSON, '$.IN_WHERE');
		BEGIN TRY
			EXEC SP_EXECUTESQL @QUERY;
			EXEC SP_EXECUTESQL @QUERY_TOTAL_COUNT, N'@OUT_TOTAL_COUNT INT OUTPUT', @OUT_TOTAL_COUNT OUTPUT;

			SET @OUT_CODE = '0';
			SET @OUT_MSG  = 'SUC';
		END TRY
		BEGIN CATCH
			SET @OUT_CODE = ERROR_NUMBER();
			SET @OUT_MSG  = ERROR_MESSAGE();
		END CATCH
	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[USER_GET_SINGLE]    Script Date: 14/06/2019 11:52:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[USER_GET_SINGLE]
	@IN_JSON  NVARCHAR(max),
	@OUT_CODE NVARCHAR(10)   OUTPUT,
	@OUT_MSG  NVARCHAR(1000) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT * FROM [dbo].[Users]
		WHERE Id = CAST( JSON_VALUE(@IN_JSON, '$.Id') as INT );

		SET @OUT_CODE = '0';
		SET @OUT_MSG  = 'SUC';
	END TRY
	BEGIN CATCH
		SET @OUT_CODE = ERROR_NUMBER();
		SET @OUT_MSG  = ERROR_MESSAGE();
	END CATCH
	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[USER_INSERT]    Script Date: 14/06/2019 11:52:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USER_INSERT]
	@IN_JSON  NVARCHAR(max),
	@OUT_CODE NVARCHAR(10)   OUTPUT,
	@OUT_MSG  NVARCHAR(1000) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
	BEGIN TRY

		INSERT INTO [dbo].[Users] ( [FullName], [Age] )
		SELECT 
			[FullName], [Age]
		FROM 
			OPENJSON(@IN_JSON)
		WITH
			(
				[FullName] NVARCHAR(255) '$.FullName',
				[Age] INT '$.Age'
			);
		
		SET @OUT_CODE = '0';
		SET @OUT_MSG  = 'SUC';
		
		COMMIT;
	END TRY
	BEGIN CATCH
		SET @OUT_CODE = ERROR_NUMBER();
		SET @OUT_MSG  = ERROR_MESSAGE();
		ROLLBACK;
	END CATCH
	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[USER_UPDATE]    Script Date: 14/06/2019 11:52:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USER_UPDATE]
	@IN_JSON  NVARCHAR(max),
	@OUT_CODE NVARCHAR(10)   OUTPUT,
	@OUT_MSG  NVARCHAR(1000) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		BEGIN TRANSACTION;
			BEGIN TRY
				UPDATE 
					[dbo].[Users]
				SET 
					[Users].FullName = J.FullName,
					[Users].Age = J.Age
				FROM 
					[dbo].[Users]
					LEFT JOIN OPENJSON( @IN_JSON )
					WITH( 
						Id INT '$.Id',
						FullName NVARCHAR(255) '$.FullName',
						Age INT '$.Age'
					) AS J

				ON ( Users.Id = J.Id )
				WHERE Users.Id = J.Id;

			SET @OUT_CODE = '0';
			SET @OUT_MSG  = 'SUC';
			COMMIT;
			END TRY
			BEGIN CATCH
				SET @OUT_CODE = ERROR_NUMBER();
				SET @OUT_MSG  = ERROR_MESSAGE();
				ROLLBACK;
			END CATCH
	SET NOCOUNT OFF;
END
GO
USE [master]
GO
ALTER DATABASE [DemoStructureDB] SET  READ_WRITE 
GO
