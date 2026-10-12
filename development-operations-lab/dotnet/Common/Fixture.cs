using System.Collections.Concurrent;
using Microsoft.Data.Sqlite;

namespace Training.Operations;

// Real private in-memory database. The single anchor is retained until disposal;
// every use is serialized by dbLock because SqliteConnection is not thread-safe.
public sealed class Fixture : IDisposable
{
    private static readonly Dictionary<string, Identity> Identities = new(StringComparer.Ordinal)
    {
        ["ALICE-DEMO"] = new("alice", "SUP-A"),
        ["BOB-DEMO"] = new("bob", "SUP-B"),
        ["NOSCOPE-DEMO"] = new("noscope", null)
    };
    private static readonly Dictionary<int, string> Owners = new()
    {
        [101] = "SUP-A", [102] = "SUP-B", [103] = "SUP-A"
    };
    public readonly object PolicyLock = new();
    private readonly object dbLock = new();
    private readonly HashSet<string> revoked = new(StringComparer.Ordinal);
    private readonly SqliteConnection anchor = new("Data Source=:memory:");
    private bool disposed;
    public int SearchQueries;
    public int ContentReads;
    public int ToolEffects;
    public Action BeforeContentQuery = () => { };
    public readonly ConcurrentQueue<string> Logs = new();
    public readonly ConcurrentQueue<string> Audit = new();

    public Fixture()
    {
        try
        {
            anchor.Open();
            using var create = anchor.CreateCommand();
            create.CommandText = "CREATE TABLE Documents (Id INTEGER PRIMARY KEY, SupplierId TEXT COLLATE BINARY, Name TEXT COLLATE BINARY, ExtraSensitiveField TEXT, Content TEXT)";
            create.ExecuteNonQuery();
            Seed(101, "SUP-A", "Contract", "INTERNAL-A", "SUP-A contract: retention evidence missing.");
            Seed(102, "SUP-B", "Contract", "INTERNAL-B", "SUP-B contract: private supplier evidence.");
            Seed(103, "SUP-A", "O'Reilly Guide", "INTERNAL-C", "SUP-A guide: synthetic reference.");
        }
        catch { anchor.Dispose(); throw; }
    }
    private void Seed(int id, string supplier, string name, string extra, string content)
    {
        using var command = anchor.CreateCommand();
        command.CommandText = "INSERT INTO Documents VALUES ($id, $supplier, $name, $extra, $content)";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$supplier", supplier);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$extra", extra);
        command.Parameters.AddWithValue("$content", content);
        command.ExecuteNonQuery();
    }
    public Identity? Authenticate(string? authorization)
    {
        // Exact synthetic token lookup outside request/model payload; NOT JWT validation.
        if (authorization is null || !authorization.StartsWith("Bearer ", StringComparison.Ordinal)) return null;
        return Identities.GetValueOrDefault(authorization[7..]);
    }
    public bool HasScope(Identity? identity)
    {
        lock (PolicyLock)
            return identity is not null && identity.Supplier is not null && !revoked.Contains(identity.Principal);
    }
    public bool Allowed(Identity? identity, int id)
    {
        lock (PolicyLock)
            return HasScope(identity) && Owners.TryGetValue(id, out var owner) && identity!.Supplier == owner;
    }
    public void Revoke(Identity identity)
    {
        lock (PolicyLock) revoked.Add(identity.Principal);
    }
    public T WithDatabase<T>(Func<SqliteConnection, T> query)
    {
        lock (dbLock)
        {
            ObjectDisposedException.ThrowIf(disposed, this);
            return query(anchor);
        }
    }
    // Deliberately no ACL here: each adapter must guard the call with current policy.
    public string Materialize(int id)
    {
        BeforeContentQuery();
        Interlocked.Increment(ref ContentReads); // Started failures are charged too.
        return WithDatabase(connection =>
        {
            using var command = connection.CreateCommand();
            command.CommandTimeout = 2;
            command.CommandText = "SELECT Content FROM Documents WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);
            return command.ExecuteScalar() as string ?? "";
        });
    }
    public static List<Dictionary<string, object>> Rows(SqliteDataReader reader, bool fullEntity)
    {
        var rows = new List<Dictionary<string, object>>();
        while (reader.Read())
        {
            var row = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["id"] = reader.GetInt32(reader.GetOrdinal("Id")),
                ["name"] = reader.GetString(reader.GetOrdinal("Name"))
            };
            if (fullEntity)
            {
                row["supplierId"] = reader.GetString(reader.GetOrdinal("SupplierId"));
                row["extraSensitiveField"] = reader.GetString(reader.GetOrdinal("ExtraSensitiveField"));
                row["content"] = reader.GetString(reader.GetOrdinal("Content"));
            }
            rows.Add(row);
        }
        return rows;
    }
    public void RecordAudit(Decision decision) => Audit.Enqueue("broker:" + decision);
    public void Dispose()
    {
        // Never acquire PolicyLock under dbLock; fixed callers use policy -> database.
        lock (dbLock)
        {
            if (disposed) return;
            disposed = true;
            anchor.Dispose();
        }
    }
}