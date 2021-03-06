USE [master]
GO
/****** Object:  Database [GamePool]    Script Date: 1/2/2018 8:04:26 PM ******/
CREATE DATABASE [GamePool]
 CONTAINMENT = NONE
GO
ALTER DATABASE [GamePool] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GamePool].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GamePool] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GamePool] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GamePool] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GamePool] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GamePool] SET ARITHABORT OFF 
GO
ALTER DATABASE [GamePool] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GamePool] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GamePool] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GamePool] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GamePool] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GamePool] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GamePool] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GamePool] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GamePool] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GamePool] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GamePool] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GamePool] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GamePool] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GamePool] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GamePool] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GamePool] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GamePool] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GamePool] SET RECOVERY FULL 
GO
ALTER DATABASE [GamePool] SET  MULTI_USER 
GO
ALTER DATABASE [GamePool] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GamePool] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GamePool] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GamePool] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GamePool] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'GamePool', N'ON'
GO
ALTER DATABASE [GamePool] SET QUERY_STORE = OFF
GO
USE [GamePool]
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
USE [GamePool]
GO
/****** Object:  UserDefinedFunction [dbo].[GET_ROLE_ID_BY_NAME]    Script Date: 1/2/2018 8:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GET_ROLE_ID_BY_NAME]
(
	@Name NVARCHAR(50)
)
RETURNS INT
AS
BEGIN
	DECLARE @Id INT

	SELECT @Id = [Id]
	FROM [dbo].[Roles]
	WHERE [Name] = @Name

	RETURN @Id
END
GO
/****** Object:  UserDefinedFunction [dbo].[GET_USER_ID_BY_NAME]    Script Date: 1/2/2018 8:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GET_USER_ID_BY_NAME]
(
	@Name NVARCHAR(50)
)
RETURNS INT
AS
BEGIN
	DECLARE @Id INT

	SELECT @Id = [Id]
	FROM [dbo].[Users]
	WHERE [Name] = @Name

	RETURN @Id
END
GO
/****** Object:  Table [dbo].[Games]    Script Date: 1/2/2018 8:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Games](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AvatarId] [int] NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](2000) NOT NULL,
	[Price] [money] NOT NULL,
	[ReleaseDate] [date] NOT NULL,
 CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_Game_GetAll]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_Game_GetAll]
AS
SELECT        Id, AvatarId, Name, Description, Price, ReleaseDate
FROM            dbo.Games AS games
GO
/****** Object:  Table [dbo].[SystemRequirements]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemRequirements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GameId] [int] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Processor] [nvarchar](100) NOT NULL,
	[Memory] [nvarchar](100) NOT NULL,
	[OperationSystem] [nvarchar](100) NOT NULL,
	[Graphics] [nvarchar](100) NOT NULL,
	[Storage] [nvarchar](100) NOT NULL,
	[DirectX] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SystemRequirements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_Game_GetAllWithSystemRequirements]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_Game_GetAllWithSystemRequirements]
AS 
	SELECT g.[Id],
			g.[AvatarId],
			g.[Name],
			g.[Description],
			g.[Price],
			g.[ReleaseDate],
			sm.[Id] AS [MinId],
			sm.[GameId] AS [MinGameId],
			sm.[Processor] AS [MinProcessor],
			sm.[OperationSystem] AS [MinOperationSystem],
			sm.[Storage] AS [Storage],
			sm.[Memory] AS [Memory],
			sm.[Graphics] AS [Graphics],
			sm.[DirectX] AS [DirectX],
			sr.[Id] AS [RecId],
			sr.[GameId] AS [RecGameId],
			sr.[Processor] AS [RecProcessor],
			sr.[OperationSystem] AS [RecOperationSystem],
			sr.[Storage] AS [RecStorage],
			sr.[Memory] AS [RecMemory],
			sr.[Graphics] AS [RecGraphics],
			sr.[DirectX] AS [RecDirectX]
	FROM [dbo].[V_Game_GetAll] g
	INNER JOIN [dbo].[SystemRequirements] sm
		ON sm.[GameId] = g.[Id] AND
			sm.[Type] = 'min'
	INNER JOIN [dbo].[SystemRequirements] sr
		ON sr.[GameId] = g.[Id] AND
			sr.[Type] = 'rec'
GO
/****** Object:  Table [dbo].[GamesGenres]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GamesGenres](
	[GameId] [int] NOT NULL,
	[GenreId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Path] [nvarchar](50) NOT NULL,
	[AlternativeText] [nvarchar](50) NOT NULL,
	[MimeType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nchar](15) NOT NULL,
	[GameId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [char](128) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Games]  WITH CHECK ADD  CONSTRAINT [FK_Games_Images] FOREIGN KEY([AvatarId])
REFERENCES [dbo].[Images] ([Id])
GO
ALTER TABLE [dbo].[Games] CHECK CONSTRAINT [FK_Games_Images]
GO
ALTER TABLE [dbo].[GamesGenres]  WITH CHECK ADD  CONSTRAINT [FK_GamesGenres_Games] FOREIGN KEY([GameId])
REFERENCES [dbo].[Games] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GamesGenres] CHECK CONSTRAINT [FK_GamesGenres_Games]
GO
ALTER TABLE [dbo].[GamesGenres]  WITH CHECK ADD  CONSTRAINT [FK_GamesGenres_Genres] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genres] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GamesGenres] CHECK CONSTRAINT [FK_GamesGenres_Genres]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Games] FOREIGN KEY([GameId])
REFERENCES [dbo].[Games] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Games]
GO
ALTER TABLE [dbo].[SystemRequirements]  WITH CHECK ADD  CONSTRAINT [FK_SystemRequirements_Games] FOREIGN KEY([GameId])
REFERENCES [dbo].[Games] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemRequirements] CHECK CONSTRAINT [FK_SystemRequirements_Games]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Roles]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Users]
GO
/****** Object:  StoredProcedure [dbo].[Game_Add]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Game_Add]
	@Id          INT OUTPUT,
	@Name        NVARCHAR(200),
	@Description NVARCHAR(2000),
	@Price       MONEY,
	@ReleaseDate DATE
AS
BEGIN

	INSERT INTO [dbo].[Games] (
								[Name],
								[Description],
								[Price],
								[ReleaseDate]
							  )
		VALUES (
				 @Name,
				 @Description,
				 @Price,
				 @ReleaseDate
			   )

	SET @Id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[Game_GetAll]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Game_GetAll]
	@PageNumber INT,
	@PageSize   INT
AS
BEGIN
	SELECT [Id],
	       [AvatarId],
		   [Name],
		   [Description],
		   [Price],
		   [ReleaseDate]
	FROM [V_Game_GetAll]
	ORDER BY [Id]
	OFFSET @PageSize * (@PageNumber - 1) ROWS
    FETCH NEXT @PageSize ROWS ONLY

	SELECT COUNT(*) AS [Count]
	FROM [V_Game_GetAll]
	
END
GO
/****** Object:  StoredProcedure [dbo].[Game_GetById]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Game_GetById]
	@Id INT
AS
BEGIN
	SELECT g.[Id],
	       g.[AvatarId],
		   g.[Name],
		   g.[Description],
		   g.[Price],
		   g.[ReleaseDate],
		   sm.[Id] AS [MinId],
		   sm.[GameId] AS [MinGameId],
		   sm.[Processor] AS [MinProcessor],
		   sm.[OperationSystem] AS [MinOperationSystem],
		   sm.[Storage] AS [MinStorage],
		   sm.[Memory] AS [MinMemory],
		   sm.[Graphics] AS [MinGraphics],
		   sm.[DirectX] AS [MinDirectX],
		   sr.[Id] AS [RecId],
		   sr.[GameId] AS [RecGameId],
		   sr.[Processor] AS [RecProcessor],
		   sr.[OperationSystem] AS [RecOperationSystem],
		   sr.[Storage] AS [RecStorage],
		   sr.[Memory] AS [RecMemory],
		   sr.[Graphics] AS [RecGraphics],
		   sr.[DirectX] AS [RecDirectX]
	FROM [dbo].[V_Game_GetAll] g
	INNER JOIN [dbo].[SystemRequirements] sm
		ON sm.[GameId] = g.[Id] AND
		   sm.[Type] = 'min'
	INNER JOIN [dbo].[SystemRequirements] sr
		ON sr.[GameId] = g.[Id] AND
		   sr.[Type] = 'rec'
	WHERE g.[Id] = @Id

	SELECT COUNT(*)
	FROM [dbo].[V_Game_GetAll] g
	INNER JOIN [dbo].[SystemRequirements] sm
		ON sm.[GameId] = g.[Id] AND
		   sm.[Type] = 'min'
	INNER JOIN [dbo].[SystemRequirements] sr
		ON sr.[GameId] = g.[Id] AND
		   sr.[Type] = 'rec'
	WHERE g.[Id] = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Game_GetByIds]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Game_GetByIds]
	@Ids NVARCHAR(MAX)
AS
BEGIN
	SELECT g.[Id],
	       g.[AvatarId],
		   g.[Name],
		   g.[Description],
		   g.[Price],
		   g.[ReleaseDate],
		   sm.[Id] AS [MinId],
		   sm.[GameId] AS [MinGameId],
		   sm.[Processor] AS [MinProcessor],
		   sm.[OperationSystem] AS [MinOperationSystem],
		   sm.[Storage] AS [MinStorage],
		   sm.[Memory] AS [MinMemory],
		   sm.[Graphics] AS [MinGraphics],
		   sm.[DirectX] AS [MinDirectX],
		   sr.[Id] AS [RecId],
		   sr.[GameId] AS [RecGameId],
		   sr.[Processor] AS [RecProcessor],
		   sr.[OperationSystem] AS [RecOperationSystem],
		   sr.[Storage] AS [RecStorage],
		   sr.[Memory] AS [RecMemory],
		   sr.[Graphics] AS [RecGraphics],
		   sr.[DirectX] AS [RecDirectX]
	FROM [dbo].[V_Game_GetAll] g
	INNER JOIN [dbo].[SystemRequirements] sm
		ON sm.[GameId] = g.[Id] AND
		   sm.[Type] = 'min'
	INNER JOIN [dbo].[SystemRequirements] sr
		ON sr.[GameId] = g.[Id] AND
		   sr.[Type] = 'rec'
	WHERE g.[Id] IN
	(
		SELECT [value]
		FROM OPENJSON(@Ids)
	)

	SELECT COUNT(*)
	FROM [dbo].[V_Game_GetAll] g
	INNER JOIN [dbo].[SystemRequirements] sm
		ON sm.[GameId] = g.[Id] AND
		   sm.[Type] = 'min'
	INNER JOIN [dbo].[SystemRequirements] sr
		ON sr.[GameId] = g.[Id] AND
		   sr.[Type] = 'rec'
	WHERE g.[Id] IN
	(
		SELECT [value]
		FROM OPENJSON(@Ids)
	)
END
GO
/****** Object:  StoredProcedure [dbo].[Game_Remove]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Game_Remove]
	@Id INT
AS
BEGIN
	DELETE FROM [dbo].[Games]
	WHERE @Id = [Id] 
END
GO
/****** Object:  StoredProcedure [dbo].[Game_Search]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Game_Search]
	@Name        NVARCHAR(200),
	@PriceFrom   DECIMAL(10, 2),
	@PriceTo     DECIMAL(10, 2),
	@ReleaseDate DATE,
	@GenreIds    NVARCHAR(MAX),
	@PageNumber  INT,
	@PageSize    INT
AS
BEGIN
	SELECT DISTINCT g.[Id],
	                g.[AvatarId],
		            g.[Name],
		            g.[Description],
		            g.[Price],
		            g.[ReleaseDate]
	FROM [dbo].[V_Game_GetAll] g
	LEFT OUTER JOIN [dbo].[GamesGenres] gg
		ON gg.[GameId] = g.[Id]
	WHERE [Name] LIKE '%' + ISNULL(@Name, [Name]) + '%' AND
		  [Price] >= ISNULL(@PriceFrom, [Price]) AND
		  [Price] <= ISNULL(@PriceTo, [Price]) AND
		  [ReleaseDate] = ISNULL(@ReleaseDate, [ReleaseDate]) AND
		  (([GenreId] IS NOT NULL AND
		    @GenreIds IS NOT NULL AND 
			[GenreId] IN (SELECT [value] FROM OPENJSON(@GenreIds, N'$.ids'))) OR
		   
		   ([GenreId] IS NOT NULL AND 
		    @GenreIds IS NULL) OR
		   
		   ([GenreId] IS NULL AND
		    @GenreIds IS NULL))
	ORDER BY [Id]
	OFFSET @PageSize * (@PageNumber - 1) ROWS
    FETCH NEXT @PageSize ROWS ONLY

	SELECT COUNT(DISTINCT g.[Id])
	FROM [dbo].[V_Game_GetAll] g
	LEFT OUTER JOIN [dbo].[GamesGenres] gg
		ON gg.[GameId] = g.[Id]
	WHERE [Name] LIKE '%' + ISNULL(@Name, [Name]) + '%' AND
			[Price] >= ISNULL(@PriceFrom, [Price]) AND
			[Price] <= ISNULL(@PriceTo, [Price]) AND
			[ReleaseDate] = ISNULL(@ReleaseDate, [ReleaseDate]) AND
			(([GenreId] IS NOT NULL AND
			@GenreIds IS NOT NULL AND 
			[GenreId] IN (SELECT [value] FROM OPENJSON(@GenreIds, N'$.ids'))) OR
		   
			([GenreId] IS NOT NULL AND 
			@GenreIds IS NULL) OR
		   
			([GenreId] IS NULL AND
			@GenreIds IS NULL))
END
GO
/****** Object:  StoredProcedure [dbo].[Game_Update]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Game_Update]
	@Id          INT OUTPUT,
	@Name        NVARCHAR(200),
	@Description NVARCHAR(2000),
	@Price       MONEY,
	@ReleaseDate DATE
AS
BEGIN
	UPDATE [dbo].[Games]
		SET [Name] = @Name,
		    [Description] = @Description,
		    [Price] = @Price,
		    [ReleaseDate] = @ReleaseDate
	WHERE @Id = [Id]
END
GO
/****** Object:  StoredProcedure [dbo].[GameGenre_AddGenresByGameId]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GameGenre_AddGenresByGameId]
	@GameId INT,
	@Ids    NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO [dbo].[GamesGenres]([GameId], [GenreId])
		SELECT @GameId, [value]
		FROM OPENJSON(@Ids)
END
GO
/****** Object:  StoredProcedure [dbo].[GameGenre_RemoveAllGenresByGameId]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GameGenre_RemoveAllGenresByGameId]
	@GameId INT
AS
BEGIN
	DELETE FROM [dbo].[GamesGenres]
	WHERE [GameId] = @GameId
END
GO
/****** Object:  StoredProcedure [dbo].[GameGenre_RemoveGenresByGameId]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GameGenre_RemoveGenresByGameId]
	@GameId INT,
	@Ids    NVARCHAR(MAX)
AS
BEGIN
	DELETE FROM [dbo].[GamesGenres]
	WHERE [GenreId] IN 
	(
		SELECT [value]
		FROM OPENJSON(@Ids)
	) AND [GameId] = @GameId
END
GO
/****** Object:  StoredProcedure [dbo].[GameGenre_UpdateGenresByGameId]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GameGenre_UpdateGenresByGameId]
	@GameId INT,
	@Ids    NVARCHAR(MAX)
AS
BEGIN
	EXEC [dbo].[GameGenre_RemoveAllGenresByGameId] @GameId

	EXEC [dbo].[GameGenre_AddGenresByGameId] @GameId, @Ids
END
GO
/****** Object:  StoredProcedure [dbo].[Genre_Add]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Genre_Add]
	@Id   INT OUTPUT,
	@Name NVARCHAR(50)
AS
BEGIN TRAN
	IF NOT EXISTS 
	(
		SELECT 1
		FROM [dbo].[Genres]
		WHERE [Name] = @Name
	)
	BEGIN
		INSERT INTO [dbo].[Genres] ([Name])
			VALUES (@Name)

		SET @Id = SCOPE_IDENTITY()

		COMMIT
	END
	ELSE
	BEGIN
		ROLLBACK
	END
GO
/****** Object:  StoredProcedure [dbo].[Genre_GetByGameId]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Genre_GetByGameId]
	@GameId INT
AS
BEGIN
	SELECT ge.[Id],
		   ge.[Name]
	FROM [dbo].[Genres] ge
	INNER JOIN [dbo].[GamesGenres] gg
		ON ge.[Id] = gg.[GenreId]
	INNER JOIN [dbo].[Games] ga
		ON ga.[Id] = gg.[GameId]
	WHERE gg.[GameId] = @GameId
END
GO
/****** Object:  StoredProcedure [dbo].[Genre_GetByIds]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Genre_GetByIds]
	@Ids NVARCHAR(MAX)
AS
BEGIN
	SELECT [Id],
		   [Name]
	FROM [dbo].[Genres]
	WHERE [Id] IN
	(
		SELECT [value]
		FROM OPENJSON(@ids, N'$.ids')
	)
END
GO
/****** Object:  StoredProcedure [dbo].[Genre_GetByNamePart]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Genre_GetByNamePart]
	@KeyWord NVARCHAR(200)
AS
BEGIN
	SELECT [Id],
		   [Name]
	FROM [dbo].[Genres]
	WHERE [Name] LIKE '%' + @keyWord + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[Image_Add]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Image_Add]
	@Id              INT OUTPUT,
	@Path            NVARCHAR(50),
	@MimeType        NVARCHAR(50),
	@AlternativeText NVARCHAR(50)
AS
BEGIN
	INSERT INTO [dbo].[Images] ([Path], [MimeType], [AlternativeText])
		VALUES (@Path, @MimeType, @AlternativeText)

	SET @Id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[Image_GetById]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Image_GetById]
	@Id INT
AS
BEGIN
	SELECT [Id],
		   [Path],
		   [MimeType],
		   [AlternativeText]
	FROM [dbo].[Images]
	WHERE @Id = [Id]
END
GO
/****** Object:  StoredProcedure [dbo].[Image_Remove]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Image_Remove]
	@Id INT
AS
BEGIN
	DELETE FROM [dbo].[Images]
	WHERE @Id = [Id]
END
GO
/****** Object:  StoredProcedure [dbo].[Image_SetAvatarForGame]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Image_SetAvatarForGame]
	@ImageId INT,
	@GameId  INT
AS
BEGIN
	DECLARE @PrevImageId INT
	
	SELECT @PrevImageId = [AvatarId]
	FROM [dbo].[Games]
	WHERE [Id] = @GameId

	IF @PrevImageId IS NOT NULL
	BEGIN
		UPDATE [dbo].[Games]
			SET [AvatarId] = NULL
		WHERE [Id] = @GameId

		DELETE FROM [dbo].[Images]
		WHERE [Id] = @PrevImageId
	END

	UPDATE [dbo].[Games]
		SET [AvatarId] = @ImageId
	WHERE @GameId = [Id]
END
GO
/****** Object:  StoredProcedure [dbo].[Order_Add]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Order_Add]
	@Id          INT OUTPUT,
	@Name        NVARCHAR(50),
	@Surname     NVARCHAR(50),
	@Email       NVARCHAR(50),
	@PhoneNumber NCHAR(15),
	@GameId      INT,
	@Quantity    INT
AS
BEGIN
	INSERT INTO [dbo].[Orders] ([Name], [Surname], [Email], [PhoneNumber], [GameId], [Quantity])
		VALUES (@Name, @Surname, @Email, @PhoneNumber, @GameId, @Quantity)

	SET @Id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[Order_GetAll]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Order_GetAll]
	@PageNumber INT,
	@PageSize   INT
AS
BEGIN
	SELECT [Id],
		   [Name],
		   [Surname],
		   [Email],
		   [PhoneNumber],
		   [GameId],
		   [Quantity]
	FROM [dbo].[Orders]
	ORDER BY [Id]
	OFFSET @PageSize * (@PageNumber - 1) ROWS
    FETCH NEXT @PageSize ROWS ONLY

	
	SELECT CEILING(
	(
		SELECT COUNT(*) AS [Count]
		FROM [dbo].[Orders]
	) / CAST(@pageSize AS FLOAT))
END
GO
/****** Object:  StoredProcedure [dbo].[Role_GetAll]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Role_GetAll]
AS
BEGIN
	SELECT [Id],
		   [Name]
	FROM [dbo].[Roles]
END
GO
/****** Object:  StoredProcedure [dbo].[SystemRequirements_Add]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SystemRequirements_Add]
	@Id              INT OUTPUT,
	@GameId          INT,
	@Type            NVARCHAR(50),
	@Processor       NVARCHAR(100),
	@Memory          NVARCHAR(100),
	@OperationSystem NVARCHAR(100),
	@Graphics        NVARCHAR(100),
	@Storage         NVARCHAR(100),
	@DirectX         NVARCHAR(50)
AS
BEGIN
	INSERT INTO [dbo].[SystemRequirements] (
										       [Processor],
											   [GameId],
											   [Type],
											   [Memory],
											   [OperationSystem],
											   [Graphics],
											   [Storage],
											   [DirectX]
										   )
		VALUES (
			       @Processor,
				   @GameId,
				   @Type,
				   @Memory,
				   @OperationSystem,
				   @Graphics,
				   @Storage,
				   @DirectX
			   )

	SET @Id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[SystemRequirements_GetById]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SystemRequirements_GetById]
	@Id INT
AS
BEGIN
	SELECT [GameId],
	       [Type],
		   [Processor],
		   [Memory],
		   [OperationSystem],
		   [Graphics],
		   [DirectX],
		   [Storage]
	FROM [dbo].[SystemRequirements]
	WHERE @Id = [Id]
END
GO
/****** Object:  StoredProcedure [dbo].[SystemRequirements_Update]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SystemRequirements_Update]
	@Id              INT,
	@Processor       NVARCHAR(100),
	@Memory          NVARCHAR(100),
	@OperationSystem NVARCHAR(100),
	@Graphics        NVARCHAR(100),
	@Storage         NVARCHAR(100),
	@DirectX         NVARCHAR(50)
AS
BEGIN
	UPDATE [dbo].[SystemRequirements]
		SET [Processor] = @Processor,
			[Memory] = @Memory,
			[OperationSystem] = @OperationSystem,
			[Graphics] = @Graphics,
			[Storage] = @Storage,
			[DirectX] = @DirectX
	WHERE @Id = [Id]
END
GO
/****** Object:  StoredProcedure [dbo].[User_Add]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Add]
	@Id       INT OUTPUT,
	@Name     NVARCHAR(50),
	@Password CHAR(128)
AS
BEGIN TRAN
	IF NOT EXISTS
	(
		SELECT 1
		FROM [dbo].[Users]
		WHERE [Name] = @Name
	)
	BEGIN
		INSERT INTO [dbo].[Users] ([Name], [Password])
			VALUES (@Name, @Password)

		SET @Id = SCOPE_IDENTITY()

		COMMIT
	END
	ELSE
	BEGIN
		ROLLBACK
	END
GO
/****** Object:  StoredProcedure [dbo].[User_GetAll]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_GetAll]
	@PageNumber INT,
	@PageSize   INT
AS
BEGIN
	SELECT [Id],
		   [Name]
	FROM [dbo].[Users]
	ORDER BY [Id]
	OFFSET @PageSize * (@PageNumber - 1) ROWS
    FETCH NEXT @PageSize ROWS ONLY

	SELECT COUNT(*) AS [Count]
	FROM [dbo].[Users]
END
GO
/****** Object:  StoredProcedure [dbo].[User_IsExist]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_IsExist]
	@Name     NVARCHAR(50),
	@Password CHAR(128)
AS
BEGIN
	SELECT 1
	FROM [dbo].[Users]
	WHERE [Name] = @Name AND
		  [Password] = @Password
END
GO
/****** Object:  StoredProcedure [dbo].[User_IsLoginExists]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_IsLoginExists]
	@Name NVARCHAR(50)
AS
BEGIN
	SELECT 1
	FROM [dbo].[Users]
	WHERE [Name] = @Name
END
GO
/****** Object:  StoredProcedure [dbo].[User_Remove]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Remove]
	@Id   INT          = 0,
	@Name NVARCHAR(50) = ''
AS
BEGIN
	IF @Id <> 0
	BEGIN
		DELETE FROM [dbo].[Users]
		WHERE [Id] = @Id

		COMMIT
	END
	ELSE IF @Name <> ''
	BEGIN
		DELETE FROM [dbo].[Users]
		WHERE [Name] = @Name

		COMMIT
	END
END
GO
/****** Object:  StoredProcedure [dbo].[UserRole_AddRoleToUser]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserRole_AddRoleToUser]
	@UserName NVARCHAR(50),
	@RoleName NVARCHAR(50)
AS
SET XACT_ABORT ON
BEGIN TRAN
	DECLARE @UserId INT = [dbo].GET_USER_ID_BY_NAME(@UserName),
			@RoleId INT = [dbo].GET_ROLE_ID_BY_NAME(@RoleName)

	IF @UserId IS NULL OR @RoleId IS NULL
	BEGIN
		ROLLBACK
	END

	INSERT INTO [dbo].[UsersRoles]([UserId], [RoleId])
		VALUES (@UserId, @RoleId)
	COMMIT

GO
/****** Object:  StoredProcedure [dbo].[UserRole_GetRolesByUserLogin]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserRole_GetRolesByUserLogin]
	@UserName NVARCHAR(50)
AS
BEGIN
	SELECT r.[Name]
	FROM [dbo].[Roles] r
	INNER JOIN [dbo].[UsersRoles] ur
		ON r.[Id] = ur.[RoleId]
	INNER JOIN [dbo].[Users] u
		ON u.[Id] = ur.[UserId]
	WHERE u.[Name] = @UserName
END
GO
/****** Object:  StoredProcedure [dbo].[UserRole_IsUserInRole]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserRole_IsUserInRole]
	@UserName NVARCHAR(50),
	@RoleName NVARCHAR(50)
AS
BEGIN
	SELECT 1
	FROM [dbo].[Roles] r
	INNER JOIN [dbo].[UsersRoles] ur
		ON r.[Id] = ur.[RoleId]
	INNER JOIN [dbo].[Users] u
		ON u.[Id] = ur.[UserId]
	WHERE r.[Name] = @RoleName AND
		  u.[Name] = @UserName
END
GO
/****** Object:  StoredProcedure [dbo].[UserRole_RemoveRoleFromUser]    Script Date: 1/2/2018 8:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserRole_RemoveRoleFromUser]
	@UserName NVARCHAR(50),
	@RoleName NVARCHAR(50)
AS
SET XACT_ABORT ON
BEGIN TRAN
	DECLARE @UserId INT = [dbo].GET_USER_ID_BY_NAME(@UserName),
			@RoleId INT = [dbo].GET_ROLE_ID_BY_NAME(@RoleName),
			@Count INT

	IF @UserId IS NULL OR @RoleId IS NULL
	BEGIN
		ROLLBACK
		RETURN
	END

	SELECT @Count = COUNT(*)
	FROM [dbo].[UsersRoles]
	WHERE [RoleId] = @RoleId

	IF @Count = 1
	BEGIN
		ROLLBACK
		RETURN
	END

	DELETE FROM [dbo].[UsersRoles]
	WHERE [UserId] = @UserId AND
		  [RoleId] = @RoleId

	COMMIT
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "games"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_Game_GetAll'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_Game_GetAll'
GO
USE [master]
GO
ALTER DATABASE [GamePool] SET  READ_WRITE 
GO
