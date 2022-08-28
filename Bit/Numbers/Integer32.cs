using System.Text;

namespace Bit.Numbers;

using static Gate;

public readonly struct Integer32 : IEquatable<Integer32>
{
    private readonly Bit[] table;

    public static Integer32 Zero => new(0);
    public static Integer32 One => new(1);

    public Integer32()
    {
        this.table = new Bit[32];
    }

    public Integer32(int number)
    {
        this.table = number.ToBits();
    }

    public Integer32(params int[] bits)
    {
        this.table = new Bit[32];
        for (var i = 0; i < bits.Length; i++)
        {
            this.table[i] = bits[bits.Length - i - 1] != 0;
        }
    }

    public Integer32(Bit[] bits)
    {
        if (bits.Length != 32) throw new ArgumentException("bits length must be 32");
        this.table = bits;
    }

    public int ToInt()
    {
        return this.table.ToInt();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < 32; i++)
        {
            sb.Insert(0, this.table[i] ? 1 : 0);
        }

        return $"{this.ToInt()} [{sb.ToString()}]";
    }

    public override bool Equals(object? obj) => obj is Integer32 other && Equals(other);
    public override int GetHashCode() => this.ToInt().GetHashCode();
    public bool Equals(Integer32 other)
    {
        for (var i = 0; i < 32; i++)
        {
            if (Xnor(this.table[i], other.table[i])) continue;
            return false;
        }

        return true;
    }

    public static Integer32 operator &(Integer32 a, Integer32 b)
    {
        var ret = new Bit[32];
        for (var i = 0; i < 32; i++)
        {
            ret[i] = And(a.table[i], b.table[i]);
        }

        return new Integer32(ret);
    }

    public static Integer32 operator |(Integer32 a, Integer32 b)
    {
        var ret = new Bit[32];
        for (var i = 0; i < 32; i++)
        {
            ret[i] = Or(a.table[i], b.table[i]);
        }

        return new Integer32(ret);
    }

    public static Integer32 operator ^(Integer32 a, Integer32 b)
    {
        var ret = new Bit[32];
        for (var i = 0; i < 32; i++)
        {
            ret[i] = Xor(a.table[i], b.table[i]);
        }

        return new Integer32(ret);
    }

    public static Integer32 operator ~(Integer32 a)
    {
        var ret = new Bit[32];
        for (var i = 0; i < 32; i++)
        {
            ret[i] = Not(a.table[i]);
        }

        return new Integer32(ret);
    }

    public static Integer32 operator >>(Integer32 a, int b)
    {
        var ret = new Bit[32];
        for (var i = b; i < 32; i++)
        {
            ret[i - b] = a.table[i];
        }

        return new Integer32(ret);
    }

    public static Integer32 operator <<(Integer32 a, int b)
    {
        var ret = new Bit[32];
        for (var i = b; i < 32; i++)
        {
            ret[i] = a.table[i - b];
        }

        return new Integer32(ret);
    }

    public static Integer32 operator -(Integer32 a)
    {
        var t = Bit.Zero;
        for (var i = 0; i < 31; i++)
        {
            t = Or(t, a.table[i]);
        }
        if (And(a.table[31], Not(t)))
        {
            throw new OverflowException();
        }

        var ret = new Bit[32];
        for (var i = 0; i < 32; i++)
        {
            ret[i] = Not(a.table[i]);
        }
        (ret[0], var c) = CustomGate.HalfAdder(ret[0], Bit.One);
        for (var i = 1; i < 32; i++)
        {
            (ret[i], c) = CustomGate.FullAdder(ret[i], Bit.Zero, c);
        }

        return new Integer32(ret);
    }

    public static Integer32 operator +(Integer32 a, Integer32 b)
    {
        var ret = new Bit[32];
        (ret[0], var c) = CustomGate.HalfAdder(a.table[0], b.table[0]);
        for (var i = 1; i < 32; i++)
        {
            (ret[i], c) = CustomGate.FullAdder(a.table[i], b.table[i], c);
        }

        if (Or(And(And(a.table[31], b.table[31]), Not(ret[31])), And(And(Not(a.table[31]), Not(b.table[31])), ret[31])))
        {
            throw new OverflowException();
        }

        return new Integer32(ret);
    }

    public static Integer32 operator -(Integer32 a, Integer32 b)
    {
        var ret = new Bit[32];
        var c = Bit.One;
        for (var i = 0; i < 32; i++)
        {
            (ret[i], c) = CustomGate.FullAdder(a.table[i], Not(b.table[i]), c);
        }

        if (Or(And(a.table[31], Nor(b.table[31], ret[31])), And(Not(a.table[31]), And(b.table[31], ret[31]))))
        {
            throw new OverflowException();
        }

        return new Integer32(ret);
    }

    public static Integer32 operator *(Integer32 a, Integer32 b)
    {
        var ret = new Bit[32];

        Bit[] Abs(Integer32 x)
        {
            if (Not(x.table[31])) return x.table;

            var xx = new Bit[32];
            for (var i = 0; i < 32; i++)
            {
                xx[i] = Not(x.table[i]);
            }
            (xx[0], var c) = CustomGate.HalfAdder(xx[0], Bit.One);
            for (var i = 1; i < 32; i++)
            {
                (xx[i], c) = CustomGate.FullAdder(xx[i], Bit.Zero, c);
            }

            return xx;
        }

        var aa = Abs(a);
        var bb = Abs(b);
        for (var i = 0; i < 31; i++)
        {
            if (Not(bb[i])) continue;
            var tmp = aa[..^i].PadLeft(32);
            (ret[0], var c) = CustomGate.HalfAdder(ret[0], tmp[0]);
            for (var j = 1; j < 32; j++)
            {
                (ret[j], c) = CustomGate.FullAdder(ret[j], tmp[j], c);
            }
        }

        if (Xor(a.table[31], b.table[31]))
        {
            for (var i = 0; i < 32; i++)
            {
                ret[i] = Not(ret[i]);
            }
            (ret[0], var c) = CustomGate.HalfAdder(ret[0], Bit.One);
            for (var i = 1; i < 32; i++)
            {
                (ret[i], c) = CustomGate.FullAdder(ret[i], Bit.Zero, c);
            }
        }

        return new Integer32(ret);
    }

    public static Integer32 operator /(Integer32 a, Integer32 b)
    {
        for (var i = 0; i < 32; i++)
        {
            if (Xnor(b.table[i], Bit.One)) goto NotZero;
        }
        throw new DivideByZeroException();

    NotZero:
        var ret = new Bit[32];

        Bit[] Abs(Integer32 x)
        {
            if (Not(x.table[31])) return x.table;

            var xx = new Bit[32];
            for (var i = 0; i < 32; i++)
            {
                xx[i] = Not(x.table[i]);
            }
            (xx[0], var c) = CustomGate.HalfAdder(xx[0], Bit.One);
            for (var i = 1; i < 32; i++)
            {
                (xx[i], c) = CustomGate.FullAdder(xx[i], Bit.Zero, c);
            }

            return xx;
        }

        var aa = Abs(a);
        var bb = Abs(b);
        var r = new Bit[32];
        for (var i = 0; i < 32; i++)
        {
            r[0] = aa[^(i + 1)];

            var tmp = new Bit[32];
            var c = Bit.One;
            for (var j = 0; j < 32; j++)
            {
                (tmp[j], c) = CustomGate.FullAdder(r[j], Not(bb[j]), c);
            }
            var ok = Not(tmp[31]);
            ret[^(i + 1)] = ok;

            c = Bit.One;
            for (var j = 0; j < 32; j++)
            {
                (r[j], c) = CustomGate.FullAdder(r[j], Not(And(bb[j], ok)), c);
            }

            for (var j = 31; j > 0; j--)
            {
                r[j] = r[j - 1];
            }
        }

        if (Xor(a.table[31], b.table[31]))
        {
            for (var i = 0; i < 32; i++)
            {
                ret[i] = Not(ret[i]);
            }
            (ret[0], var c) = CustomGate.HalfAdder(ret[0], Bit.One);
            for (var i = 1; i < 32; i++)
            {
                (ret[i], c) = CustomGate.FullAdder(ret[i], Bit.Zero, c);
            }
        }

        return new Integer32(ret);
    }

    public static bool operator ==(Integer32 left, Integer32 right) => left.Equals(right);
    public static bool operator !=(Integer32 left, Integer32 right) => !(left == right);
}