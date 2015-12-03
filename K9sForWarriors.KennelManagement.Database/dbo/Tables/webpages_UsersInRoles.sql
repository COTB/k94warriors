CREATE TABLE [dbo].[webpages_UsersInRoles] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [PK_dbo.webpages_UsersInRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.webpages_UsersInRoles_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserID]),
    CONSTRAINT [FK_dbo.webpages_UsersInRoles_dbo.webpages_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId])
);

