CREATE TABLE [dbo].[Users]
(
	[UserSeqID] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(256) NOT NULL, 
    [Password] NVARCHAR(128) NOT NULL, 
    [RoleID] TINYINT  NOT NULL, 
    [CreatedDate] DATETIME NOT NULL DEFAULT GetDate(), 
    [LastModifiedDate] DATETIME NOT NULL DEFAULT GetDate(), 
)
