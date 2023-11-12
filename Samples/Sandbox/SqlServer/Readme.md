# Sandbox (SqlServer)


## Migrations

``` bash
dotnet ef migrations add Step01 --context AppDbContext --project .\src\SandboxSqlServerApp\SandboxSqlServerApp.csproj --verbose
```


## Script

``` bash
dotnet ef migrations script --context AppDbContext --project .\src\SandboxSqlServerApp\SandboxSqlServerApp.csproj  --output .\Step01.sql --verbose
```
