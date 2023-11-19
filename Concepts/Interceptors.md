# Interceptors


``` csharp
public interface IAuditab1e
{
    DateTime CreatedOnUtc { get; )
    DateTime? ModifiedOnUtc { get; }
}
```


``` csharp
internal sealed class updateAuditab1eInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResu1t<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResu1t<int> result,
        CancellationToken cancellationToken = default )
{



}
if (eventData . Context is not null)
UpdateAuditab IeEntities (eventData. Context ) ;
return base. SavingChangesAsync(eventData, result, cancellationToken) ;
private static void UpdateAuditabIeEntities(DbContext context)
DateTime = DateTine .1.Jtctbw;
var entities
foreach entry in entities)
if (entry. State
EntityState. Added)
SetCurrentPropertyVa1ue(
entry, nameof(IAuditab1e.CreatedOnUtc)
if (entry. State
EntityState.
SetCurrentPropertyVaIue(
entry,
static void SetCurrentPropertyVa1ue(
EntityEntry entry,
string propertyName,
DateTime utcNow)
entry. Property (propertyNane) .CurrentVa1ue =
utcbbw) ;
}
```
