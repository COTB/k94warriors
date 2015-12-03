CREATE TABLE [dbo].[DogNotes] (
    [NoteID]          INT            IDENTITY (1, 1) NOT NULL,
    [DogProfileID]    INT            NOT NULL,
    [Note]            NVARCHAR (MAX) NULL,
    [IsCritical]      BIT            NOT NULL,
    [NoteTypeId]      INT            NOT NULL,
    [CreatedDate]     DATETIME       NOT NULL,
    [CreatedByUserId] INT            NOT NULL,
    CONSTRAINT [PK_dbo.DogNotes] PRIMARY KEY CLUSTERED ([NoteID] ASC),
    CONSTRAINT [FK_dbo.DogNotes_dbo.DogProfiles_DogProfileID] FOREIGN KEY ([DogProfileID]) REFERENCES [dbo].[DogProfiles] ([ProfileID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.DogNotes_dbo.NoteTypes_NoteTypeId] FOREIGN KEY ([NoteTypeId]) REFERENCES [dbo].[NoteTypes] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.DogNotes_dbo.Users_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DogProfileID]
    ON [dbo].[DogNotes]([DogProfileID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NoteTypeId]
    ON [dbo].[DogNotes]([NoteTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedByUserId]
    ON [dbo].[DogNotes]([CreatedByUserId] ASC);

