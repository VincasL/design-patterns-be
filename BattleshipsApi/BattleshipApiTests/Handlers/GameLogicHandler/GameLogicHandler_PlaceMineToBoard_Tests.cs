namespace BattleshipApiTests.Handlers.GameLogicHandler;

public class GameLogicHandler_PlaceMineToBoard_Tests
{
    private readonly BattleshipsApi.Handlers.GameLogicHandler _gameLogicHandler = new();

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void PlaceSmallMineToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(1);
        var mine = new SmallMine();

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceMineToBoard(mine, board, coordinates))
            .Should().NotThrow();
    }

    [Test]
    public void PlaceRemoteMineToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(1);
        var mine = new RemoteControlledMIne();

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceMineToBoard(mine, board, coordinates))
            .Should().NotThrow();
    }

    [Test]
    public void PlaceHugeMineToBoard_overflow()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(1);
        var mine = new HugeMine();

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceMineToBoard(mine, board, coordinates))
            .Should().Throw<Exception>();
    }

    [Test]
    public void PlaceHugeMineToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(1, 1);
        var board = new Board(10);
        var mine = new HugeMine();

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceMineToBoard(mine, board, coordinates))
            .Should().NotThrow();
    }

    [Test]
    public void PlaceNullMineToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(1, 1);
        var board = new Board(10);
        Mine mine = null;

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceMineToBoard(mine, board, coordinates))
            .Should().Throw<Exception>();
    }

    [Test]
    public void PlaceMineOnMineToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(1, 1);
        var board = new Board(10);
        var mine = new HugeMine();

        //Act & Assert
        _gameLogicHandler.PlaceMineToBoard(mine, board, coordinates);
        _gameLogicHandler.Invoking(handler => handler.PlaceMineToBoard(mine, board, coordinates))
            .Should().Throw<Exception>();
    }

}