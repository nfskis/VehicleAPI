CREATE PROCEDURE [dbo].[Track_UpdateTrack]
	@TrackSeqID NVARCHAR(128),	
	@Latitude float,
	@Longitude float
AS
BEGIN
		UPDATE Tracks
		SET Latitude = @Latitude, 
				Longitude = @Longitude, 
				LastModifiedDate = GETDATE()
		WHERE 
				TrackSeqID = @TrackSeqID
END
