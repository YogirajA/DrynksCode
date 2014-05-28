CREATE TABLE [dbo].[Campaign]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[MerchantId] int NOT NULL,
	[IsActive] bit NOT NULL Default 0,
	[StartDate] DateTime null,
	[EndDate] DateTime null,
	[Name] nvarchar(200) null,
	[Description] nvarchar(600) null
	CONSTRAINT [FK_Campaign_Merchant] FOREIGN KEY ([MerchantId]) REFERENCES [Merchant]([Id])
	
)

