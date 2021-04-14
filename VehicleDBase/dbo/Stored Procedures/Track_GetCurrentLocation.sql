CREATE PROCEDURE [dbo].[Track_GetCurrentLocation]
	@vehicleSeqID NVARCHAR(128)

AS
BEGIN

    SELECT TOP 1
        Vehicles.VehicleSeqID as VehicleSeqID,
        Vehicles.PlateNumber as PlateNumber,
        Tracks.TrackSeqID as TrackSeqID,
        Tracks.Latitude as Latitude,
        Tracks.Longitude as Longitude,   
        Tracks.CreatedDate as CreatedDate
    FROM Vehicles
    INNER JOIN Tracks ON Tracks.VehicleSeqID = Vehicles.VehicleSeqID
    WHERE Vehicles.VehicleSeqID = @vehicleSeqID
    ORDER BY Tracks.CreatedDate DESC

END