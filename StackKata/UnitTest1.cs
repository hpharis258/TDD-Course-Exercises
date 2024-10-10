namespace StackKata;

[TestFixture]
public class StackTests
{
    [Test]
    public void IsEmpty_EmptyStack_ReturnsTrue()
    {
        var stack = new Stack<int>();
        Assert.IsTrue(stack.IsEmpty());
    }
    
    [Test]
    public void Count_PushOneItem_ReturnsOne()
    {
        var stack = new Stack<int>();
        stack.Push(1);
        Assert.AreEqual(1, stack.Count);
        Assert.IsFalse(stack.IsEmpty());
    }
    
    [Test]
    public void PopEmptyStackThrowsExecption()
    {
        var stack = new Stack<int>();
        Assert.Throws<InvalidOperationException>(() => stack.Pop());
    }
    
    [Test]
    public void PeekAfterTwoItemsReturnsHeadItem()
    {
        var stack = new Stack<int>();
        stack.Push(1);
        stack.Push(2);
        Assert.AreEqual(2, stack.Peek());
    }

    [Test]
    public void PeekPushTwoItemsAndPopReturnsHeadElement()
    {
        var stack = new Stack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Pop();
        Assert.AreEqual(1, stack.Peek());
    }
}


public class Stack<T>
{
   
    private List<T> _items = new List<T>();
    public int Count => _items.Count;
    public void Push(T value)
    {
        _items.Add(value);
    }
    public bool IsEmpty()
    {
        return Count == 0;
    }
    public void Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException();
        }
        _items.RemoveAt(Count - 1);
    }
    
    public T Peek()
    {
        return _items[Count - 1];
    }
}