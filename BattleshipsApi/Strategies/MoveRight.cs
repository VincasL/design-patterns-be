using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Helpers;
using System;

public class MoveRight : MoveStrategy
{
	public MoveRight()
	{
	}

    public override void MoveDifferently(Missile ship, Board board)
    {
        if (ship.IsHorizontal)
        {
            for (var y = coordinates.Y; y < coordinates.Y + ship.Length; y++)
            {
                var cell = board.Cells[coordinates.X][y];
                if (cell.Ship != null)
                {
                    // rollback
                    while (y != coordinates.Y)
                    {
                        y--;
                        cell = board.Cells[coordinates.X][y];
                        cell.Ship = null;
                    }

                    throw new Exception("Ships overlap");
                }

                cell.Ship = ship;
            }
        }
}
