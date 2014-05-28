Alter Table Merchant 
	Add IsPremierMerchant Bit NOT NULL Default 0	

IF (EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'MerchantType'))
BEGIN
    Drop Table dbo.MerchantType
END


CREATE TABLE [dbo].[MerchantType]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[TypeDescription] nvarchar(255) null
	
)

sp_RENAME 'MerchantType.MerchantType', 'TypeDescription' , 'COLUMN'