package training.operations;

import java.sql.SQLException;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import static training.operations.Contract.*;

/** Same class of missing authorisation across local adapters. Intentionally unsafe. */
public final class Surfaces {
    private final Fixture fixture;
    private final Map<String, String> cache = new HashMap<>();
    public volatile Runnable afterInitialCheck = () -> { };
    public Surfaces(Fixture fixture) { this.fixture = fixture; }
    public Content read(Identity who, int id) {
        if (who == null) return Content.denied(401);
        return retrieve(id);
    }
    private Content retrieve(int id) {
        try { return new Content(200, fixture.materialize(id)); }
        catch (SQLException failure) { return Content.denied(500); }
    }
    public Content preview(Identity who, int id) { return read(who, id); }
    public Content download(Identity who, int id) { return read(who, id); }
    public Content storage(Identity who, int id) { return read(who, id); }
    public Content export(Identity who, List<Integer> ids) {
        StringBuilder out = new StringBuilder();
        for (int id : ids) out.append(read(who, id).body());
        return new Content(200, out.toString());
    }
    public Content cached(Identity who, int id, String name) {
        if (cache.containsKey(name)) return new Content(200, cache.get(name));
        Content result = read(who, id); cache.put(name, result.body()); return result;
    }
    public Job enqueue(Identity who, int id) { return fixture.allowed(who, id) ? new Job(who, id) : null; }
    public Content run(Job job) {
        if (job == null) return Content.denied(403);
        return retrieve(job.documentId()); // Trusts stale enqueue decision.
    }
    public Content deliver(Job job, Content completed) { return completed; }
    public Content guardedRead(Identity who, int id) {
        if (!fixture.allowed(who, id)) return Content.denied(403);
        afterInitialCheck.run();
        return retrieve(id); // Check/use race, no recheck or shared guard.
    }
}