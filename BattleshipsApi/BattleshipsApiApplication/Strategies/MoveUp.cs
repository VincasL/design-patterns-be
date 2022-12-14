using BattleshipsApi.Entities;

public class MoveUp : MoveStrategy
{
    public MoveUp()
    {
    }

    public override void MoveDifferently(Board board, Unit unit)
    {
		var unitCoordinates = new List<CellCoordinates>();

		foreach (var cell in board.Cells)
		{
			if (cell.Unit(unit.GetType()) == unit)
			{
				unitCoordinates.Add(new CellCoordinates { X = cell.X, Y = cell.Y });
			}
		}

		if (unitCoordinates.Count == 0)
		{
			throw new Exception("couldnt find unit in cell");
		}

		foreach (var cell in unitCoordinates)
		{
			if (cell.Y == 0)
			{
				throw new Exception("out of bounds");
			}
		}

		foreach (var cell in unitCoordinates)
		{
			var boardCellUnit = board.Cells[cell.X,cell.Y - 1].Unit(unit.GetType().BaseType);
			if (boardCellUnit != null && boardCellUnit != unit)
			{
				throw new Exception("Unit already exists above");
			}
		}

		foreach (var cell in unitCoordinates)
		{
			board.Cells[cell.X, cell.Y].Remove(unit);
			board.Cells[cell.X, cell.Y - 1].Set(unit, unit.GetType());
		}
	}
}
