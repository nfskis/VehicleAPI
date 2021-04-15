CREATE PROCEDURE [dbo].[Account_UpdateUser]
		@FirstName			 NVARCHAR (50) ,
		@LastName				 NVARCHAR (50) ,
		@Email					 NVARCHAR (256),
		@Password				 NVARCHAR (128),
		@RoleID					 tinyint
AS
BEGIN

		DECLARE
				@UserSeqID NVARCHAR(128)

		SET @UserSeqID = (SELECT UserSeqID FROM Users WHERE Email = @Email)

		UPDATE
				Users
		SET 
				FirstName = @FirstName,
				LastName = @LastName,
				Email		= @Email,
				Password = @Password,
				RoleID = @RoleID,
				LastModifiedDate = GETDATE()
		WHERE 
				UserSeqID = @UserSeqID

END