CREATE TABLE [dbo].[DogMedicalRecordImages] (
    [DogImageID]                INT              IDENTITY (1, 1) NOT NULL,
    [DogMedicalRecordID]        INT              NOT NULL,
    [BlobKey]                   UNIQUEIDENTIFIER NOT NULL,
    [MimeType]                  NVARCHAR (MAX)   NULL,
    [DogMedicalRecord_RecordID] INT              NULL,
    CONSTRAINT [PK_dbo.DogMedicalRecordImages] PRIMARY KEY CLUSTERED ([DogImageID] ASC),
    CONSTRAINT [FK_dbo.DogMedicalRecordImages_dbo.DogMedicalRecords_DogMedicalRecord_RecordID] FOREIGN KEY ([DogMedicalRecord_RecordID]) REFERENCES [dbo].[DogMedicalRecords] ([RecordID])
);


GO
CREATE NONCLUSTERED INDEX [IX_DogMedicalRecord_RecordID]
    ON [dbo].[DogMedicalRecordImages]([DogMedicalRecord_RecordID] ASC);

