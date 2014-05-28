if NOT exists(select 1 from sys.columns 
            where Name = N'Share' and Object_ID = Object_ID(N'User'))    
begin
    Alter Table [User]
		Add Share bit not null default 1

end

if NOT exists(select 1 from sys.columns 
            where Name = N'Longitude' and Object_ID = Object_ID(N'Merchant'))    
begin
	Alter Table Merchant
		Add Longitude Decimal(18,6) null
end


if NOT exists(select 1 from sys.columns 
            where Name = N'Latitude' and Object_ID = Object_ID(N'Merchant'))    
begin
	Alter Table Merchant
		Add Latitude Decimal(18,6) null

end
