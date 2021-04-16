CREATE PROCEDURE [dbo].[UpdateVehicle]
		@PlateNumber NVARCHAR(50)
	, @Brand NVARCHAR(50)
	, @Model NVARCHAR(50)
AS
BEGiN

		UPDATE 
				Vehicles
		SET 
				Brand = @Brand,
				Model = @Model,
				LastModifiedDate = GetDate()
		WHERE 
				PlateNumber = @PlateNumber

END

