
#region Strategy 1: Use a collection of enums

//using EnumsApp;
//using EnumsApp.Entities;

//using ( var context = new AppDbContext() )
//{
//    context.Database.EnsureDeleted();
//    context.Database.EnsureCreated();


//    Livre livre1 = new()
//    {
//        Titre = "Titre1",
//        Categories = new List<Categorie>() { Categorie.Aventure, Categorie.Biographie }
//    };

//    Livre livre2 = new()
//    {
//        Titre = "Titre2",
//        Categories = new List<Categorie>() { Categorie.Roman }
//    };

//    Livre livre3 = new()
//    {
//        Titre = "Titre3",
//    };

//    context.Livres.AddRange( livre1, livre2, livre3 );

//    context.SaveChanges();
//}

//Console.WriteLine( "------------------------------------------------------" );
//Console.WriteLine( "-- Results" );

//using ( var context = new AppDbContext() )
//{
//    Livre result1 = context.Livres.Where( x => x.Titre == "Titre1" )
//                                  .First();

//    Console.WriteLine( $"{result1.Titre} Categories: {result1.Categories.Count()}" );
//}
#endregion


#region Strategy 2: Use a many to many
//using EnumsApp;
//using EnumsApp.Entities;
//using Microsoft.EntityFrameworkCore;

//using ( var context = new AppDbContext() )
//{
//    context.Database.EnsureDeleted();
//    context.Database.EnsureCreated();

//    context.Categories.Add( new() { CategorieId = (int) Category.Aventure, Code = "Aventure" } );
//    context.Categories.Add( new() { CategorieId = (int) Category.Biographie, Code = "Biographie" } );
//    context.Categories.Add( new() { CategorieId = (int) Category.Roman, Code = "Roman" } );

//    context.SaveChanges();

//    context.ChangeTracker.Clear();


//    Livre livre1 = new()
//    {
//        Titre = "Titre1"
//    };

//    // Fetch
//    Categorie categorie1 = context.Categories.Find( (int) Category.Aventure );
//    livre1.Categories.Add( categorie1 );

//    Livre livre2 = new()
//    {
//        Titre = "Titre2",
//    };

//    ICollection<Categorie> categories = context.Categories.Where( x => x.Code == "Aventure" || x.Code == "Roman" ).ToList();
//    livre2.Categories.AddRange( categories );
//    ////livre2.Categories.Add( new() { CategorieId = (int) Category.Roman } );
//    ////livre2.Categories.Add( new() { CategorieId = (int) Category.Aventure } );


//    context.Livres.AddRange( livre1, livre2 );

//    context.SaveChanges();
//}

//Console.WriteLine( "------------------------------------------------------" );
//Console.WriteLine( "-- Results" );

//using ( var context = new AppDbContext() )
//{
//    Livre result1 = context.Livres.Where( x => x.Titre == "Titre1" )
//                                  .Include( x => x.Categories )
//                                  .First();

//    Console.WriteLine( $"{result1.Titre} Categories: {result1.Categories.Count()}" );
//}
#endregion


#region Strategy 3: Use a many to many (v2)

//using EnumsApp;
//using EnumsApp.Entities;

//using( var context = new AppDbContext() )
//{
//    //context.Database.EnsureDeleted();
//    //context.Database.EnsureCreated();

//    Livre livre1 = new()
//    {
//        Titre = "Titre1",
//        LivreCategories = new List<LivreCategorie>() { new() { CategoriesId = 1 } },
//    };

//    Livre livre2 = new()
//    {
//        Titre = "Titre2",
//    };

//    livre2.LivreCategories.Add( new() { CategoriesId = 1 } );
//    livre2.LivreCategories.Add( new() { CategoriesId = 2 } );
//    livre2.LivreCategories.Add( new() { CategoriesId = 3 } );


//    context.Livres.AddRange( livre1, livre2 );

//    context.SaveChanges();
//}

//Console.WriteLine( "------------------------------------------------------" );
//Console.WriteLine( "-- Results" );

//using( var context = new AppDbContext() )
//{
//    Livre result1 = context.Livres.Where( x => x.Titre == "Titre1" )
//                                  .Include( x => x.Categories )
//                                  .First();

//    Console.WriteLine( $"{result1.Titre} Categories: {result1.Categories.Count()}" );
//}
#endregion region


#region Strategy 4: Mapping

//using EnumsApp;
//using EnumsApp.Entities;
//using Microsoft.EntityFrameworkCore;

//using( var context = new AppDbContext() )
//{
//    string createTableCategories =
//        """
//        Create Database EnumsApp;
//        GO

//        Use EnumsApp;
//        GO

//        CREATE TABLE [dbo].[Categories](
//           	[CategorieId] [int] NOT NULL,
//           	[Code] [nvarchar](200) NOT NULL,
//         CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED
//        (
//           	[CategorieId] ASC
//        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
//        ) ON [PRIMARY]
//        GO

//        CREATE TABLE [dbo].[Livres](
//           	[LivreId] [int] IDENTITY(1,1) NOT NULL,
//           	[Titre] [nvarchar](150) NOT NULL,
//         CONSTRAINT [PK_Livres] PRIMARY KEY CLUSTERED
//        (
//           	[LivreId] ASC
//        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
//        ) ON [PRIMARY]
//        GO

//        CREATE TABLE [dbo].[LivresCategories](
//              	[LivresId] [int] NOT NULL,
//              	[CategoriesId] [int] NOT NULL,
//         CONSTRAINT [PK_LivreCategorie] PRIMARY KEY CLUSTERED
//        (
//              	[LivresId] ASC,
//              	[CategoriesId] ASC
//        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
//        ) ON [PRIMARY]
//        GO

//        ALTER TABLE [dbo].[LivresCategories]  WITH CHECK ADD  CONSTRAINT [FK_LivreCategorie_Categories_CategorieId] FOREIGN KEY([CategoriesId])
//        REFERENCES [dbo].[Categories] ([CategorieId])
//        ON DELETE CASCADE
//        GO

//        ALTER TABLE [dbo].[LivresCategories] CHECK CONSTRAINT [FK_LivreCategorie_Categories_CategorieId]
//        GO

//        ALTER TABLE [dbo].[LivresCategories]  WITH CHECK ADD  CONSTRAINT [FK_LivresCategories_Livres_LivreId] FOREIGN KEY([LivresId])
//        REFERENCES [dbo].[Livres] ([LivreId])
//        ON DELETE CASCADE
//        GO

//        ALTER TABLE [dbo].[LivresCategories] CHECK CONSTRAINT [FK_LivresCategories_Livres_LivreId]
//        GO

//        insert into Categories (CategorieId, Code) values (1, 'Aventure'), (2, 'Biographie'), (3, 'Roman')
//        GO
//        """;

//    Livre livre1 = new()
//    {
//        Titre = "Titre1",
//    };
//    Livre livre3 = new()
//    {
//        Titre = "Titre1",
//    };

//    context.Livres.AddRange( livre1, livre3 );
//    context.SaveChanges();

//    livre1.LivreCategories.Add( new() { CategoriesId = (int) Categorie.Biographie } );

//    context.SaveChanges();

//    Livre livre2 = new()
//    {
//        Titre = "Titre2",
//    };

//    context.Livres.AddRange( livre2 );

//    livre2.LivreCategories.AddRange( [new LivreCategorie() { CategoriesId = (int) Categorie.Biographie, Livre = livre2 }, new LivreCategorie() { CategoriesId = (int) Categorie.Roman, Livre = livre2 }] );

//    context.SaveChanges();
//}

//Console.WriteLine( "------------------------------------------------------" );
//Console.WriteLine( "-- Results" );

//using( var context = new AppDbContext() )
//{
//    Livre result1 = context.Livres.Where( x => x.Titre == "Titre1" )
//                                  .Include( x => x.LivreCategories )
//                                  .First();

//    Console.WriteLine( $"{result1.Titre} Categories: {result1.LivreCategories.Count()}" );
//}
#endregion region


#region Strategy 5: Domain-Driven Design

//using Enums;
//using EnumsApp;
//using EnumsApp.Entities;
//using Microsoft.EntityFrameworkCore;

//using( var context = new AppDbContext() )
//{
//    Livre livre1 = new()
//    {
//        Titre = "Titre1",
//        Categories = new List<Categorie>() { Categorie.Aventure, Categorie.Biographie },
//    };


//    // Add
//    context.Livres.Add( livre1 );
//    context.SaveChanges();

//    foreach( Categorie categorie in livre1.Categories )
//    {
//        // Insert into table LivresCategories
//        context.Database.ExecuteSqlInterpolated( $"INSERT INTO LivresCategories (LivresId, CategoriesId) VALUES ({livre1.LivreId}, {(int) categorie})" );
//    }

//    context.SaveChanges();
//}

//Console.WriteLine( "------------------------------------------------------" );
//Console.WriteLine( "-- Results" );

//using( var context = new AppDbContext() )
//{
//    Livre result1 = context.Livres.Where( x => x.Titre == "Titre1" )
//                                  .First();

//    // Load Enums
//    //List<int> ids = context.Database.SqlQuery<int>( $"select CategoriesId from LivresCategories where LivresId = ({result1.LivreId})" ).ToList();
//    //foreach( int id in ids )
//    //{
//    //    result1.Categories.Add( (Categorie) id );
//    //}

//    //result1.Categories = [.. context.Database.SqlQuery<Categorie>( $"select CategoriesId from LivresCategories where LivresId = ({result1.LivreId})" )];

//    result1.Categories = SqlExtensions.GetEnums<Categorie>( $"select CategoriesId from LivresCategories where LivresId = ({result1.LivreId})", context ).ToList();


//    Console.WriteLine( $"{result1.Titre} Categories: {result1.Categories.Count()}" );
//    foreach( Categorie categorie in result1.Categories )
//    {
//        Console.WriteLine( $"  - {categorie}" );
//    }
//}


#endregion region


#region Strategy 6 : Json

using EnumsApp;
using EnumsApp.Entities;
using Microsoft.EntityFrameworkCore;

using( var context = new AppDbContext() )
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    Livre livre1 = new()
    {
        Titre = "Titre1",
        Categories = new List<Categorie>() { "Biographie", "Roman" },
    };

    context.Livres.Add( livre1 );
    context.SaveChanges();
}

Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using( var context = new AppDbContext() )
{
    Livre result1 = context.Livres.Where( x => x.Titre == "Titre1" )
                                  .Include( x => x.Categories )
                                  .First();

    Console.WriteLine( $"{result1.Titre} Categories: {result1.Categories.Count()}" );
    foreach( Categorie categorie in result1.Categories )
    {
        Console.WriteLine( $"  - {categorie.Value}" );
    }
}


// LivreId  Titre   Categories
// 1	    Titre1  [{"Value":"Biographie"},{ "Value":"Roman"}]

#endregion region




// https://softdevpractice.com/blog/many-to-many-ef-core/
// https://www.learnentityframeworkcore5.com/whats-new-in-ef-core-5/many-to-many-relationship
// https://blog.jetbrains.com/dotnet/2023/02/14/getting-started-entity-framework-core-7-json-support/
// https://code-maze.com/efcore-store-json-in-an-entity-field/
// https://devblogs.microsoft.com/dotnet/announcing-ef7-release-candidate-2/
// https://devblogs.microsoft.com/dotnet/announcing-ef8-rc1/