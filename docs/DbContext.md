# DbContext


Types Included in the model

- `DbSet<TEntity>`
- Navigation Properties
- OnModelCreating()


Exclude

``` csharp
protected override void OnModelCreating( ModelBuilder modelBuilder )
{
    modelBuilder.Ignore<SomeEntity>();
}
```

Exclude from migrations

``` csharp
protected override void OnModelCreating( ModelBuilder modelBuilder )
{
    modelBuilder.Entity<IdentityUser>()
                .ToTable( "AspNetUsers", x => x.ExcludeFromMigrations() );
}
```
