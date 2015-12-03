CREATE TABLE [dbo].[DogSkillNames] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.DogSkillNames] PRIMARY KEY CLUSTERED ([ID] ASC)
);

