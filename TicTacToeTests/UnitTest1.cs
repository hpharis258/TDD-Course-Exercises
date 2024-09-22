namespace TicTacToeTests;

[TestFixture]
public class TicTacToeTests
{
    [Test]
    public void CreateGameZeroMoves()
    {
        Game game = new Game();
        Assert.AreEqual(0, game.MovesCounter);
    }
    
    [Test]
    public void MakeMoveCounterShifts()
    {
        Game game = new Game();
        game.MakeMove(1);
        Assert.AreEqual(1, game.MovesCounter);
    }
    
    [Test]
    public void MakeInvalidMoveThrowsException()
    {

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var game = new Game();
            game.MakeMove(10);
            
        });
    }
    [Test]
    public void MakeMoveOnTheSameSquare()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            var game = new Game();
            game.MakeMove(1);
            game.MakeMove(1);
        });
    }
    [Test]
    public void MakingMovesSetStateCorrectly()
    {
        
            var game = new Game();
            MakeMoves(game, 1, 2, 3, 4);
            Assert.AreEqual(State.Cross, game.GetState(1));
            Assert.AreEqual(State.Zero, game.GetState(2));
            Assert.AreEqual(State.Cross, game.GetState(3));
            Assert.AreEqual(State.Zero, game.GetState(4));
            
       
    }
    [Test]
    public void CreateBoardWithAllStatesEmpty()
    {
        var game = new Game();
        for (int i = 1; i <= 9; i++)
        {
            Assert.AreEqual(State.Empty, game.GetState(i));
        }
    }

    [Test]
    public void GetWinnerZeroWinsVertically()
    {
        Game game = new Game();
        MakeMoves(game, 1,2,3,5,7,8);
        Assert.AreEqual(Winnner.Zero, game.GetWinner());
    }
    [Test]
    public void GetWinnerCrossesWinsDiagonal()
    {
        Game game = new Game();
        MakeMoves(game, 1,4,5,2,9);
        Assert.AreEqual(Winnner.Cross, game.GetWinner());
    }
    [Test]
    public void GetWinnerGameIsUnfinishedReturnCorrectly()
    {
        Game game = new Game();
        MakeMoves(game, 1,2,4);
        Assert.AreEqual(Winnner.GameIsUnfinished, game.GetWinner());
    }
    private void MakeMoves(Game game, params int[] moves)
    {
        foreach (var move in moves)
        {
            game.MakeMove(move);
        }
    }
}

public class Game
{
    public int MovesCounter { get; private set; }
    private readonly State[] board = new State[9];
    public Game()
    {
        for (int i = 0; i < board.Length; i++)
        {
            board[i] = State.Empty;
        }
        MovesCounter = 0;
    }
    public void MakeMove(int index)
    {
        if (index < 1 || index > 9)
        {
            throw new ArgumentOutOfRangeException();
        }
        if(GetState(index) != State.Empty)
        {
            throw new InvalidOperationException();
        }

        board[index - 1] = MovesCounter % 2 == 0 ? State.Cross : State.Zero; 
        MovesCounter++;
    }
    
    public State GetState(int index)
    {
        return board[index - 1];
    }
    
    public Winnner GetWinner()
    {
        return GetWinner(1, 4,7,2,5,8,3,6,9,1,2,3,4,5,6,7,8,9,1,5,9,3,5,7);
    }
    private Winnner GetWinner(params int[] indexes)
    {
        for (int i = 0; i < indexes.Length; i += 3)
        {
           bool same = AreSame(indexes[i], indexes[i + 1], indexes[i + 2]);
              if (same)
              {
                 State state = GetState(indexes[i]);
                 if (state != State.Empty)
                 {
                     return state == State.Cross ? Winnner.Cross : Winnner.Zero;
                 }
              }
        }

        if (MovesCounter < 9)
        {
            return Winnner.GameIsUnfinished;
        }
        return Winnner.Draw;
    }
    private bool AreSame(int a, int b, int c)
    {
        return GetState(a) == GetState(b) && GetState(a) == GetState(c);
    }

}

public enum  State
{
    Cross,
    Zero,
    Empty
}

public enum Winnner
{
    Cross,
    Zero,
    Draw,
    GameIsUnfinished
}