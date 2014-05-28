Alter table [dbo].[User]
   Add TwitterPicUrl varchar(max) null

Alter table [dbo].[DrinkUser]
   Add UserNotes varchar(max) null

Alter table [dbo].[DrinkUser]
   Add UserRating bit not null Default 1

Alter table [dbo].[DrinkUser]
   Add UserUploadedPictureUrl varchar(max) null

