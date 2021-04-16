CREATE PROCEDURE [dbo].[deleteVehicle]
	@PlateNumber NVARCHAR(128)
AS
BEGIN
		DELETE Vehicles
		WHERE PlateNumber = @PlateNumber
END