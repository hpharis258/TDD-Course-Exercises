namespace FizzBuzzTests;

[TestFixture]
public class FizzBuzzTests
{
    /*
     *  FizzBuzz is a simple interview question that gets asked a lot for junior developer roles, and was actually a question that I got asked in my first job. 
     *  If Divisible by 3 => Fizz
     *  If Divisible by 5 => Buzz
     *  If Divisible by 3 and 5 => FizzBuzz
     *  If not Divisible by 3 or 5 => return empty string
     */
    [TestCase("", 1)]
    [TestCase("", 2)]
    [TestCase("Fizz", 3)]
    [TestCase("", 4)]
    [TestCase("Buzz", 5)]
    [TestCase("FizzBuzz", 15)]
    [TestCase("FizzBuzz", 30)]
    public void TestFizzBuzz(string expected, int number)
    {
        Assert.AreEqual(expected, FizzBuzz(number));
    }
    
    private string FizzBuzz(int number)
    {
        string result = "";
        if(number % 3 == 0)
        {
            result += "Fizz";
        }
        if(number % 5 == 0)
        {
            result += "Buzz";
        }
        return result;
    }
}

