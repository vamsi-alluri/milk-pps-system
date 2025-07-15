CREATE TABLE [dbo].[Roles] (
    [ID]         INT            IDENTITY(1,1) PRIMARY KEY,
    [Name]       NVARCHAR(50)   NOT NULL UNIQUE, -- e.g., "Admin", "Manager"
    [RoleLevel]  INT            NOT NULL         -- e.g., Admin = 3
);
