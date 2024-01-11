using InheritanceApp;
using Microsoft.EntityFrameworkCore;
using ProjectionApp.Entities;

using ( var context = new AppDbContext() )
{
    //context.Database.EnsureDeleted();
    //context.Database.EnsureCreated();


    //Auteur auteur1 = new()
    //{
    //    Nom = "Auteur 1",
    //    Biographie = new Biographie
    //    {
    //        Texte = "Biographie 1"
    //    }
    //};

    //Auteur auteur2 = new()
    //{
    //    Nom = "Auteur 2",
    //};

    //context.AddRange( auteur1, auteur2 );
    //context.SaveChanges();
}

Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using ( var context = new AppDbContext() )
{
    List<AuteurReadModel> auteurs = context.Auteurs
        .Include( x => x.Biographie )
        .Select( x => new AuteurReadModel
        {
            AuteurId = x.AuteurId,
            AuteurNom = x.Nom,
            BiographieTexte = x.Biographie!.Texte
        } )
    .ToList();

    Console.WriteLine( "-----" );
    Console.WriteLine( $"{auteurs[0].AuteurId}" );
    Console.WriteLine( $"{auteurs[0].AuteurNom}" );
    Console.WriteLine( $"{auteurs[0].BiographieTexte ?? "NULL"}" );

    Console.WriteLine( "-----" );
    Console.WriteLine( $"{auteurs[1].AuteurId}" );
    Console.WriteLine( $"{auteurs[1].AuteurNom}" );
    Console.WriteLine( $"{auteurs[1].BiographieTexte ?? "NULL"}" );
}
