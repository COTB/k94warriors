CREATE TABLE [dbo].[DogEvents] (
    [EventID]      INT            IDENTITY (1, 1) NOT NULL,
    [Subject]      NVARCHAR (200) NOT NULL,
    [Body]         NVARCHAR (MAX) NULL,
    [IsComplete]   BIT            NOT NULL,
    [EventDate]    DATETIME       NOT NULL,
    [DogProfileID] INT            NOT NULL,
    [EventTypeId]  INT            NOT NULL,
    CONSTRAINT [PK_dbo.DogEvents] PRIMARY KEY CLUSTERED ([EventID] ASC),
    CONSTRAINT [FK_dbo.DogEvents_dbo.DogProfiles_DogProfileID] FOREIGN KEY ([DogProfileID]) REFERENCES [dbo].[DogProfiles] ([ProfileID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.DogEvents_dbo.EventTypes_EventTypeId] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[EventTypes] ([ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DogProfileID]
    ON [dbo].[DogEvents]([DogProfileID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EventTypeId]
    ON [dbo].[DogEvents]([EventTypeId] ASC);

