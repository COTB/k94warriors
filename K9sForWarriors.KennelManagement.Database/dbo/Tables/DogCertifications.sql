CREATE TABLE [dbo].[DogCertifications] (
    [DogCertificationID] INT            IDENTITY (1, 1) NOT NULL,
    [DogProfileID]       INT            NOT NULL,
    [Name]               NVARCHAR (200) NOT NULL,
    [DateReceived]       DATETIME       NOT NULL,
    [ExpirationDate]     DATETIME       NULL,
    CONSTRAINT [PK_dbo.DogCertifications] PRIMARY KEY CLUSTERED ([DogCertificationID] ASC),
    CONSTRAINT [FK_dbo.DogCertifications_dbo.DogProfiles_DogProfileID] FOREIGN KEY ([DogProfileID]) REFERENCES [dbo].[DogProfiles] ([ProfileID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DogProfileID]
    ON [dbo].[DogCertifications]([DogProfileID] ASC);

