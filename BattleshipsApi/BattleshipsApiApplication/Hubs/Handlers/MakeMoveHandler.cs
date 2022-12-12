using BattleshipsApi.Entities;
using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;
using BattleshipsApi.VisitorPattern;

namespace BattleshipsApi.Hubs.Handlers;
public record MakeMoveCommand(CellCoordinates CellCoordinates, string ContextConnectionId) : ICommand;

public class MakeMoveHandler : BaseHandler<MakeMoveCommand>
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

        var enemyPlayer = session.GetEnemyPlayerByConnectionId(connectionId);
        var player = session.GetPlayerByConnectionId(connectionId);
        var enemyBoard = enemyPlayer.Board;
        var visitor = new ShipVisitor(session.GameStartedDateTime);

        var (hasShipBeenHit, hasShipBeenDestroyed) = BattleshipsFacade.MakeMoveToEnemyBoard(command.CellCoordinates, enemyBoard);

        // moves heat seeking mine
        if (!hasShipBeenDestroyed && !hasShipBeenHit)
        {
            var mine = enemyBoard.GetHeatSeakingMine();
            if (mine != null && mine.HasExploded == false)
            {
                mine.MoveStrategy.MoveDifferently(enemyBoard, mine);
            }
        }

        if (hasShipBeenDestroyed)
        {
            enemyBoard.DestroyedShipCount++;
        }

        if (!hasShipBeenHit)
        {
            var destroyedShipCountByMines = BattleshipsFacade.ExplodeMinesInCellsIfThereAreShips(enemyBoard);
            if (destroyedShipCountByMines > 0)
            {
                hasShipBeenDestroyed = true;
                enemyBoard.DestroyedShipCount += destroyedShipCountByMines;
            }
            session.SetMoveToNextPlayer();
        }

        // check if all ships destroyed
        if (session.Settings.ShipCount <= enemyBoard.DestroyedShipCount)
        {
            session.IsGameOver = true;
            player.Winner = true;
        }

        foreach (Unit unit in enemyPlayer.PlacedShips)
        {
            if (unit is Ship)
            {
                ((Ship)unit).Accept(visitor);
            }
        }

        await Mediator.Send(new SendGameDataCommand(session));
    }

    public MakeMoveHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}