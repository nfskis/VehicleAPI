CREATE PROCEDURE [dbo].[Vehicle_GetPage]
		 @PageNumber int
		 , @RowsOfPage int
AS
BEGIN

		SELECT 
				VehicleSeqID
				, UserSeqID
				, PlateNumber
				, Brand
				, Model
				, CreatedDate		
		FROM 
				Vehicles
		ORDER BY 
				CreatedDate
		OFFSET (@PageNumber-1) * @RowsOfPage ROWS
		FETCH NEXT @RowsOfPage ROWS ONLY

END

