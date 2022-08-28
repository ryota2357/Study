using Bit.Numbers;

namespace Bit.Tests.Numbers;

public class Integer32Test
{
    [Fact]
    public void ConstructFromInt()
    {
        var a = new Integer32(1234);
        var b = new Integer32(1234);
        var c = new Integer32(4321);
        Assert.True(a == b);
        Assert.False(a == c);
        Assert.False(b == c);

        var zero = new Integer32(0);
        Assert.True(zero == Integer32.Zero);

        var one = new Integer32(1);
        Assert.True(one == Integer32.One);
    }

    [Fact]
    public void ConstructFromBit()
    {
        var a = new Integer32(1, 1, 0, 1);
        Assert.True(a == new Integer32(8 + 4 + 1));

        var b = new Integer32();
        Assert.True(b == Integer32.Zero);

        var c = new Integer32(Bit.Fill(32, Bit.One));
        Assert.True(c == new Integer32(-1));
    }

    [Fact]
    public void ToInt()
    {
        var a = new Integer32(123456789);
        Assert.Equal(123456789, a.ToInt());

        var b = new Integer32(-123456789);
        Assert.Equal(-123456789, b.ToInt());

        var c = new Integer32(0);
        Assert.Equal(0, c.ToInt());

        var d = new Integer32(1);
        Assert.Equal(1, d.ToInt());

        var e = new Integer32(-1);
        Assert.Equal(-1, e.ToInt());

        var max = new Integer32(int.MaxValue);
        Assert.Equal(int.MaxValue, max.ToInt());

        var min = new Integer32(int.MinValue);
        Assert.Equal(int.MinValue, min.ToInt());
    }

    [Fact]
    public void LeftShift()
    {
        var a = new Integer32(0b1011);
        Assert.Equal(new Integer32(0b1011), a << 0);
        Assert.Equal(new Integer32(0b10110), a << 1);
        Assert.Equal(new Integer32(new[] { Bit.One }.PadLeft(32)), a << 31);
        Assert.Equal(Integer32.Zero, a << 32);
        Assert.Equal(Integer32.Zero, a << 100);
    }

    [Fact]
    public void RightShift()
    {
        var a = new Integer32(0b1011);
        var b = new Integer32(0b1011000000000000000000000000000);
        Assert.Equal(new Integer32(0b1011), a >> 0);
        Assert.Equal(new Integer32(0b101), a >> 1);
        Assert.Equal(new Integer32(new[] { Bit.Zero }.PadLeft(32)), b >> 31);
        Assert.Equal(Integer32.Zero, b >> 32);
        Assert.Equal(Integer32.Zero, b >> 100);
    }

    [Fact]
    public void Minus()
    {
        var a = 0b00110000010000010000001000000011;
        var ia = new Integer32(a);
        Assert.Equal(new Integer32(-a), -ia);

        var b = Integer32.Zero;
        Assert.Equal(Integer32.Zero, -b);

        var c = Integer32.One;
        Assert.Equal(new Integer32(-1), -c);

        var d = new Integer32(int.MinValue);
        Assert.Throws<OverflowException>(() => -d);
    }

    [Fact]
    public void Add()
    {
        var a = new Integer32(123456789);
        var b = new Integer32(987654321);
        Assert.Equal(new Integer32(1111111110), a + b);

        var c = new Integer32(-123456789);
        var d = new Integer32(-987654321);
        Assert.Equal(new Integer32(-1111111110), c + d);

        var e = new Integer32(0);
        var f = new Integer32(0);
        Assert.Equal(new Integer32(0), e + f);

        var g = new Integer32(1);
        var h = new Integer32(-1);
        Assert.Equal(new Integer32(0), g + h);
        Assert.Equal(new Integer32(0), h + g);

        var i = new Integer32(-12345);
        var j = new Integer32(98765);
        Assert.Equal(new Integer32(86420), i + j);
        Assert.Equal(new Integer32(86420), j + i);

        var k = new Integer32(int.MaxValue);
        Assert.Throws<OverflowException>(() => k + Integer32.One);

        var l = new Integer32(int.MinValue);
        Assert.Throws<OverflowException>(() => l + new Integer32(-1));
    }

    [Fact]
    public void Sub()
    {
        var a = new Integer32(123456789);
        var b = new Integer32(987654321);
        Assert.Equal(new Integer32(-864197532), a - b);

        var c = new Integer32(-123456789);
        var d = new Integer32(-987654321);
        Assert.Equal(new Integer32(864197532), c - d);

        var e = new Integer32(0);
        var f = new Integer32(0);
        Assert.Equal(new Integer32(0), e - f);

        var g = new Integer32(1);
        var h = new Integer32(1);
        Assert.Equal(new Integer32(0), g - h);
        Assert.Equal(new Integer32(0), h - g);

        var i = new Integer32(-12345);
        var j = new Integer32(98765);
        Assert.Equal(new Integer32(-111110), i - j);
        Assert.Equal(new Integer32(111110), j - i);

        var k = new Integer32(int.MinValue);
        Assert.Throws<OverflowException>(() => k - Integer32.One);

        var l = new Integer32(int.MaxValue);
        Assert.Throws<OverflowException>(() => l - new Integer32(-1));
    }

    [Fact]
    public void Multiply()
    {
        var a = new Integer32(12345);
        var b = new Integer32(98765);
        Assert.Equal(new Integer32(1219253925), a * b);

        var c = new Integer32(-12345);
        var d = new Integer32(-98765);
        Assert.Equal(new Integer32(1219253925), c * d);

        var e = new Integer32(0);
        var f = new Integer32(0);
        Assert.Equal(new Integer32(0), e * f);
        Assert.Equal(new Integer32(0), e * Integer32.Zero);

        var g = new Integer32(1);
        var h = new Integer32(1);
        Assert.Equal(new Integer32(1), g * h);
        Assert.Equal(new Integer32(1), h * Integer32.One);

        var i = new Integer32(-12345);
        var j = new Integer32(98765);
        Assert.Equal(new Integer32(-1219253925), i * j);
        Assert.Equal(new Integer32(-1219253925), j * i);

        var k = new Integer32(123);
        Assert.Equal(new Integer32(0), k * Integer32.Zero);
        Assert.Equal(new Integer32(123), k * Integer32.One);
    }

    [Fact]
    public void Division()
    {
        var a = new Integer32(512);
        var b = new Integer32(16);
        Assert.Equal(new Integer32(32), a / b);
        Assert.Equal(new Integer32(-32), (-a) / b);
        Assert.Equal(new Integer32(-32), a / (-b));
        Assert.Equal(new Integer32(32), (-a) / (-b));

        var c = new Integer32(98765432);
        var d = new Integer32(12345678);
        Assert.Equal(new Integer32(8), c / d);
        Assert.Equal(new Integer32(-8), (-c) / d);
        Assert.Equal(new Integer32(-8), c / (-d));
        Assert.Equal(new Integer32(8), (-c) / (-d));

        var e = new Integer32(123);
        var f = new Integer32(12736);
        Assert.Equal(Integer32.Zero, e / f);
        Assert.Equal(Integer32.Zero, (-e) / f);
        Assert.Equal(Integer32.Zero, e / (-f));
        Assert.Equal(Integer32.Zero, (-e) / (-f));

        var g = new Integer32(31);
        Assert.Equal(Integer32.Zero, Integer32.Zero / g);
        Assert.Equal(Integer32.Zero, Integer32.Zero / (-g));

        Assert.Throws<DivideByZeroException>(() => g / Integer32.Zero);
    }
}