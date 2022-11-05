using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities;

public class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    public CellType Type { get; set; }
    public List<Unit> Units { get; set; }

    public Cell(int x, int y)
    {
        Type = CellType.NotShot;
        X = x;
        Y = y;
        Units = new List<Unit>();
    }

    public Cell(int x, int y, CellType type, List<Unit> units)
    {
        X = x;
        Y = y;
        Type = type;
        Units = units;
    }

    public bool UnitTypeExistsInCell(Type type) => Units.Any(x => x.GetType() == type);

    public void Add(Unit unit)
    {
        if (UnitTypeExistsInCell(unit.GetType()))
        {
            throw new Exception("Ship already exists in this cell");
        }

        Units.Add(unit);
    }

    public void Set(Unit? unit, Type type)
    {
        var oldUnit = Units.Find(x => IsSameOrSubclass(type, x.GetType()));
        if (oldUnit != null)
            Units.Remove(oldUnit);
        if (unit != null)
            Units.Add(unit);
    }

    public void Remove(Unit unit)
    {
        Units.Remove(unit);
    }


    // consider making generic
    public Ship? Ship
    {
        get => Units.Find(x => IsSameOrSubclass(typeof(Ship), x.GetType())) as Ship;
        set
        {
            if (Ship == null && value != null)
            {
                Add(value);
            }
            else
            {
                Set(value, typeof(Ship));
            }
        }
    }

    public Mine? Mine
    {
        get => Units.Find(x => IsSameOrSubclass(typeof(Mine), x.GetType())) as Mine;
        set
        {
            if (Mine == null && value != null)
            {
                Add(value);
            }
            else
            {
                Set(value, typeof(Mine));
            }
        }
    }

    public Missile? Missile
    {
        get => Units.Find(x => IsSameOrSubclass(typeof(Missile), x.GetType())) as Missile;
        set
        {
            if (Missile == null && value != null)
            {
                Add(value);
            }
            else
            {
                Set(value, typeof(Missile));
            }
        }
    }

    public bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
    {
        return potentialDescendant.IsSubclassOf(potentialBase)
               || potentialDescendant == potentialBase;
    }

    public Unit? Unit(Type? unitType = null)
    {
        return Units.Find(x => IsSameOrSubclass(unitType ?? typeof(Ship),x.GetType() ));
    }
}



