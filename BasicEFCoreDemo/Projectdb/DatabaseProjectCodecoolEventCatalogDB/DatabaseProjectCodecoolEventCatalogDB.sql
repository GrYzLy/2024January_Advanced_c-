CREATE TABLE [dbo].[Events] (
    [EventId]     UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (MAX)   NULL,
    [Price]       INT              NOT NULL,
    [Artist]      NVARCHAR (MAX)   NULL,
    [Date]        DATETIME2 (7)    NOT NULL,
    [Description] NVARCHAR (MAX)   NULL,
    [ImageUrl]    NVARCHAR (MAX)   NULL,
    [CategoryId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([EventId] ASC),
    CONSTRAINT [FK_Events_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId]) ON DELETE CASCADE
);


GO

CREATE TABLE [dbo].[__EFMigrationsHistory] (
    [MigrationId]    NVARCHAR (150) NOT NULL,
    [ProductVersion] NVARCHAR (32)  NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC)
);


GO

CREATE TABLE [dbo].[Tickets] (
    [TicketId] UNIQUEIDENTIFIER NOT NULL,
    [Name]     NVARCHAR (MAX)   NULL,
    [Price]    INT              NOT NULL,
    [EventId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED ([TicketId] ASC),
    CONSTRAINT [FK_Tickets_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([EventId]) ON DELETE CASCADE
);


GO

CREATE TABLE [dbo].[Categories] (
    [CategoryId] UNIQUEIDENTIFIER NOT NULL,
    [Name]       NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);


GO

CREATE NONCLUSTERED INDEX [IX_Tickets_EventId]
    ON [dbo].[Tickets]([EventId] ASC);


GO

CREATE NONCLUSTERED INDEX [IX_Events_CategoryId]
    ON [dbo].[Events]([CategoryId] ASC);


GO

GRANT VIEW ANY COLUMN MASTER KEY DEFINITION TO PUBLIC;


GO

GRANT VIEW ANY COLUMN ENCRYPTION KEY DEFINITION TO PUBLIC;


GO

