-- ==========================================================
-- Create Stored Procedure Template for SQL Azure Database
-- ==========================================================
if exists (select 1 from dbo.sysobjects where id = object_id(N'[dbo].[SpGetDrinksByUserIdAndType]') 
		and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	Drop procedure SpGetDrinksByUserIdAndType
End
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yogi>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE SpGetDrinksByUserIdAndType 
	-- Add the parameters for the stored procedure here
	@UserId int,
	@TagName nvarchar(200),
	@startRow int,
	@endRow int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	   Declare @Test Table(
		DrinkTagId int,
		TagId int,
		DrinkId int,
		TagName nvarchar(200)
	)

	insert into @Test
	select DT.DrinkTagId,DT.TagId, DT.DrinkId,T.TagName from DrinkTag DT
	inner join Tag T on DT.TagId = T.Id
	where @TagName='All' or T.TagName = @TagName;

	With seq as( select T.TagName, D.*, DU.IsLiked,DU.UserNotes, ROW_NUMBER() OVER (ORDER BY D.ID ASC) RowNumber
										,Count(*) OVER () Total from Drink D
							inner join DrinkUser DU on D.Id = DU.DrinkId
							inner join @Test T on D.Id = T.DrinkId
							where DU.UserId =  @UserId)
	Select * from seq 
	WHERE seq.RowNumber BETWEEN @startRow AND @endRow
	ORDER BY seq.RowNumber
END
GO
