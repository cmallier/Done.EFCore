# Inheritance


- Animal (abstract)
  - FarmAnimal
  - Pet
    - Cat
    - Dog

- Animal (abstract) { Id, Species, DnaSequence}
  - FarmAnimal    { Value }
  - Pet           { Name }
    - Cat         {EducationLevel}
    - Dog         {FavoriteToy}



## TPH

- 1 Table per hyararchy
- Fastest (Modeling for performance)
- By default, EF Core maps
- Recommended
- People are often concerned that the table can become very wide, with many columns only sparsely populated. While this can be true, it is rarely a problem with modern database systems.


| Id | Species          | DnaId | Discriminator | Value   | Name  | EducationLevel | FavoriteToy  |
|----|------------------|-------|---------------|---------|-------|----------------|--------------|
| 1  | Felis catus      | 1     | Cat           | NULL    | Alice | MBA            | NULL         |
| 2  | Felis catus      | 1     | Cat           | NULL    | iMac  | BA             | NULL         |
| 3  | Canis familiaris | 2     | Dog           | NULL    | Toast | NULL           | Mr. Squirrel |
| 4  | Ovis aries       | 3     | FarmAnimal    | 1000.00 | NULL  | NULL           | NULL         |

Sql

``` sql
-- context.Animals.Where
SELECT [a].[Id], [a].[Discriminator], [a].[DnaId], [a].[Species], [a].[Value], [a].[Name], [a].[EducationLevel], [a].[FavoriteToy], [d].[Id], [d].[Sequence]
FROM [Animals] AS [a]
LEFT JOIN [DnaSequence] AS [d] ON [a].[DnaId] = [d].[Id]
WHERE
  [a].[Species] LIKE N'F%'

-- context.Pets.Where
SELECT [a].[Id], [a].[Discriminator], [a].[DnaId], [a].[Species], [a].[Name], [a].[EducationLevel], [a].[FavoriteToy], [d].[Id], [d].[Sequence]
FROM [Animals] AS [a]
LEFT JOIN [DnaSequence] AS [d] ON [a].[DnaId] = [d].[Id]
WHERE
  [a].[Discriminator] IN (N'Pet', N'Cat', N'Dog')
  AND ([a].[Species] LIKE N'F%')

-- context.Cats.Where
SELECT [a].[Id], [a].[Discriminator], [a].[DnaId], [a].[Species], [a].[Name], [a].[EducationLevel], [d].[Id], [d].[Sequence]
LEFT JOIN [DnaSequence] AS [d] ON [a].[DnaId] = [d].[Id]
WHERE
  [a].[Discriminator] = N'Cat'
  AND ([a].[Species] LIKE N'F%')
```


## TPT

- 1 Table per type
- Globally Slower
- Slower for Parent Queries (A lof of joins. joins are expensive)
- Each type has its own table (Hyper normalized)
- Rarely a good choice
  - Used when it is considered important that the data is stored in a normalized form, which is in turn often the case for legacy existing databases or databases managed independently from the application development team.
 - Main issue with the TPT strategy is that almost all queries involve joining multiple tables because the data for any given entity instance is split across multiple tables.

Animals

| Id | Species          | DnaId |
|----|------------------|-------|
| 1  | Felis catus      | 1     |
| 2  | Felis catus      | 1     |
| 3  | Canis familiaris | 2     |
| 4  | Ovis aries       | 3     |


Pets
| Id | Name  |
|----|-------|
| 1  | Alice |
| 2  | iMac  |
| 3  | Toast |


Cats
| Id | EducationLevel |
|----|----------------|
| 1  | MBA            |
| 2  | BA             |



``` Sql

-- context.Animals.Where
SELECT [a].[Id], [a].[DnaId], [a].[Species], [f].[Value], [p].[Name], [c].[EducationLevel], [d].[FavoriteToy],
CASE
    WHEN [d].[Id] IS NOT NULL THEN N'Dog'
    WHEN [c].[Id] IS NOT NULL THEN N'Cat'
    WHEN [p].[Id] IS NOT NULL THEN N'Pet'
    WHEN [f].[Id] IS NOT NULL THEN N'FarmAnimal'
    END AS [Discriminator],
[d0].[Id],
[d0].[Sequence]
FROM [Animals] AS [a]
LEFT JOIN [FarmAnimals] AS [f] ON [a].[Id] = [f].[Id]
LEFT JOIN [Pets] AS [p] ON [a].[Id] = [p].[Id]
LEFT JOIN [Cats] AS [c] ON [a].[Id] = [c].[Id]
LEFT JOIN [Dogs] AS [d] ON [a].[Id] = [d].[Id]
LEFT JOIN [DnaSequence] AS [d0] ON [a].[DnaId] = [d0].[Id]
WHERE [a].[Species] LIKE N'F%'


-- context.Pets.Where
SELECT [a].[Id], [a].[DnaId], [a].[Species], [p].[Name], [c].[EducationLevel], [d].[FavoriteToy],
CASE
    WHEN [d].[Id] IS NOT NULL THEN N'Dog'
    WHEN [c].[Id] IS NOT NULL THEN N'Cat'
END AS [Discriminator],
[d0].[Id],
[d0].[Sequence]
FROM [Animals] AS [a]
INNER JOIN [Pets] AS [p] ON [a].[Id] = [p].[Id]
LEFT JOIN [Cats] AS [c] ON [a].[Id] = [c].[Id]
LEFT JOIN [Dogs] AS [d] ON [a].[Id] = [d].[Id]
LEFT JOIN [DnaSequence] AS [d0] ON [a].[DnaId] = [d0].[Id]
WHERE [a].[Species] LIKE N'F%'


-- context.Cats.Where
SELECT [a].[Id], [a].[DnaId], [a].[Species], [p].[Name], [c].[EducationLevel], [d].[Id], [d].[Sequence]
FROM [Animals] AS [a]
INNER JOIN [Pets] AS [p] ON [a].[Id] = [p].[Id]
INNER JOIN [Cats] AS [c] ON [a].[Id] = [c].[Id]
LEFT JOIN [DnaSequence] AS [d] ON [a].[DnaId] = [d].[Id]
WHERE [a].[Species] LIKE N'F%'
```


## TPC

- 1 Table per concrete type
  - tables are not created for abstract types
  - Unlike TPT mapping, each table contains columns for every property in the concrete type AND its base


No table Animals (abstract)


Cats
Include
 - Id, Species, DnaId : from Animals
 - Name : from Pets
 - EducationLevel : from Cats

| Id | Species     | DnaId | Name  | EducationLevel |
|----|-------------|-------|-------|----------------|
| 1  | Felis catus | 1     | Alice | MBA            |
| 2  | Felis catus | 1     | iMac  | BA             |



``` sql
-- Cats
CREATE TABLE [Cats] (
    [Id] int NOT NULL DEFAULT (NEXT VALUE FOR [AnimalIds]),
    [Species] nvarchar(max) NOT NULL,
    [DnaId] int NULL,
    [Name] nvarchar(max) NOT NULL,
    [EducationLevel] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Cats] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cats_DnaSequence_DnaId] FOREIGN KEY ([DnaId]) REFERENCES [DnaSequence] ([Id])
);


-- context.Animals.Where
SELECT [t].[Id], [t].[DnaId], [t].[Species], [t].[Value], [t].[Name], [t].[EducationLevel], [t].[FavoriteToy], [t].[Discriminator], [d].[Id], [d].[Sequence]
FROM (
        SELECT [f].[Id], [f].[DnaId], [f].[Species], [f].[Value], NULL AS [Name], NULL AS [EducationLevel], NULL AS [FavoriteToy], N'FarmAnimal' AS [Discriminator]
        FROM [FarmAnimals] AS [f]
    UNION ALL
          SELECT [p].[Id], [p].[DnaId], [p].[Species], NULL AS [Value], [p].[Name], NULL AS [EducationLevel], NULL AS [FavoriteToy], N'Pet' AS [Discriminator]
          FROM [Pets] AS [p]
    UNION ALL
          SELECT [c].[Id], [c].[DnaId], [c].[Species], NULL AS [Value], [c].[Name], [c].[EducationLevel], NULL AS [FavoriteToy], N'Cat' AS [Discriminator]
          FROM [Cats] AS [c]
    UNION ALL
          SELECT [d0].[Id], [d0].[DnaId], [d0].[Species], NULL AS [Value], [d0].[Name], NULL AS [EducationLevel], [d0].[FavoriteToy], N'Dog' AS [Discriminator]
          FROM [Dogs] AS [d0]
      ) AS [t]
LEFT JOIN [DnaSequence] AS [d] ON [t].[DnaId] = [d].[Id]
WHERE [t].[Species] LIKE N'F%'


-- context.Pets.Where
 SELECT [t].[Id], [t].[DnaId], [t].[Species], [t].[Name], [t].[EducationLevel], [t].[FavoriteToy], [t].[Discriminator], [d].[Id], [d].[Sequence]
FROM (
          SELECT [p].[Id], [p].[DnaId], [p].[Species], [p].[Name], NULL AS [EducationLevel], NULL AS [FavoriteToy], N'Pet' AS [Discriminator]
          FROM [Pets] AS [p]
    UNION ALL
          SELECT [c].[Id], [c].[DnaId], [c].[Species], [c].[Name], [c].[EducationLevel], NULL AS [FavoriteToy], N'Cat' AS [Discriminator]
          FROM [Cats] AS [c]
    UNION ALL
          SELECT [d0].[Id], [d0].[DnaId], [d0].[Species], [d0].[Name], NULL AS [EducationLevel], [d0].[FavoriteToy], N'Dog' AS [Discriminator]
          FROM [Dogs] AS [d0]
      ) AS [t]
LEFT JOIN [DnaSequence] AS [d] ON [t].[DnaId] = [d].[Id]
WHERE ([t].[Species] IS NOT NULL) AND ([t].[Species] LIKE N'F%')


-- context.Cats.Where
SELECT [c].[Id], [c].[DnaId], [c].[Species], [c].[Name], [c].[EducationLevel], [d].[Id], [d].[Sequence]
FROM [Cats] AS [c]
LEFT JOIN [DnaSequence] AS [d] ON [c].[DnaId] = [d].[Id]
WHERE [c].[Species] LIKE N'F%'
```

## Resources

- https://devblogs.microsoft.com/dotnet/announcing-ef7-preview5/
- https://learn.microsoft.com/en-us/ef/core/performance/modeling-for-performance
- https://www.youtube.com/watch?v=HaL6DKW1mrg
