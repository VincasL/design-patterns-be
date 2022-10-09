using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities;

public class Ship:Unit
{
    public ShipType Type { get; set; }
    public bool IsHorizontal { get; set; }
    public int ShieldStrenth { get; set; }
    public int ArmourStrenth { get; set; }
    public int Fuel { get; set; }

}