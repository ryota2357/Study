namespace Bit;

using static Gate;

public static class CustomGate
{
    public static (Bit sum, Bit carry) HalfAdder(Bit x, Bit y)
    {
        return (Xor(x, y), And(x, y));
    }

    public static (Bit sum, Bit carry) FullAdder(Bit x, Bit y, Bit c)
    {
        var (s1, c1) = HalfAdder(x, y);
        var (s2, c2) = HalfAdder(s1, c);
        return (s2, Or(c1, c2));
    }
}