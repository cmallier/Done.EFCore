# Scaffold

``` bash
dotnet ef scaffold [options] <connectionString> <provider> [options]

dotnet ef dbcontext scaffold --project .\ScaffoldApp\ScaffoldApp.csproj "Data Source=Desktop-Home; Initial Catalog={DATABASE}; Integrated Security=True; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --context-dir Data --output-dir Models
```


## EF Core Power Tools



## Power Tools CLI

```
dotnet tool install --global ErikEJ.EFCorePowerTools.Cli --version 8.0.*-*

efcpt --help "Server=.\SQLExpress; Initial Catalog={DATABASE}; Integrated Security=True; TrustServerCertificate=True; Encrupt=false;" mssql
```
