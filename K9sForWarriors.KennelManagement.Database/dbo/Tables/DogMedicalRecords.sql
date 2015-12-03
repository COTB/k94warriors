CREATE TABLE [dbo].[DogMedicalRecords] (
    [RecordID]             INT            IDENTITY (1, 1) NOT NULL,
    [DogProfileID]         INT            NOT NULL,
    [Title]                NVARCHAR (200) NULL,
    [RecordExpirationDate] DATETIME       NULL,
    [RecordURL]            NVARCHAR (255) NULL,
    [MedicalRecordTypeID]  INT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_dbo.DogMedicalRecords] PRIMARY KEY CLUSTERED ([RecordID] ASC),
    CONSTRAINT [FK_dbo.DogMedicalRecords_dbo.DogProfiles_DogProfileID] FOREIGN KEY ([DogProfileID]) REFERENCES [dbo].[DogProfiles] ([ProfileID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.DogMedicalRecords_dbo.MedicalRecordTypes_MedicalRecordTypeID] FOREIGN KEY ([MedicalRecordTypeID]) REFERENCES [dbo].[MedicalRecordTypes] ([MedicalRecordTypeID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DogProfileID]
    ON [dbo].[DogMedicalRecords]([DogProfileID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MedicalRecordTypeID]
    ON [dbo].[DogMedicalRecords]([MedicalRecordTypeID] ASC);

