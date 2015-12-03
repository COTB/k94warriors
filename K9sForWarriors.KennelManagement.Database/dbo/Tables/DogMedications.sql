CREATE TABLE [dbo].[DogMedications] (
    [DogMedicationID]   INT            IDENTITY (1, 1) NOT NULL,
    [DogProfileID]      INT            NOT NULL,
    [MedicationName]    NVARCHAR (100) NOT NULL,
    [AmountDescription] NVARCHAR (100) NOT NULL,
    [VetNotes]          NVARCHAR (MAX) NULL,
    [AMDose]            BIT            NOT NULL,
    [NoonDose]          BIT            NOT NULL,
    [PMDose]            BIT            NOT NULL,
    [StartDate]         DATETIME       NOT NULL,
    [EndDate]           DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.DogMedications] PRIMARY KEY CLUSTERED ([DogMedicationID] ASC),
    CONSTRAINT [FK_dbo.DogMedications_dbo.DogProfiles_DogProfileID] FOREIGN KEY ([DogProfileID]) REFERENCES [dbo].[DogProfiles] ([ProfileID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DogProfileID]
    ON [dbo].[DogMedications]([DogProfileID] ASC);

