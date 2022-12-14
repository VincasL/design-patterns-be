using BattleshipsApi.Entities;

public class MoveLeft : MoveStrategy
{
    public MoveLeft()
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
			if (cell.X == 0)
			{
				throw new Exception("out of bounds");
			}
		}

		foreach (var cell in unitCoordinates)
		{
			var boardCellUnit = board.Cells[cell.X - 1,cell.Y].Unit(unit.GetType().BaseType);
			if (boardCellUnit != null && boardCellUnit != unit)
			{
				throw new Exception("ship already exists to the left");
			}
		}

		foreach (var cell in unitCoordinates)
		{
			board.Cells[cell.X,cell.Y].Remove(unit);
            board.Cells[cell.X - 1, cell.Y].Set(unit, unit.GetType());
        }
	}
}

