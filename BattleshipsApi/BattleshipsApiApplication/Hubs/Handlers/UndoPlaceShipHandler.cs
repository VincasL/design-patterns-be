using BattleshipsApi.Entities;
using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;

namespace BattleshipsApi.Hubs.Handlers;

public record UndoPlaceShipCommand(CellCoordinates CellCoordinates, string ContextConnectionId) : ICommand;

public class UndoPlaceShipHandler: BaseHandler<UndoPlaceShipCommand>
{
    public override async Task Handle(UndoPlaceShipCommand command)
    {
        var session = BattleshipsFacade.GetSessionByConnectionId(command.ContextConnectionId);
        var player = session.GetPlayerByConnectionId(command.ContextConnectionId);
        var board = player.Board;
        
        if (player.AreAllUnitsPlaced || session.AllPlayersPlacedUnits)
        {
            throw new Exception("all ships are placed");
        }

        var ship = BattleshipsFacade.GetUnitByCellCoordinates(command.CellCoordinates, board) as Ship;
        if (ship == null)
        {
            throw new Exception("no ship here");
        }
        
        BattleshipsFacade.UndoPlaceShipToBoardByCell(ship, board);
        var removed = player.PlacedShips.Remove(ship);
        if (!removed)
        {
            throw new Exception("Ship not removed");
        }

        await Mediator.Send(new SendGameDataCommand(session));
    }

    public UndoPlaceShipHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}

