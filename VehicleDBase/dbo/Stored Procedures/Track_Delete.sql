CREATE PROCEDURE [dbo].[Track_Delete]
	@TrackSeqID NVARCHAR(128)
	
AS
BEGIN
		DELETE Users
		WHERE @TrackSeqID = @TrackSeqID 
END
