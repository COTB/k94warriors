CREATE TABLE [dbo].[MedicalRecordTypes] (
    [MedicalRecordTypeID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_dbo.MedicalRecordTypes] PRIMARY KEY CLUSTERED ([MedicalRecordTypeID] ASC)
);

