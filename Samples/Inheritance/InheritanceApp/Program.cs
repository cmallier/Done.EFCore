using InheritanceApp;
using InheritanceApp.Entities;
using Microsoft.EntityFrameworkCore;

using( var context = new AppDbContext() )
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    var catDna = new DnaSequence { Sequence = "CAT" };
    var dogDna = new DnaSequence { Sequence = "DOG" };
    var sheepDna = new DnaSequence { Sequence = "SHEEP" };


    Console.WriteLine( "------------------------------------------------------" );
    Console.WriteLine( "-- Add Range" );


    context.AddRange(
        new Cat
        {
            Name = "Alice",
            Species = "Felis catus",
            EducationLevel = "MBA",
            Dna = catDna
        },
        new Cat
        {
            Name = "iMac",
            Species = "Felis catus",
            EducationLevel = "BA",
            Dna = catDna
        },
        new Dog
        {
            Name = "Toast",
            Species = "Canis familiaris",
            FavoriteToy = "Mr. Squirrel",
            Dna = dogDna
        },
        new FarmAnimal
        {
            Species = "Ovis aries",
            Value = 1000,
            Dna = sheepDna
        }
    );

    context.SaveChanges();
}


/*
 Animal
   - FarmAnimal
   - Pet
     - Cat
     - Dog
*/


Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using( var context = new AppDbContext() )
{
    List<Animal> result1 = context.Animals.Where( x => x.Species.StartsWith( "F" ) )
                                 .Include( x => x.Dna )
                                 .TagWith( "-----Result 1-----" )
                                 .ToList();

    List<Pet> result2 = context.Pets.Where( x => x.Species.StartsWith( "F" ) )
                                    .Include( x => x.Dna )
                                    .TagWith( "-----Result2-----" )
                                    .ToList();

    List<Cat> result3 = context.Cats.Where( x => x.Species.StartsWith( "F" ) )
                                    .Include( x => x.Dna )
                                    .TagWith( "-----Result 3-----" )
                                    .ToList();
}