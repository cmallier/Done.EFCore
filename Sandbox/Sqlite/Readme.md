# Sandbox (Sqlite)


## Migrations

``` bash
dotnet ef migrations add Step01 --context AppDbContext --project .\src\SandboxSqliteApp\SandboxSqliteApp.csproj --verbose
```


## Script

``` bash
dotnet ef migrations script --context AppDbContext --project .\src\SandboxSqliteApp\SandboxSqliteApp.csproj  --outpls
ut .\Step01.sql --verbose
```