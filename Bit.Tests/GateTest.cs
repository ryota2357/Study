namespace Bit.Tests;

public class GateTest
{
    [Fact]
    public void And()
    {
        Assert.False(Gate.And(new Bit(0), new Bit(0)));
        Assert.False(Gate.And(new Bit(0), new Bit(1)));
        Assert.False(Gate.And(new Bit(1), new Bit(0)));
        Assert.True(Gate.And(new Bit(1), new Bit(1)));
    }

    [Fact]
    public void Or()
    {
        Assert.False(Gate.Or(new Bit(0), new Bit(0)));
        Assert.True(Gate.Or(new Bit(0), new Bit(1)));
        Assert.True(Gate.Or(new Bit(1), new Bit(0)));
        Assert.True(Gate.Or(new Bit(1), new Bit(1)));
    }

    [Fact]
    public void Xor()
    {
        Assert.False(Gate.Xor(new Bit(0), new Bit(0)));
        Assert.True(Gate.Xor(new Bit(0), new Bit(1)));
        Assert.True(Gate.Xor(new Bit(1), new Bit(0)));
        Assert.False(Gate.Xor(new Bit(1), new Bit(1)));
    }

    [Fact]
    public void Not()
    {
        Assert.True(Gate.Not(new Bit(0)));
        Assert.False(Gate.Not(new Bit(1)));
    }

    [Fact]
    public void Nand()
    {
        Assert.True(Gate.Nand(new Bit(0), new Bit(0)));
        Assert.True(Gate.Nand(new Bit(0), new Bit(1)));
        Assert.True(Gate.Nand(new Bit(1), new Bit(0)));
        Assert.False(Gate.Nand(new Bit(1), new Bit(1)));
    }

    [Fact]
    public void Nor()
    {
        Assert.True(Gate.Nor(new Bit(0), new Bit(0)));
        Assert.False(Gate.Nor(new Bit(0), new Bit(1)));
        Assert.False(Gate.Nor(new Bit(1), new Bit(0)));
        Assert.False(Gate.Nor(new Bit(1), new Bit(1)));
    }

    [Fact]
    public void Xnor()
    {
        Assert.True(Gate.Xnor(new Bit(0), new Bit(0)));
        Assert.False(Gate.Xnor(new Bit(0), new Bit(1)));
        Assert.False(Gate.Xnor(new Bit(1), new Bit(0)));
        Assert.True(Gate.Xnor(new Bit(1), new Bit(1)));
    }
}