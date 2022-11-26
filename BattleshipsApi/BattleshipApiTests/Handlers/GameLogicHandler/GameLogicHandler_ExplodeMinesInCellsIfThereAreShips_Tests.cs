using BattleshipsApi.Entities.Mines;

namespace BattleshipApiTests.Handlers.GameLogicHandler;

public class GameLogicHandler_ExplodeMinesInCellsIfThereAreShips_Tests
{
    private readonly BattleshipsApi.Handlers.GameLogicHandler _gameLogicHandler = new();

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void ExplodeMinesInCellsIfThereAreShipsTest()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(10);

        var destroyer = new Destroyer();
        _gameLogicHandler.PlaceShipToBoard(destroyer, board, coordinates);

        var mine = new HugeMine();
        _gameLogicHandler.PlaceMineToBoard(mine, board, coordinates);

        var destroyedShipsCount = 1;

        //Act & Assert
        _gameLogicHandler.ExplodeMinesInCellsIfThereAreShips(board)
            .Should().Be(destroyedShipsCount);
    }

}