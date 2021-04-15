CREATE PROCEDURE [dbo].[Account_GetUsers]

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
END
