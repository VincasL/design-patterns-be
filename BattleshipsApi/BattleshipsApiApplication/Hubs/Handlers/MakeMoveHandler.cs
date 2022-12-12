using BattleshipsApi.Entities;
using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;

namespace BattleshipsApi.Hubs.Handlers;
public record MakeMoveCommand(CellCoordinates CellCoordinates, string ContextConnectionId) : ICommand;

public class MakeMoveHandler: BaseHandler<MakeMoveCommand>
{
    public override async Task Handle(MakeMoveCommand command)
    {
        var connectionId = command.ContextConnectionId;
        //TODO: validation
        var session = BattleshipsFacade.GetSessionByConnectionId(connectionId);

        if (!session.AllPlayersPlacedUnits)
        {
            throw new Exception("Not all ships placed");
        }

        if (session.IsGameOver)
        {
            throw new Exception("Game is over bro");
        }

        if (session.NextPlayerTurnConnectionId != connectionId)
        {
            throw new Exception("it's not your move bro");
        }

        var player = session.GetEnemyPlayerByConnectionId(connectionId);
        var board = player.Board;

        var (hasShipBeenHit, hasShipBeenDestroyed) = BattleshipsFacade.MakeMoveToEnemyBoard(command.CellCoordinates, board);
        
        // moves heat seeking mine
        if (!hasShipBeenDestroyed && !hasShipBeenHit)
        {
            var mine = board.getHeatSeakingMine();
            if (mine != null && mine.HasExploded == false)
            {
                mine.MoveStrategy.MoveDifferently(board, mine);
            }
        }

        if (hasShipBeenDestroyed)
        {
            player.Board.DestroyedShipCount++;
        }
        
        if (!hasShipBeenHit)
        {
            var destroyedShipCountByMines = BattleshipsFacade.ExplodeMinesInCellsIfThereAreShips(board);
            if (destroyedShipCountByMines > 0)
            {
                hasShipBeenDestroyed = true;
                player.Board.DestroyedShipCount += destroyedShipCountByMines;
            }
            session.SetMoveToNextPlayer();
        }

        // check if all ships destroyed
        if (session.Settings.ShipCount <= player.Board.DestroyedShipCount)
        {
            session.IsGameOver = true;
            session.GetPlayerByConnectionId(connectionId).Winner = true;
        }

        await Mediator.Send(new SendGameDataCommand(session));
    }

    public MakeMoveHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}