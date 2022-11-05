using System;
using BattleshipsApi.Entities;

public abstract class MoveStrategy
{
	public abstract void MoveDifferently(Board board, Unit unit);

}
