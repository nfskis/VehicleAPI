CREATE PROCEDURE [dbo].[Account_DeleteUser]
		@UserSeqID			 NVARCHAR (128)

AS
BEGIN

		DELETE Users
		WHERE UserSeqID = @userSeqID 

END
