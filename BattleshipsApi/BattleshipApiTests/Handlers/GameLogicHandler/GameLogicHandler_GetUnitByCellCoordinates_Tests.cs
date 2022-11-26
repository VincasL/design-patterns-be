using BattleshipsApi.Entities.Ships;
using System.Threading;

namespace BattleshipApiTests.Handlers.GameLogicHandler;

public class GameLogicHandler_GetUnitByCellCoordinates_Tests
{
    private readonly BattleshipsApi.Handlers.GameLogicHandler _gameLogicHandler = new();

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void GetUnitByCellCoordinatesTest()
    {
        //Arrange
        var coordinates = new CellCoordinates(0, 0);
        var board = new Board(10);
        var battleship = new Battleship();
        _gameLogicHandler.PlaceShipToBoard(battleship, board, coordinates);

        //Act & Assert
        var unit = _gameLogicHandler.GetUnitByCellCoordinates(coordinates, board)
            .Should().Be(battleship);
    }
}