CREATE TABLE [dbo].[TaskEmailRecipients] (
    [TaskEmailRecipientID] INT            IDENTITY (1, 1) NOT NULL,
    [TaskType]             INT            NOT NULL,
    [EmailAddress]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.TaskEmailRecipients] PRIMARY KEY CLUSTERED ([TaskEmailRecipientID] ASC)
);

