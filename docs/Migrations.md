# Migrations

Keep the database schema `in sync` with the EF Core model

![Alt text](images/Migrations01.png)


## 1. Enable Migrations

Shared design-time components for Entity Framework Core tools.

Packages `Microsoft.EtityFrameworkCore.Design`




## 2. Methods

1. Package Manager Console
2. Powershell
3. .Net Core CLI Tools 👍



2.1 / 2.2 Package Manager Console / Powershell

Visual Studio -> Tools -> NuGet Package Manager -> Package Manager Console

```
Add-Migration <NameOfMigration>
```

2.3 .Net Core CLI Tools 👍

```
dotnet ef migrations add <NameOfMigration> // Create a snapshot
```


`<timestamp>_<NameOfMigration>.cs`

``` csharp
public partial class NameOfMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "dbo");

        migrationBuilder.CreateTable(
            name: "Blogs",
            schema: "dbo",
            columns: table => new
            {
                BlogId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Rating = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Blogs", x => x.BlogId);
            });

        migrationBuilder.CreateTable(
            name: "Posts",
            columns: table => new
            {
                PostId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Content = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                BlogId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Posts", x => x.PostId);
                table.ForeignKey(
                    name: "FK_Posts_Blogs_BlogId",
                    column: x => x.BlogId,
                    principalSchema: "dbo",
                    principalTable: "Blogs",
                    principalColumn: "BlogId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "dbo",
            table: "Blogs",
            columns: new[] { "BlogId", "Rating", "Url" },
            values: new object[] { 1, 3, "http://sample.com" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Posts");

        migrationBuilder.DropTable(
            name: "Blogs",
            schema: "dbo");
    }
}


```


## 3. Apply Migrations

3.3 .Net Core CLI Tools 👍

``` bash
dotnet ef database update
```




## 4. Revert Migrations

4.3 .Net Core CLI Tools 👍

``` bash
dotnet ef database update <NameOfMigration>

# Remove last migration
dotnet ef database remove
```


## Generate Sql Script

``` bash
# Generates a SQL Script for all migrations
dotnet ef migrations script

# Generates a SQL Script from <name> to the latest
dotnet ef migrations script <FromMigrationName>

# Generates a SQL Script from <name> to <name>
dotnet ef migrations script <FromMigrationName> <ToMigrationName>

# Generates idempotent scripts, which internally check which migrations have already been applied, and only apply missing ones.
dotnet ef migrations script --idempotent

# List all existing migrations.
dotnet ef migrations list

# Generate a SQL script file
dotnet ef migrations script --output Migration01.sql

# Generate a SQL script file from <name> to <name>
dotnet ef migrations script Step02 Step03 --idempotent --output Migration03_i.sql
```


Not Idempotent
``` sql
    DECLARE @var0 sysname;

    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[Blogs]') AND [c].[name] = N'Url');

    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[Blogs] DROP CONSTRAINT [' + @var0 + '];');

    ALTER TABLE [dbo].[Blogs] ALTER COLUMN [Url] nvarchar(150) NOT NULL;
    GO
```

Idempotent
``` sql
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231021172515_Step02')
BEGIN
    DECLARE @var0 sysname;

    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[Blogs]') AND [c].[name] = N'Url');

    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[Blogs] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [dbo].[Blogs] ALTER COLUMN [Url] nvarchar(150) NOT NULL;
END;
GO
```



``` csharp
// Create the database and schema for the first time.
// ⚠️ Note that it creates a database first time only.
dbContext.Database.EnsureCreated()
```




## Bundle

``` bash
dotnet ef migrations bundle --self-contained
```

Output: efbundle.exe
