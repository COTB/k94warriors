CREATE TABLE [dbo].[Locations] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_dbo.Locations] PRIMARY KEY CLUSTERED ([ID] ASC)
);

