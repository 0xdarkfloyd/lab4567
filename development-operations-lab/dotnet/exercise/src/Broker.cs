namespace Training.Operations;

// Intentionally permissive mock broker. Never executes any external tool.
public sealed class Broker(Fixture fixture, Budget budget)
{
    public Action AfterInitialCheck = () => { };
    public Outcome Call(Identity? requester, Proposal? proposal)
    {
        var decision = Decision.FAILED;
        try
        {
            if (requester is null) { decision = Decision.DENIED; return new(decision, ""); }
            if (!budget.ReserveUnsafe()) { decision = Decision.STOPPED; return new(decision, ""); }
            AfterInitialCheck();
            object raw = proposal!.Arguments!.GetValueOrDefault("documentId", 101);
            int id = raw is int number ? number : 101;
            string body = "";
            // Match Java null tool failure, rather than silently accepting null.
            if (proposal.Tool!.Equals("readDocument", StringComparison.Ordinal)) body = fixture.Materialize(id);
            Interlocked.Increment(ref fixture.ToolEffects);
            decision = Decision.ALLOWED;
            return new(decision, body);
        }
        catch (Exception) { return new(decision, ""); }
        finally { fixture.RecordAudit(decision); }
    }
}