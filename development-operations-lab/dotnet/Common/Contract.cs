namespace Training.Operations;

// Numeric console contract: no HTTP middleware, JWT validation or public identity binding.
public sealed record Identity(string Principal, string? Supplier);
public sealed record Response(int Status, List<Dictionary<string, object>> Rows)
{
    public static Response Empty(int status) => new(status, new());
}
public sealed record Content(int Status, string Body)
{
    public static Content Denied(int status) => new(status, "");
}
public sealed record Job(Identity Requester, int DocumentId);
public sealed record Proposal(string? Tool, Dictionary<string, object>? Arguments);
public enum Decision { ALLOWED, DENIED, REJECTED, STOPPED, FAILED }
public sealed record Outcome(Decision Decision, string Body);