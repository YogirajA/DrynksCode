Declare @setDate datetime

begin tran
select @setDate = GetDate()
Insert into Drink
(
DrinkName,
DrinkDescription,
FoundAt,
Origin,
CreateDt,
UpdateDt)
Select "Cabarnet","Red wine","Everywhere","France",@setDate,@setDate
union
Select "Red Stripe","Awesome beer","Everywhere","Jamaica",@setDate,@setDate
union
Select "Chianti","Red wine","Everywhere","Italy",@setDate,@setDate
union
Select "Sangiovese","Red wine","Everywhere","Italy",@setDate,@setDate
union
Select "Corona","Another good beer","Everywhere","Mexico",@setDate,@setDate

Insert into dbo.[User]
(
UserName,
FirstName,
LastName,
MiddleInitial,
CreateDt,
UpdateDt)
Select "JaneDOE","Jane","Doe","M",@setDate,@setDate
union
Select "JohnSmith","John","Smith","L",@setDate,@setDate
union
Select "JoeSmith","Joe","Smith","N",@setDate,@setDate


Insert into DrinkUser
(DrinkId,
UserId,
IsLiked,
CreateDt,
UpdateDt)
Select 1,1,1,@setDate,@setDate
union
Select 2,2,1,@setDate,@setDate
union
Select 3,3,1,@setDate,@setDate
union
Select 2,1,0,@setDate,@setDate

rollback
--commit



