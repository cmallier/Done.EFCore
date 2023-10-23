BEGIN TRANSACTION;
GO

DECLARE @description AS sql_variant;
SET @description = N'Blogs are here';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'dbo', 'TABLE', N'Blogs';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[Blogs]') AND [c].[name] = N'Url');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[Blogs] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [dbo].[Blogs] ALTER COLUMN [Url] nvarchar(150) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231021172515_Step02', N'7.0.12');
GO

COMMIT;
GO

