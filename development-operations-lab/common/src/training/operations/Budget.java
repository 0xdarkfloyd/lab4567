package training.operations;

import java.util.concurrent.atomic.AtomicInteger;

/** Per-run one-unit quota; no durable/distributed reservations or monetary accounting. */
public final class Budget {
    private final AtomicInteger remaining;
    private int unsafeRemaining;
    public volatile Runnable unsafeAfterSnapshot = () -> { };
    public Budget(int slots) {
        if (slots < 0) throw new IllegalArgumentException("negative quota");
        remaining = new AtomicInteger(slots); unsafeRemaining = slots;
    }
    public boolean reserve() {
        int current;
        do {
            current = remaining.get();
            if (current <= 0) return false;
        } while (!remaining.compareAndSet(current, current - 1));
        return true;
    }
    public void release() { remaining.incrementAndGet(); }
    public boolean reserveUnsafe() {
        int snapshot = unsafeRemaining;
        unsafeAfterSnapshot.run();
        if (snapshot <= 0) return false;
        unsafeRemaining = snapshot - 1; return true;
    }
    public int remaining() { return remaining.get(); }
}