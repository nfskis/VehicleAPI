CREATE PROCEDURE [dbo].[Track_UpdateTrack]
	@TrackSeqID NVARCHAR(128),	
	@Latitude float,
	@Longitude float,
	@CreatedDate DateTime
AS
		UPDATE Tracks
		SET Latitude = @Latitude, 
				Longitude = @Longitude, 
				CreatedDate = @CreatedDate
		WHERE TrackSeqID = @TrackSeqID
RETURN 0
