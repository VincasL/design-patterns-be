using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;

namespace BattleshipsApi.Hubs.Handlers;

public record PlaceMineCommand(CellCoordinates CellCoordinates, MineType Type, string ContextConnectionId) : ICommand;

public class PlaceMineHandler : BaseHandler<PlaceMineCommand>
{
    public override async Task Handle(PlaceMineCommand command)
    {
        var factory = BattleshipsFacade.Factory;

        var mine = factory.CreateMine(command.Type, NationType.American);
        
        var session = BattleshipsFacade.GetSessionByConnectionId(command.ContextConnectionId);
        var player = session.GetPlayerByConnectionId(command.ContextConnectionId);
        var enemyPlayer = session.GetEnemyPlayerByConnectionId(command.ContextConnectionId);
        var enemyBoard = enemyPlayer.Board;
        
        if (player.AreAllUnitsPlaced || session.AllPlayersPlacedUnits)
        {
            throw new Exception("all units are placed");
        }

        if (player.PlacedMines.Count > session.Settings.MineCount)
        {
            throw new Exception("all mines are placed");
        }

        if(player.PlacedMines.Any(placedMine => placedMine.Type == mine.Type))
        {
            throw new Exception("Such ship has already been placed");
        }
        
        BattleshipsFacade.PlaceMineToBoard(mine , enemyBoard, command.CellCoordinates);
        player.PlacedMines.Add(mine);
        await Mediator.Send(new SendGameDataCommand(session));
    }

    public PlaceMineHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}