CREATE PROCEDURE [dbo].[Vehicle_FindVehicleByVehicleID]
	@vehicleID NVARCHAR(128)
AS
BEGIN

		SELECT 
				VehicleSeqID
				, PlateNumber
				, Brand
				, Model
				, CreatedDate
				, LastModifiedDate
		FROM 
				Vehicles 
		WHERE 
				VehicleSeqID = @vehicleID

END
