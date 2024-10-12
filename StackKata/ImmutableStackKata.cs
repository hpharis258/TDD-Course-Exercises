namespace StackKata;

public interface IStack<T>
{
    IStack<T> Push(T value);
    IStack<T> Pop();
    T Peek();
    bool IsEmpty { get;  } 
} 

[TestFixture]
public class ImmutableStackTests
{
   [Test]
    public void IsEmpty_EmptyStack_ReturnsTrue()
    {
         var EmptyStack = new ImmutableStack<int>().Empty;
         Assert.IsTrue(EmptyStack.IsEmpty);
    }
    
    [Test]
    public void PeekAndPop_EmptyStack_ThrowsExecption()
    {
        var EmptyStack = new ImmutableStack<int>().Empty;
        Assert.Throws<InvalidOperationException>(() => EmptyStack.Peek());
        Assert.Throws<InvalidOperationException>(() => EmptyStack.Pop());
    }

    [Test]
    public void PushOnEmptyStackTwoItems_PeekOneElement_ReturnCorrectValue()
    {
        var stack = new ImmutableStack<int>().Empty;
        var stack1 = stack.Push(1);
        var stack2 = stack1.Push(2);
        Assert.AreEqual(2, stack2.Peek());
    }

    [Test]
    public void PushOnEmptyStackOneElement_PopOneElement_ReturnsEmptyStack()
    {
        var stack = new ImmutableStack<int>().Empty;
        stack = stack.Push(1);
        var result = stack.Pop();
        Assert.IsTrue(result.IsEmpty);
    }
}

public class ImmutableStack<T> : IStack<T>
{
    private T _head;
    
    private sealed class EmptyStack : IStack<T>
    {
        public IStack<T> Push(T value)
        {
            return new ImmutableStack<T>(value, this);
        }

        public IStack<T> Pop()
        {
            throw new InvalidOperationException("Can't pop empty stack");
        }

        public T Peek()
        {
            throw new InvalidOperationException("Can't peek empty stack");
        }

        public bool IsEmpty => true;
    }

    private ImmutableStack(T head, IStack<T> tail) 
    {
        _head = head;
        _tail = tail;
        
    }

    public ImmutableStack()
    {
    }
    
    public IStack<T> Push(T value)
    {
        return new ImmutableStack<T>(value, this);
    }

    public IStack<T> Pop()
    {
        return _tail;
    }

    public T Peek()
    {
        return _head;
    }

    public bool IsEmpty { get; }
    private static readonly EmptyStack _empty = new EmptyStack();
    private readonly IStack<T> _tail;
    public IStack<T> Empty => _empty;
}