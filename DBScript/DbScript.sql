/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2019 (15.0.4382)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2019
    Target Database Engine Edition : Microsoft SQL Server Express Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [UserDB]
GO
/****** Object:  StoredProcedure [dbo].[SP_UserAccount]    Script Date: 2/2/2025 9:37:08 AM ******/
DROP PROCEDURE IF EXISTS [dbo].[SP_UserAccount]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAccount]') AND type in (N'U'))
ALTER TABLE [dbo].[UserAccount] DROP CONSTRAINT IF EXISTS [DF_Users_ModifiedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAccount]') AND type in (N'U'))
ALTER TABLE [dbo].[UserAccount] DROP CONSTRAINT IF EXISTS [DF_Users_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAccount]') AND type in (N'U'))
ALTER TABLE [dbo].[UserAccount] DROP CONSTRAINT IF EXISTS [DF_Users_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAccount]') AND type in (N'U'))
ALTER TABLE [dbo].[UserAccount] DROP CONSTRAINT IF EXISTS [DF_Users_LoingDateTime]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAccount]') AND type in (N'U'))
ALTER TABLE [dbo].[UserAccount] DROP CONSTRAINT IF EXISTS [DF_Users_IsLoggedIn]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAccount]') AND type in (N'U'))
ALTER TABLE [dbo].[UserAccount] DROP CONSTRAINT IF EXISTS [DF_Users_DynamicsCredits]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 2/2/2025 9:37:08 AM ******/
DROP TABLE IF EXISTS [dbo].[UserAccount]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 2/2/2025 9:37:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
	[AccountNumber] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Password] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DynamicsCredits] [int] NULL,
	[IsLoggedIn] [bit] NULL,
	[LoginDateTime] [datetime] NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[AccountNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[UserAccount] TO  SCHEMA OWNER 
GO
ALTER TABLE [dbo].[UserAccount] ADD  CONSTRAINT [DF_Users_DynamicsCredits]  DEFAULT ((0)) FOR [DynamicsCredits]
GO
ALTER TABLE [dbo].[UserAccount] ADD  CONSTRAINT [DF_Users_IsLoggedIn]  DEFAULT ((0)) FOR [IsLoggedIn]
GO
ALTER TABLE [dbo].[UserAccount] ADD  CONSTRAINT [DF_Users_LoingDateTime]  DEFAULT (NULL) FOR [LoginDateTime]
GO
ALTER TABLE [dbo].[UserAccount] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserAccount] ADD  CONSTRAINT [DF_Users_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserAccount] ADD  CONSTRAINT [DF_Users_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
/****** Object:  StoredProcedure [dbo].[SP_UserAccount]    Script Date: 2/2/2025 9:37:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SP_UserAccount] 
(
	@Mode				VARCHAR(100) = 'GetUserById',
	@AccountNumber		INT = 0,
	@UserName			VARCHAR(255) = '',
	@Password			VARCHAR(MAX) = '',
	@DynamicsCredits	INT = 0,
	@IsLoggedIn			BIT = 0,
	@LoginDateTime		DATETIME = NULL,
	@IsActive			BIT = 1,
	@CreatedDate		DATETIME = NULL,
	@ModifiedDate		DATETIME = NULL
)
AS
BEGIN
	/*
		EXEC [SP_UserAccount] @Mode='LoginUser', @AccountNumber=1
	*/
	SET NOCOUNT ON;
	IF @Mode = 'RegisterUser'
	BEGIN
		IF EXISTS (SELECT AccountNumber FROM UserAccount WHERE UserName = @UserName)
		BEGIN
			SELECT 0 AS ReturnVal;
		END
		ELSE
		BEGIN
			INSERT INTO UserAccount 
			(
				UserName, 
				[Password], 
				DynamicsCredits, 
				IsLoggedIn, 
				LoginDateTime, 
				IsActive
			)
			VALUES 
			(
				@UserName, 
				@Password,
				@DynamicsCredits, 
				@IsLoggedIn, 
				GETDATE(), 
				@IsActive
			)

			SELECT SCOPE_IDENTITY() AS ReturnVal;
		END
	END
	IF @Mode = 'AddDynamicsCredit'
	BEGIN
		IF EXISTS (SELECT AccountNumber FROM UserAccount WHERE AccountNumber = @AccountNumber)
		BEGIN
			UPDATE UserAccount
				SET DynamicsCredits = DynamicsCredits + 1
			WHERE AccountNumber = @AccountNumber;

			SELECT DynamicsCredits AS ReturnVal
			FROM UserAccount
			WHERE AccountNumber = @AccountNumber;
		END
		ELSE
		BEGIN
			SELECT 0 AS ReturnVal;
		END
	END
	IF @Mode = 'GetUserByUsername'
	BEGIN
		SELECT AccountNumber, UserName, [Password], DynamicsCredits, IsLoggedIn, LoginDateTime, IsActive
		FROM UserAccount
		WHERE UserName = @UserName;
	END
	IF @Mode = 'UpdateIsLoggedIn'
	BEGIN
		UPDATE UserAccount
			SET IsLoggedIn = 1
		WHERE AccountNumber = @AccountNumber;
		
		SELECT @AccountNumber AS ReturnVal;
	END
	IF @Mode = 'LogoutUser'
	BEGIN
		UPDATE UserAccount
			SET IsLoggedIn = 0
		WHERE AccountNumber = @AccountNumber;
		
		SELECT @AccountNumber AS ReturnVal;
	END
	IF @Mode = 'GetUserById'
	BEGIN
		SELECT AccountNumber, UserName, DynamicsCredits, IsLoggedIn
		FROM UserAccount
		WHERE AccountNumber = @AccountNumber
	END
END
GO
ALTER AUTHORIZATION ON [dbo].[SP_UserAccount] TO  SCHEMA OWNER 
GO
