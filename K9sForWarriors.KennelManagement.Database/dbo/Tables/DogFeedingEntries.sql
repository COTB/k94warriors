CREATE TABLE [dbo].[DogFeedingEntries] (
    [DogFeedingEntryID] INT            IDENTITY (1, 1) NOT NULL,
    [DogProfileID]      INT            NOT NULL,
    [AmountDescription] NVARCHAR (100) NOT NULL,
    [FoodName]          NVARCHAR (100) NOT NULL,
    [AMFeeding]         BIT            NOT NULL,
    [NoonFeeding]       BIT            NOT NULL,
    [PMFeeding]         BIT            NOT NULL,
    [Notes]             NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.DogFeedingEntries] PRIMARY KEY CLUSTERED ([DogFeedingEntryID] ASC),
    CONSTRAINT [FK_dbo.DogFeedingEntries_dbo.DogProfiles_DogProfileID] FOREIGN KEY ([DogProfileID]) REFERENCES [dbo].[DogProfiles] ([ProfileID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DogProfileID]
    ON [dbo].[DogFeedingEntries]([DogProfileID] ASC);

