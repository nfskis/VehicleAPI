CREATE PROCEDURE [dbo].[Vehicle_GetAllVehicles]
AS
BEGIN

	SELECT 
		VehicleSeqID, UserSeqID, PlateNumber, Brand, Model, CreatedDate		
	FROM 
		Vehicles

END
