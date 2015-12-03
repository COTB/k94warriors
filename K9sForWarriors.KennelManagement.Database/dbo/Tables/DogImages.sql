CREATE TABLE [dbo].[DogImages] (
    [DogImageID]   INT              IDENTITY (1, 1) NOT NULL,
    [DogProfileID] INT              NOT NULL,
    [BlobKey]      UNIQUEIDENTIFIER NOT NULL,
    [MimeType]     NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_dbo.DogImages] PRIMARY KEY CLUSTERED ([DogImageID] ASC),
    CONSTRAINT [FK_dbo.DogImages_dbo.DogProfiles_DogProfileID] FOREIGN KEY ([DogProfileID]) REFERENCES [dbo].[DogProfiles] ([ProfileID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DogProfileID]
    ON [dbo].[DogImages]([DogProfileID] ASC);

