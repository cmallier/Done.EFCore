using Microsoft.EntityFrameworkCore;

namespace Enums;

internal static class SqlExtensions
{
    public static IEnumerable<T> GetEnums<T>( FormattableString formattableSql, DbContext context ) where T : Enum
    {
        List<int> ids = [.. context.Database.SqlQuery<int>( formattableSql )];

        foreach( int enumId in ids )
        {
            yield return (T) (object) enumId;
        }
    }
}
