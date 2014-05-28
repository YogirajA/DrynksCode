--select * from UserProfile
--select * from [User]
--select D.* from Drink D
--inner join DrinkUser DU on D.Id = DU.DrinkId
--inner join [User] U on DU.UserId = DU.UserId
--where U.DeviceId = 2

Alter Table [User]
	Add [UserProfileId] int not null

Alter Table [User]
	ADD CONSTRAINT fk_user_userprofile FOREIGN KEY(UserProfileId)REFERENCES UserProfile(UserId)

Alter Table [User]
	Drop column UserName
