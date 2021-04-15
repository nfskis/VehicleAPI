CREATE PROCEDURE [dbo].[Vehicle_GetAllVehicles]
AS
BEGIN

	SELECT 
		VehicleSeqID, PlateNumber, Brand, Model, CreatedDate		
	FROM 
		Vehicles

END
