using BattleshipsApi.Entities;
using System;

public class MoveToShip : MoveStrategy
{
    public MoveToShip()
    {
    }
    //works only for mines and seeks ships
    public override void MoveDifferently(Board board, Unit unit)
    {
        var unitCoordinates = new List<CellCoordinates>();
        var allEntities = new List<CellCoordinates>();
        //find all mine and ship coordinates
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
        //create coordinates for what happened if mine moved
        foreach (CellCoordinates cell in unitCoordinates)
        {
            rightUnitCoordinates.Add(new CellCoordinates { X = cell.X + 1, Y = cell.Y });
            leftUnitCoordinates.Add(new CellCoordinates { X = cell.X - 1, Y = cell.Y });
            downUnitCoordinates.Add(new CellCoordinates { X = cell.X, Y = cell.Y + 1 });
            upUnitCoordinates.Add(new CellCoordinates { X = cell.X, Y = cell.Y - 1 });
        }
        bool isFound = false;
        //check if after moving mine colides with a ship (for all 4 directions)
        foreach (CellCoordinates cell in rightUnitCoordinates)
        {
            if (allEntities.Contains(cell))
            {
                isFound = true;
            }
        }
        //if it colides, move to that direction
        if (isFound)
        {
            MoveToDirection(board, unitCoordinates, rightUnitCoordinates, unit);
            return;
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
            MoveToDirection(board, unitCoordinates, leftUnitCoordinates, unit);
            return;
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
            MoveToDirection(board, unitCoordinates, upUnitCoordinates, unit);
            return;
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
            MoveToDirection(board, unitCoordinates, downUnitCoordinates, unit);
            return;
        }
        //if could not find ship nearby, move to random direction, also move only to direction, where mine wouldnt move out of bounds
        else {
            int minx=1000;
            int miny=1000;
            int maxx = -1; 
            int maxy = -1;
            foreach (CellCoordinates cell in unitCoordinates)
            {
                if(cell.X < minx)
                {
                    minx = cell.X;
                }
                else if(cell.X > maxx)
                {
                    maxx = cell.X;
                }
                else if(cell.Y<miny)
                {
                    miny = cell.Y;
                }
                else if(cell.Y>maxy)
                {
                    maxy = cell.Y;
                }
            }
            List<string> directions = new();
            if (minx>0)
            {
                directions.Add("left");
            }
            if (maxx<board.BoardSize)
            {
                directions.Add("right");
            }
            if (miny>0)
            {
                directions.Add("up");
            }
            if (maxy < board.BoardSize)
            {
                directions.Add("down");
            }
            Random rnd = new Random();
            int dir = rnd.Next(0, directions.Count());
            if (directions[dir]== "left")
            {
                MoveToDirection(board, unitCoordinates, leftUnitCoordinates, unit);
            }
            else if (directions[dir] == "right")
            {
                MoveToDirection(board, unitCoordinates, rightUnitCoordinates, unit);
            }
            else if (directions[dir] == "up")
            {
                MoveToDirection(board, unitCoordinates, upUnitCoordinates, unit);
            }
            else if (directions[dir] == "down")
            {
                MoveToDirection(board, unitCoordinates, downUnitCoordinates, unit);
            }
        }



    }
    private void MoveToDirection(Board board, List<CellCoordinates> oldUnitCoordinates,List<CellCoordinates> unitCoordinates, Unit unit)
    {
        foreach (CellCoordinates cell in oldUnitCoordinates)
        {
            board.Cells[cell.X, cell.Y].Mine = null;
        }
        foreach (CellCoordinates cell in unitCoordinates)
        {
            board.Cells[cell.X, cell.Y].Mine = unit as Mine;
        }
        
    }
}
