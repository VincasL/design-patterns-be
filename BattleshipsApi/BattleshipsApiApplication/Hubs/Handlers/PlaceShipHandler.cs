using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;

namespace BattleshipsApi.Hubs.Handlers;

public record PlaceShipCommand(CellCoordinates CellCoordinates, ShipType Type, string ContextConnectionId) : ICommand;

public class PlaceShipHandler : BaseHandler<PlaceShipCommand>
{
    public override async Task Handle(PlaceShipCommand command)
    {
        var session = BattleshipsFacade.GetSessionByConnectionId(command.ContextConnectionId);
        var player = session.GetPlayerByConnectionId(command.ContextConnectionId);
        var board = player.Board;

        var factory = BattleshipsFacade.Factory;
        var ship = factory.CreateShip(command.Type, player.nationType);

        if (player.AreAllUnitsPlaced || session.AllPlayersPlacedUnits)
        {
            throw new Exception("all ships are placed");
        }

        if (player.PlacedShips.Count > session.Settings.ShipCount)
        {
            throw new Exception("enough ships are placed");
        }
        
        if(player.PlacedShips.Any(placedShip => placedShip.Type == ship.Type))
        {
            throw new Exception("Such ship has already been placed");
        }
        
        BattleshipsFacade.PlaceShipToBoard(ship , board, command.CellCoordinates);
        player.PlacedShips.Add(ship);

        await Mediator.Send(new SendGameDataCommand(session));
    }

    public PlaceShipHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}