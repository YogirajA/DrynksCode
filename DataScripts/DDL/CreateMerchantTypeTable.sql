CREATE TABLE [dbo].[MerchantTypeAssociation]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[MerchantId] int NOT NULL,
	[MerchantTypeId] int NOT NULL,
	CONSTRAINT [FK_MerchantTypeAssociation_Merchant] FOREIGN KEY ([MerchantId]) REFERENCES [Merchant]([Id]),
	CONSTRAINT [FK_MerchantTypeAssociation_MerchantType] FOREIGN KEY ([MerchantTypeId]) REFERENCES [MerchantType]([Id])
)