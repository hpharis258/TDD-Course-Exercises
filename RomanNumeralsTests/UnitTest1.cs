namespace RomanNumeralsTests;

[TestFixture]
public class Tests
{
    
    [TestCase(1, "I")]
    [TestCase(2, "II")]
    [TestCase(3, "III")]
    [TestCase(4, "IV")]
    [TestCase(5, "V")]
    [TestCase(9, "IX")]
    [TestCase(10, "X")]
    [TestCase(14, "XIV")]
    public void ParseTest(int expected, string roman)
    {
        Assert.AreEqual(expected, Roman.Parse(roman));
    }
}

public class Roman
{
    private static readonly Dictionary<char, int> map = new Dictionary<char, int>()
    {
        {'I', 1},
        {'V', 5},
        {'X', 10 },
        {'L', 50},
        {'C', 100},
        {'D', 500},
        {'M', 1000}
    };
    public static int Parse(string roman)
    {
        int output = 0;
        for (int i = 0; i < roman.Length; i++)
        {
            if (i + 1 < roman.Length && IsSubtractive(roman[i], roman[i+1]))
            {
                output -= map[roman[i]];
            }
            else
            {
                output += map[roman[i]];
            }
        }
        return output;
    }

    public static bool IsSubtractive(char a, char b)
    {
        return  map[a] < map[b];
    }
}