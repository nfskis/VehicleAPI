CREATE PROCEDURE [dbo].[Track_GetPage]
		 @PageNumber int,
		 @RowsOfPage int
AS
BEGIN

		SELECT 
				TrackSeqID
				, Vehicles.VehicleSeqID AS VehicleSeqID
				, Vehicles.PlateNumber AS PlateNumber
				, Latitude
				, Longitude
				, Tracks.CreatedDate AS CreatedDate
		FROM 
				Tracks, Vehicles
		WHERE 
				Tracks.VehicleSeqID = Vehicles.VehicleSeqID
		ORDER BY 
				CreatedDate
		OFFSET (@PageNumber-1) * @RowsOfPage ROWS
		FETCH NEXT @RowsOfPage ROWS ONLY

END