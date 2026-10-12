package training.operations;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.UUID;
import java.util.concurrent.atomic.AtomicInteger;
import training.operations.Contract.Decision;
import training.operations.Contract.Identity;

/** Trusted synthetic identities, real relational fixture, and observable local effects. */
public final class Fixture implements AutoCloseable {
    private static final Map<String, Identity> IDENTITIES = Map.of(
        "ALICE-DEMO", new Identity("alice", "SUP-A"),
        "BOB-DEMO", new Identity("bob", "SUP-B"),
        "NOSCOPE-DEMO", new Identity("noscope", null));
    private static final Map<Integer, String> OWNERS = Map.of(101, "SUP-A", 102, "SUP-B", 103, "SUP-A");
    public final Object policyLock = new Object();
    private final Set<String> revoked = new HashSet<>();
    private final String url = "jdbc:h2:mem:lab_" + UUID.randomUUID();
    private final Connection anchor;
    public final AtomicInteger searchQueries = new AtomicInteger();
    public final AtomicInteger contentReads = new AtomicInteger();
    public final AtomicInteger toolEffects = new AtomicInteger();
    public volatile Runnable beforeContentQuery = () -> { };
    public final List<String> logs = Collections.synchronizedList(new ArrayList<>());
    public final List<String> audit = Collections.synchronizedList(new ArrayList<>());

    public Fixture() throws SQLException {
        anchor = DriverManager.getConnection(url);
        try (Statement s = anchor.createStatement()) {
            s.execute("CREATE TABLE Documents (Id INT PRIMARY KEY, SupplierId VARCHAR(10), Name VARCHAR(200), ExtraSensitiveField VARCHAR(100), Content VARCHAR(1000))");
        }
        try (PreparedStatement s = anchor.prepareStatement("INSERT INTO Documents VALUES (?, ?, ?, ?, ?)")) {
            seed(s, 101, "SUP-A", "Contract", "INTERNAL-A", "SUP-A contract: retention evidence missing.");
            seed(s, 102, "SUP-B", "Contract", "INTERNAL-B", "SUP-B contract: private supplier evidence.");
            seed(s, 103, "SUP-A", "O'Reilly Guide", "INTERNAL-C", "SUP-A guide: synthetic reference.");
        }
    }
    private static void seed(PreparedStatement s, int id, String supplier, String name, String extra, String content) throws SQLException {
        s.setInt(1, id); s.setString(2, supplier); s.setString(3, name);
        s.setString(4, extra); s.setString(5, content); s.executeUpdate();
    }
    public Connection connection() throws SQLException { return DriverManager.getConnection(url); }
    public Identity authenticate(String authorization) {
        // Exact fixture lookup outside request/model payload; NOT JWT validation.
        if (authorization == null || !authorization.startsWith("Bearer ")) return null;
        return IDENTITIES.get(authorization.substring(7));
    }
    public boolean hasScope(Identity identity) {
        synchronized (policyLock) {
            return identity != null && identity.supplier() != null && !revoked.contains(identity.principal());
        }
    }
    public boolean allowed(Identity identity, int id) {
        synchronized (policyLock) {
            // Non-disclosing trusted metadata check; no content query or returned owner/existence.
            return hasScope(identity) && identity.supplier().equals(OWNERS.get(id));
        }
    }
    public void revoke(Identity identity) {
        synchronized (policyLock) { revoked.add(identity.principal()); }
    }
    /** Caller must enforce current permission; deliberately not enforced here for the exercise. */
    public String materialize(int id) throws SQLException {
        beforeContentQuery.run(); // Instrumentation seam, not a policy check.
        contentReads.incrementAndGet();
        try (Connection c = connection(); PreparedStatement s = c.prepareStatement("SELECT Content FROM Documents WHERE Id = ?")) {
            s.setQueryTimeout(2); s.setInt(1, id);
            try (ResultSet rs = s.executeQuery()) { return rs.next() ? rs.getString(1) : ""; }
        }
    }
    public static List<Map<String, Object>> summaries(ResultSet rs) throws SQLException {
        List<Map<String, Object>> rows = new ArrayList<>();
        while (rs.next()) rows.add(Map.of("id", rs.getInt("Id"), "name", rs.getString("Name")));
        return rows;
    }
    public static List<Map<String, Object>> entities(ResultSet rs) throws SQLException {
        List<Map<String, Object>> rows = new ArrayList<>();
        while (rs.next()) {
            Map<String, Object> row = new HashMap<>();
            row.put("id", rs.getInt("Id")); row.put("name", rs.getString("Name"));
            row.put("supplierId", rs.getString("SupplierId"));
            row.put("extraSensitiveField", rs.getString("ExtraSensitiveField"));
            row.put("content", rs.getString("Content")); rows.add(row);
        }
        return rows;
    }
    public void audit(Decision decision) { audit.add("broker:" + decision.name()); }
    @Override public void close() throws SQLException { anchor.close(); }
}