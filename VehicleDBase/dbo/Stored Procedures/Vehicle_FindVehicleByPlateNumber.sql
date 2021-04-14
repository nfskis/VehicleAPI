CREATE PROCEDURE [dbo].[Vehicle_FindVehicleByPlateNumber]
	@PlateNumber NVARCHAR(128)
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
				PlateNumber = @PlateNumber

END
