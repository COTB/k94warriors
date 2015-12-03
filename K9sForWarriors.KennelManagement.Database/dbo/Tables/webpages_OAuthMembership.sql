CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider]       NVARCHAR (30)  NOT NULL,
    [ProviderUserId] NVARCHAR (100) NOT NULL,
    [UserId]         INT            NOT NULL,
    CONSTRAINT [PK_dbo.webpages_OAuthMembership] PRIMARY KEY CLUSTERED ([Provider] ASC, [ProviderUserId] ASC),
    CONSTRAINT [FK_dbo.webpages_OAuthMembership_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserID])
);

