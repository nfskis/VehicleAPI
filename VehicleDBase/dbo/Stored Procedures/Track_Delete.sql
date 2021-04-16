CREATE PROCEDURE [dbo].[Track_Delete]
	@TrackSeqID NVARCHAR(128)
	
AS
BEGIN
		DELETE Tracks
		WHERE @TrackSeqID = @TrackSeqID 
END
