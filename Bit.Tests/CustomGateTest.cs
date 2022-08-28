namespace Bit.Tests;

public class CustomGateTest
{
    [Fact]
    public void HalfAdder()
    {
        var ha00 = CustomGate.HalfAdder(Bit.Zero, Bit.Zero);
        Assert.Equal((Bit.Zero, Bit.Zero), ha00);
        
        var ha01 = CustomGate.HalfAdder(Bit.Zero, Bit.One);
        Assert.Equal((Bit.One, Bit.Zero), ha01);
        
        var ha10 = CustomGate.HalfAdder(Bit.One, Bit.Zero);
        Assert.Equal((Bit.One, Bit.Zero), ha10);
        
        var ha11 = CustomGate.HalfAdder(Bit.One, Bit.One);
        Assert.Equal((Bit.Zero, Bit.One), ha11);
    }

    [Fact]
    public void FullAdder()
    {
        var fa000 = CustomGate.FullAdder(Bit.Zero, Bit.Zero, Bit.Zero);
        Assert.Equal((Bit.Zero, Bit.Zero), fa000);
        
        var fa001 = CustomGate.FullAdder(Bit.Zero, Bit.Zero, Bit.One);
        Assert.Equal((Bit.One, Bit.Zero), fa001);
        
        var fa010 = CustomGate.FullAdder(Bit.Zero, Bit.One, Bit.Zero);
        Assert.Equal((Bit.One, Bit.Zero), fa010);
        
        var fa011 = CustomGate.FullAdder(Bit.Zero, Bit.One, Bit.One);
        Assert.Equal((Bit.Zero, Bit.One), fa011);
     
        var fa100 = CustomGate.FullAdder(Bit.One, Bit.Zero, Bit.Zero);
        Assert.Equal((Bit.One, Bit.Zero), fa100);
    
        var fa101 = CustomGate.FullAdder(Bit.One, Bit.Zero, Bit.One);
        Assert.Equal((Bit.Zero, Bit.One), fa101);
   
        var fa110 = CustomGate.FullAdder(Bit.One, Bit.One, Bit.Zero);
        Assert.Equal((Bit.Zero, Bit.One), fa110);
  
        var fa111 = CustomGate.FullAdder(Bit.One, Bit.One, Bit.One);
        Assert.Equal((Bit.One, Bit.One), fa111);
    }
}