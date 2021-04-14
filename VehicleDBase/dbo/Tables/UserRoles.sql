CREATE TABLE [dbo].[UserRoles]
(
    [RoleID] TINYINT       NOT NULL,
    [TypeName]   NVARCHAR (50) NOT NULL,
    [RoleGroup]   NVARCHAR (50) NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GetDate(), 
    PRIMARY KEY CLUSTERED ([RoleID] ASC)
)
