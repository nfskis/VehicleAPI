CREATE PROCEDURE [dbo].[Account_DeleteUser]
		@Email			 NVARCHAR (128)

AS
BEGIN


		DECLARE
				@UserSeqID NVARCHAR(128)

		SET @UserSeqID = (SELECT UserSeqID FROM Users WHERE Email = @Email)


		DELETE Users
		WHERE UserSeqID = @userSeqID 

END
