CREATE TABLE [dbo].[Vehicles]
(
	[VehicleSeqID] NVARCHAR(100) NOT NULL PRIMARY KEY, 
    [PlateNumber] NVARCHAR(50) NOT NULL, 
    [Brand] NVARCHAR(50) NOT NULL, 
    [Model] NVARCHAR(50) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL DEFAULT GetDate(), 
    [LastModifiedDate] DATETIME NOT NULL DEFAULT getDate(), 
)
