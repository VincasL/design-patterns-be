using BattleshipsApi.Entities;
using BattleshipsApi.Facades;
using BattleshipsApi.Mediator;

namespace BattleshipsApi.Hubs.Handlers;

public record RotateShipCommand(CellCoordinates CellCoordinates, string ContextConnectionId): ICommand;


public class RotateShipHandler: BaseHandler<RotateShipCommand>
{
    public override async Task Handle(RotateShipCommand command)
    {
        var session = BattleshipsFacade.GetSessionByConnectionId(command.ContextConnectionId);
        var board = session.GetPlayerByConnectionId(command.ContextConnectionId).Board;
        var ship = BattleshipsFacade.GetUnitByCellCoordinates(command.CellCoordinates, board) as Ship;
        
        if (ship == null)
        {
            throw new Exception("No ship to rotate in this cell");
        }

        var unitCoordinates = new List<CellCoordinates>();

        foreach (var cell in board.Cells)
        {
            if (cell.Ship == ship)
            {
                unitCoordinates.Add(new CellCoordinates { X = cell.X, Y = cell.Y });
            }
        }
        foreach (var cell in unitCoordinates)
        {
            if ((cell.Y + ship.Length > board.BoardSize && !ship.IsHorizontal) || (cell.X + ship.Length > board.BoardSize && ship.IsHorizontal))
            {
                throw new Exception("out of bounds");
            }
        }
        if (!ship.IsHorizontal)
        {
            var first_coord = unitCoordinates[0];
            for (int i = 1; i < ship.Length; i++)
            {
                if (board.Cells[first_coord.X, first_coord.Y + i].Ship != null)
                {
                    throw new Exception("ship already exists below");
                }
            }
            for (int i = 1; i < unitCoordinates.Count; i++)
            {
                board.Cells[unitCoordinates[i].X, unitCoordinates[i].Y].Ship = null;
                board.Cells[first_coord.X, first_coord.Y + i].Ship = ship;
            }
            ship.IsHorizontal = true;
        }
        else
        {
            var first_coord = unitCoordinates[0];
            for (int i = 1; i < ship.Length; i++)
            {
                if (board.Cells[first_coord.X + i, first_coord.Y].Ship != null)
                {
                    throw new Exception("ship already exists to the right");
                }
            }
            for (int i = 1; i < unitCoordinates.Count; i++)
            {
                board.Cells[unitCoordinates[i].X, unitCoordinates[i].Y].Ship = null;
                board.Cells[first_coord.X + i, first_coord.Y].Ship = ship;
            }
            ship.IsHorizontal = false;
        }

        await Mediator.Send(new SendGameDataCommand(session));
    }

    public RotateShipHandler(BattleshipsFacade battleshipsFacade, IMediator mediator = null) : base(battleshipsFacade, mediator)
    {
    }
}