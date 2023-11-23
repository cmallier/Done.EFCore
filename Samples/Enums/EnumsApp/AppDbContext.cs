using EnumsApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EnumsApp;

public class AppDbContext : DbContext
{
    // Configure from Entities to use SqlServer with local Sql mdf file
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        => optionsBuilder
          .UseSqlServer( @"Data Source=Desktop-Home; Initial Catalog=EnumsApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          //.UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=ManyToManyApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();

    // DbSets
    public DbSet<Livre> Livres => Set<Livre>();
    public DbSet<Categorie> Categories => Set<Categorie>();


    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        // NoTable
        // Categories : [0,1]

        // Convesion
        // Categories : Aventure,Biographie
        //modelBuilder.Entity<Livre>()
        //            .Property( x => x.Categories )
        //            .HasConversion( x => string.Join( ",", x.Select( y => y.ToString() ) ),
        //                            x => x.Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries ).Select( y => (Categorie) Enum.Parse( typeof( Categorie ), y ) ).ToList()
        //                          );


        // Strategy 2
        modelBuilder.Entity<Livre>().ToTable( "Livres" );
        modelBuilder.Entity<Categorie>().ToTable( "Categories" );
        modelBuilder.Entity<Categorie>()
                    .Property( x => x.CategorieId )
                    .ValueGeneratedNever();


        // Strategy 3
        //modelBuilder.Entity<Livre>().ToTable( "Livres" );


        //modelBuilder.Entity<Categorie>().ToTable( "Categories" );


        //modelBuilder.Entity<Categorie>()
        //            .HasKey( e => e.CategorieId );

        //modelBuilder.Entity<Categorie>()
        //            .Property( x => x.CategorieId )
        //            .ValueGeneratedNever();

        //modelBuilder.Entity<Categorie>()
        //            .Property( b => b.Code )
        //            .HasField( "_code" )
        //            .IsRequired( false )
        //            .HasColumnType( "nvarchar(100)" );

        //modelBuilder.Entity<Livre>()
        //    .HasMany( e => e.Categories )
        //    .WithMany( e => e.Livres )
        //    .UsingEntity( joinEntityName: "LivreXCategoryX",
        //        r => r.HasOne( typeof( Categorie ) ).WithMany().HasForeignKey( "CategorieId" ).HasPrincipalKey( nameof( Categorie.CategorieId ) ),
        //        l => l.HasOne( typeof( Livre ) ).WithMany().HasForeignKey( "LivreId" ).HasPrincipalKey( nameof( Livre.LivreId ) ),
        //        j => j.HasKey( "LivreId", "CategorieId" )
        //    );
    }
}


// Serialize a collection of enums to a string using Json





// Generate a EnumCollectionJsonValueConverter<T> for EFCOre

//public class EnumCollectionJsonValueConverter<T> : ValueConverter<ICollection<T>, string> where T : Enum
//{
//    public EnumCollectionJsonValueConverter() : base(
//      v => JsonConverter
//        .SerializeObject( v.Select( e => e.ToString() ).ToList() ),
//      v => JsonConvert
//        .DeserializeObject<ICollection<string>>( v )
//        .Select( e => (T) Enum.Parse( typeof( T ), e ) ).ToList() )
//    {
//    }
//}
