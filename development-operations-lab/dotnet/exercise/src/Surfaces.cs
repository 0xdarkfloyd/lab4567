using System.Text;
using Microsoft.Data.Sqlite;

namespace Training.Operations;

// Intentionally unsafe local adapters, not real storage, URLs or durable jobs.
public sealed class Surfaces(Fixture fixture)
{
    private readonly Dictionary<string, string> cache = new(StringComparer.Ordinal);
    public Action AfterInitialCheck = () => { };
    public Content Read(Identity? who, int id) => who is null ? Content.Denied(401) : Retrieve(id);
    private Content Retrieve(int id)
    {
        try { return new(200, fixture.Materialize(id)); }
        catch (Exception failure) when (failure is SqliteException or InvalidOperationException)
        { return Content.Denied(500); }
    }
    public Content Preview(Identity? who, int id) => Read(who, id);
    public Content Download(Identity? who, int id) => Read(who, id);
    public Content Storage(Identity? who, int id) => Read(who, id);
    public Content Export(Identity? who, IReadOnlyList<int>? ids)
    {
        var output = new StringBuilder();
        foreach (int id in ids!) output.Append(Read(who, id).Body);
        return new(200, output.ToString());
    }
    public Content Cached(Identity? who, int id, string name)
    {
        if (cache.TryGetValue(name, out var body)) return new(200, body);
        var result = Read(who, id);
        cache[name] = result.Body;
        return result;
    }
    public Job? Enqueue(Identity? who, int id) => fixture.Allowed(who, id) ? new(who!, id) : null;
    public Content Run(Job? job) => job is null ? Content.Denied(403) : Retrieve(job.DocumentId);
    public Content Deliver(Job? job, Content completed) => completed;
    public Content GuardedRead(Identity? who, int id)
    {
        if (!fixture.Allowed(who, id)) return Content.Denied(403);
        AfterInitialCheck();
        return Retrieve(id);
    }
}