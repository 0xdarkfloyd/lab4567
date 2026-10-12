namespace Training.Operations;

// One-unit in-process quota, not monetary accounting or a distributed reservation.
public sealed class Budget
{
    private int remaining;
    private int unsafeRemaining;
    public Action UnsafeAfterSnapshot = () => { };
    public Budget(int slots)
    {
        if (slots < 0) throw new ArgumentOutOfRangeException(nameof(slots));
        remaining = unsafeRemaining = slots;
    }
    public bool Reserve()
    {
        int current;
        do
        {
            current = Volatile.Read(ref remaining);
            if (current <= 0) return false;
        } while (Interlocked.CompareExchange(ref remaining, current - 1, current) != current);
        return true;
    }
    public void Release() => Interlocked.Increment(ref remaining);
    public int Remaining => Volatile.Read(ref remaining);
    public bool ReserveUnsafe()
    {
        int snapshot = unsafeRemaining;
        UnsafeAfterSnapshot();
        if (snapshot <= 0) return false;
        unsafeRemaining = snapshot - 1;
        return true;
    }
}