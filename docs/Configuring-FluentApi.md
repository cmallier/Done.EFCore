# Configuring - FluentApi

https://learn.microsoft.com/en-us/ef/core/modeling/


``` csharp
public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure( EntityTypeBuilder<Blog> builder )
    {
        // Table
        builder.ToTable( name: "Blogs", schema: "dbo", tableBuilder =>
        {
            tableBuilder.HasComment( "Blogs are here" );
            tableBuilder.ExcludeFromMigrations();
        } );


        builder.Property( x => x.Url )
               .HasMaxLength( 150 );

        // Primary Key
        //builder.HasKey( b => b.BlogId );

        // Properties
        //builder.Property( b => b.BlogId )
        //       .HasColumnName( "BlogId" )
        //       .HasColumnType( "int" )
        //       .HasDefaultValue( 1 )
        //       .HasDefaultValueSql( "NEXT VALUE FOR dbo.BlogId" )
        //       .HasMaxLength( 10 )

        //       .HasPrecision( precision: 10, scale: 0 )
        //       .HasColumnOrder( 1 )
        //       .HasComment( "Primary key for Blog objects." )
        //       .HasField( "_blogId" )
        //       .UseIdentityColumn( seed: 1, increment: 1 )

        //       .IsConcurrencyToken()
        //       .IsRequired()
        //       .IsRowVersion()
        //       .ValueGeneratedOnAdd();   // ValueGeneratedOnAdd is the default for primary key properties, but we're being explicit here.
        //                                 //.ValueGeneratedNever(); // ValueGeneratedNever is the default for non-key properties, but we're being explicit here.

        //builder.HasChangeTrackingStrategy( ChangeTrackingStrategy.Snapshot );

        // Data seeding
        builder.HasData( new Blog { BlogId = 1, Url = "http://sample.com" } );

        //builder.HasIndex();


        // Relationships
        //builder.HasMany( b => b.Posts )
        //       .WithOne( p => p.Blog )
        //       .HasForeignKey( p => p.BlogId )
        //       .OnDelete( DeleteBehavior.Cascade ) // Cascade, ClientCascade, ClientNoAction, ClientSetNull, NoAction, Restrict, SetNull
        //       .IsRequired();

    }
}
```
