using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ValueConversionsApp.Entities;

namespace ValueConversionsApp;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        => optionsBuilder
          .UseSqlServer( @"Data Source=Desktop-Home; Initial Catalog=ValueConversionsApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();


    // DbSets
    public DbSet<Livre> Livres => Set<Livre>();

    protected override void ConfigureConventions( ModelConfigurationBuilder configurationBuilder )
    {
        //configurationBuilder.Properties<bool>()
        //                    .HaveConversion<BoolToZeroOneConverter<int>>();

        configurationBuilder
      .Properties<DateTime>()
      .HaveConversion<long>();
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Livre>().ToTable( "Livres" );

        // List
        // https://github.com/dotnet/efcore/blob/main/src/EFCore/Storage/ValueConversion/BoolToStringConverter.cs
        // EnumToStringConverter
        // EnumToNumberConverter
        // BoolToZeroOneConverter
        // TimeSpanToTicksConverter
        // StringToTimeSpanConverter

        var boolToZeroOneConverter = new BoolToZeroOneConverter<int>();
        var enumGenreToStringConverter = new EnumToStringConverter<Genre>();
        var boolToStringConverter = new BoolToStringConverter( "Y", "N" );
        var boolToShortConverter = new BoolToZeroOneConverter<short>();
        var stringToBoolConverter = new StringToBoolConverter( new ConverterMappingHints() )


        // Enum Conversion
        // Enum -> string
        var genreEnumConverter = new ValueConverter<Genre, string>(
                v => v.ToString(),
                v => (Genre) Enum.Parse( typeof( Genre ), v ) );

        // Enum Conversion
        // Enum -> short
        modelBuilder.Entity<Livre>()
            .Property( e => e.Genre )
            .HasConversion<EnumToNumberConverter<Genre, short>>();


        // Enum Conversion
        // enum -> string
        modelBuilder.Entity<Livre>()
                    .Property( e => e.Genre )
                    .HasConversion( genreEnumConverter );


        // ValueObject Conversion
        // Value -> decimal
        modelBuilder.Entity<Livre>()
                    .Property( e => e.Price )
                    .HasConversion(
                        v => v.Amount,
                        v => new Dollars( v )
                    );

        // BoolToInt
        // bool -> int
        modelBuilder.Entity<Livre>()
                    .Property( e => e.IsActive )
                    .HasConversion<int>();


        modelBuilder.Entity<Livre>()
                    .Property( e => e.IsActive )
                    .HasConversion( boolToZeroOneConverter );


        modelBuilder.Entity<Livre>()
                    .Property( e => e.Currency )
                    .HasConversion<CurrencyToSymbolConverter>();

    }
}


//public class MoneyConverter : ValueConverter<Money, string>
//{
//    public MoneyConverter()
//        : base(
//            v => JsonSerializer.Serialize( v, (JsonSerializerOptions) null ),
//            v => JsonSerializer.Deserialize<Money>( v, (JsonSerializerOptions) null ) )
//    {
//    }
//}

public class CurrencyToSymbolConverter : ValueConverter<Currency, string>
{
    public CurrencyToSymbolConverter()
        : base(
            v => v == Currency.PoundsSterling ? "£" : v == Currency.Euros ? "€" : "$",
            v => v == "£" ? Currency.PoundsSterling : v == "€" ? Currency.Euros : Currency.UsDollars )
    {
    }
}

//public class CurrencyConverter : ValueConverter<Currency, decimal>
//{
//    public CurrencyConverter()
//        : base(
//            v => v.Amount,
//            v => new Currency( v ) )
//    {
//    }
//}