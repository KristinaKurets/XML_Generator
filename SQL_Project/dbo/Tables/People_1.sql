CREATE TABLE [dbo].[People] (
    [ID]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX) NULL,
    [LastName] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED ([ID] ASC)
);

