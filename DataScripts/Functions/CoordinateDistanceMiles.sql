CREATE FUNCTION CoordinateDistanceMiles(
	@Latitude1 float,
	@Longitude1 float,
	@Latitude2 float,
	@Longitude2 float
)
RETURNS float 
AS  
BEGIN 
	-- CONSTANTS
	DECLARE @EarthRadiusInMiles float;
	SET @EarthRadiusInMiles = 3963.1
	DECLARE @PI  float;
	SET @PI = PI();
	-- RADIANS conversion
	DECLARE @lat1Radians float;
	DECLARE @long1Radians float;
	DECLARE @lat2Radians float;
	DECLARE @long2Radians float;
	SET @lat1Radians = @Latitude1 * @PI / 180;
	SET @long1Radians = @Longitude1 * @PI / 180;
	SET @lat2Radians = @Latitude2 * @PI / 180;
	SET @long2Radians = @Longitude2 * @PI / 180;
	RETURN Acos(
	Cos(@lat1Radians) * Cos(@long1Radians) * Cos(@lat2Radians) * Cos(@long2Radians) + 
	Cos(@lat1Radians) * Sin(@long1Radians) * Cos(@lat2Radians) * Sin(@long2Radians) + 
	Sin(@lat1Radians) * Sin(@lat2Radians)
	) * @EarthRadiusInMiles;
END