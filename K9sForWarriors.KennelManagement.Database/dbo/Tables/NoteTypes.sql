CREATE TABLE [dbo].[NoteTypes] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_dbo.NoteTypes] PRIMARY KEY CLUSTERED ([ID] ASC)
);

