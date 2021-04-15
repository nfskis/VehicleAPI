CREATE PROCEDURE [dbo].[Account_LoginUser]
	  @Email            NVARCHAR (256),
		@Password					NVARCHAR (128)

AS
Begin	
		SELECT	UserSeqID
						,FirstName
						,LastName
						,Email, 
						UserRoles.TypeName AS Role		
    FROM   
						Users, UserRoles
    WHERE  
						users.RoleID = UserRoles.RoleID 
						AND Email = @Email 
						AND Password = @Password
End