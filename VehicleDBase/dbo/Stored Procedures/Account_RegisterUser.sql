CREATE PROCEDURE [dbo].[Account_RegisterUser]
		@UserSeqID			 NVARCHAR (128),
		@FirstName			 NVARCHAR (50) ,
		@LastName				 NVARCHAR (50) ,
		@Email					 NVARCHAR (256),
		@Password				 NVARCHAR (128),
		@RoleID					 tinyint
AS
begin
	
	INSERT INTO Users
		(UserSeqID
		,FirstName
		,LastName
		,Email
		,Password
		,RoleID)
  VALUES
		(@UserSeqID
		,@FirstName
		,@LastName
		,@Email
		,@Password
		,@RoleID)

end
