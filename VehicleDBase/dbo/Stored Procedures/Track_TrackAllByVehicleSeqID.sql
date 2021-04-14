CREATE PROCEDURE [dbo].[Track_TrackAllByVehicleSeqID]
	@vehicleSeqID NVARCHAR(128)
AS
BEGIN
		SELECT 
				Vehicles.VehicleSeqID as VehicleSeqID,
				Vehicles.PlateNumber as PlateNumber,
				Tracks.Latitude as Latitude,
				Tracks.Longitude as Longitude,   
				Tracks.CreatedDate as CreatedDate
		FROM Vehicles
		INNER JOIN Tracks ON Tracks.VehicleSeqID = Vehicles.VehicleSeqID
		WHERE Vehicles.VehicleSeqID = @vehicleSeqID
		ORDER BY CreatedDate DESC
END;
