namespace BattleshipApiTests.Handlers.GameLogicHandler;

public class GameLogicHandler_PlaceShipToBoard_Tests
{
    private readonly BattleshipsApi.Handlers.GameLogicHandler _gameLogicHandler = new();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PlaceBattleshipToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(10);
        var battleship = new Battleship();

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceShipToBoard(battleship, board, coordinates))
                    .Should().NotThrow();
    }

    [Test]
    public void PlaceCarrierToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(10);
        var carrier = new Carrier();

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceShipToBoard(carrier, board, coordinates))
                .Should().NotThrow();
    }

    [Test]
    public void PlaceCruiserToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(10);
        var cruiser = new Cruiser();

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceShipToBoard(cruiser, board, coordinates))
                    .Should().NotThrow();
    }

    [Test]
    public void PlaceDestroyerToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(10);
        var destroyer = new Destroyer();

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceShipToBoard(destroyer, board, coordinates))
                    .Should().NotThrow();
    }

    [Test]
    public void PlaceSubmarineToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(10);
        var submarine = new Submarine();

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceShipToBoard(submarine, board, coordinates))
                    .Should().NotThrow();
    }

    [Test]
    public void PlaceBattleshipOverlapToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(10);
        var battleship = new Battleship();

        _gameLogicHandler.PlaceShipToBoard(battleship, board, coordinates);

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceShipToBoard(battleship, board, coordinates))
                    .Should().Throw<Exception>();
    }

    [Test]
    public void PlaceBattleshipHorizontalToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(10);
        var battleship = new Battleship();
        battleship.IsHorizontal = true;

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceShipToBoard(battleship, board, coordinates))
                    .Should().NotThrow();
    }


    [Test]
    public void PlaceBattleshipHorizontalOverlapToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(10);
        var battleship = new Battleship();
        battleship.IsHorizontal = true;

        _gameLogicHandler.PlaceShipToBoard(battleship, board, coordinates);

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.PlaceShipToBoard(battleship, board, coordinates))
                    .Should().Throw<Exception>();
    }

}