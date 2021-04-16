CREATE PROCEDURE [dbo].[Track_GetAllTracks]
AS
BEGIN
		SELECT 
				TrackSeqID
				, VehicleSeqID
				, Latitude
				, Longitude
				, CreatedDate
		FROM 
				Tracks
END