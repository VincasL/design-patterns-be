using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;
using BattleshipsApi.Strategies;

namespace BattleshipsApi.Hubs.Handlers;

public record MoveUnitCommand(CellCoordinates Coordinates, MoveDirection Direction, bool IsEnemyBoard, string ContextConnectionId) : ICommand;


public class MoveUnitHandler : BaseHandler<MoveUnitCommand>
{
    public override async Task Handle(MoveUnitCommand command)
    {
        var connectionId = command.ContextConnectionId;
        var session = BattleshipsFacade.GetSessionByConnectionId(connectionId);
        var board = command.IsEnemyBoard
            ? session.GetEnemyPlayerByConnectionId(connectionId).Board
            : session.GetPlayerByConnectionId(connectionId).Board;
        var unit = BattleshipsFacade.GetUnitByCellCoordinates(command.Coordinates, board, command.IsEnemyBoard ? typeof(Mine) : typeof(Ship));

        if (unit == null)
        {
            throw new Exception("no unit :(");
        }
        foreach (var cell in board.Cells)
        {
            if (cell.Ship == (Ship)unit)
            {
                if (cell.Type == CellType.DamagedShip || cell.Type == CellType.DestroyedShip)
                {
                    throw new Exception("Your ship can't move bro, its broken");
                }
            }
        }

        if (session.AllPlayersPlacedUnits)
        {
            if (((Ship)unit).Speed <= 0)
            {
                throw new Exception("can't move no more :("); ;
            }
            MoveStrategy strategy = command.Direction switch
            {
                MoveDirection.Up => new MoveUp(),
                MoveDirection.Right => new MoveRight(),
                MoveDirection.Down => new MoveDown(),
                MoveDirection.Left => new MoveLeft(),
                _ => new DontMove()
            };
            unit.MoveStrategy = strategy;
            unit.MoveStrategy.MoveDifferently(board, unit);
            await Mediator.Send(new SendGameDataCommand(session));

            ((Ship)unit).Speed--;
        }
        else
        {
            MoveStrategy strategy = command.Direction switch
            {
                MoveDirection.Up => new MoveUp(),
                MoveDirection.Right => new MoveRight(),
                MoveDirection.Down => new MoveDown(),
                MoveDirection.Left => new MoveLeft(),
                _ => new DontMove()
            };
            unit.MoveStrategy = strategy;
            unit.MoveStrategy.MoveDifferently(board, unit);
            await Mediator.Send(new SendGameDataCommand(session));
        }
    }

    public MoveUnitHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}