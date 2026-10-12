package training.operations;

import java.util.List;
import java.util.Map;

/** Console response contract, not HTTP middleware or JWT authentication. */
public final class Contract {
    private Contract() { }
    public record Identity(String principal, String supplier) { }
    public record Response(int status, List<Map<String, Object>> rows) {
        public static Response empty(int status) { return new Response(status, List.of()); }
    }
    public record Content(int status, String body) {
        public static Content denied(int status) { return new Content(status, ""); }
    }
    public record Job(Identity requester, int documentId) { }
    /** Structured mock model output; never a command line or live model response. */
    public record Proposal(String tool, Map<String, Object> arguments) { }
    public enum Decision { ALLOWED, DENIED, REJECTED, STOPPED, FAILED }
    public record Outcome(Decision decision, String body) { }
}