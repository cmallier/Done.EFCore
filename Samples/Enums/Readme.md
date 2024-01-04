# Enums



``` sql
Create Database EnumsApp;
GO

Use EnumsApp;
GO

CREATE TABLE [dbo].[Categories](
       [CategorieId][int] NOT NULL,
       [Code][nvarchar]( 200 ) NOT NULL,
 CONSTRAINT[PK_Categories] PRIMARY KEY CLUSTERED
(
       [CategorieId] ASC
)WITH( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF ) ON[PRIMARY]
) ON[PRIMARY]
        GO

        CREATE TABLE [dbo].[Livres](
               [LivreId][int] IDENTITY( 1, 1 ) NOT NULL,
               [Titre][nvarchar]( 150 ) NOT NULL,
         CONSTRAINT[PK_Livres] PRIMARY KEY CLUSTERED
        (
               [LivreId] ASC
        )WITH( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF ) ON[PRIMARY]
        ) ON[PRIMARY]
        GO

        CREATE TABLE [dbo].[TheLink](
                  [MyLivresId][int] NOT NULL,
                  [MyCategoriesId][int] NOT NULL,
         CONSTRAINT[PK_LivreCategorie] PRIMARY KEY CLUSTERED
        (
                  [MyLivresId] ASC,
                  [MyCategoriesId] ASC
        )WITH( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF ) ON[PRIMARY]
        ) ON[PRIMARY]
        GO

        ALTER TABLE [dbo].[TheLink]  WITH CHECK ADD  CONSTRAINT [FK_LivreCategorie_Categories_CategorieId] FOREIGN KEY([MyCategoriesId])
        REFERENCES[dbo].[Categories]( [CategorieId] )
        ON DELETE CASCADE
        GO

        ALTER TABLE [dbo].[LivresCategories] CHECK CONSTRAINT[FK_LivreCategorie_Categories_CategorieId]
        GO

        ALTER TABLE [dbo].[TheLink]  WITH CHECK ADD  CONSTRAINT [FK_LivresCategories_Livres_LivreId] FOREIGN KEY([MyLivresId])
        REFERENCES[dbo].[Livres]( [LivreId] )
        ON DELETE CASCADE
        GO

        ALTER TABLE [dbo].[LivresCategories] CHECK CONSTRAINT[FK_LivresCategories_Livres_LivreId]
        GO

        insert into Categories (CategorieId, Code) values (1, 'Aventure'), (2, 'Biographie'), (3, 'Roman')
        GO
```
