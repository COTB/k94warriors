CREATE TABLE [dbo].[EventTypes] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_dbo.EventTypes] PRIMARY KEY CLUSTERED ([ID] ASC)
);

