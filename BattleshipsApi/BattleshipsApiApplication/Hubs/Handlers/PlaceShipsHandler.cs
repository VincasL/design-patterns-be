using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;

namespace BattleshipsApi.Hubs.Handlers;

public record PlaceShipsCommand(string ConnectionId) : ICommand;

public class PlaceShipsHandler : BaseHandler<PlaceShipsCommand>
{
    public override async Task Handle(PlaceShipsCommand command)
    {
        var session = BattleshipsFacade.GetSessionByConnectionId(command.ConnectionId);
        var player = session.GetPlayerByConnectionId(command.ConnectionId);
        var board = player.Board;

        if (session.AllPlayersPlacedUnits || player.AreAllUnitsPlaced)
        {
            throw new Exception("Ships already placed");
        }
        
        if (session.IsGameOver)
        {
            throw new Exception("Game is over bro");
        }

        if (player.PlacedShips.Count != session.Settings.ShipCount ||
            player.PlacedMines.Count != session.Settings.MineCount) 
        {
            throw new Exception("Not all units placed");
        }

        player.AreAllUnitsPlaced = true;

        await Mediator.Send(new SendGameDataCommand(session));
    }

    public PlaceShipsHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}

