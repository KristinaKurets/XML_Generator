CREATE TABLE [dbo].[Payments] (
    [ID]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [Sum]      INT           NOT NULL,
    [Date]     DATETIME2 (7) NOT NULL,
    [PersonId] BIGINT        NOT NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED ([ID] ASC)
);

