CREATE TABLE [dbo].[DogSkills] (
    [DogSkilID]    INT IDENTITY (1, 1) NOT NULL,
    [DogProfileID] INT NOT NULL,
    [Level]        INT NOT NULL,
    [SkillNameId]  INT NOT NULL,
    CONSTRAINT [PK_dbo.DogSkills] PRIMARY KEY CLUSTERED ([DogSkilID] ASC),
    CONSTRAINT [FK_dbo.DogSkills_dbo.DogProfiles_DogProfileID] FOREIGN KEY ([DogProfileID]) REFERENCES [dbo].[DogProfiles] ([ProfileID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.DogSkills_dbo.DogSkillNames_SkillNameId] FOREIGN KEY ([SkillNameId]) REFERENCES [dbo].[DogSkillNames] ([ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DogProfileID]
    ON [dbo].[DogSkills]([DogProfileID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SkillNameId]
    ON [dbo].[DogSkills]([SkillNameId] ASC);

