
#region Strategy 1 : Use a collection of enums

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

using EnumsApp;
using EnumsApp.Entities;
using Microsoft.EntityFrameworkCore;

using ( var context = new AppDbContext() )
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    context.Database.ExecuteSqlRaw( "insert into Categories (CategorieId, Code) values (1, 'Aventure'), (2, 'Biographie'), (3, 'Roman')" );
    context.Database.ExecuteSqlRaw( "insert into Categories (CategorieId, Code) values (1, 'Aventure'), (2, 'Biographie'), (3, 'Roman')" );


    Livre livre1 = new()
    {
        Titre = "Titre1",
        Categories = new List<LivreCategorie>() { new() { CategorieId = 1 } },
    };

    //Livre livre2 = new()
    //{
    //    Titre = "Titre2",
    //};

    ////livre2.Categories.Add( new() { CategorieId = (int) Category.Roman } );
    ////livre2.Categories.Add( new() { CategorieId = (int) Category.Aventure } );


    context.Livres.AddRange( livre1 );

    context.SaveChanges();
}

Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

//using( var context = new AppDbContext() )
//{
//    Livre result1 = context.Livres.Where( x => x.Titre == "Titre1" )
//                                  .Include( x => x.Categories )
//                                  .First();

//    Console.WriteLine( $"{result1.Titre} Categories: {result1.Categories.Count()}" );
//}
#endregion region


// https://softdevpractice.com/blog/many-to-many-ef-core/
// https://www.learnentityframeworkcore5.com/whats-new-in-ef-core-5/many-to-many-relationship