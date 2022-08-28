namespace Bit;

public static class Gate
{
    private readonly static Bit Zero = new(0);
    private readonly static Bit One = new(1);

    public static Bit And(Bit a, Bit b)
    {
        return a == One && b == One;
    }

    public static Bit Or(Bit a, Bit b)
    {
        return a == One || b == One;
    }

    public static Bit Xor(Bit a, Bit b)
    {
        // (a == One && b == Zero) || (a == Zero && b == One);
        return a != b;
    }

    public static Bit Not(Bit a)
    {
        return a == Zero ? One : Zero;
    }

    public static Bit Nand(Bit a, Bit b)
    {
        return a != One || b != One;
    }

    public static Bit Nor(Bit a, Bit b)
    {
        return a != One && b != One;
    }

    public static Bit Xnor(Bit a, Bit b)
    {
        // (a == Zero || b == One) && (a == One || b == Zero);
        return a == b;
    }
}