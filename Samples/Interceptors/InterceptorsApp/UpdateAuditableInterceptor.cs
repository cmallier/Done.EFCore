using InterceptorsApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InterceptorsApp;

internal class UpdateAuditableInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges( DbContextEventData eventData, InterceptionResult<int> result )
    {
        if( eventData.Context is not null )
        {
            UpdateAuditableEntities( eventData.Context );
        }

        return base.SavingChanges( eventData, result );
    }


    public override ValueTask<InterceptionResult<int>> SavingChangesAsync( DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default )
    {
        if( eventData.Context is not null )
        {
            UpdateAuditableEntities( eventData.Context );
        }


        return base.SavingChangesAsync( eventData, result, cancellationToken );
    }

    private static void UpdateAuditableEntities( DbContext context )
    {
        DateTime utcNow = DateTime.UtcNow;
        IEnumerable<EntityEntry<IAuditable>> entities = context.ChangeTracker.Entries<IAuditable>()
                                                                .Where( e => e.State == EntityState.Added || e.State == EntityState.Modified )
                                                                .Select( e => e );

        foreach( EntityEntry<IAuditable> entity in entities )
        {
            if( entity.State == EntityState.Added )
            {
                //entity.Entity.CreatedOnUtc = utcNow;
                SetCurrentPropertyValue( entity, nameof( IAuditable.CreatedOnUtc ), utcNow );
            }

            if( entity.State == EntityState.Modified )
            {
                //entity.Entity.ModifiedOnUtc = utcNow;
                SetCurrentPropertyValue( entity, nameof( IAuditable.ModifiedOnUtc ), utcNow );
            }
        }
    }

    private static void SetCurrentPropertyValue( EntityEntry entry, string propertyName, DateTime utcNow )
    {
        entry.Property( propertyName ).CurrentValue = utcNow;
    }
}