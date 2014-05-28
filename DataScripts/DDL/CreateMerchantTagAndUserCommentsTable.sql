--Drop Table MerchantTag

CREATE TABLE [dbo].[MerchantTag](
		 [Id] [int] IDENTITY(1,1) NOT NULL Primary Key
		,[MerchantId] int NOT NULL
		,[TagId] int NOT NULL
	    ,constraint FK_MerchantTag_Merchant
		foreign key (MerchantId) references Merchant(Id)
	    ,constraint FK_MerchantTag_Tag
		foreign key (TagId) references Tag(Id)
	) 


CREATE TABLE [dbo].[UserMerchantComment](
		 [Id] [int] IDENTITY(1,1) NOT NULL Primary Key
		 ,[UserId] int null
		 ,[MerchantId] int null
		 ,[Comment] nvarchar(500) null
		 ,IsApproved bit not null Default 1
		 ,IsShared bit not null Default 1
		 ,CreateDate DateTime null
		 ,UpdateDate DateTime null
    )
