namespace FibUnitTests;

[TestFixture]
public class Tests
{
    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    public void TestFibonacci(int expected, int index)
    {
        Assert.That(GetFibonacci(index), Is.EqualTo(expected));
    }

    
    private int GetFibonacci(int index)
    {
        if(index == 0) return 0;
        if(index == 1) return 1;
        return GetFibonacci(index - 1) + GetFibonacci(index - 2);
    }
}