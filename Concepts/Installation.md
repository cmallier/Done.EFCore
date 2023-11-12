# Entity Framework Core - Installation



Packages - Command line

``` bash
# Microsoft providers

dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Sqlite    --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Cosmos    --version 8.0.0

# InMemory provider
# While some users use the in-memory database for testing, this is discouraged
# New features are not being added to the in-memory database.
dotnet add package Microsoft.EntityFrameworkCore.InMemory  --version 8.0.0

# Third party providers

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL   --version 7.0.11
dotnet add package MySql.EntityFrameworkCore               --version 7.0.5
dotnet add package Pomelo.EntityFrameworkCore.MySql        --version 7.0.5
dotnet add package MongoDB.EntityFrameworkCore             --version 7.0.0-preview.1
dotnet add package Oracle.EntityFrameworkCore              --version 7.21.12
```

https://learn.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli



Tools - Command line

``` bash
dotnet tool install --global dotnet-ef

dotnet new tool-manifest
dotnet tool install --local dotnet-ef
```

⚠️ Avertissement

Utilisez toujours la version du package d’outils qui correspond à la version majeure des packages d’exécution.


Packages (.csproj)

``` xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.12" />
```
