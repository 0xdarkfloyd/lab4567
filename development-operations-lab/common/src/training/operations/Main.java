package training.operations;

import java.util.List;
import java.util.Map;
import static training.operations.Contract.*;

public final class Main {
    private Main() { }
    public static void main(String[] args) throws Exception {
        if (args.length != 2 || !List.of("exercise", "fixed").contains(args[0]) ||
                !List.of("demo", "test", "lab1", "lab2").contains(args[1])) {
            System.err.println("Expected track exercise|fixed and command demo|test|lab1|lab2");
            System.exit(2); return;
        }
        if (!args[1].equals("demo")) { System.exit(Harness.run(args[0], args[1])); return; }
        System.out.println("LOCAL CONSOLE ADAPTER. H2 2.3.232 real SQL. Not Spring/HTTP/JWT. Track=" + args[0]);
        try (Fixture f = new Fixture()) {
            Search search = new Search(f);
            for (String name : List.of("Contract", "O'Reilly Guide", "' OR '1'='1")) {
                Response result = search.search("Bearer ALICE-DEMO", name);
                System.out.println("OBSERVED search status=" + result.status() + " IDs=" +
                    result.rows().stream().map(row -> row.get("id")).toList() + " fields=" +
                    (result.rows().isEmpty() ? List.of() : result.rows().get(0).keySet()));
            }
            System.out.println("OBSERVED captured token leakage=" + f.logs.stream().anyMatch(line -> line.contains("ALICE-DEMO")));
        }
        for (MockModel.Example example : MockModel.examples()) {
            System.out.println("SCRIPTED MOCK variant=" + example.variant());
            System.out.println("Synthetic source: " + example.source());
            System.out.println(example.mockOutput());
        }
        List<Proposal> proposals = List.of(
            new Proposal("readDocument", Map.of("documentId", 101)),
            new Proposal("readDocument", Map.of("documentId", 102)),
            new Proposal("approveRisk", Map.of("documentId", 101)),
            new Proposal("shell", Map.of("documentId", 101)),
            new Proposal("readDocument", Map.of("documentId", "101")),
            new Proposal("readDocument", Map.of("documentId", 101)));
        String[] labels = {"authorised", "cross-supplier", "risk approval", "unknown tool", "malformed", "zero budget"};
        for (int i = 0; i < proposals.size(); i++) {
            try (Fixture f = new Fixture()) {
                Broker broker = new Broker(f, new Budget(i == 5 ? 0 : 1));
                Outcome result = broker.call(f.authenticate("Bearer ALICE-DEMO"), proposals.get(i));
                System.out.printf("OBSERVED MOCK broker %s: %s contentReads=%d toolEffects=%d audit=%s%n",
                    labels[i], result.decision(), f.contentReads.get(), f.toolEffects.get(), f.audit);
            }
        }
        System.out.println("Demo records observations only; run test/lab1/lab2 for security-oracle exit status.");
    }
}