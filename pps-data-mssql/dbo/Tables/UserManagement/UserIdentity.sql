CREATE TABLE [dbo].[UserIdentity] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [Username]        NVARCHAR (50) NOT NULL,
    [PasswordHash]    NVARCHAR (150) NOT NULL,
    [Active]          BIT           NOT NULL,
    [RoleID]          INT           NOT NULL,
    [LastUpdated]     DATETIME      NOT NULL DEFAULT GETDATE(),
    [Created]         DATETIME      NOT NULL DEFAULT GETDATE(),
    [IsPasswordStale] BIT           NOT NULL DEFAULT 0,
    CONSTRAINT [PK_UserIdentity] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_UserIdentity_Roles] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles]([ID])
);
