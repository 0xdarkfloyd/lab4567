package training.operations;

import java.sql.SQLException;
import static training.operations.Contract.*;

/** Intentionally permissive mock broker. Never executes an external tool. */
public final class Broker {
    private final Fixture fixture;
    private final Budget budget;
    public volatile Runnable afterInitialCheck = () -> { };
    public Broker(Fixture fixture, Budget budget) { this.fixture = fixture; this.budget = budget; }
    public Outcome call(Identity requester, Proposal proposal) {
        Decision decision = Decision.FAILED;
        try {
            if (requester == null) { decision = Decision.DENIED; return new Outcome(decision, ""); }
            if (!budget.reserveUnsafe()) { decision = Decision.STOPPED; return new Outcome(decision, ""); }
            afterInitialCheck.run();
            Object rawId = proposal.arguments().getOrDefault("documentId", 101);
            int id = rawId instanceof Integer number ? number : 101;
            // Unknown tools fall through to a mock effect. Extra identity/arguments are ignored.
            String body = "";
            if (proposal.tool().equals("readDocument")) body = fixture.materialize(id);
            fixture.toolEffects.incrementAndGet();
            decision = Decision.ALLOWED; return new Outcome(decision, body);
        } catch (SQLException | RuntimeException failure) {
            return new Outcome(decision, "");
        } finally { fixture.audit(decision); }
    }
}