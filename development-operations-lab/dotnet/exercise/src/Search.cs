using Microsoft.Data.Sqlite;

namespace Training.Operations;

// Intentionally vulnerable teaching code. Never deploy.
public sealed class Search(Fixture fixture)
{
    public Response Find(string? authorization, string? name)
    {
        var identity = fixture.Authenticate(authorization);
        if (identity is null) return Response.Empty(401);
        fixture.Logs.Enqueue("Auth=" + authorization);
        // Preserve Java's null-as-literal behavior and actual quote/control syntax.
        string sql = "SELECT * FROM Documents WHERE Name = '" + (name ?? "null") + "' ORDER BY Id";
        Interlocked.Increment(ref fixture.SearchQueries);
        try
        {
            return fixture.WithDatabase(connection =>
            {
                using var command = connection.CreateCommand();
                command.CommandText = sql;
                using var reader = command.ExecuteReader();
                return new Response(200, Fixture.Rows(reader, true));
            });
        }
        catch (Exception failure) when (failure is SqliteException or InvalidOperationException)
        {
            return Response.Empty(500);
        }
    }
}