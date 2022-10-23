﻿using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities;

public class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    public CellType Type { get; set; }
    public Unit? Unit { get; set; }

    public Cell(int x, int y)
    {
        Type = CellType.NotShot;
        X = x;
        Y = y;
    }
}

