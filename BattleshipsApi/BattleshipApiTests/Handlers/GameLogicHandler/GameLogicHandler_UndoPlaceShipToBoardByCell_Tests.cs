namespace BattleshipApiTests.Handlers.GameLogicHandler;

public class GameLogicHandler_UndoPlaceShipToBoardByCell_Tests
{
    private readonly BattleshipsApi.Handlers.GameLogicHandler _gameLogicHandler = new();

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void UndoPlaceBattleshipToBoard()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(10);
        var ship = new Battleship();
        _gameLogicHandler.PlaceShipToBoard(ship, board, coordinates);

        //Act & Assert
        _gameLogicHandler.Invoking(handler => handler.UndoPlaceShipToBoardByCell(ship, board))
            .Should().NotThrow();
    }

}