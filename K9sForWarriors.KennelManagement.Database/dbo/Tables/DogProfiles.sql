CREATE TABLE [dbo].[DogProfiles] (
    [ProfileID]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (200) NOT NULL,
    [Breed]               NVARCHAR (200) NOT NULL,
    [Color]               NVARCHAR (200) NOT NULL,
    [Gender]              INT            NOT NULL,
    [PickedUpDate]        DATETIME       NULL,
    [IsFixed]             BIT            NOT NULL,
    [GraduationDate]      DATETIME       NULL,
    [CreatedTimeUTC]      DATETIME       NOT NULL,
    [CreatedByUserID]     INT            NOT NULL,
    [IsApproved]          BIT            NOT NULL,
    [LocationId]          INT            NOT NULL,
    [BirthYear]           INT            NULL,
    [HealthCondition]     NVARCHAR (MAX) NULL,
    [Discriminator]       NVARCHAR (128) DEFAULT ('') NOT NULL,
    [LocationDescription] NVARCHAR (MAX) NULL,
    [Deleted]             BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.DogProfiles] PRIMARY KEY CLUSTERED ([ProfileID] ASC),
    CONSTRAINT [FK_dbo.DogProfiles_dbo.Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_LocationId]
    ON [dbo].[DogProfiles]([LocationId] ASC);

