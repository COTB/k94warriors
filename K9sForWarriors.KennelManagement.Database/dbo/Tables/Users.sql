CREATE TABLE [dbo].[Users] (
    [UserID]         INT            IDENTITY (1, 1) NOT NULL,
    [Email]          NVARCHAR (300) NOT NULL,
    [Phone]          NVARCHAR (30)  NULL,
    [DisplayName]    NVARCHAR (200) NULL,
    [CreatedTimeUTC] DATETIME       NOT NULL,
    [PhoneProvider]  NVARCHAR (200) NULL,
    [UserTypeId]     INT            NOT NULL,
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_dbo.Users_dbo.UserTypes_UserTypeID] FOREIGN KEY ([UserTypeId]) REFERENCES [dbo].[UserTypes] ([ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserTypeID]
    ON [dbo].[Users]([UserTypeId] ASC);

