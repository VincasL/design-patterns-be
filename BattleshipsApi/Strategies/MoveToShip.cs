using BattleshipsApi.Entities;
using System;

public class MoveToShip : MoveStrategy
{
    public MoveToShip()
    {
    }

    public override void MoveDifferently(Board board, Unit unit)
    {
        var unitCoordinates = new List<CellCoordinates>();
        var allEntities = new List<CellCoordinates>();

        foreach (var cell in board.Cells)
        {
            if (cell.Mine == unit)
            {
                unitCoordinates.Add(new CellCoordinates { X = cell.X, Y = cell.Y });
            }
            else if (cell.Ship !=null)
            {
                allEntities.Add(new CellCoordinates { X = cell.X, Y = cell.Y });
            }
        }
        var rightUnitCoordinates = new List<CellCoordinates>();
        var leftUnitCoordinates = new List<CellCoordinates>();
        var upUnitCoordinates = new List<CellCoordinates>();
        var downUnitCoordinates = new List<CellCoordinates>();
        foreach (CellCoordinates cell in unitCoordinates)
        {
            rightUnitCoordinates.Add(new CellCoordinates { X = cell.X + 1, Y = cell.Y });
            leftUnitCoordinates.Add(new CellCoordinates { X = cell.X - 1, Y = cell.Y });
            downUnitCoordinates.Add(new CellCoordinates { X = cell.X, Y = cell.Y + 1 });
            upUnitCoordinates.Add(new CellCoordinates { X = cell.X, Y = cell.Y - 1 });
        }
        bool isFound = false;
        foreach (CellCoordinates cell in rightUnitCoordinates)
        {
            if (allEntities.Contains(cell))
            {
                isFound = true;
            }
        }
        if (isFound)
        {
            DoStuff(rightUnitCoordinates);
        }
        foreach (CellCoordinates cell in leftUnitCoordinates)
        {
            if (allEntities.Contains(cell))
            {
                isFound = true;
            }
        }
        if (isFound)
        {
            DoStuff(leftUnitCoordinates);
        }
        foreach (CellCoordinates cell in upUnitCoordinates)
        {
            if (allEntities.Contains(cell))
            {
                isFound = true;
            }
        }
        if (isFound)
        {
            DoStuff(upUnitCoordinates);
        }
        foreach (CellCoordinates cell in downUnitCoordinates)
        {
            if (allEntities.Contains(cell))
            {
                isFound = true;
            }
        }
        if (isFound)
        {
            DoStuff(downUnitCoordinates);
        }
        else {

        }



    }
    private void DoStuff(List<CellCoordinates> unitCoordinates)
    {

    }
}
