using BattleshipsApi.Entities;
using System;

public class FastMoveLeft : MoveStrategy
{
    public FastMoveLeft()
    {
    }

    public override void MoveDifferently(Board board, Unit unit)
    {
		var unitCoordinates = new List<CellCoordinates>();

		foreach (var row in board.Cells)
		{
			foreach (var cell in row)
			{
				if (cell.Unit == unit)
				{
					unitCoordinates.Add(new CellCoordinates { X = cell.X, Y = cell.Y });
				}
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
				throw new Exception("overflows");
			}
		}

		foreach (var cell in unitCoordinates)
		{
			if (cell.X == 0)
			{
				continue;
			}

			var boardCellUnit = board.Cells[cell.X - 1][cell.Y].Unit;
			if (boardCellUnit != null && boardCellUnit != unit)
			{
				throw new Exception("ship already exists to the right");
			}
		}

		foreach (var cell in unitCoordinates)
		{
			board.Cells[cell.X][cell.Y].Unit = null;
		}

		foreach (var cell in unitCoordinates)
		{
			board.Cells[cell.X - 1][cell.Y].Unit = unit;
		}
	}
}
}
