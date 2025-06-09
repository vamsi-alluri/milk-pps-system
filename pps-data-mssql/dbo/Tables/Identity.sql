CREATE TABLE [dbo].[Identity] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [Username] NVARCHAR (50) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    [Active]   BIT           NOT NULL, 
    CONSTRAINT [PK_Identity] PRIMARY KEY ([ID])
);