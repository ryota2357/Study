namespace Bit;

public readonly struct Bit : IEquatable<Bit>
{
    private readonly bool value;

    public static Bit Zero => new(false);
    public static Bit One => new(true);

    public Bit(int value)
    {
        this.value = value != 0;
    }

    public Bit(bool value)
    {
        this.value = value;
    }

    public static Bit[] Fill(int length, Bit value)
    {
        var bits = new Bit[length];
        for (var i = 0; i < length; i++)
        {
            bits[i] = value;
        }

        return bits;
    }

    public static implicit operator Bit(bool value)
    {
        return new Bit(value);
    }

    public static implicit operator bool(Bit bit)
    {
        return bit.value;
    }

    public override string ToString()
    {
        return this.value ? "1" : "0";
    }

    public bool Equals(Bit other) => this.value == other.value;
    public override bool Equals(object? obj) => obj is Bit other && Equals(other);
    public override int GetHashCode() => this.value ? 1 : 0;
    public static bool operator ==(Bit left, Bit right) => left.Equals(right);
    public static bool operator !=(Bit left, Bit right) => !(left == right);
}

public static class BitUtil
{
    public static Bit[] Reverse(this Bit[] bits)
    {
        var length = bits.Length;
        var result = new Bit[length];
        for (var i = 0; i < length; i++)
        {
            result[i] = bits[length - i - 1];
        }

        return result;
    }

    public static Bit[] PadLeft(this Bit[] bits, int length)
    {
        if (bits.Length >= length) return bits;
        var result = new Bit[length];
        Array.Copy(bits, 0, result, length - bits.Length, bits.Length);
        return result;
    }

    public static Bit[] PadRight(this Bit[] bits, int length)
    {
        if (bits.Length >= length) return bits;
        var result = new Bit[length];
        Array.Copy(bits, 0, result, 0, bits.Length);
        return result;
    }

    public static Bit[] ToBits(this int value)
    {
        var bits = new Bit[32];
        for (var i = 0; i < 32; i++)
        {
            bits[i] = (value & (1 << i)) != 0;
        }

        return bits;
    }

    public static Bit[] ToBits(this string value)
    {
        var bits = new Bit[value.Length];
        for (var i = 0; i < value.Length; i++)
        {
            bits[i] = value[i] switch
            {
                '1' => new Bit(1),
                '0' => new Bit(0),
                _ => throw new ArgumentException($"{value[i]} is not a valid bit")
            };
        }

        return bits;
    }

    public static int ToInt(this Bit[] bits)
    {
        var value = 0;
        for (var i = 0; i < bits.Length; i++)
        {
            if (bits[i])
            {
                value |= 1 << i;
            }
        }

        return value;
    }
}