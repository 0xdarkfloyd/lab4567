namespace Training.Operations;

public static class Program
{
    public static int Main(string[] args)
    {
        if (args.Length != 2 || (args[0] != "exercise" && args[0] != "fixed") ||
            !new[] { "demo", "test", "lab1", "lab2" }.Contains(args[1]))
        {
            Console.Error.WriteLine("Expected track exercise|fixed and command demo|test|lab1|lab2");
            return 2;
        }
        // Prevent misleading track labels when invoking the executable directly.
        if (typeof(Program).Assembly.GetName().Name != args[0])
        {
            Console.Error.WriteLine("Track does not match the selected project.");
            return 2;
        }
        if (args[1] != "demo") return Harness.Run(args[0], args[1]);
        try { Demo(args[0]); return 0; }
        catch (Exception) { Console.Error.WriteLine("Demo infrastructure failure; no raw exception printed."); return 1; }
    }
    private static void Demo(string track)
    {
        Console.WriteLine("LOCAL CONSOLE ADAPTER. Microsoft.Data.Sqlite 8.0.11 real SQL. Not ASP.NET/HTTP/JWT. Track=" + track);
        using (var fixture = new Fixture())
        {
            var search = new Search(fixture);
            foreach (string name in new[] { "Contract", "O'Reilly Guide", "' OR '1'='1" })
            {
                var result = search.Find("Bearer ALICE-DEMO", name);
                Console.WriteLine($"OBSERVED search status={result.Status} IDs=[{string.Join(",", result.Rows.Select(r => r["id"]))}] fields=[{string.Join(",", result.Rows.FirstOrDefault()?.Keys.ToArray() ?? Array.Empty<string>())}]");
            }
            Console.WriteLine("OBSERVED captured token leakage=" + fixture.Logs.Any(line => line.Contains("ALICE-DEMO")));
        }
        foreach (var example in MockModel.Examples())
        {
            Console.WriteLine("SCRIPTED MOCK variant=" + example.Variant);
            Console.WriteLine("Synthetic source: " + example.Source);
            Console.WriteLine(example.MockOutput);
        }
        var proposals = new[]
        {
            Harness.Read(101), Harness.Read(102),
            new Proposal("approveRisk", new() { ["documentId"] = 101 }),
            new Proposal("shell", new() { ["documentId"] = 101 }),
            new Proposal("readDocument", new() { ["documentId"] = "101" }), Harness.Read(101)
        };
        string[] labels = { "authorised", "cross-supplier", "risk approval", "unknown tool", "malformed", "zero budget" };
        for (int i = 0; i < proposals.Length; i++)
        {
            using var fixture = new Fixture();
            var broker = new Broker(fixture, new Budget(i == 5 ? 0 : 1));
            var result = broker.Call(fixture.Authenticate("Bearer ALICE-DEMO"), proposals[i]);
            Console.WriteLine($"OBSERVED MOCK broker {labels[i]}: {result.Decision} contentReads={fixture.ContentReads} toolEffects={fixture.ToolEffects} audit=[{string.Join(",", fixture.Audit)}]");
        }
        Console.WriteLine("Demo records observations only; run test/lab1/lab2 for security-oracle exit status.");
    }
}