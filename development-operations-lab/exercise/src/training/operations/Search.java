package training.operations;

import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.ResultSet;
import static training.operations.Contract.*;

/** Intentionally vulnerable teaching code. Never deploy. */
public final class Search {
    private final Fixture fixture;
    public Search(Fixture fixture) { this.fixture = fixture; }
    public Response search(String authorization, String name) {
        Identity identity = fixture.authenticate(authorization);
        if (identity == null) return Response.empty(401);
        fixture.logs.add("Auth=" + authorization);
        String sql = "SELECT * FROM Documents WHERE Name = '" + name + "' ORDER BY Id";
        fixture.searchQueries.incrementAndGet();
        try (Connection c = fixture.connection(); Statement s = c.createStatement()) {
            try (ResultSet rs = s.executeQuery(sql)) { return new Response(200, Fixture.entities(rs)); }
        } catch (SQLException failure) {
            // Safe console error, but the vulnerable query still fails for legitimate apostrophes.
            return Response.empty(500);
        }
    }
}