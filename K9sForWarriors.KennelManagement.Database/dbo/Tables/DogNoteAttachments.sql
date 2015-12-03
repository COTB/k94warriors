CREATE TABLE [dbo].[DogNoteAttachments] (
    [DogNoteAttachmentID] INT              IDENTITY (1, 1) NOT NULL,
    [BlobKey]             UNIQUEIDENTIFIER NOT NULL,
    [MimeType]            NVARCHAR (MAX)   NULL,
    [DogNoteID]           INT              NULL,
    [FileExtension]       NVARCHAR (MAX)   NULL,
    [FileName]            NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_dbo.DogNoteAttachments] PRIMARY KEY CLUSTERED ([DogNoteAttachmentID] ASC),
    CONSTRAINT [FK_dbo.DogNoteAttachments_dbo.DogNotes_DogNoteID] FOREIGN KEY ([DogNoteID]) REFERENCES [dbo].[DogNotes] ([NoteID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DogNoteID]
    ON [dbo].[DogNoteAttachments]([DogNoteID] ASC);

