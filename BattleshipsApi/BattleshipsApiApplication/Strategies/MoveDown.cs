using BattleshipsApi.Entities;

public class MoveDown : MoveStrategy
{
    public MoveDown()
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
			if (cell.Y + 1 >= board.BoardSize)
			{
				throw new Exception("out of bounds");
			}
		}

		foreach (var cell in unitCoordinates)
		{
			if (cell.Y + 1 == board.BoardSize)
			{
				continue;
			}

			var boardCellUnit = board.Cells[cell.X,cell.Y + 1].Unit(unit.GetType().BaseType);
			if (boardCellUnit != null && boardCellUnit != unit)
			{
				throw new Exception("unit already exists below");
			}
		}

		foreach (var cell in unitCoordinates.AsEnumerable().Reverse())
		{
			board.Cells[cell.X,cell.Y].Remove(unit);
            board.Cells[cell.X, cell.Y + 1].Set(unit, unit.GetType());
        }
	}
}
