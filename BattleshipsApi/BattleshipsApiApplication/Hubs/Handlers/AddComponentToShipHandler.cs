using BattleshipsApi.Entities;
using BattleshipsApi.Entities.ShipModules;
using BattleshipsApi.Entities.Ships;
using BattleshipsApi.Enums;
using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;

namespace BattleshipsApi.Hubs.Handlers;

public record AddComponentToShipCommand(CellCoordinates CellCoordinates, string ContextConnectionId) : ICommand;

public enum ComponentType
{
    
}

public class AddComponentToShipHandler : BaseHandler<AddComponentToShipCommand>
{
    public AddComponentToShipHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }

    public override async Task Handle(AddComponentToShipCommand command)
    {
        var session = BattleshipsFacade.GetSessionByConnectionId(command.ContextConnectionId);
        var player = session
            .GetPlayerByConnectionId(command.ContextConnectionId);
        var board = player.Board;

        var coordinates = command.CellCoordinates;
        var cell = board.Cells[coordinates.X, coordinates.Y];

        var ship = cell.Ship;
        if (ship == null)
        {
            throw new Exception("No ship in cell");
        }
        ship.Children.Add(new BigCannon());
        ship.Children.Add(BattleshipsFacade.Factory.CreateShip(ShipType.Cruiser, NationType.American));
        

        await Mediator.Send(new SendGameDataCommand(session));
    }
}