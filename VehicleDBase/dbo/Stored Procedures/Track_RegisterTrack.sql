CREATE PROCEDURE [dbo].[Track_RegisterTrack]
		@TrackSeqID NVARCHAR(128), 
		@VehicleSeqID NVARCHAR(128), 
		@Latitude float, 
		@Longitude float
AS
BEGIN

				INSERT INTO Tracks(
						TrackSeqID
						,VehicleSeqID 
						,Latitude
						,Longitude)
				VALUES(
						@TrackSeqID 
						,@VehicleSeqID 
						,@Latitude 
						,@Longitude)

END
