Use DrynksDB
GO
IF (EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'DrinkUser'))
BEGIN
    Drop Table dbo.[DrinkUser]
END

IF (EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'UserTag'))
BEGIN
    Drop Table dbo.UserTag
END

IF (EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'DrinkPrice'))
BEGIN
    Drop Table dbo.DrinkPrice
END

IF (EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'DrinkTag'))
BEGIN
    Drop Table dbo.DrinkTag
END
IF (EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'MerchantDrink'))
BEGIN
    Drop Table dbo.[MerchantDrink]
END

IF (EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Drink'))
BEGIN
    Drop Table dbo.Drink
END

IF (EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'User'))
BEGIN
    Drop Table dbo.[User]
END

IF (EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'DrinkSize'))
BEGIN
    Drop Table dbo.DrinkSize
END

IF (EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Tag'))
BEGIN
    Drop Table dbo.Tag
END

IF (EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Campaign'))
BEGIN
    Drop Table dbo.[Campaign]
END

IF (EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Merchant'))
BEGIN
    Drop Table dbo.Merchant
END
--IF (EXISTS (SELECT 1 
--                 FROM INFORMATION_SCHEMA.TABLES 
--                 WHERE TABLE_SCHEMA = 'dbo' 
--                 AND  TABLE_NAME = 'MerchantProfile'))
--BEGIN
--    Drop Table dbo.[MerchantProfile]
--END


	CREATE TABLE [dbo].[Drink]
	(
		[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
		[DrinkName] nvarchar(400) NULL, 
		[DrinkDescription] nvarchar(MAX) NULL,
		[FoundAt] nvarchar(Max) NULL,
		[Origin] nvarchar(Max) NULL,
		[ImageSmall] nvarchar(max) null,
		[ImageLarge] nvarchar(max) null,
		[ImageMedium] nvarchar(max) null,
		[CreateDt] DateTime,
		[UpdateDt] DateTime

	)


	CREATE TABLE [dbo].[User]
	(
		[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
		[UserName] nvarchar(100) NULL, 
		[FirstName] nvarchar(200) NULL, 
		[LastName] nvarchar(200) NULL, 
		[MiddleInitial] nvarchar(20) NULL,
		[DeviceId] nvarchar(200) NULL,
		[TwitterId] nvarchar(200) NULL,
		[FacebookId] nvarchar(200) NULL, 
		[CreateDt] DateTime,
		[UpdateDt] DateTime
	)


	CREATE TABLE [dbo].[Tag]
	(
		[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
		[TagName] nvarchar(200) null,
		[TagDescription] nvarchar(max) null,
		[IsAdminGenerated] bit DEFAULT 0,
		[CreateDt] DateTime,
		[UpdateDt] DateTime
	)



	CREATE TABLE [dbo].[DrinkPrice]
	(
		[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
		[DrinkId] INT NOT NULL,
		[Size] nvarchar(400) NULL,		
		RetailPrice DECIMAL(10,4),
		[CreateDt] DateTime,
		[UpdateDt] DateTime
		CONSTRAINT [FK_DrinkPriceForSize_Drinks] FOREIGN KEY ([DrinkId]) REFERENCES [Drink]([Id])
	 		
	)

	CREATE TABLE [dbo].[Merchant]
	(
		[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
		[MerchantName] nvarchar(200) null,
		[ProfileName] nvarchar(200) null,
		[Description] nvarchar(max) null,
		[VenueType] nvarchar(max) null,
		[ShortVenueInformation] nvarchar(500) null,
		[Location] nvarchar(max) null,
		[AddressInfo] nvarchar(max) null,
		[City] nvarchar(200) null,
		[State] nvarchar(50) null,
		[Zip] nvarchar(40) null,
		[TwitterHandle] nvarchar(max) null,
		[ImageSmall] nvarchar(max) null,
		[ImageLarge] nvarchar(max) null,
		[ImageMedium] nvarchar(max) null,
		[Website] nvarchar(max) null,
		[PrimaryContact] nvarchar(100) null,
		[ContactNumber] nvarchar(100) null,
		[CustomerNumber] nvarchar(100) null,
		[CreateDt] DateTime,
		[UpdateDt] DateTime
	)
	
	CREATE TABLE [dbo].[MerchantDrink]
	(
		[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
		[MerchantId] int NOT NULL,
		[DrinkId] int NOT NULL,
		IsSignature bit,
		CONSTRAINT [FK_MerchantDrink_Merchant] FOREIGN KEY ([MerchantId]) REFERENCES [Merchant]([Id]),
		CONSTRAINT [FK_MerchantDrink_Drink] FOREIGN KEY ([DrinkId]) REFERENCES [Drink]([Id])

    )

	CREATE TABLE [dbo].[DrinkUser]
	(
		[Id] INT IDENTITY(1,1) NOT NULL, 
		[DrinkId] INT NOT NULL , 
		[UserId] INT NOT NULL, 
		[IsLiked] bit DEFAULT 0,
		[CreateDt] DateTime,
		[UpdateDt] DateTime
		CONSTRAINT [PK_Table] PRIMARY KEY ([Id]), 
		CONSTRAINT [FK_DrinksUsers_Drink] FOREIGN KEY ([DrinkId]) REFERENCES [Drink]([Id]),
		CONSTRAINT [FK_DrinksUsers_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) 
	)

	CREATE TABLE [dbo].[UserTag]
	(
		[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
		[UserId] int NOT NULL,
		[TagId] int NOT NULL,
		[CreateDt] DateTime,
		[UpdateDt] DateTime
		CONSTRAINT [FK_UserTag_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
		CONSTRAINT [FK_UserTag_Tag] FOREIGN KEY ([TagId]) REFERENCES [Tag]([Id])
	
	)

	CREATE TABLE [dbo].[DrinkTag]
	(
		[DrinkTagId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
		[DrinkId] int NOT NULL,
		[TagId] int NOT NULL,
		[CreateDt] DateTime,
		[UpdateDt] DateTime
		CONSTRAINT [FK_DrinkTag_Drinks] FOREIGN KEY ([DrinkId]) REFERENCES [Drink]([Id]),
		CONSTRAINT [FK_DrinkTag_Tag] FOREIGN KEY ([TagId]) REFERENCES [Tag]([Id])
	)

	CREATE TABLE [dbo].[Campaign]
	(
		[CampaignId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
		[MerchantDrinkId] int not  null,
		[Name] nvarchar(200) null,
		[Description] nvarchar(max) null,
		[ExpiresOn] DateTime,
		[CreateDt] DateTime,
		[UpdateDt] DateTime
		CONSTRAINT [FK_Campaign_MerchantDrink] FOREIGN KEY ([MerchantDrinkId]) REFERENCES [MerchantDrink]([Id])
	)