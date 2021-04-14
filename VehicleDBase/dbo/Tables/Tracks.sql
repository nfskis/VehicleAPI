CREATE TABLE [dbo].[Tracks]
(
	[TrackSeqID] NVARCHAR(128) NOT NULL, 
	[VehicleSeqID] NVARCHAR(128) NOT NULL , 
    [Latitude] FLOAT NOT NULL, 
    [Longitude] FLOAT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL DEFAULT Getdate(), 
    PRIMARY KEY ([TrackSeqID])
)
