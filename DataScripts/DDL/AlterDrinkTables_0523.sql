Alter table [dbo].[User]
   Add TwitterPicUrl varchar(max) null

Alter table [dbo].[DrinkUser]
   Add UserNotes varchar(max) null


Alter table [dbo].[Drink]
   Add AlcoholContent decimal(5,2) null

Alter table [dbo].[Drink]
   Add StandardPicUrl varchar(max) null