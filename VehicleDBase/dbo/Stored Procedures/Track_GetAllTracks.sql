CREATE PROCEDURE [dbo].[Track_GetAllTracks]
AS
BEGIN
		SELECT 
				TrackSeqID
				, Vehicles.VehicleSeqID AS VehicleSeqID
				, Vehicles.PlateNumber AS PlateNumber
				, Latitude
				, Longitude
		FROM 
				Tracks, Vehicles
		WHERE 
				Tracks.VehicleSeqID = Vehicles.VehicleSeqID

END