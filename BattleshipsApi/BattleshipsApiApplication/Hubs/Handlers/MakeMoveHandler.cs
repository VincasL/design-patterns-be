using BattleshipsApi.Entities;
using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;
using BattleshipsApi.Proxy;
using BattleshipsApi.VisitorPattern;

namespace BattleshipsApi.Hubs.Handlers;
public record MakeMoveCommand(CellCoordinates CellCoordinates, string ContextConnectionId) : ICommand;

public class MakeMoveHandler : BaseHandler<MakeMoveCommand>
{
    public override async Task Handle(MakeMoveCommand command)
    {
        var connectionId = command.ContextConnectionId;
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
        var visitor = new ShipVisitor(session.GameStartedDateTime);
        var something = DateTime.UtcNow.Ticks;
        Console.WriteLine("Proxy initialization Start " + something);
        var proxy = new VisitorProxy(session.GameStartedDateTime);
        var somethingElse = DateTime.UtcNow.Ticks;
        Console.WriteLine("Proxy initialization End " + somethingElse);
        Console.WriteLine("Difference (ticks) : " + (something - somethingElse));

        var (hasShipBeenHit, hasShipBeenDestroyed) = BattleshipsFacade.MakeMoveToEnemyBoard(command.CellCoordinates, enemyPlayer.Board);

        // moves heat seeking mine
        if (!hasShipBeenDestroyed && !hasShipBeenHit)
        {
            var mine = enemyPlayer.Board.GetHeatSeakingMine();
            if (mine != null && mine.HasExploded == false)
            {
                mine.MoveStrategy.MoveDifferently(enemyPlayer.Board, mine);
            }
        }

        if (hasShipBeenDestroyed)
        {
            enemyPlayer.Board.DestroyedShipCount++;
        }

        if (!hasShipBeenHit)
        {
            var destroyedShipCountByMines = BattleshipsFacade.ExplodeMinesInCellsIfThereAreShips(enemyPlayer.Board);
            if (destroyedShipCountByMines > 0)
            {
                hasShipBeenDestroyed = true;
                enemyPlayer.Board.DestroyedShipCount += destroyedShipCountByMines;
            }
            session.SetMoveToNextPlayer();
        }

        if (session.Settings.ShipCount <= enemyPlayer.Board.DestroyedShipCount)
        {
            session.IsGameOver = true;
            player.Winner = true;
        }

        foreach (Unit unit in enemyPlayer.PlacedShips)
        {
            if (unit is Ship ship)
            {
                ship.Accept(proxy);
            }
        }

        foreach (Unit unit in enemyPlayer.PlacedShips)
        {
            if (unit is Ship ship)
            {
                ship.Accept(visitor);
            }
        }

        await Mediator.Send(new SendGameDataCommand(session));
    }

    public MakeMoveHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}