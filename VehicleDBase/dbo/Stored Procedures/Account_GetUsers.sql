CREATE PROCEDURE [dbo].[Account_GetUsers]
		@PageNumber int,
		@RowsOfPage int
AS
BEGIN

		SELECT 
				UserSeqID
				,FirstName
				,LastName
				,Email
				,RoleID
		FROM 
				Users
		ORDER BY 
				CreatedDate
		OFFSET (@PageNumber-1) * @RowsOfPage ROWS
		FETCH NEXT @RowsOfPage ROWS ONLY

END
