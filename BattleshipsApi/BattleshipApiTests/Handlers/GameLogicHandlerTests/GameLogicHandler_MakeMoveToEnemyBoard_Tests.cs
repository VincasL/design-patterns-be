namespace BattleshipApiTests.Handlers.GameLogicHandlerTests;

public class GameLogicHandler_MakeMoveToEnemyBoard_Tests
{
    private readonly GameLogicHandler _gameLogicHandler = new();
    private const int BoardSize = 10;

    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void MakeMoveToEnemyBoard_ShootAndMiss_HasShipBeenHitIsFalse()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(1);
        
        //Act
        var result = _gameLogicHandler.MakeMoveToEnemyBoard(coordinates, board);
        
        //Assert
        result.hasShipBeenHit.Should().BeFalse();
        result.hasShipBeenDestroyed.Should().BeFalse();
        board.Cells[coordinates.X, coordinates.Y].Type.Should().Be(CellType.Empty);
    }
    
    [Test]
    public void MakeMoveToEnemyBoard_ShootAndHitShip_HasShipBeenTrue()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(2);
        var ship = new Battleship();
        board.Cells[coordinates.X, coordinates.Y].Ship = ship;
        board.Cells[coordinates.X + 1, coordinates.Y].Ship = ship;
        
        //Act
        var result = _gameLogicHandler.MakeMoveToEnemyBoard(coordinates, board);
        
        //Assert
        result.hasShipBeenHit.Should().BeTrue();
        result.hasShipBeenDestroyed.Should().BeFalse();
        board.Cells[coordinates.X, coordinates.Y].Type.Should().Be(CellType.DamagedShip);
    }
    
    [Test]
    public void MakeMoveToEnemyBoard_ShootAndDestroyShip_HasShipBeenTrue()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var coordinates2 = new CellCoordinates(1, 0);
        var board = new Board(2);
        var ship = new Destroyer();
        board.Cells[coordinates.X, coordinates.Y].Ship = ship;
        board.Cells[coordinates2.X, coordinates2.Y].Ship = ship;
        board.Cells[coordinates2.X, coordinates2.Y].Type = CellType.DamagedShip;

        //Act
        var result = _gameLogicHandler.MakeMoveToEnemyBoard(coordinates, board);
        
        //Assert
        result.hasShipBeenHit.Should().BeTrue();
        result.hasShipBeenDestroyed.Should().BeTrue();
        board.Cells[coordinates.X, coordinates.Y].Type.Should().Be(CellType.DestroyedShip);
        board.Cells[coordinates2.X , coordinates2.Y].Type.Should().Be(CellType.DestroyedShip);
    }
    
    [Test]
    [TestCase(-1, 0)]
    [TestCase(0, -1)]
    [TestCase(BoardSize, 0)]
    [TestCase(0, BoardSize )]
    public void MakeMoveToEnemyBoard_CellCoordinatesOutOfBounds_ShouldThrow(int x, int y)
    {
        //Arrange
        var coordinates = new CellCoordinates(x, y);
        var board = new Board(BoardSize);
        
        //Act
        
        //Assert
        _gameLogicHandler
            .Invoking(handler => handler.MakeMoveToEnemyBoard(coordinates, board))
            .Should().Throw<Exception>();
    }
    
    [Test]
    public void MakeMoveToEnemyBoard_CellAlreadyBeenHit_ShouldThrow()
    {
        //Arrange
        var coordinates = new CellCoordinates(0,0);
        var board = new Board(1);
        
        //Act

        _gameLogicHandler.MakeMoveToEnemyBoard(coordinates, board);
        
        //Assert
        _gameLogicHandler
            .Invoking(handler => handler.MakeMoveToEnemyBoard(coordinates, board))
            .Should().Throw<Exception>();
    }
}