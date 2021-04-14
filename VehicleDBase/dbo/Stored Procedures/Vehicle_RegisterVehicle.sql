CREATE PROCEDURE [dbo].[Vehicle_RegisterVehicle]
	@VehicleSeqID NVARCHAR(128)
	, @PlateNumber NVARCHAR(50)
	, @Brand NVARCHAR(50)
	, @Model NVARCHAR(50)
AS
BEGIN

		INSERT INTO Vehicles(
						VehicleSeqID
						, PlateNumber
						, Brand
						, Model)
		VALUES (@VehicleSeqID
						, @PlateNumber
						, @Brand
						, @Model)

END
