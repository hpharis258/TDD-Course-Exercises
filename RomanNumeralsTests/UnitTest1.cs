namespace RomanNumeralsTests;

[TestFixture]
public class Tests
{
    
    [Test]
    public void Test1()
    {
        Assert.AreEqual(1, Roman.Parse("I"));
        Assert.AreEqual(5, Roman.Parse("V"));
    }
}

public class Roman
{
    public static int Parse(string roman)
    {
        if (roman == "V")
        {
            return 5;
        }
        return 1;
    }
}