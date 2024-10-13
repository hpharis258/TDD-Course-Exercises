namespace StackKata;

[TestFixture]
public class LinkedListKataTests
{
    [Test]
    public void CreateNode_SetsValueAndNextIsNull()
    {
        ListNode<int> node = new ListNode<int>(1);
         
        Assert.AreEqual(1, node.Value);
        Assert.IsNull(node.Next);
    }

    [Test]
    public void AddFirst_HeadAndTailAreSame()
    {
        LinkedList<int> list = new LinkedList<int>();
        list.AddFirst(1);
        Assert.AreEqual(1, list.Head.Value);
        Assert.AreEqual(1, list.Tail.Value);
        Assert.AreSame(list.Head, list.Tail);
    }
    
    [Test]
    public void AddFirst_TwoElementsListIsInCorrectState()
    {
        LinkedList<int> list = new LinkedList<int>();
        list.AddFirst(1);
        list.AddFirst(2);
        
        Assert.AreEqual(1, list.Tail.Value);
        Assert.AreEqual(2, list.Head.Value);
        Assert.AreEqual(2, list.Count);
        Assert.AreSame(list.Head.Next, list.Tail);
    }
    
    [Test]
    public void AddLast_HeadAndTailAreSame()
    {
        LinkedList<int> list = new LinkedList<int>();
        list.AddFirst(1);
        
        Assert.AreSame(list.Head, list.Tail);
    }

    [Test]
    public void RemoveFirst_EmptyListThrows()
    {
        var list = new LinkedList<int>();
        Assert.Throws<InvalidOperationException>(() =>
        {
            list.RemoveFirst();
        });
    }
    
    [Test]
    public void RemoveFirst_OneElementListCorrectState()
    {
        var list = new LinkedList<int>();
        list.AddFirst(1);
        list.RemoveFirst();
        Assert.IsNull(list.Head);
        Assert.IsNull(list.Tail);
        Assert.AreEqual(0, list.Count);
    }
    
    [Test]
    public void RemoveLast_EmptyList_ThrowsExecption()
    {
        var list = new LinkedList<int>();
        
        Assert.Throws<InvalidOperationException>(() =>
        {
            list.RemoveLast();
        });
    }
    
    [Test]
    public void RemoveLast_OneElement_HeadAndTailAreNull()
    {
        var list = new LinkedList<int>();
        
        list.AddFirst(1);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
        Assert.IsNull(list.Head);
        Assert.IsNull(list.Tail);
    }
    [Test]
    public void RemoveLast_TwoElements_StateIsCorrect()
    {
        var list = new LinkedList<int>();

        list.AddLast(1);
        list.AddLast(2);
        list.RemoveLast();
        Assert.AreSame(list.Head, list.Tail);
        Assert.AreEqual(1, list.Count);
        
    }
}

public class LinkedList<T>
{
    public ListNode<T> Tail { get; private set; }
    public ListNode<T> Head { get; private set; }
    public int Count { get; private set; }

    public void AddFirst(T value)
    {
        AddFirst(new ListNode<T>(value));
    }
    public void AddFirst(ListNode<T> node)
    { 
        // Save the head node
        ListNode<T> temp = Head;
        // Point head to the new node
        Head = node;
        Head.Next = temp;
        Count++;
        
        if(Count == 1)
        {
            Tail = Head;
        }
    }
    
    public void AddLast(T value)
    {
        AddLast(new ListNode<T>(value));
    }
    public void AddLast(ListNode<T> node)
    {
        if(Count == 0)
        {
            Head = node;
        }
        else
        {
            Tail.Next = node;
        }
        Tail = node;
        Count++;
    }
    public void RemoveFirst()
    {
        if(Count == 0)
        {
            throw new InvalidOperationException();
        }
        Head = Head.Next;
        Count--;
        if(Count == 0)
        {
            Tail = null;
        }
    }

    public void RemoveLast()
    {
        if(Count == 0)
        {
            throw new InvalidOperationException();
        }

        if (Count == 1)
        {
            Head = null;
            Tail = null;
        }
        else
        {
            ListNode<T> current = Head;
            while(current.Next != Tail)
            {
                current = current.Next;
            }
            current.Next = null;
            Tail = current;
        }
       
        Count--;
    }
}

public class ListNode<T>
{
    public T Value { get; }
    public ListNode<T> Next { get; set; }
    public ListNode(T value)
    {
        Value = value;
    }
}