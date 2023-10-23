IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [dbo].[Blogs] (
    [BlogId] int NOT NULL IDENTITY,
    [Url] nvarchar(100) NOT NULL,
    [Rating] int NOT NULL,
    CONSTRAINT [PK_Blogs] PRIMARY KEY ([BlogId])
);
GO

CREATE TABLE [Posts] (
    [PostId] int NOT NULL IDENTITY,
    [Title] nvarchar(100) NOT NULL,
    [Content] nvarchar(100) NOT NULL,
    [BlogId] int NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([PostId]),
    CONSTRAINT [FK_Posts_Blogs_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [dbo].[Blogs] ([BlogId]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BlogId', N'Rating', N'Url') AND [object_id] = OBJECT_ID(N'[dbo].[Blogs]'))
    SET IDENTITY_INSERT [dbo].[Blogs] ON;
INSERT INTO [dbo].[Blogs] ([BlogId], [Rating], [Url])
VALUES (1, 3, N'http://sample.com');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BlogId', N'Rating', N'Url') AND [object_id] = OBJECT_ID(N'[dbo].[Blogs]'))
    SET IDENTITY_INSERT [dbo].[Blogs] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231021171207_InitialCreate', N'7.0.12');
GO

COMMIT;
GO

