using MappingApp;
using MappingApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

static void DisplayStates( IEnumerable<EntityEntry> entries )
{
    foreach( EntityEntry entry in entries )
    {
        Console.WriteLine( $"Entity: {entry.Entity.GetType().Name}, State: {entry.State} " );

        foreach( string propertyName in entry.Properties.Select( p => p.Metadata.Name ) )
        {
            Console.WriteLine( $"Property: {propertyName}, OriginalValue: {entry.OriginalValues[propertyName]}, CurrentValue: {entry.CurrentValues[propertyName]}, IsModified: {entry.Members.First( x => x.Metadata.Name == propertyName ).IsModified}" );
        }
    }
}

//using( var context = new AppDbContext() )
//{
//    context.Database.EnsureDeleted();
//    context.Database.EnsureCreated();

//    Livre livre = new();

//    livre.Titre = "Titre1";
//    livre.IsActive = true;
//    livre.Number = 1;

//    context.AddRange( livre );
//    context.SaveChanges();
//}


//Console.WriteLine( "------------------------------------------------------" );
//Console.WriteLine( "-- Results" );

//using( var context = new AppDbContext() )
//{
//    Livre livre = context.Livres
//                         .TagWith( "----- Result 1 -----" )
//                         .First();

//    Console.WriteLine( $"Result 1: {livre.Titre}" );
//    Console.WriteLine( $"Result 1: {livre.IsActive}" );
//    Console.WriteLine( $"Result 1: {livre.Number}" );

//    livre.Titre = "Titre2";         // Detected as modified
//    livre.Number = 1;               // Not detected as modified

//    // Tracker: On (Connected scenario)
//    // EF detects this change and marks marks Title as modified.
//    // UPDATE Livres SET Titre = @p0
//    // WHERE LivreId = @p1;

//    DisplayStates( context.ChangeTracker.Entries() );

//    context.SaveChanges();
//}

//using( var context = new AppDbContext() )
//{
//    Livre livre = context.Livres
//                         .TagWith( "----- Result 2 -----" )
//                         .First();

//    Console.WriteLine( $"Result 2: {livre.Titre}" );
//    Console.WriteLine( $"Result 2: {livre.IsActive}" );
//    Console.WriteLine( $"Result 2: {livre.Number}" );

//    livre.Titre = "Titre2";

//    // Update = Disconnected scenario
//    // Should be used when an entity object is not attached to a context
//    // All Properties will be marked as modified
//    context.Update( livre );

//    // UPDATE Livres SET IsActive = @p0, Number = @p1, Titre = @p2
//    // WHERE LivreId = @p3;
//    context.SaveChanges();
//}

//using( var context = new AppDbContext() )
//{
//    Livre livre = context.Livres
//                         .TagWith( "----- Result 3 -----" )
//                         .First();

//    Console.WriteLine( $"Result 2: {livre.Titre}" );
//    Console.WriteLine( $"Result 2: {livre.IsActive}" );
//    Console.WriteLine( $"Result 2: {livre.Number}" );

//    Livre updatedLivre = new() { LivreId = 1, Titre = "Titre3", IsActive = true, Number = 2 };

//    // Track modified values fron DbContext (livre) and apply them to updatedLivre
//    // Ex: Only Titre and Number is modified
//    context.Entry( livre ).CurrentValues.SetValues( updatedLivre );

//    // UPDATE Livres SET Number = @p0, Titre = @p1
//    // WHERE LivreId = @p2;
//    context.SaveChanges();
//}


// Note
//public static void InsertOrUpdate( AppDbContext context, Livre livre )
//{
//    Livre existingLivre = context.Livres.Find( livre.LivreId );
//    if( existingLivre is null )
//    {
//        context.Add( livre );
//    }
//    else
//    {
//        context.Entry( existingLivre ).CurrentValues.SetValues( livre );
//    }

//    context.SaveChanges();
//}





// ----------------------------------------------------------------------------
// Record

//using MappingApp;
//using MappingApp.Entities;

//using( var context = new AppDbContext() )
//{
//    context.Database.EnsureDeleted();
//    context.Database.EnsureCreated();

//    Livre livre = new( Titre: "Titre1", IsActive: true, Number: 1 );

//    context.Add( livre );
//    context.SaveChanges();
//}

//using( var context = new AppDbContext() )
//{
//    Livre livre = context.Livres
//                         .TagWith( "----- Result 1 -----" )
//                         .AsNoTracking() //when using record
//                         .First();

//    Console.WriteLine( $"Result 1: {livre.Titre}" );
//    Console.WriteLine( $"Result 1: {livre.IsActive}" );
//    Console.WriteLine( $"Result 1: {livre.Number}" );

//    livre = livre with
//    {
//        Titre = "Titre2",
//        IsActive = false,
//        Number = 2,
//    };

//    DisplayStates( context.ChangeTracker.Entries() );       // Nothing is tracked
//    context.Update( livre );                                // We have to manually call the RecordCars.Update() method to inform EF core of the changes
//    DisplayStates( context.ChangeTracker.Entries() );
//    context.SaveChanges();                                  // Update ALL properties... UPDATE Livres SET IsActive = @p0, Number = @p1, Titre = @p2 WHERE LivreId = @p3;
//}


//using( var context = new AppDbContext() )
//{
//    Livre livre = context.Livres
//                         .TagWith( "----- Result 2 -----" )
//                         .First();

//    Console.WriteLine( $"Result 1: {livre.Titre}" );
//    Console.WriteLine( $"Result 1: {livre.IsActive}" );
//    Console.WriteLine( $"Result 1: {livre.Number}" );

//    Livre updatedLivre = livre with
//    {
//        Titre = "Titre3",
//        IsActive = false,
//        Number = 2,
//    };

//    context.Entry( livre ).CurrentValues.SetValues( updatedLivre ); // When using record
//    DisplayStates( context.ChangeTracker.Entries() );
//    context.SaveChanges();                                          // Update only the changed properties...  UPDATE Livres SET Titre = @p0
//}

//using( var context = new AppDbContext() )
//{
//    Livre livre = context.Livres
//                         .TagWith( "----- Result 1 -----" )
//                         //.AsNoTracking() when using record
//                         .First();

//    Console.WriteLine( $"Result 1: {livre.Titre}" );
//    Console.WriteLine( $"Result 1: {livre.IsActive}" );
//    Console.WriteLine( $"Result 1: {livre.Number}" );

//    Livre updatedLivre = livre with
//    {
//        Titre = "Titre2",
//        IsActive = false,
//        Number = 2,
//    };

//    context.Entry( livre ).State = EntityState.Detached; // When using record

//    context.Update( updatedLivre );
//    context.SaveChanges();
//}

//using( var context = new AppDbContext() )
//{
//    Livre livre = context.Livres
//                         .TagWith( "----- Result 2 -----" )
//                         .First();

//    Console.WriteLine( $"Result 1: {livre.Titre}" );
//    Console.WriteLine( $"Result 1: {livre.IsActive}" );
//    Console.WriteLine( $"Result 1: {livre.Number}" );

//    Livre updatedLivre = livre with
//    {
//        Titre = "Titre2",
//        IsActive = true,
//        Number = 2,
//    };

//    context.Update( updatedLivre ); // Nor working, 2 entities with the same key
//    context.SaveChanges();
//}


//using( var context = new AppDbContext() )
//{
//    Livre livre = context.Livres
//                         .TagWith( "----- Result 2 -----" )
//                         .First();

//    Console.WriteLine( $"Result 1: {livre.Titre}" );
//    Console.WriteLine( $"Result 1: {livre.IsActive}" );
//    Console.WriteLine( $"Result 1: {livre.Number}" );

//    Livre updatedLivre = livre with
//    {
//        LivreId = 1,
//        Titre = "Titre2",
//        IsActive = true,
//        Number = 2,
//    };

//    context.Attach( updatedLivre );                                           // Not working 2 entities with the same key
//    context.Entry( updatedLivre ).Property( p => p.Titre ).IsModified = true; // Not working
//    context.SaveChanges();
//}


//using( var context = new AppDbContext() )
//{
//    Livre livre = context.Livres
//                         .TagWith( "----- Result 2 -----" )
//                         .AsNoTracking()
//                         .First();

//    Console.WriteLine( $"Result 1: {livre.Titre}" );
//    Console.WriteLine( $"Result 1: {livre.IsActive}" );
//    Console.WriteLine( $"Result 1: {livre.Number}" );

//    Livre updatedLivre = livre with
//    {
//        LivreId = 1,
//        Titre = "Titre2",
//        IsActive = true,
//        Number = 2,
//    };

//    context.Attach( updatedLivre );

//    context.Entry( updatedLivre ).Property( p => p.Titre ).IsModified = true;       // Change only the Titre property
//    context.Entry( updatedLivre ).Property( p => p.IsActive ).IsModified = true;    // Same as above, but force

//    // UPDATE Livres SET IsActive = @p0, Titre = @p1
//    // WHERE LivreId = @p2;


//    context.SaveChanges();
//}


// ----------------------------------------------------------------------------
// Class with constructor

using( var context = new AppDbContext() )
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    Livre livre = new( titre: "Titre1" );
    livre.IsActive = true;
    livre.Number = 1;

    context.AddRange( livre );
    context.SaveChanges();
}


Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using( var context = new AppDbContext() )
{
    Livre livre = context.Livres
                         .TagWith( "----- Result 1 -----" )
                         .First();

    Console.WriteLine( $"Result 1: {livre.Titre}" );        // Titre1suffixsuffix (x2) set through the constructor
    Console.WriteLine( $"Result 1: {livre.IsActive}" );     // Set through the property setter
    Console.WriteLine( $"Result 1: {livre.Number}" );       // Set through the property setter
}