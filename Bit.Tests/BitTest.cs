namespace Bit.Tests;

public class BitTest
{
    [Fact]
    public void ConstructByBoolean()
    {
        var a = new Bit(true);
        var b = new Bit(true);
        Assert.True(a == b);

        var c = new Bit(false);
        var d = new Bit(false);
        Assert.True(c == d);

        Assert.False(a == c);
        Assert.False(b == d);
    }

    [Fact]
    public void ConstructBy01()
    {
        var a = new Bit(1);
        var b = new Bit(1);
        Assert.True(a == b);

        var c = new Bit(0);
        var d = new Bit(0);
        Assert.True(c == d);

        Assert.False(a == c);
        Assert.False(b == d);
    }

    [Fact]
    public void ConstructByInt()
    {
        var a = new Bit(1);
        var b = new Bit(2);
        var c = new Bit(-10);
        var d = new Bit(0);
        Assert.True(a == b);
        Assert.True(a == c);
        Assert.False(b == d);
        Assert.False(c == d);
    }

    [Fact]
    public void ConvertToBoolean()
    {
        var a = new Bit(true);
        var b = new Bit(false);
        Assert.True(a);
        Assert.False(b);

        var c = new Bit(1);
        var d = new Bit(0);
        Assert.True(c);
        Assert.False(d);
    }

    [Fact]
    public void ConvertFromBoolean()
    {
        Bit a = true;
        Bit b = false;
        Assert.True(a == new Bit(true));
        Assert.False(b == new Bit(true));
    }

    [Fact]
    public void Fill()
    {
        void Act0(Bit i) => Assert.Equal(Bit.Zero, i);
        void Act1(Bit i) => Assert.Equal(Bit.One, i);

        var a = Bit.Fill(5, Bit.Zero);
        Assert.Collection(a, Act0, Act0, Act0, Act0, Act0);

        var b = Bit.Fill(6, Bit.One);
        Assert.Collection(b, Act1, Act1, Act1, Act1, Act1, Act1);
    }
}

public class BitUtilTest
{
    private readonly Action<Bit> b0;
    private readonly Action<Bit> b1;

    public BitUtilTest()
    {
        this.b0 = i => Assert.Equal(Bit.Zero, i);
        this.b1 = i => Assert.Equal(Bit.One, i);
    }

    [Fact]
    public void PadLeft()
    {
        var a = (new[] { Bit.One, Bit.Zero, Bit.One }).PadLeft(6);
        Assert.Collection(a, this.b0, this.b0, this.b0, this.b1, this.b0, this.b1);

        var b = Array.Empty<Bit>().PadLeft(4);
        Assert.Collection(b, this.b0, this.b0, this.b0, this.b0);

        var c = (new[] { Bit.One, Bit.Zero, Bit.One }).PadLeft(2);
        Assert.Collection(c, this.b1, this.b0, this.b1);
    }

    [Fact]
    public void PadRight()
    {
        var a = new[] { Bit.One, Bit.Zero, Bit.One }.PadRight(6);
        Assert.Collection(a, this.b1, this.b0, this.b1, this.b0, this.b0, this.b0);

        var b = Array.Empty<Bit>().PadRight(4);
        Assert.Collection(b, this.b0, this.b0, this.b0, this.b0);

        var c = new[] { Bit.One, Bit.Zero, Bit.One }.PadRight(2);
        Assert.Collection(c, this.b1, this.b0, this.b1);
    }

    [Fact]
    public void ToBitsFromInt()
    {
        var a = 0b1011101.ToBits();
        var ca = new[] { Bit.One, Bit.Zero, Bit.One, Bit.One, Bit.One, Bit.Zero, Bit.One }
            .PadLeft(32)
            .Reverse()
            .ToArray();
        Assert.Equal(ca, a);

        var b = (-1).ToBits();
        Assert.Equal(Bit.Fill(32, Bit.One), b);

        var c = 0.ToBits();
        Assert.Equal(Bit.Fill(32, Bit.Zero), c);
    }

    [Fact]
    public void ToInt()
    {
        // 0b1010 = 10
        var a = new[] { Bit.One, Bit.Zero, Bit.One, Bit.Zero }.Reverse().ToArray();
        Assert.Equal(10, a.ToInt());

        var b = Bit.Fill(32, Bit.One).ToInt();
        Assert.Equal(-1, b);

        var c = Bit.Fill(32, Bit.Zero).ToInt();
        Assert.Equal(0, c);
    }
}