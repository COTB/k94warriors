CREATE TABLE [dbo].[webpages_Roles] (
    [RoleId]   INT            IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.webpages_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

